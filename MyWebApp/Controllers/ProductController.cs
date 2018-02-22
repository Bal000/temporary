using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using MyWebApp.Models;
using MyWebApp.Extensions.Controllers;
using BusinessEntities.Enums;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace MyWebApp.Controllers
{
    public class ProductController : Controller
    {
        public ProductController()
        {
            
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPagedProducts(PageViewModel pageViewModel)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:82/api/product/GetPaginatedProducts");
                var task = httpClient.GetAsync("?page=" + pageViewModel.Page + "&pageSize=" + pageViewModel.PageSize);

                task.Wait();
                var result = task.Result;
                if (result .IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var serializer = new JavaScriptSerializer();
                        List<ProductViewModel> deserializedProducts = serializer.Deserialize<List<ProductViewModel>>(readTask.Result);
                    products = deserializedProducts;
                }
                else //web api sent error response
                {                    
                    //log response status here..
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }

            }
                var paginationViewModel = new PaginationViewModel<ProductViewModel>(pageViewModel, products);
            return new JsonViewResult<PaginationViewModel<ProductViewModel>>("PagedProducts", paginationViewModel , ViewState.Valid);
        }

    }
}
