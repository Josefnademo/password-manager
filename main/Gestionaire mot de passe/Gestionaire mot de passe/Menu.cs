using System;
using System.Collections.Generic;
using System.IO;

namespace Gestionaire_mot_de_passe
{
    public class Menu
    {
        public static string basePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string PasswordPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\..\..\password"));

        // Character array for password generation
        private char[] symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[]{};:'\",.<>?/|\\`~".ToCharArray();

        private string serviceName;

        public Menu()
        {
            Directory.CreateDirectory(PasswordPath); // Ensure the password directory exists
            DisplayMenu();
        }

        public void DisplayMenu()
        {

            /* Console.WriteLine(@"
                             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%#*++++++*#%@@@@@@@@@@@@@@@@@@
                             @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%++++++++++++++===+#@@@@@@@@@@@@@@
                             @@@@@@@@@@@@@@@%%@@@@@@@@@@@@@@@@@@@@*+++++++++++++++++++++++#@@@@@@@@@@@
                             @@@@@@@@@@@@@@------=#@@@@@@@@@@@@@*+++++++++++++++++++++++++++#@@@@@@@@@
                             @@@@@@@@@@@@@@*--------+@@@@@@@@@*+++++++++++++++++=========+++++@@@@@@@@
                             @@@@@@@@@@@@@@@@#=-----=##=%@@@@#+++++++++++++++++++=---=+=+++++++%@@@@@@
                             @@@@@@@@@@@@@@@@@@@%#+*%%#---=@%++++++++++++++++++++++=--+=+=++++++@@@@@@
                             @@@@@@@@@@@@@@@@@@@@@@@@%%++--=+++++++++++++++++++++++++=-==++++++++@@@@@
                             @@@@@@@@@@@%#*#+*-------=#=---+++++++**+++++++++++++++++++++++++++++#@@@@
                             @@@@%-----=--=+*-------=#%#+-=++++*+==+%+=+++++++++++++++++++++++++++@@@@
                             @@@%-------=*+++-----==+*---+++++*+*#*-%%===+++++++++++++++++++++++++@@@@
                             @@@+----==++==------=+-=-------+===##%%@%=====+++++++++++++++++++++++@@@@
                             @@@@%#@@@@@@@@@@#=--=*=-=---===*====+*##*==++++++++++++++++++++++++++@@@@
                             @@@@@@@@@%%#*=---=====+*+===--==+=====++==+++++++++++++++++++++++++++@@@@
                             @@@@@@@@----=------============-=+======++++++++++++++++++++++++++++#@@@@
                             @@@@@@@------=========+*++++==+*+++======++++++++++++++++++++++++++*@@@@@
                             @@@@@@#=---*###%%@@@@@@@@@@@@%*===++====++==+**+++++++++++++++++++*@@@@@@
                             @@@@@@@%%@@@@@@@@@@@@@@@@@@@%%%#=#@%*+##++%+=+++++**+++++++++++++*@@@@@@@
                             @@@@@@@@@@@@@@@@@@@@@@@@@+--+#%%@@@@**##%%@@****++++++++++++++++%@@@@@@@@
                             @@@@@@@@@@@@@@@@@@@@@@@@+-----#@@@@@@@#*++*#***+**********+=-+%@@@@@@@@@@
                             @@@@@@@@@@@@@@@@@@@@@@@#---=*@@@@@@@@@@@@@#++************+==*@@@@@@@@@@@@
                             @@@@@@@@@@@@@@@@@@@@@@@@#*%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%@@@@@@@@@@@@@@@");*/
            string[] menuOptions = new string[] { "1.  Consulter un mot de passe", "2.  Ajouter un mot de passe", "3.  Supprimer un mot de passe", "4.  Quitter le programme" };
            int menuSelect = 0;

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine("Hello and welcome! Please choose type of registration:");

                for (int i = 0; i < menuOptions.Length; i++)
                {
                    Console.WriteLine((i == menuSelect ? "  -->   " : "") + menuOptions[i] + (i == menuSelect ? "   <--" : ""));
                }

                //detecte key pressed,like(this key, which is pressed, will be the value of "keyPressed" variable).
                var keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.DownArrow && menuSelect != menuOptions.Length - 1)
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

        private void AddPassword()
        {
            Console.WriteLine("Pour quel service voulez-vous créer un mot de passe? : ");
            serviceName = Console.ReadLine();
            string passwordFilePath = Path.Combine(PasswordPath, $"{serviceName}.txt");

            string newPassword = GenerateRandomPassword(12); // Generate a password with 12 characters
            SavePassword(passwordFilePath, newPassword);

            Console.WriteLine($"Mot de passe pour {serviceName} a été créé : {newPassword}");
            Console.ReadKey();//ajoute
            DisplayMenu();//ajoute
        }

        private void ViewPassword()
        {
            //string password = File.ReadAllText(string filePath);

           // Console.WriteLine($"{serviceName} : {password}    ");
            Console.WriteLine("Entrez le nom du service pour consulter le mot de passe : ");
            serviceName = Console.ReadLine();
            string passwordFilePath = Path.Combine(PasswordPath, $"{serviceName}.txt");

            LoadPassword(passwordFilePath);
        }

        private void DeletePassword()
        {
            Console.WriteLine("Entrez le nom du service à supprimer : ");
            serviceName = Console.ReadLine();

            string passwordFilePath = Path.Combine(PasswordPath, $"{serviceName}.txt");

            if (File.Exists(passwordFilePath))
            {
                File.Delete(passwordFilePath);
                Console.WriteLine($"Le mot de passe pour {serviceName} a été supprimé.");
                Console.ReadKey();
                DisplayMenu();
            }
            else
            {                
                string[] menuOptions = new string[] { "Oui", "Non" };
                int menuSelectDelete = 0;

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\tAucun mot de passe trouvé pour ce service. Essayer un autre nom? ");
                    Console.CursorVisible = false;
                    

                    for (int i = 0; i < menuOptions.Length; i++)
                    {
                        Console.WriteLine((i == menuSelectDelete ? "\t\t\t\t\t-->  " : "") + menuOptions[i] + (i == menuSelectDelete ? "   <--" : ""));
                    }

                    //detecte key pressed,like(this key, which is pressed, will be the value of "keyPressed" variable).
                    var keyPressed = Console.ReadKey();

                    if (keyPressed.Key == ConsoleKey.DownArrow && menuSelectDelete != menuOptions.Length - 1)
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
                File.WriteAllText(filePath, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde du mot de passe : {ex.Message}");
            }
        }

        private void LoadPassword(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string password = File.ReadAllText(filePath);
                    Console.WriteLine($"Mot de passe chargé : {password}");
                }
                else
                {
                    Console.WriteLine("Aucun mot de passe trouvé pour ce service.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement du mot de passe : {ex.Message}");
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
