using DigitalWatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;


namespace DigitalWatch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IOptions<MyOptions> _options;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment,IOptions<MyOptions> options)
        {
            _logger = logger;
            _environment = environment;
            this._options = options;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string OnGetAjax()
        {
            Random random = new Random();
            return random.Next(0, 1000).ToString();
        }

        public string OnGetAdd()
        {
            return _options.Value.MyAdd();
        }
        public string OnGetDateTimeTest()
        {
            return Program.DateTimeTest;
        }
        public ActionResult FontVazir()
        {
            return View();
        }


        public IActionResult Capture()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Capture(string name)
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var fileNameToStore = string.Concat(Convert.ToString(Guid.NewGuid()), Path.GetExtension(fileName));
                            //  Path to store the snapshot in local folder
                            var filepath = Path.Combine(_environment.WebRootPath, "Photos") + $@"\{fileNameToStore}";

                            // Save image file in local folder
                            if (!string.IsNullOrEmpty(filepath))
                            {
                                using (FileStream fileStream = System.IO.File.Create(filepath))
                                {
                                    file.CopyTo(fileStream);
                                    fileStream.Flush();
                                }
                            }

                            // Save image file in database
                            var imgBytes = System.IO.File.ReadAllBytes(filepath);
                            if (imgBytes != null)
                            {
                                if (imgBytes != null)
                                {
                                    string base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                                    string imageUrl = string.Concat("data:image/jpg;base64,", base64String);

                                    // Code to store into database
                                    // save filename and image url(base 64 string) to the database
                                }
                            }
                        }
                    }
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
    }
