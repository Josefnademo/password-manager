using System;
using System.Collections.Generic;
using System.IO;

namespace Gestionaire_mot_de_passe
{
    public class Menu
    {
        public static string basePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string PasswordPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\..\..\passwords"));

        // Character array for password generation
        private char[] symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[]{};:'\",.<>?/|\\`~".ToCharArray();



        public string[] methodes = new string[] { "Random","Viginère","Créer par soi meme","Accueil"};
        public int menuSelect = 0;
        public string Master;



        public Menu()
        {
            Directory.CreateDirectory(PasswordPath); // Ensure the password directory exists
            DisplayMenu();
        }

        //class to store User's informations
        public static class UserInfo
        {
            public static string ServiceName;
            public static string URL;
            public static string Login;
            public static string Password;
        }

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
        private void ViewPassword()
        {
            var Padding = (Console.WindowWidth) / 4;
            var PaddingChoice = (Console.WindowWidth) / 3;

            //Console.WriteLine($"{UserInfo.ServiceName} : {UserInfo.Password}    ");
            Console.Write(new string(' ',Padding)+("Entrez le nom du service pour consulter le mot de passe : "));
            UserInfo.ServiceName = Console.ReadLine();
            
            string passwordFilePath = Path.Combine(PasswordPath, $"{UserInfo.ServiceName}.txt");

            LoadPassword(passwordFilePath);
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

            if (File.Exists(passwordFilePath))
            {
                File.Delete(passwordFilePath);
                Console.WriteLine(new string(' ', PaddingResult) + ($"\n\n\nLe mot de passe pour -{UserInfo.ServiceName}- a été supprimé."));
                Console.ReadKey();
                DisplayMenu();
            }
            else
            {
                var Padding = (Console.WindowWidth) / 4;
                var PaddingChoice = (Console.WindowWidth) / 3;

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
        private void SavePassword(string filePath, string password)
        {
            try
            {
                string content = $"Service: {UserInfo.ServiceName}\nURL: {UserInfo.URL}\nLogin: {UserInfo.Login}\nPassword: {UserInfo.Password}";
                File.WriteAllText(filePath, content);

                /* // Преобразование в JSON
                 string jsonContent = System.Text.Json.JsonSerializer.Serialize(passwordData);

                 // Запись в файл
                 File.WriteAllText(filePath, jsonContent);
                 Console.WriteLine("Mot de passe sauvegardé avec succès en format JSON.");*/
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde du mot de passe : {ex.Message}");
            }
        }

        private void LoadPassword(string filePath)
        {
            var PaddingInfo = (int)(Console.WindowWidth / 2.5);
            var PaddingResult = (int)(Console.WindowWidth / 4.5);
            try
            {
                if (File.Exists(filePath))// if there is a wanted service, programme load the data ,so those infos are going to be showed
                {
                    string password = File.ReadAllText(filePath);
                    Console.WriteLine(new string(' ', PaddingInfo) + ($"Non de service : {UserInfo.ServiceName}"));
                    Console.WriteLine(new string(' ', PaddingInfo) + ($"Login du compte : {UserInfo.Login}"));
                    Console.WriteLine(new string(' ', PaddingInfo) + ($"Mot de passe du compte: {UserInfo.Password}"));
                    Console.WriteLine(new string(' ', PaddingInfo) + ($"Lien du service : {UserInfo.URL}"));
                    Console.ReadLine();

                    Console.WriteLine(new string(' ', PaddingResult) +("Appuyez sur une touche pour revenir au menu principal..."));
                    Console.ReadKey();

                }
                else    // if there is wanted no service, programme can't load the data ,so this line of code is going to be executed
                {
                    Console.WriteLine("Aucun mot de passe trouvé pour ce service.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)//exeption if programme can't load the data this line of code is going to be executed
            {
                Console.WriteLine($"Erreur lors du chargement du mot de passe : {ex.Message}");
                Console.ReadLine();
            }
        }

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

    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
        }
    }
}
