using System;
using System.Collections.Generic;
using System.IO;

namespace Gestionaire_mot_de_passe
{
    public class Menu
    {
        public static string basePath = AppDomain.CurrentDomain.BaseDirectory;                                    //current program path
        public static string PasswordPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\..\..\passwords"));// path to passwords folder

        private char[] symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[]{};:'\",.<>?/|\\`~".ToCharArray(); // Character array for password generation //all charaters for crypting

        public string[] methodes = new string[] { "Random","Viginère","Créer par soi meme","Accueil"};
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
            string header = "Hello and welcome! Please choose one of the options below:";
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
                    Console.WriteLine(new string(' ', PaddingChoice) +(i == menuSelect ? "  -->" : "") + menuOptions[i] + (i == menuSelect ? "   <--" : ""));
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

        private void NoYesDeletePassword()
        {
            var Padding = (Console.WindowWidth) / 4;
            var PaddingChoice = (Console.WindowWidth) / 3;
            var PaddingResult = (int)(Console.WindowWidth / 4.5);

            if (File.Exists(passwordFilePath))
            {
                File.Delete(passwordFilePath);
                Console.WriteLine(new string(' ', PaddingResult) + ($"\n\n\nLe mot de passe pour -{UserInfo.ServiceName}- a été supprimé."));
                Console.ReadKey();
                DisplayMenu();
            }
            else
            {
                string[] Options = new string[] { "  Oui", "  Non" };
                int menuSelectDelete = 0;
                
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\tAucun mot de passe trouvé pour ce service. Essayer un autre nom? ");
                    Console.CursorVisible = false;



                    for (int i = 0; i < Options.Length; i++)
                    {
                        Console.WriteLine(new string(' ', PaddingChoice) + (i == menuSelectDelete ? "  -->" : "") + Options[i] + (i == menuSelectDelete ? "  <--" : ""));
                    }

                    //detecte key pressed,like(this key, which is pressed, will be the value of "keyPressed" variable).
                    var keyPressed = Console.ReadKey();

                    if (keyPressed.Key == ConsoleKey.DownArrow && menuSelectDelete != Options.Length - 1)
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
                            //1.  OUI. retaper
                            case 0:
                                Console.Clear();
                                DeletePassword();
                                break;

                            //2. NON. ouvrir le menu
                            case 1:
                                Console.Clear();
                                DisplayMenu();
                                break;
                        }
                    }
                }
            }
        }
        private void NoYesForViewPassword()
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

        private void ViewPassword()
        {
            var Padding = (Console.WindowWidth) / 4;
            var PaddingChoice = (Console.WindowWidth) / 3;
            var PaddingResult = (int)(Console.WindowWidth / 4.5);

            //Console.WriteLine($"{UserInfo.ServiceName} : {UserInfo.Password}    ");
            Console.Write(new string(' ',Padding)+("Entrez le nom du service pour consulter le mot de passe : "));
            UserInfo.ServiceName = Console.ReadLine();
            
            string passwordFilePath = Path.Combine(PasswordPath, $"{UserInfo.ServiceName}.txt");

            LoadPasswordFromFile(passwordFilePath);
            NoYesForViewPassword();
        }
        

        private void AddPassword()
        {                                                                                                                                                                                                                          
            var Padding = (Console.WindowWidth) / 4;
            var PaddingChoice = (Console.WindowWidth) / 3;
            var PaddingInfo = (int)(Console.WindowWidth/ 2.5);
            var PaddingResult = (int)(Console.WindowWidth / 4.5);
            string header1 = "Quelle méthode de shiffrement vous voulez utiliser? : ";
            /////////

            while (true)
            { Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine(new string(' ', Padding) + header1 + "\n");

                for (int i = 0; i < methodes.Length; i++) { Console.WriteLine(new string(' ', PaddingChoice) + (i == menuSelect ? "  -->" : "") + methodes[i] + (i == menuSelect ? "   <--" : "")); }

                //detecte key pressed,like(this key, which is pressed, will be the value of "keyPressed" variable).
                var keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != methodes.Length - 1 || keyPressed.Key == ConsoleKey.S && menuSelect != methodes.Length - 1) { menuSelect++;}
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
                            Console.Write(new string(' ', PaddingChoice)+("Pour quel service voulez-vous créer un mot de passe? : "));
                            UserInfo.ServiceName = Console.ReadLine();

                            Console.Write(new string(' ', PaddingChoice) + ("Quel est votre Login pour ce service? : "));
                            UserInfo.Login = Console.ReadLine();
                            
                            Console.Write(("\t\t *pas obligatoire*      Quel est URL de son site web? (sinon, juste mettez un 'Espace': "));
                            UserInfo.URL = Console.ReadLine();

                            passwordFilePath = Path.Combine(PasswordPath, $"{UserInfo.ServiceName}.txt");

                            Console.Write(new string(' ', PaddingInfo) +"Tapper votre mot de passe : ");
                            UserInfo.Password = Console.ReadLine(); // create your own password

                            SavePassword(passwordFilePath, UserInfo.Password);

                            Console.WriteLine(new string(' ', PaddingInfo)+$"Mot de passe pour -{UserInfo.ServiceName}- a été créé : {UserInfo.Password}\n\n\n");
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
            Console.ReadKey();//ajoute
            DisplayMenu();//ajoute
        }

        private void DeletePassword()
        {
            var PaddingResult = (int)(Console.WindowWidth / 4.5);

            Console.WriteLine("Entrez le nom du service à supprimer : ");
            UserInfo.ServiceName = Console.ReadLine();

            string passwordFilePath = Path.Combine(PasswordPath, $"{UserInfo.ServiceName}.txt");

            NoYesDeletePassword(); // no yes methode
        }
       /* private void DeletePassword()
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

            Console.WriteLine(new string(' ', PaddingResult) + "\nEnter the number of the file you want to delete:");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= files.Length)
            {
                passwordFilePath = files[choice - 1];
                //ConfirmDeletePassword();
            }
            else
            {
                Console.WriteLine(new string(' ', PaddingResult) + "\t\tInvalid file number.");
                Console.ReadKey();
                Console.Clear();
                DeletePassword();
            }
        }*/

        private void SavePassword(string filePath, string password)
        {
            try
            {
                string content = $"Service: {UserInfo.ServiceName}\nURL: {UserInfo.URL}\nLogin: {UserInfo.Login}\nPassword: {UserInfo.Password}";
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

        private void LoadPasswordFromFile(string filePath)
        {
            var PaddingInfo = (int)(Console.WindowWidth / 2.5);
            var PaddingResult = (int)(Console.WindowWidth / 4.5);

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                // Display a list of services
                Console.WriteLine(new string(' ', PaddingInfo) + "Choisissez un service pour consulter le mot de passe :");

                for (int i = 0; i < lines.Length / 4; i++)
                {
                    Console.WriteLine(new string(' ', PaddingInfo) + $"{i + 1}. {lines[i * 4]}"); // Show service name
                }

                Console.WriteLine(new string(' ', PaddingResult) + "\nEntrez le numéro du service que vous souhaitez consulter :");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= lines.Length / 4)
                {
                    int index = (choice - 1) * 4;
                    Console.WriteLine(new string(' ', PaddingInfo) + $"Nom du service : {lines[index]}");
                    Console.WriteLine(new string(' ', PaddingInfo) + $"Login du compte : {lines[index + 2]}");
                    Console.WriteLine(new string(' ', PaddingInfo) + $"Mot de passe du compte : {lines[index + 3]}");
                    Console.WriteLine(new string(' ', PaddingInfo) + $"Lien du service : {lines[index + 1]}");
                }
                else
                {
                    Console.WriteLine(new string(' ', PaddingResult) + "\t\tNuméro de service invalide.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement du mot de passe : {ex.Message}");
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

        static void Main(string[] args)
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
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
        }
    }
}