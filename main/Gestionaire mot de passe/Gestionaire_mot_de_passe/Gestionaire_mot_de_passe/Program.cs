using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Gestionaire_mot_de_passe
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
        }
    }

    public class Menu
        {
            public static string basePath = AppDomain.CurrentDomain.BaseDirectory;                                    //current program path
            public static string PasswordPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\..\..\passwords"));// path to passwords folder

            private char[] symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[]{};:'\",.<>?/|\\`~".ToCharArray(); // Character array for password generation //all charaters for crypting

            public string[] methodes = new string[] { "Random", "Viginère", "Créer par soi meme", "Accueil" };
            public int menuSelect = 0;                                                                                //Your option choosen               
            public string Master;                                                                                     //master password
            public string passwordFilePath = Path.Combine(PasswordPath, $"{UserInfo.ServiceName}.txt");               //password's  (files)

            public Menu()
            {
                Directory.CreateDirectory(PasswordPath); // Ensure the password directory exists
                DisplayMenu();
            }

            /// <summary>
            /// class to store User's informations
            /// </summary>
            public static class UserInfo
            {
                public static string ServiceName;
                public static string URL;
                public static string Login;
                public static string Password;
            }


            /// <summary>
            /// class to store display basic menu
            /// </summary>
            public void DisplayMenu()
            {
                // Header
                string header = "Bonjour et bienvenue! Choisissez une option dessous:";
                var Padding = (Console.WindowWidth) / 4;
                var PaddingChoice = (Console.WindowWidth) / 3;

                string[] menuOptions = new string[] { "1.  Consulter un mot de passe",
                                                  "2.  Ajouter un mot de passe",
                                                  "3.  Supprimer un mot de passe",
                                                  "4.  Quitter le programme" };

                while (true)
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    Console.WriteLine(new string(' ', Padding) + header + "\n");

                    for (int i = 0; i < menuOptions.Length; i++)
                    {
                        Console.WriteLine(new string(' ', PaddingChoice) + (i == menuSelect ? "  -->" : "") + menuOptions[i] + (i == menuSelect ? "   <--" : ""));
                    }

                    //detecte key pressed,like(this key, which is pressed, will be the value of "keyPressed" variable).
                    var keyPressed = Console.ReadKey();


                    if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != menuOptions.Length - 1 || keyPressed.Key == ConsoleKey.S && menuSelect != menuOptions.Length - 1)
                    {
                        menuSelect++;
                    }
                    else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1 || keyPressed.Key == ConsoleKey.W && menuSelect >= 1)
                    {
                        menuSelect--;
                    }
                    else if (keyPressed.Key == ConsoleKey.Enter || keyPressed.Key == ConsoleKey.Spacebar)
                    {
                        switch (menuSelect)
                        {
                            //1.  Consulter un mot de passe
                            case 0:
                                Console.Clear();
                                ViewPassword();
                                break;

                            //2.Ajouter un mot de passe
                            case 1:
                                Console.Clear();
                                AddPassword();
                                break;

                            //3.Supprimer un mot de passe
                            case 2:
                                Console.Clear();
                                DeletePassword();
                                break;

                            //4.Quitter le programme
                            case 3:
                                Environment.Exit(0);
                                break;

                            //rien
                            default:
                                /*ajoute*/
                                Console.WriteLine("Choix invalide, veuillez réessayer.\n \n \n \t\t\t\t***pour recommencer cliquer sur une touche***");
                                Console.ReadKey();
                                Console.Clear();
                                DisplayMenu();
                                break; /*ajoute*/
                        }
                    }
                }
            }

          
            private void ConfirmDeletePassword()
            {
            int Padding = Console.WindowWidth / 4;
            int PaddingChoice = Console.WindowWidth / 3;
            int PaddingResult = (int)(Console.WindowWidth / 4.5);

            if (File.Exists(passwordFilePath))
            {
                File.Delete(passwordFilePath);
                Console.WriteLine(new string(' ', PaddingResult) + ($"\n\n\nPassword for -{UserInfo.ServiceName}- has been deleted."));
                Console.ReadKey();
                DisplayMenu();
            }
            else
            {
                string[] options = new string[] { "Yes", "No" };
                int menuSelectDelete = 0;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\tNo password found for this service. Try another name? ");
                    Console.CursorVisible = false;

                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.WriteLine(new string(' ', PaddingChoice) + (i == menuSelectDelete ? "  -->" : "") + options[i] + (i == menuSelectDelete ? "  <--" : ""));
                    }

                    var keyPressed = Console.ReadKey();

                    if (keyPressed.Key == ConsoleKey.DownArrow && menuSelectDelete != options.Length - 1)
                    {
                        menuSelectDelete++;
                    }
                    else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelectDelete >= 1)
                    {
                        menuSelectDelete--;
                    }
                    else if (keyPressed.Key == ConsoleKey.Enter || keyPressed.Key == ConsoleKey.Spacebar)
                    {
                        switch (menuSelectDelete)
                        {
                            case 0: // Yes
                                Console.Clear();
                                DeletePassword();
                                return;
                            case 1: // No
                                Console.Clear();
                                DisplayMenu();
                                return;
                        }
                    }
                }
            }
        }


        private void ViewPassword()
        {
            var PaddingInfo = (int)(Console.WindowWidth / 2.5);
            var Padding = (Console.WindowWidth) / 4;
            var PaddingChoice = (Console.WindowWidth) / 3;
            var PaddingResult = (int)(Console.WindowWidth / 4.5);

            Console.Clear();
            Console.WriteLine(new string(' ', PaddingInfo) + "Vos mots de passe enregistrés:");

            //Get all password's files
            string[] passwordFiles = Directory.GetFiles(PasswordPath, "*.txt");

            if (passwordFiles.Length == 0)
            {
                Console.WriteLine(new string(' ', PaddingResult) + "Aucun mot de passe enregistré. Appuyez sur n'importe quelle touche pour revenir.");
                Console.ReadKey();
                DisplayMenu();
                return;
            }
          
            //Show all services
            for (int i = 0; i < passwordFiles.Length; i++)
            {
                string serviceName = Path.GetFileNameWithoutExtension(passwordFiles[i]);
                Console.WriteLine(new string(' ', PaddingInfo) + $"{i + 1}. {serviceName}");
            }

            Console.Write(new string(' ', PaddingResult) + "\nEntrez le numéro du service que vous souhaitez consulter : ");

            // Read user's choosen option
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= passwordFiles.Length) //write result in int "choice"
            {
                string selectedFile = passwordFiles[choice - 1];
                LoadPasswordFromFile(selectedFile);
            }
            else
            {
                NoYes(menuSelect =>
                {
                    switch (menuSelect)
                    {
                        case 0: // Oui
                            Console.Clear();
                            ViewPassword();
                            return true; // completing the execution
                        case 1: // Non
                            Console.Clear();
                            DisplayMenu();
                            return true; // completing the execution
                    }
                    return false; // Продолжаем цикл
                });
            }
        }

        private void LoadPasswordFromFile(string filePath)
        {
            var PaddingInfo = (int)(Console.WindowWidth / 2.5);
            Console.Clear();

            if (!File.Exists(filePath))
            {
                Console.WriteLine(new string(' ', PaddingInfo) + "Fichier de mot de passe introuvable.");
                Console.WriteLine("Appuyez sur n'importe quelle touche pour revenir.");
                Console.ReadKey();
                DisplayMenu();
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length >= 4)
            {
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[0]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[1]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[2]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[3]}");
            }
            else
            {
                Console.WriteLine(new string(' ', PaddingInfo) + "Le format du fichier est incorrect.");
            }

            Console.WriteLine("\nAppuyez sur n'importe quelle touche pour revenir au menu principal.");
            Console.ReadKey();
            DisplayMenu();
        }




        private void AddPassword()
            {
                var Padding = (Console.WindowWidth) / 4;
                var PaddingChoice = (Console.WindowWidth) / 3;
                var PaddingInfo = (int)(Console.WindowWidth / 2.5);
                var PaddingResult = (int)(Console.WindowWidth / 4.5);
                string header1 = "Quelle méthode de shiffrement vous voulez utiliser? : ";
                /////////

                while (true)
                {
                    Console.Clear();
                    Console.CursorVisible = false;
                    Console.WriteLine(new string(' ', Padding) + header1 + "\n");

                    for (int i = 0; i < methodes.Length; i++) { Console.WriteLine(new string(' ', PaddingChoice) + (i == menuSelect ? "  -->" : "") + methodes[i] + (i == menuSelect ? "   <--" : "")); }

                    //detecte key pressed,like(this key, which is pressed, will be the value of "keyPressed" variable).
                    var keyPressed = Console.ReadKey();
                    if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != methodes.Length - 1 || keyPressed.Key == ConsoleKey.S && menuSelect != methodes.Length - 1) { menuSelect++; }
                    else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1 || keyPressed.Key == ConsoleKey.W && menuSelect >= 1) { menuSelect--; }
                    else if (keyPressed.Key == ConsoleKey.Enter || keyPressed.Key == ConsoleKey.Spacebar)
                    {
                        switch (menuSelect)
                        {
                            //1.  Random
                            case 0:
                                Console.Clear();
                                Console.Write(new string(' ', PaddingChoice) + ("Pour quel service voulez-vous créer un mot de passe? : "));
                                UserInfo.ServiceName = Console.ReadLine();

                                Console.Write(new string(' ', PaddingChoice) + ("Quel est votre Login pour ce service? : "));
                                UserInfo.Login = Console.ReadLine();

                                Console.Write(("\t\t *pas obligatoire*      Quel est URL de son site web? (sinon, juste mettez un 'Espace': "));
                                UserInfo.URL = Console.ReadLine();

                                string passwordFilePath = Path.Combine(PasswordPath, $"{UserInfo.ServiceName}.txt");

                                UserInfo.Password = GenerateRandomPassword(12); // Generate a password with 12 characters
                                SavePassword(passwordFilePath, UserInfo.Password);

                                Console.WriteLine(new string(' ', PaddingInfo) + $"Mot de passe pour -{UserInfo.ServiceName}- a été créé : {UserInfo.Password}\n\n\n");
                                Console.WriteLine(new string(' ', PaddingResult) + ("Tout etiat sauvgarder avec succses! Maintenant appuyez sur une touche pour continuer..."));
                                Console.ReadKey();//ajoute
                                break;

                            //2.  Viginère
                            case 1:
                                Console.Clear();
                                Console.WriteLine("Cette méthode de chiffrement est en cours de développement.");
                                Console.WriteLine("Appuyez sur une touche pour revenir au menu principal...");
                                Console.ReadKey();
                                break;

                            //2.  By your own
                            case 2:
                                Console.Clear();
                                Console.Write(new string(' ', PaddingChoice) + ("Pour quel service voulez-vous créer un mot de passe? : "));
                                UserInfo.ServiceName = Console.ReadLine();

                                Console.Write(new string(' ', PaddingChoice) + ("Quel est votre Login pour ce service? : "));
                                UserInfo.Login = Console.ReadLine();

                                Console.Write(("\t\t *pas obligatoire*      Quel est URL de son site web? (sinon, juste mettez un 'Espace': "));
                                UserInfo.URL = Console.ReadLine();

                                passwordFilePath = Path.Combine(PasswordPath, $"{UserInfo.ServiceName}.txt");

                                Console.Write(new string(' ', PaddingInfo) + "Tapper votre mot de passe : ");
                                UserInfo.Password = Console.ReadLine(); // create your own password

                                SavePassword(passwordFilePath, UserInfo.Password);

                                Console.WriteLine(new string(' ', PaddingInfo) + $"Mot de passe pour -{UserInfo.ServiceName}- a été créé : {UserInfo.Password}\n\n\n");
                                Console.WriteLine(new string(' ', PaddingResult) + ("Tout etiat sauvgarder avec succses! Maintenant appuyez sur une touche pour continuer..."));
                                Console.ReadKey();//ajoute
                                break;

                            //2.  Back
                            case 3:
                                Console.Clear();
                                DisplayMenu();
                                break;

                            //rien
                            default:
                                /*ajoute*/
                                Console.WriteLine("Choix invalide, veuillez réessayer.\n \n \n \t\t\t\t***pour recommencer cliquer sur une touche***");
                                Console.ReadKey();
                                Console.Clear();
                                DisplayMenu();
                                break; /*ajoute*/
                        }
                    }
                }
               // Console.ReadKey();//ajoute
              //  DisplayMenu();//ajoute
            }

        private void SavePassword(string filePath, string password)
            {
                try
                {
                    string content = $"Nom du service: {UserInfo.ServiceName}\nLien du service : {UserInfo.URL}\nLogin du compte : {UserInfo.Login}\nMot de passe du compte : {UserInfo.Password}";
                    File.WriteAllText(filePath, content);

                    /* // trnaslation in JSON
                     string jsonContent = System.Text.Json.JsonSerializer.Serialize(passwordData);

                     // recording file
                     File.WriteAllText(filePath, jsonContent);
                     Console.WriteLine("Mot de passe sauvegardé avec succès en format JSON.");*/
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la sauvegarde du mot de passe : {ex.Message}");
                }
            }


        /// <summary>
        /// 
        /// </summary>
        private void DeletePassword()
        {
            int Padding = Console.WindowWidth / 4;
            int PaddingChoice = Console.WindowWidth / 3;
            int PaddingResult = (int)(Console.WindowWidth / 4.5);

            Console.WriteLine(new string(' ', Padding) + "Choose a file to delete:");

            var files = Directory.GetFiles(PasswordPath, "*.txt");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(new string(' ', Padding) + $"{i + 1}. {Path.GetFileName(files[i])}");
            }

            Console.Write(new string(' ', PaddingResult) + "\nEnter the number of the file you want to delete: ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= files.Length)
            {
                passwordFilePath = files[choice - 1];
                ConfirmDeletePassword();
            }
            else
            {
                NoYes(menuSelect =>
                {
                    switch (menuSelect)
                    {
                        case 0: // Oui
                            Console.Clear();
                            DeletePassword();
                            return true; // completing the execution
                        case 1: // Non
                            Console.Clear();
                            DisplayMenu();
                            return true; // completing the execution
                    }
                    return false; // Продолжаем цикл
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        private void NoYes(Func<int, bool> action)
        {
            var PaddingChoice = Console.WindowWidth / 3;
            string[] Options = new string[] { "  Oui", "  Non" };
            int menuSelect = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\t\t\tAucun mot de passe trouvé pour ce service. Essayer un autre nom? ");
                Console.CursorVisible = false;

                for (int i = 0; i < Options.Length; i++)
                {
                    Console.WriteLine(new string(' ', PaddingChoice) + (i == menuSelect ? "  -->" : "") + Options[i] + (i == menuSelect ? "  <--" : ""));
                }

                var keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != Options.Length - 1)
                {
                    menuSelect++;
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow && menuSelect >= 1)
                {
                    menuSelect--;
                }
                else if (keyPressed.Key == ConsoleKey.Enter || keyPressed.Key == ConsoleKey.Spacebar)
                {
                    switch (menuSelect)
                    {
                        case 0: // Oui
                            Console.Clear();
                            ViewPassword();
                            return;
                        case 1: // Non
                            Console.Clear();
                            DisplayMenu();
                            return;
                    }
                }
            }
        }

        /// <summary>
        /// Random password generator
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GenerateRandomPassword(int length)
            {
                Random rand = new Random();
                char[] password = new char[length];

                for (int i = 0; i < length; i++)
                {
                    password[i] = symbols[rand.Next(symbols.Length)];
                }
                return new string(password);
            }
        }

    ///////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        class VigenereCipher
        {
            // Метод для шифрования текста
            public static string Encrypt(string text, string key)
            {
                string result = "";
                key = key.ToUpper();
                int keyIndex = 0;

                foreach (char character in text)
                {
                    if (char.IsLetter(character))
                    {
                        bool isUpper = char.IsUpper(character);
                        char offset = isUpper ? 'A' : 'a';

                        // Сдвиг символа с использованием ключа
                        char encryptedChar = (char)((character - offset + (key[keyIndex] - 'A')) % 26 + offset);
                        result += encryptedChar;

                        // Переход к следующей букве ключа
                        keyIndex = (keyIndex + 1) % key.Length;
                    }
                    else
                    {
                        // Если символ не буква, добавляем его без изменений
                        result += character;
                    }
                }

                return result;
            }

            // Метод для расшифровки текста
            public static string Decrypt(string text, string key)
            {
                string result = "";
                key = key.ToUpper();
                int keyIndex = 0;

                foreach (char character in text)
                {
                    if (char.IsLetter(character))
                    {
                        bool isUpper = char.IsUpper(character);
                        char offset = isUpper ? 'A' : 'a';

                        // Обратный сдвиг символа с использованием ключа
                        char decryptedChar = (char)((character - offset - (key[keyIndex] - 'A') + 26) % 26 + offset);
                        result += decryptedChar;

                        // Переход к следующей букве ключа
                        keyIndex = (keyIndex + 1) % key.Length;
                    }
                    else
                    {
                        // Если символ не буква, добавляем его без изменений
                        result += character;
                    }
                }

                return result;
            }

            // Проверка валидности ключа
            public static bool IsKeyValid(string key)
            {
                foreach (char c in key)
                {
                    if (!char.IsLetter(c))
                        return false;
                }
                return true;
            }
       /*
           // static void Main(string[] args)
            {
                try
                {
                    Console.Write("Введите текст для шифрования: ");
                    string text = Console.ReadLine();

                    Console.Write("Введите ключ: ");
                    string key = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(key) || !IsKeyValid(key))
                    {
                        throw new ArgumentException("Ключ должен содержать только буквы и не быть пустым.");
                    }

                    string encryptedText = Encrypt(text, key);
                    Console.WriteLine($"Зашифрованный текст: {encryptedText}");

                    string decryptedText = Decrypt(encryptedText, key);
                    Console.WriteLine($"Расшифрованный текст: {decryptedText}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
            }*/
        }
}
