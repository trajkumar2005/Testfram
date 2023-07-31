using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Testfram.Models;

namespace Testfram.Controllers
{
    public class TestController : Controller
    {
        // GET: TestController
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TestController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: TestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestController/Create
        [HttpPost]
       public ActionResult Create(String data1)

        {
           string file1 = $"{Guid.NewGuid()}"+".xml";
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, file1);
            System.IO.File.WriteAllText(filePath, data1);

            return Json(new { success = true, message = "Drawing saved successfully." });
        }

        [HttpPost]
        public  async Task<IActionResult> Savedraw([FromBody] Draw_model dr)
        {

            return Ok();
        }

        public ActionResult  Load( string filename)
        {
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "drawing.xml");
            if (System.IO.File.Exists(filePath))
            {
                XDocument xmlData = XDocument.Load(filePath);
                var points = xmlData.Root.Elements("point")
                    .Select(p => new
                    {
                        x = int.Parse(p.Attribute("x").Value),
                        y = int.Parse(p.Attribute("y").Value)
                    }).ToList();

                return Json(points);
            }

            return Json(null);
        }
    }
       
}
