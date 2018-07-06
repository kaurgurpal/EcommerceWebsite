using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;


namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductsRepository repository;

        public int PageSize = 100;

        public ProductController(IProductsRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        // GET: Search
        public PartialViewResult Search()
        {
            //ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Products
                                            .Select(p => p.Category)
                                            .Distinct()
                                            .OrderBy(p => p);
            return PartialView("SearchBox", categories);
        }

        [HttpPost]
        public PartialViewResult SearchBox(string category, int page = 1, string searchText = null)
        {
            category = category == "All" ? null : category;

            ProductsListViewModel model;
            //Search with category & text 
            if (category != null && !string.IsNullOrEmpty(searchText))
            {
                model = new ProductsListViewModel
                {
                    Products = repository.Products.Where
                                                    (p =>
                                                    (p.Category == category) && (p.Name.ToUpper()).Contains(searchText.ToUpper()))
                                                    .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PageInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Where(p =>
                                                   (p.Category == category) && (p.Name.ToUpper()).Contains(searchText.ToUpper())).Count()
                    },
                    CurrentCategory = category
                };
            }
            // search with category & text empty
            else if (category != null && string.IsNullOrEmpty(searchText))
            {
                model = new ProductsListViewModel
                {
                    Products = repository.Products.Where(p => p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PageInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Where(e => e.Category == category).Count()
                    },
                    CurrentCategory = category
                };
            }
            // search with category null & text
            else if (category == null && !string.IsNullOrEmpty(searchText))
            {
                model = new ProductsListViewModel
                {
                    Products = repository.Products.Where
                                                    (p => (p.Name.ToUpper()).Contains(searchText.ToUpper()))
                                                    .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PageInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Where(p => (p.Name.ToUpper()).Contains(searchText.ToUpper())).Count()
                    },
                    CurrentCategory = category
                };
            }
            // search with category null & text empty
            else
            {
                model = new ProductsListViewModel
                {
                    Products = repository.Products.OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
                    PagingInfo = new PageInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Products.Count()
                    },
                    CurrentCategory = category
                };
            }
            ViewBag.ActionName = "Search";
            return PartialView("List", model);

            #region old
            //if (!string.IsNullOrEmpty(searchText))
            //{
            //    model = new ProductsListViewModel
            //    {
            //        Products = repository.Products.Where
            //                                        (p =>
            //                                        (
            //                                        category != null ?

            //                                        (p.Category == category) && (p.Name.ToUpper()).Contains(searchText.ToUpper())

            //                                        :

            //                                        ((p.Name.ToUpper()).Contains(searchText.ToUpper()))

            //                                        )
            //                                        )
            //                                        .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
            //        PagingInfo = new PageInfo
            //        {
            //            CurrentPage = page,
            //            ItemsPerPage = PageSize,
            //            TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
            //        },
            //        CurrentCategory = category
            //    };
            //}
            //else
            //{
            //    model = new ProductsListViewModel
            //    {
            //        Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
            //        PagingInfo = new PageInfo
            //        {
            //            CurrentPage = page,
            //            ItemsPerPage = PageSize,
            //            TotalItems = (category == null) ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
            //        },
            //        CurrentCategory = (category == null) ? null : category
            //    };
            //}
            #endregion


        }
       
    }
}