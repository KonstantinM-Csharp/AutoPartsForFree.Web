using AutoPartsForFree.Web.Models;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System.Net.Mail;

namespace AutoPartsForFree.Web.Infrastructure
{
    public class EmailService
    {
        public static bool DownloadAttachmentFromEmail(ref EmailSettingsModel settings)
        {
            using (var client = new ImapClient())
            {
                client.Connect(settings.Host, settings.Port, settings.UseSsl);
                client.Authenticate(settings.Username, settings.Password);

                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly);

                // Поиск сообщений от конкретного отправителя
                var searchQuery = SearchQuery.FromContains(settings.SenderAddress);
                var uids = inbox.Search(searchQuery);

                // Проверяем, найдены ли какие-либо письма
                if (uids.Any())
                {
                    // Сортируем письма по дате в обратном порядке (от новых к старым)
                    var uidsArray = uids.ToArray();
                    Array.Reverse(uidsArray);

                    // Проверяем наличие вложений в последнем письме
                    for (int i = 0; i < uidsArray.Length; i++)
                    {
                        var message = inbox.GetMessage(uidsArray[i]);
                        if (message.Attachments.Any())
                        {
                            var csvAttachment = message.Attachments.OfType<MimePart>().Where(IsCsvAttachment).First();

                            var fileName = csvAttachment.FileName;
                            var filePath = Path.Combine(settings.DownloadFolder, fileName);
                            settings.DownloadFolder = filePath;
                            using (var stream = File.Create(filePath))
                            {
                                csvAttachment.Content.DecodeTo(stream);
                            }
                            Console.WriteLine($"Файл {fileName} скачан.");
                            return true;
                        }
                        else break;
                    }
                    Console.WriteLine("Нет писем от указанного отправителя.");
                }
                client.Disconnect(true);
                return false;
            }
        }

        private static bool IsCsvAttachment(MimePart attachment)
        {
            var contentType = attachment.ContentType.MimeType.ToLower();
            return contentType.StartsWith("text/csv") || contentType.StartsWith("application/csv");
        }

    }
}
