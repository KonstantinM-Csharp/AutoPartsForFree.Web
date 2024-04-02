using AutoPartsForFree.Web.Infrastructure;
using AutoPartsForFree.Web.Models;
using DatabaseMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Download()
        {
            try
            {
                // Отправляем сообщения о прогрессе в частичном представлении
                string progressMessage = "Получение настроек из файла...";
                progressMessage += "<br>Скачивание файла...";

                string jsonFilePath = "config.json"; // Путь к JSON файлу

                // Чтение данных из JSON файла
                ConfigJsonSettings configJsonSettings = JsonSettingsConverter.ConvertFromJsonFile(jsonFilePath);

                // Получение настроек из JSON
                EmailSettingsModel settings = configJsonSettings.EmailSettings;
                ColumnModel columnModel = configJsonSettings.ColumnModel;

                progressMessage += "<br>Скачивание файла...";
                // Ваш код для загрузки и сохранения прайс-листа
                if (EmailService.DownloadAttachmentFromEmail(ref settings))
                {
                    progressMessage += "Обработка данных.";
                    string delimiter = ";";
                    var priceItems = CsvHelperService.ParseCsvToPriceItem(settings.DownloadFolder, columnModel, delimiter);
                    progressMessage += "<br>Сохранение данных.";
                    await _context.AddRangeAsync(priceItems);
                    await _context.SaveChangesAsync();
                }
                else progressMessage += "<br>Ошибка с загрузкой файла.";

                progressMessage += "<br>Прайс-лист успешно загружен и сохранен в базу данных.";

                return PartialView("_ProgressMessages", progressMessage);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ошибка: " + ex.Message;
                return PartialView("_ProgressMessages", "<div class='alert alert-danger'>Ошибка: " + ex.Message + "</div>");
            }
        }
    }
}

