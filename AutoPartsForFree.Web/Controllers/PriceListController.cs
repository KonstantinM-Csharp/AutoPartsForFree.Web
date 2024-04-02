using DatabaseMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoPartsForFree.Web.Controllers
{
    public class PriceListController : Controller
    {
        private readonly DataContext _context;
        public PriceListController(DataContext context)
        {
            _context = context;
        }
        // GET: Price/Index
        public ActionResult Index()
        {
            return View();
        }

        // POST: Price/Download
        [HttpPost]
        public ActionResult Download()
        {
            // Здесь вызывается метод загрузки из почтового ящика и сохранения в базу данных
            try
            {
                ViewBag.Message = "Прайс-лист успешно загружен и сохранен в базу данных.";
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ошибка: " + ex.Message;
            }

            return View("Index");
        }
    }
}

