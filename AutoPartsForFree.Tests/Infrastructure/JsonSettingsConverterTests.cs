using AutoPartsForFree.Web.Infrastructure;
using AutoPartsForFree.Web.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPartsForFree.Tests.Infrastructure
{
    public class JsonSettingsConverterTests
    {
        [Fact]
        public void ConvertFromJsonFile_ReturnsExpectedConfig()
        {
            // Arrange
            string testJsonFilePath = "test_config.json";
            string testJson = @"
            {
            ""EmailSettings"": {
                ""host"": ""imap.mail.ru"",
                ""port"": 993,
                ""useSsl"": true,
                ""username"": ""forever_021@mail.ru"",
                ""password"": """",
                ""downloadFolder"": ""C:\\download"",
                ""senderAddress"": ""kmerkulov21@gmail.com""
            },
            ""ColumnModel"": {
                ""nameVendorColumn"": ""Бренд"",
                ""nameNumberColumn"": ""Каталожный номер"",
                ""nameDescriptionColumn"": ""Описание"",
                ""namePriceColumn"": ""Цена, руб."",
                ""nameCountColumn"": ""Наличие""
            }
            }";
            File.WriteAllText(testJsonFilePath, testJson);
            ConfigJsonSettings expected = new ConfigJsonSettings()
            {
                ColumnModel = new ColumnModel
                {
                    NameVendorColumn = "Бренд",
                    NameNumberColumn = "Каталожный номер",
                    NameDescriptionColumn = "Описание",
                    NamePriceColumn = "Цена, руб.",
                    NameCountColumn = "Наличие"

                },
                EmailSettings = new EmailSettingsModel
                {
                    Host = "imap.mail.ru",
                    Port = 993,
                    UseSsl = true,
                    Username = "forever_021@mail.ru",
                    Password = "",
                    DownloadFolder = "C:\\download",
                    SenderAddress = "kmerkulov21@gmail.com"

                }
            };

            // Act
            var result = JsonSettingsConverter.ConvertFromJsonFile(testJsonFilePath);

            // Asser
            Assert.Equal(expected.EmailSettings.Host, result.EmailSettings.Host);
            Assert.Equal(expected.EmailSettings.Port, result.EmailSettings.Port);
            Assert.Equal(expected.EmailSettings.UseSsl, result.EmailSettings.UseSsl);
            Assert.Equal(expected.EmailSettings.Username, result.EmailSettings.Username);
            Assert.Equal(expected.EmailSettings.Password, result.EmailSettings.Password);
            Assert.Equal(expected.EmailSettings.SenderAddress, result.EmailSettings.SenderAddress);

            Assert.Equal(expected.ColumnModel.NameVendorColumn, result.ColumnModel.NameVendorColumn);
            Assert.Equal(expected.ColumnModel.NameNumberColumn, result.ColumnModel.NameNumberColumn);
            Assert.Equal(expected.ColumnModel.NameDescriptionColumn, result.ColumnModel.NameDescriptionColumn);
            Assert.Equal(expected.ColumnModel.NamePriceColumn, result.ColumnModel.NamePriceColumn);
        }
    }
}