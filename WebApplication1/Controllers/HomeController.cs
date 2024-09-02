using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApplication1.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private Productcontroller conpro = new Productcontroller();
        // GET: HomeController

        [HttpPost]
        [Route("api/selectDataProducte")]
        public List<ProductModel> Index()
        {
            // select Product
            List<ProductModel> products = new List<ProductModel>();
            products = conpro.Index(@"
                        select pr.*, st.* from Product pr
                        left join Stock st
                        on pr.Product_ID = st.Product_ID");
            return products;
        }

        // GET: HomeController/Details/5
        [HttpPost]
        [Route("api/productShopping")]
        public ActionResult productShopping(ProductModel product)
        {
            //update Stock -
            //List<ProductModel> products = new List<ProductModel>();
            HttpStatusCode res = conpro.update(@"
                        UPDATE [dbo].[Stock]
                            SET [Stok_Amount] = " + product.Amount + @"
                            WHERE [Product_ID] =" + product.Id + @"
                        ");
            return View();
        }

        // GET: HomeController/Create
        [HttpPost]
        [Route("api/shoppingCart")]
        public ActionResult shoppingCart(ProductModel product)
        {
            //update Stock +
            //List<ProductModel> products = new List<ProductModel>();
            HttpStatusCode res = conpro.update(@"
                        UPDATE [dbo].[Stock]
                            SET [Stok_Amount] = " + product.Amount + @"
                            WHERE [Product_ID] =" + product.Id + @"
                        ");
            return View();
        }

        // GET: HomeController/Create
        [HttpPost]
        [Route("api/edit")]
        public ActionResult editShoppingCart(ProductModel product)
        {
            // รวมราคา
            //List<ProductModel> products = new List<ProductModel>();
            HttpStatusCode res = conpro.update(@"
                        UPDATE [dbo].[Stock]
                            SET [Stok_Amount] = " + product.Amount + @"
                            WHERE [Product_ID] =" + product.Id + @"
                        ");
            return View();
        }
    }
}
