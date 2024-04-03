using AutoPartsForFree.Web.Models;
using CsvHelper;
using CsvHelper.Configuration;
using DatabaseMaster.Entities;
using System.Formats.Asn1;
using System.Globalization;

namespace AutoPartsForFree.Web.Infrastructure
{
    public class CsvHelperService
    {
        /// <summary>
        /// Конвертирует .csv файл в коллекцию  PriceItem.
        /// </summary>
        /// <param name="csvFilePath">Строка, содержащая путь к файлу .csv с данными.</param>
        /// <param name="columnModel"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static List<PriceItem> ParseCsvToPriceItem(string csvFilePath, ColumnModel columnModel, string delimiter, ref List<string> errors)
        {
            List<PriceItem> priceItems = new List<PriceItem>();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = delimiter, // Установка символа разделителя
                BadDataFound = null  // Игнорирование плохих данных
            };

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, config))
            {
                // Чтение записей CSV
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    try
                    {
                        PriceItem priceItem = new PriceItem();

                        // Получение значений из CSV по указанным именам столбцов
                        priceItem.Vendor = csv.GetField(columnModel.NameVendorColumn);
                        priceItem.Number = csv.GetField(columnModel.NameNumberColumn);
                        string description = csv.GetField(columnModel.NameDescriptionColumn);
                        if (description.Length > 512)
                            priceItem.Description = description.Substring(0, 512);
                        else
                            priceItem.Description = description;
                        priceItem.Price = csv.GetField<decimal>(columnModel.NamePriceColumn);
                        priceItem.Count = StringConverter.ParseCount(csv.GetField(columnModel.NameCountColumn));

                        // Редактирование данных перед записью
                        priceItem.SearchVendor = StringConverter.RemoveSpecialCharactersAndToUpper(priceItem.Vendor);
                        priceItem.SearchNumber = StringConverter.RemoveSpecialCharactersAndToUpper(priceItem.Number);

                        priceItems.Add(priceItem);
                    }
                    catch (CsvHelper.TypeConversion.TypeConverterException ex)
                    {
                        // Запись данных об ошибке конвертации
                        errors.Add($"Ошибка преобразования: {ex.Text}, {ex.HResult}");
                        // Пропустить эту строку и перейти к следующей
                        continue;
                    }

                }
            }
            return priceItems;
        }
    }
}
