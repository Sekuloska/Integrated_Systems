using AdminApplication.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdminApplication.Controllers
{
    public class ConcertController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImportConcerts(IFormFile file)
        {
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";

            using (FileStream filestream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(filestream);
                filestream.Flush();
            }

            List<Concert> concerts = getAllConcertsFromFile(file.FileName);
            HttpClient client = new HttpClient();
            string URL = "http://localhost:5054/api/Admin/ImportAllConcerts";

            HttpContent content = new StringContent(JsonConvert.SerializeObject(concerts), Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            return RedirectToAction("Index", "Order");


        }

        private List<Concert> getAllConcertsFromFile(string fileName)
        {
            List<Concert> concerts = new List<Concert>();

            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        concerts.Add(new Concert
                        {
                            Id=Guid.NewGuid(),
                            ConcertName = reader.GetValue(0).ToString(),
                            ConcertDescription = reader.GetValue(1).ToString(),
                            ConcertImage = reader.GetValue(2).ToString(),
                           Rating= Double.Parse(reader.GetValue(3).ToString())
                        });

                    }
                }
            }
            return concerts;
        }


    }
}

