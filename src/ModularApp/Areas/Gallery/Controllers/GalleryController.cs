using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Gallery.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Gallery.Controllers
{
    [Area("Gallery")]
    public class GalleryController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var gallery = new Gallery.Models.Gallery();
            return View();
        }
    }
}
