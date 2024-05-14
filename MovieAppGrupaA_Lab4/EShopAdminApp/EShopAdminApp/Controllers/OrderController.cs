using EShopAdminApp.Models;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EShopAdminApp.Controllers
{
    public class OrderController : Controller
    {
        public OrderController() {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            //pravam povik do drugata aplikacija na admin Controllerot
            //za da gi zemam site orders
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5054/api/Admin/GetAllOrders";
            HttpResponseMessage response=client.GetAsync(URL).Result;
            var data = response.Content.ReadAsAsync<List<Order>>().Result;
            return View(data);
        }

        public IActionResult Details(string id)
        {
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5054/api/Admin/GetDetails";

            var model = new
            {
                Id = id
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response=client.PostAsync(URL, content).Result;
            var result=response.Content.ReadAsAsync<Order>().Result;
            return View(result);
        }
        public FileContentResult CreateInvoice(string id)
        {
            HttpClient client = new HttpClient();

            string URL = "http://localhost:5054/api/Admin/GetDetails";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var result=response.Content.ReadAsAsync<Order>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document=DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}", result.Id.ToString());
            document.Content.Replace("{{UserName}}", result.Owner.UserName);

            StringBuilder sb=new StringBuilder();
            var total = 0;
            foreach(var item in result.ProductInOrders)
            {
                sb.AppendLine("Product " + item.OrderedProduct.Movie.MovieName + " has quantity " + item.Quantity + " with price " + item.OrderedProduct.Price);
                total = (int)(total + (item.Quantity * item.OrderedProduct.Price));
            }

            document.Content.Replace("{{ProductList}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", total.ToString() + "$");

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());
            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");

        }
    }
}
