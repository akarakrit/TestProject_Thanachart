using Microsoft.AspNetCore.Mvc;
using MVC_WebApp.Models;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace MVC_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7081/api/selectDataProducte");
        //    //var response = await client.SendAsync(request);
        //    //response.EnsureSuccessStatusCode();
        //    return View();
        //}


        public async Task<IActionResult> Index(string jsonStr)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (HttpRequestMessage, cert, chain, sslPolicyErrors) => true;

            using HttpClient client = new HttpClient(handler);
            //var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7081/api/selectDataProducte");
            var response = await client.SendAsync(request);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response to a Product object
            var product = JsonSerializer.Deserialize<List<ProductModel>>(jsonResponse);
            response.EnsureSuccessStatusCode();

            return View(product);
            //return (IActionResult)response.Content.ReadAsStringAsync();
        }

        public async Task<IActionResult> productShopping(string jsonStr)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (HttpRequestMessage, cert, chain, sslPolicyErrors) => true;

            using HttpClient client = new HttpClient(handler);
            //var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7081/api/selectDataProducte");
            
            var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/UpdateInspection", content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response to a Product object
            var product = JsonSerializer.Deserialize<List<ProductModel>>(jsonResponse);
            response.EnsureSuccessStatusCode();

            return View(product);
            //return (IActionResult)response.Content.ReadAsStringAsync();
        }

        public async Task<IActionResult> shoppingCart(string jsonStr)
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                (HttpRequestMessage, cert, chain, sslPolicyErrors) => true;

            using HttpClient client = new HttpClient(handler);
            //var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7081/api/selectDataProducte");

            var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/shoppingCart", content);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var product = JsonSerializer.Deserialize<List<ProductModel>>(jsonResponse);
            response.EnsureSuccessStatusCode();

            return View(product);
            //return (IActionResult)response.Content.ReadAsStringAsync();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}