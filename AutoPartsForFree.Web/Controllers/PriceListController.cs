using AutoPartsForFree.Web.Infrastructure;
using AutoPartsForFree.Web.Models;
using DatabaseMaster;
using DatabaseMaster.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AutoPartsForFree.Web.Controllers
{
    public class PriceListController : Controller
    {
        private string progressMessage;
        const string csvDelimiter = ";";

        private readonly DataContext _context;
        public PriceListController(DataContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> DownloadCSVFileFromEmail()
        {
            List<string> convertErrors = new List<string>();
            try
            {
                // Сообщения для отслеживания этапов программы
                 progressMessage = "Получение настроек из файла.";

                string jsonFilePath = "config.json"; // Путь к JSON файлу
                // Чтение данных из JSON файла
                ConfigJsonSettings configJsonSettings = JsonSettingsConverter.ConvertFromJsonFile(jsonFilePath);

                // Получение настроек из JSON
                EmailSettingsModel settings = configJsonSettings.EmailSettings;
                ColumnModel columnModel = configJsonSettings.ColumnModel;

                progressMessage += "Скачивание файла.";

                // Загрузка и сохранение прайс-листа
                if (EmailCSVFileService.DownloadAttachmentFromEmail(ref settings))
                {
                    progressMessage += "Файл скачан.";
                    progressMessage += "Обработка данных.";

                    
                    // Конвертация файла List<priceItems>
                    List<PriceItem> priceItems = CsvHelperService.ParseCsvToPriceItem(settings.DownloadFolder, columnModel, csvDelimiter, ref convertErrors);

                    progressMessage += "Сохранение данных.";
                    await _context.AddRangeAsync(priceItems);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    progressMessage += "Ошибка с загрузкой файла.";
                    return View("Index");
                }

                progressMessage += "Прайс-лист успешно загружен и сохранен в базу данных.";
                ViewBag.Message = progressMessage;
                ViewBag.CustomErrors = convertErrors;
                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = progressMessage;
                ViewBag.Message += "Ошибка: " + ex.Message;
                return View("Index");
            }
        }
    }
}

