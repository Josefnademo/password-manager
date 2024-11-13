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
            Console.WriteLine("***************************************");
            Console.WriteLine("*     Selectionnez une action         *");
            Console.WriteLine("*     1.  Consulter un mot de passe   *");
            Console.WriteLine("*     2.  Ajouter un mot de passe     *");
            Console.WriteLine("*     3.  Supprimer un mot de passe   *");
            Console.WriteLine("*     4.  Quitter le programme        *");
            Console.WriteLine("***************************************");

            Console.Write("Faites votre choix : ");
            string answer = Console.ReadLine();

            switch (answer)
            {
                case "1":
                    Console.Clear();
                    ViewPassword();
                    break;
                case "2":
                    Console.Clear();
                    AddPassword();
                    break;
                case "3":
                    Console.Clear();
                    DeletePassword();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    /*ajoute*/
                    Console.WriteLine("Choix invalide, veuillez réessayer.\n \n \n \t\t\t\t***pour recommencer cliquer sur une touche***");
                    Console.ReadKey();
                    Console.Clear();
                    DisplayMenu();
                    break; /*ajoute*/
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
            }
            else
            {
                Console.WriteLine("Aucun mot de passe trouvé pour ce service.");
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