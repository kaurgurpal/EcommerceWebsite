using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class SearchController : Controller
    {
        private IProductsRepository repository;

        public int PageSize = 100;

        public SearchController(IProductsRepository repo)
        {
            repository = repo;
        }

        //// GET: Search
        //public PartialViewResult Search()
        //{
        //    //ViewBag.SelectedCategory = category;
        //    IEnumerable<string> categories = repository.Products
        //                                    .Select(p => p.Category)
        //                                    .Distinct()
        //                                    .OrderBy(p => p);
        //    return PartialView("SearchBox", categories);
        //}

        //[HttpPost]
        //public PartialViewResult SearchBox(string category, int page = 1, string searchText = null)
        //{
        //    category = category == "All" ? null : category;

        //    ProductsListViewModel model;
        //    if (!string.IsNullOrEmpty(searchText))
        //    {
        //        model = new ProductsListViewModel
        //        {
        //            Products = repository.Products.Where
        //                                            (p => 
        //                                            (
        //                                            category!=null?

        //                                            (p.Category == category) && (p.Name.ToUpper()).Contains(searchText.ToUpper())
                                                    
        //                                            :
                                                    
        //                                            ((p.Name.ToUpper()).Contains(searchText.ToUpper()))

        //                                            )                                                    
        //                                            )
        //                                            .OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
        //            PagingInfo = new PageInfo
        //            {
        //                CurrentPage = page,
        //                ItemsPerPage = PageSize,
        //                TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
        //            },
        //            CurrentCategory = category
        //        };
        //    }
        //    else
        //    {
        //        model = new ProductsListViewModel
        //        {
        //            Products = repository.Products.Where(p => category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1) * PageSize).Take(PageSize),
        //            PagingInfo = new PageInfo
        //            {
        //                CurrentPage = page,
        //                ItemsPerPage = PageSize,
        //                TotalItems = (category == null) ? repository.Products.Count() : repository.Products.Where(e => e.Category == category).Count()
        //            },
        //            CurrentCategory = (category == null )? null : category
        //        };
        //    }
        //    ViewBag.ControllerName = "Search";
        //    return PartialView("../Product/List", model);
        //}
    }
}