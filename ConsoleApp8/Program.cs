
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;


namespace ConsoleApp8
{
    internal class Program
    {
        
        static string token = "5922847029:AAH_EAUc2gYx9N_KXNuPw8zspjT8GuzLCd4";
        static TelegramBotClient client;

        static void Main(string[] args)
        {
            client = new TelegramBotClient(token);
            client.StartReceiving(updateHandler, pollingErrorHandler);
            Console.ReadLine();

        }

        #region createButtons zone
        public static InlineKeyboardMarkup CreateTextButtonsList(InlineKeyboardButton[][] buttons, List<string> itemList)
        {
            int count = 0;
            for (int i = 1; i < itemList.Count() + 1; i += 2)
            {
                buttons[count] = new[]
                {
                            InlineKeyboardButton.WithCallbackData(itemList[i-1], itemList[i])
                };
                count++;
            }
            buttons[buttons.Length - 1] = BackButton();
            return buttons;
        }

        public static InlineKeyboardMarkup CreateTextButtonsList(InlineKeyboardButton[][] buttons, List<string> itemList, int objectInLine)
        {
            int count = 1;
           
            var buttonLine = new InlineKeyboardButton[objectInLine];
            for (int i = 0; i < itemList.Count()/objectInLine/2; i ++)
            {
                var inlineKeyboardButton = new InlineKeyboardButton[objectInLine];
                for (int k = 0; k < objectInLine; k++)
                {
                    inlineKeyboardButton[k] = InlineKeyboardButton.WithCallbackData(itemList[count - 1], itemList[count]);
                    count += 2;
                }
                buttons[i] = inlineKeyboardButton;
            }
            int mod = itemList.Count() % (objectInLine * 2);
            if (mod != 0)
            {
                Array.Resize(ref buttons, itemList.Count() / objectInLine / 2+1);
                Array.Resize(ref buttonLine, mod/2);
                for (int i = 0; i < mod/2; i++)
                {
                    buttonLine[i] = InlineKeyboardButton.WithCallbackData(itemList[count - 1], itemList[count]);
                    count += 2;
                }
                buttons[buttons.Length - 2] = buttonLine;
            }
            buttons[buttons.Length - 1] = BackButton();
            Console.WriteLine("Hello world");
            Console.WriteLine(buttons.GetType());
            Console.WriteLine(buttons[0].GetType()  );
            return buttons;
        }

        private static InlineKeyboardMarkup StartButton()
        {
            var inlineButton = new[]
            {
                InlineKeyboardButton.WithCallbackData("Начать погружение в программу Каркас безопасности", "start"),
            };
            return inlineButton;
        }

        private static InlineKeyboardButton[] BackButton() 
        {
            var z = new string[]
            {
                "Авиа"
            };
            var BackButton = new InlineKeyboardButton[1];
            BackButton[0] = InlineKeyboardButton.WithCallbackData("Назад", "Back");
            return BackButton;
        }

        static public InlineKeyboardMarkup Getbutton(string messageText)
        {
            
            InlineKeyboardButton[][] buttons;
            List<string> mainButtonsList = new List<string>()
            {
                "Что такое программа Каркас безопасности", "Choose_1",
                "Паспорт барьеров", "Choose_2",
                "Обеспечение работоспособности барьеров", "Choose_3",
                "Оценка работоспособности барьеров", "Choose_4"
            };
            List<string> secondChooseItems = new List<string>()
            {
                "Авиа","Авиа",
                "ВЗР", "ВЗР",
                "ГАЗ(H2S)", "ГАЗ(H2S)",
                "ГАЗ", "ГАЗ",
                "ГНВП", "ГНВП",
                "ГРУЗ", "ГРУЗ",
                "ДВИЖ", "ДВИЖ",
                "ДТП", "ДТП",
                "ЗЕМЛ", "ЗЕМЛ",
                "ОЗ", "ОЗ",
                "РВ", "РВ",
                "РВ(drops)", "РВ(drops)",
                "СБУ", "СБУ",
                "ЭКО", "ЭКО",
                "ЭЛБ", "ЭЛБ",
                "ШЕЛФ", "ШЕЛФ",
            };
            
            var Description = new[]
            {
                InlineKeyboardButton.WithCallbackData("Ok", "Back")
            };
            switch (messageText)
            {
                case "start":
                    buttons = new InlineKeyboardButton[mainButtonsList.Count / 2 +1][];
                    return CreateTextButtonsList(buttons, mainButtonsList);
                case "Choose_1":
                    return Description;
                case "Choose_2":
                    buttons = new InlineKeyboardButton[secondChooseItems.Count / 8 + 1][];
                    return CreateTextButtonsList(buttons, secondChooseItems, 4); // Если менять количество элементов в строке, то надо менять последнюю переменную в данной функции, и размер массива в строке выше по формуле(количетсво элементов в строке * 2)
                case "Back":
                    buttons = new InlineKeyboardButton[mainButtonsList.Count/2][];
                    return CreateTextButtonsList(buttons, mainButtonsList);

                default:
                    break;
            }

            buttons = new InlineKeyboardButton[mainButtonsList.Count / 2 + 1][];
            return CreateTextButtonsList(buttons, mainButtonsList);
        }

        #endregion



        private static async Task updateHandler(ITelegramBotClient arg1, Update arg2, CancellationToken arg3)
        {

            var msg = arg2.Message;
            string messageText;
           

            if (msg == null && arg2.Type == UpdateType.CallbackQuery)
            {
                
                var callback = arg2.CallbackQuery;
                messageText = callback.Data;
            }
            else
            {
                await Console.Out.WriteLineAsync(msg.Chat.Id.ToString());
                messageText = msg.Text;
            }
           
            switch (messageText)
            {
                case "/start":
                    await arg1.SendTextMessageAsync(msg.Chat.Id, "Привет, Knight! \r\nМеня зовут Дима Рисковик, я твой помощник в реализации" +
                                                            " программы Каркас безопасности.", replyMarkup: StartButton());
                    break;
                case "start":
                   
                    await arg1.SendTextMessageAsync(arg2.CallbackQuery.From.Id, "Выберите раздел", replyMarkup: Getbutton(messageText));
                    break;
                case "Choose_1":
                    await arg1.SendTextMessageAsync(arg2.CallbackQuery.From.Id, "Программа Каркас безопасности - система отбора и внедрения барьеров на пути" +
                        " реализации ключевых рисков Компании в области производственной безопасности; \r\n" +
                        "  \r\nБарьер - Совокупность мероприятий, направленных на снижение вероятности наступления" +
                        " происшествия или на уменьшение возможных последствий от него; \r\n \r\nМесто установки барьера - место," +
                        " на котором предусматривается  установка барьера и на котором барьер должен выполнять свое защитное предназначение;" +
                        " \r\n \r\nКритерии работоспособности барьера - неотъемлемые признаки барьера, необходимые и совокупно достаточные для" +
                        " обеспечения способности барьера выполнять своё заданное защитное предназначение: \r\n \r\n- снижать вероятность" +
                        " наступления происшествия ПБ или \r\n- уменьшать возможные последствия происшествия ПБ; \r\n \r\nЧек-лист - вопросы," +
                        " необходимые для определения соответствия проверяемого барьера критериям работоспособности барьера.", replyMarkup: Getbutton(messageText));
                    break;
                case "Choose_2":
                    await arg1.SendTextMessageAsync(arg2.CallbackQuery.From.Id, "Паспорта барьеров", replyMarkup: Getbutton(messageText));
                    break;
                case "Back":
                    await arg1.SendTextMessageAsync(arg2.CallbackQuery.From.Id, "Выберите раздел", replyMarkup: Getbutton(messageText));
                    break;
                case "Авиа":    
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                        {
                            InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);
                            
                            await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");
                            

                        }
                    break;
                case "ВЗР":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");


                    }
                    break;
                case "ГАЗ(H2S)":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");


                    }
                    break;
                case "ГАЗ":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");


                    }
                    break;
                case "ГНВП":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");


                    }
                    break;
                case "ГРУЗ":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");


                    }
                    break;
                case "ДВИЖ":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");


                    }
                    break;
                case "ДТП":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");


                    }
                    break;
                case "ЗЕМЛ":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");
                    }
                    break;
                case "ОЗ":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");
                    }
                    break;
                case "РВ":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");
                    }
                    break;
                case "РВ(drops)":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");
                    }
                    break;
                case "СБУ":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");
                    }
                    break;
                case "ЭКО":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");
                    }
                    break;
                case "ЭЛБ":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");
                    }
                    break;
                case "ШЕЛФ":
                    using (var stream = System.IO.File.OpenRead($"Паспорта барьеров\\{messageText}.xlsx"))
                    {
                        InputOnlineFile inputOnlineFile = new InputOnlineFile(stream);

                        await arg1.SendDocumentAsync(arg2.CallbackQuery.From.Id, inputOnlineFile, caption: $"{messageText}.xlsx");
                    }
                    break;
                default:
                    await Console.Out.WriteLineAsync("неожиданный ответ");
                    break;
            }







        }



        private static  Task pollingErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            Console.WriteLine(arg2.Message);
            throw new NotImplementedException();
        }
    }
}
