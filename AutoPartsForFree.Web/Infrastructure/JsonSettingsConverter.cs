using AutoPartsForFree.Web.Models;
using Newtonsoft.Json;

namespace AutoPartsForFree.Web.Infrastructure
{
    public class JsonSettingsConverter
    {
        /// <summary>
        /// Считывает данные для конфигурации почтового ящика и колонок файла из JSON-файла.
        /// </summary>
        /// <param name="filePath">Путь к JSON-файлу.</param>
        /// <returns>Объект ConfigJsonSettings, содержащий данные конфигурации.</returns>
        public static ConfigJsonSettings ConvertFromJsonFile(string filePath)
        {
            string jsonText = File.ReadAllText(filePath);

            var settings = JsonConvert.DeserializeObject<ConfigJsonSettings>(jsonText);

            var emailSettings = new EmailSettingsModel
            {
                Host = settings.EmailSettings.Host,
                Port = settings.EmailSettings.Port,
                UseSsl = settings.EmailSettings.UseSsl,
                Username = settings.EmailSettings.Username,
                Password = settings.EmailSettings.Password,
                DownloadFolder = settings.EmailSettings.DownloadFolder,
                SenderAddress = settings.EmailSettings.SenderAddress
            };

            var columnModel = new ColumnModel
            {
                NameVendorColumn = settings.ColumnModel.NameVendorColumn,
                NameNumberColumn = settings.ColumnModel.NameNumberColumn,
                NameDescriptionColumn = settings.ColumnModel.NameDescriptionColumn,
                NamePriceColumn = settings.ColumnModel.NamePriceColumn,
                NameCountColumn = settings.ColumnModel.NameCountColumn
            };
            ConfigJsonSettings configJsonSettings = new ConfigJsonSettings()
            {
                EmailSettings = emailSettings,
                ColumnModel = columnModel,
            };
            return configJsonSettings;
        }

    }
}
