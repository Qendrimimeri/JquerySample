using Microsoft.AspNetCore.Mvc;
using SampleApp.Models;
using System.Diagnostics;

namespace SampleApp.Controllers
{
    public class HomeController : Controller
    {
        private List<Data> Data = new ();
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult LoadData(int page = 1, int pageSize = 10000)
        {
            // Logic to fetch data from your data source
            // Adjust this part based on your data retrieval mechanism
            MyData();
            var data = YourDataFetchingLogic(page, pageSize);

            return PartialView("_DataPartialView", data.OrderBy(x => x.Id));
        }

        // Your actual data fetching logic goes here
        private IEnumerable<Data> YourDataFetchingLogic(int page, int pageSize)
        {
            var startIndex = (page - 1) * pageSize;
            var dataPage = Data.OrderByDescending(x => x.Id).Skip(startIndex).Take(pageSize).ToList();
            return dataPage;
        }
        
        private void MyData()
        {
            var test = new List<Data>();
            var random = new Random();
            for (int i = 0; i < 100000; i++) 
            {
                test.Add(new Data
                {
                    Id = i,
                    Name = GenerateRandomString(7)
                });
            }
            Data = test;
        }
        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new ();

            char[] randomArray = new char[length];
            for (int i = 0; i < length; i++)
            {
                randomArray[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomArray);
        }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
