/******************************************************************************* 
** Projet      Gestionaire de mots de passes                                  ** 
**                                                                            ** 
** Lieu       : ETML - section informatique                                   ** 
** Auteur     : Yosef Nademo                                                  ** 
** Date       : 08.01.2025                                                    ** 
**                                                                            ** 
** Modifications                                                              ** 
**   Auteur   :                                                               ** 
**   Version  :                                                               ** 
**   Date     :                                                               ** 
**   Raisons  :                                                               ** 
**                                                                            ** 
**                                                                            ** 
*******************************************************************************/

/****************************************************************************************************** 
** DESCRIPTION :                                                                                     ** 
**   Ce programme implémente un gestionnaire de mots de passe sécurisé en C++.                       ** 
**   Il permet de stocker, gérer et récupérer des mots de passe de manière sécurisée.                ** 
**   L'utilisateur peut ajouter, supprimer, rechercher et afficher les mots de passe.                ** 
**                                                                                                   ** 
** PRINCIPALES FONCTIONNALITÉS                                                                       ** 
** - Ajout de nouveaux mots de passe avec un chiffrement sécurisé.                                   ** 
** - Recherche et récupération d'un mot de passe existant par un identifiant unique.                 ** 
** - Suppression d'un mot de passe spécifique.                                                       ** 
** - Affichage de la liste des identifiants enregistrés.                                             ** 
** - Sauvegarde et chargement des mots de passe depuis un fichier sécurisé.                          ** 
**                                                                                                   ** 
** MÉTHODES PRINCIPALES :                                                                            ** 
**                                                                                                   **
** - public static void SetMasterPassword(string filePath)                                           ** 
**    Permet à l'utilisateur de configurer un mot de passe principal. L'utilisateur est invité à     **
**    saisir un nouveau mot de passe et à le confirmer. Si les mots de passe correspondent, le mot de**
**    passe principal est enregistré dans un fichier pour une authentification ultérieure.           ** 
**                                                                                                   ** 
** - public static void AuthenticateMasterPassword(string filePath)                                  **
**    Authentifie l'utilisateur en vérifiant le mot de passe principal. L'utilisateur est invité à   **
**    saisir son mot de passe principal. Si le mot de passe saisi correspond à celui enregistré,     **
**    l'authentification est réussie. Sinon, l'utilisateur est invité à réessayer.                   **
**                                                                                                   **
**- public Menu()                                                                                    ** 
**     Initialise l'environnement du programme en s'assurant que le répertoire des mots de passe     ** 
**     existe et lance l'affichage du menu.                                                          ** 
**                                                                                                   ** 
** - public void DisplayMenu()                                                                       ** 
**     Affiche le menu principal et gère la navigation de l'utilisateur. Permet de sélectionner      ** 
**     les différentes options grâce aux touches du clavier.                                         ** 
**                                                                                                   ** 
** - private void ViewPassword()                                                                     ** 
**     Affiche une liste des mots de passe enregistrés. Permet à l'utilisateur de sélectionner un    ** 
**     fichier pour consulter son contenu.                                                           ** 
**                                                                                                   ** 
** - private void NoYes(Func<int, bool> action)                                                      ** 
**     Affiche une confirmation (Oui/Non) pour certaines actions utilisateur.                        ** 
**     Gère la navigation via les touches fléchées et valide le choix. Retourne au menu ou           ** 
**     lance l'action en conséquence.                                                                ** 
**                                                                                                   ** 
** - private void LoadPasswordFromFile(string filePath)                                              ** 
**     Charge et affiche le contenu d'un fichier de mot de passe sélectionné. Gère les erreurs       ** 
**     si le fichier est manquant ou mal formaté.                                                    ** 
**                                                                                                   ** 
** - private void GenerateRandomPassword(int length)                                                 ** 
**     Génère un mot de passe aléatoire en fonction de la longueur spécifiée et d'une liste          ** 
**     de symboles prédéfinis. Retourne une chaîne de caractères contenant le mot de passe.          ** 
**                                                                                                   ** 
**      - private void DeletePassword()                                                              ** 
**     Permet à l'utilisateur de choisir un fichier de mot de passe à supprimer.                     ** 
**     Affiche les fichiers disponibles, demande la confirmation pour supprimer le fichier choisi,   ** 
**     et retourne au menu principal si l'utilisateur annule l'action.                               **   
**                                                                                                   ** 
** - private void AddPassword()                                                                      ** 
**     Permet à l'utilisateur de créer un nouveau mot de passe en choisissant l'une des méthodes     ** 
**     proposées (aléatoire, chiffrement Vigenère ou manuel). Sauvegarde ensuite le mot de passe.    ** 
**     Gère les saisies et les validations pour chaque option choisie.                               ** 
**                                                                                                   ** 
** - private void SavePassword(string filePath, string password)                                     ** 
**     Sauvegarde les informations d'un mot de passe dans un fichier texte, y compris les détails    ** 
**     comme le nom du service, l'URL, le login, ainsi que la version chiffrée si applicable.        ** 
**     Gère les erreurs de sauvegarde en cas de problème d'accès au fichier.                         ** 
**                                                                                                   ** 
** - private void ConfirmDeletePassword()                                                            ** 
**     Supprime un fichier de mot de passe après confirmation. Si le fichier n'existe pas, propose   ** 
**     à l'utilisateur de réessayer ou de revenir au menu principal.                                 ** 
**                                                                                                   **
**\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\** 
**                                                                                                   **
** - public static string Encrypt(string text, string key)                                           ** 
**     Encrypte le texte donné en utilisant le chiffrement Vigenère avec la clé spécifiée.           ** 
**     La clé doit uniquement contenir des caractères alphabétiques.                                 ** 
**     Retourne le texte chiffré.                                                                    ** 
**                                                                                                   ** 
** - public static string Decrypt(string text, string key)                                           ** 
**     Décrypte le texte donné en utilisant le chiffrement Vigenère avec la clé spécifiée.           ** 
**     La clé doit uniquement contenir des caractères alphabétiques.                                 ** 
**     Retourne le texte déchiffré.                                                                  ** 
**                                                                                                   ** 
** - public static bool IsKeyValid(string key)                                                       ** 
**     Vérifie si la clé fournie est valide. Une clé valide ne doit contenir que des caractères      ** 
**     alphabétiques. Retourne `true` si la clé est valide, sinon `false`.                           ** 
**                                                                                                   ** 
**                                                                                                   **
**                                                                                                   **
**                                                                                                   **
******************************************************************************************************/



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
            // Ensure the master-password directory exists
            Directory.CreateDirectory(Menu.MasterPasswordPath);

            // Check if the master password is already set by verifying its existence in a file
            string masterPasswordFilePath = Path.Combine(Menu.MasterPasswordPath, "masterPassword.txt");

            if (File.Exists(masterPasswordFilePath))
            {
                // Authenticate with the existing master password
                Menu.AuthenticateMasterPassword(masterPasswordFilePath);
            }
            else
            {
                // If not set, prompt the user to create a new master password
                Menu.SetMasterPassword(masterPasswordFilePath);
            }

            // Proceed to the main menu after authentication
            Menu menu = new Menu();
            menu.DisplayMenu();
        }
    }

    public class Menu
        {
            private static string masterPassword;                                                                     // Stores the master password
            public static string basePath = AppDomain.CurrentDomain.BaseDirectory;                                    // current program path
            public static string PasswordPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\..\..\passwords"));// path to passwords folder
            public static string MasterPasswordPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\..\..\passwords\config"));           // path to passwords folder

            private char[] symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()-_=+[]{};:'\",.<>?/|\\`~".ToCharArray(); // Character array for password generation //all charaters for crypting

            public string[] methodes = new string[] { "Random", "Viginère", "Créer par soi meme", "Accueil" };        //possible methods to create a password
            public int menuSelect = 0;                                                                                //Your option choosen               
            public string passwordFilePath = Path.Combine(PasswordPath, $"{UserInfo.ServiceName}.txt");               //password's  (files)
            private  enum UserChoice { view , add, remove, exit};                                                     //choice of menu

            public Menu()
            {
                Directory.CreateDirectory(PasswordPath); // Ensure the password directory exists
                DisplayMenu();
            }

            /// <summary>
            /// class to store and share user information between methods 
            /// </summary>
            public static class UserInfo
            {
                public static string ServiceName;
                public static string URL;
                public static string Login;
                public static string Password;
                public static string PasswordEncrypted;
                public static string key;
            }

        /// <summary>
        /// Set the master password and save it to a file for future authentication.
        /// </summary>
        /// <param name="filePath">The file path to save the master password.</param>
        public static void SetMasterPassword(string filePath)
        {
            Console.WriteLine("Setting up the master password.");
            Console.Write("Enter a new master password: ");
            string password = Console.ReadLine();
            Console.Write("Confirm the master password: ");
            string confirmPassword = Console.ReadLine();

            if (password == confirmPassword)
            {
                // Save the master password to a file (in this case, we use plaintext for simplicity)
                File.WriteAllText(filePath, password);
                masterPassword = password;  // Store it in memory (for this example)
                Console.WriteLine("Master password successfully set!");
            }
            else
            {
                Console.WriteLine("Passwords do not match. Please try again.  -(click on Enter to continue)");
                Console.ReadLine();
                Console.Clear();
                SetMasterPassword(filePath);  // Retry if passwords don't match
            }
        }

        /// <summary>
        /// Authenticate the user by comparing the entered password with the stored master password.
        /// </summary>
        /// <param name="filePath">The file path where the master password is stored.</param>
        public static void AuthenticateMasterPassword(string filePath)
        {
            string storedPassword = File.ReadAllText(filePath);  // Read the stored master password

            Console.WriteLine("Authentication required.");
            Console.Write("Enter your master password: ");
            string enteredPassword = Console.ReadLine();

            if (enteredPassword == storedPassword)
            {
                masterPassword = enteredPassword;  // Store it in memory for this session
                Console.WriteLine("Authentication successful!");
            }
            else
            {
                Console.WriteLine("Incorrect password. Please try again.    -(click on Enter to continue)");
                Console.ReadLine();
                Console.Clear();
                AuthenticateMasterPassword(filePath);  // Retry if incorrect
            }
        }
    


    /// <summary>
    /// Displays the main menu with options for managing passwords.
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
                        switch ((UserChoice)menuSelect)
                        {
                            //1.  Consulter un mot de passe
                            case UserChoice.view:
                                Console.Clear();
                                ViewPassword();
                                break;

                            //2.Ajouter un mot de passe
                            case UserChoice.add:
                                Console.Clear();
                                AddPassword();
                                break;

                            //3.Supprimer un mot de passe
                            case UserChoice.remove:
                                Console.Clear();
                                DeletePassword();
                                break;

                            //4.Quitter le programme
                            case UserChoice.exit:
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

            /// <summary>
            /// Confirms the deletion of a password by asking the user for confirmation.
            /// </summary>
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

        /// <summary>
        /// Displays a list of saved passwords for the user to view.
        /// </summary>
        private void ViewPassword()
        {
            var PaddingInfo = (int)(Console.WindowWidth / 2.5);
            var Padding = (Console.WindowWidth) / 4;
            var PaddingChoice = (Console.WindowWidth) / 3;
            var PaddingResult = (int)(Console.WindowWidth / 4.5);

            Console.Clear();
            Console.WriteLine(new string(' ', PaddingInfo) + "Vos mots de passe enregistrés:");

            // Get all password files
            string[] passwordFiles = Directory.GetFiles(PasswordPath, "*.txt");

            if (passwordFiles.Length == 0)
            {
                Console.WriteLine(new string(' ', PaddingResult) + "Aucun mot de passe enregistré. Appuyez sur n'importe quelle touche pour revenir.");
                Console.ReadKey();
                DisplayMenu();
                return;
            }

            // Display all services
            for (int i = 0; i < passwordFiles.Length; i++)
            {
                string serviceName = Path.GetFileNameWithoutExtension(passwordFiles[i]);
                Console.WriteLine(new string(' ', PaddingInfo) + $"{i + 1}. {serviceName}");
            }

            Console.Write(new string(' ', PaddingResult) + "\nEntrez le numéro du service que vous souhaitez consulter : ");

            // Read user's chosen option
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
                    return false; // Let's continue the cycle
                });
            }
        }

        /// <summary>
        /// Loads and displays the content of a selected password file.
        /// </summary>
        /// <param name="filePath">The path to the password file.</param>
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

            if (lines.Length >= 6) //for files with ecryption method
            {
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[0]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[1]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[2]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[3]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[4]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[5]}");
            }
            else if(lines.Length >= 4)//for files without ecryption method
            {

                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[0]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[1]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[2]}");
                Console.WriteLine(new string(' ', PaddingInfo) + $"{lines[3]}"); 
            }
            
            else{
                Console.WriteLine(new string(' ', PaddingInfo) + "Le format du fichier est incorrect.");
            }

            Console.WriteLine("\nAppuyez sur n'importe quelle touche pour revenir au menu principal.");
            Console.ReadKey();
            DisplayMenu();
        }


        /// <summary>
        /// Adds a new password to the password manager, allowing the user to choose a method for password creation.
        /// </summary>
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
                            try
                            {
                                Console.Write(new string(' ', PaddingChoice) + ("Pour quel service voulez-vous créer un mot de passe? : "));
                                UserInfo.ServiceName = Console.ReadLine();

                                Console.Write(new string(' ', PaddingChoice) + ("Quel est votre Login pour ce service? : "));
                                UserInfo.Login = Console.ReadLine();

                                Console.Write(("\t\t *pas obligatoire*      Quel est URL de son site web? (sinon, juste mettez un 'Espace': "));
                                UserInfo.URL = Console.ReadLine();

                                Console.Write("Entrez le mot de passe pour chiffrer: ");
                                string text = Console.ReadLine();

                                Console.Write("Entrez le master-password (La clé): ");
                                string key = Console.ReadLine()/*masterPassword*/;

                                if (string.IsNullOrWhiteSpace(key) || !VigenereCipher.IsKeyValid(key))
                                {
                                    throw new ArgumentException("La clé doit contenir uniquement des lettres et ne pas être vide.");
                                }

                                string encryptedText = VigenereCipher.Encrypt(text, key);
                                Console.WriteLine($"mot de passe crypté : {encryptedText}");
                                UserInfo.PasswordEncrypted = encryptedText;

                               /* string decryptedText = VigenereCipher.Decrypt(encryptedText, key);
                                Console.WriteLine($"mot de passe decrypté : {decryptedText}");
                                UserInfo.Password = decryptedText;
                                UserInfo.key = key;*/

                                passwordFilePath = Path.Combine(PasswordPath, $"{UserInfo.ServiceName}.txt");
                                SavePassword(passwordFilePath, UserInfo.Password);

                                Console.WriteLine(new string(' ', PaddingInfo) + $"Mot de passe pour -{UserInfo.ServiceName}- a été créé et crypté  : {UserInfo.Password}\n\n\n");
                                Console.WriteLine(new string(' ', PaddingResult) + ("Tout etiat sauvgarder avec succses! Maintenant appuyez sur une touche pour continuer..."));
                                Console.ReadKey();//ajoute
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
                            }
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
                                Console.WriteLine("Choix invalide, veuillez réessayer.\n \n \n \t\t\t\t***pour recommencer cliquer sur une touche***");
                                Console.ReadKey();
                                Console.Clear();
                                DisplayMenu();
                                break; 
                        }
                    }
                }
            }

        /// <summary>
        /// Saves the user-entered password information to a file.
        /// </summary>
        private void SavePassword(string filePath, string password)
            {
                try
                {
                    string content = $"Nom du service: {UserInfo.ServiceName}\nLien du service : {UserInfo.URL}\nLogin du compte : {UserInfo.Login}\nMot de passe du compte : {UserInfo.Password}\nMot de passe *encrypté* du compte : {UserInfo.PasswordEncrypted}\nclé de chiffrement :  {UserInfo.key}";
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
        /// Deletes a password from the password manager based on user confirmation.
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
                    return false; // Let's continue the cycle
                });
            }
        }

        /// <summary>
        /// Helper function for Yes/No confirmation prompts
        /// </summary>
        /// <param name="action">The action to perform based on the user's choice (Yes/No).</param>
        /// <returns>True if an action is completed, false otherwise.</returns>
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
        /// Generates a random password using a specified length and characters.
        /// </summary> 
        /// <param name="length"></param>
        /// <returns>A randomly generated password string.</returns>
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
    /// Vigenere Cipher class to encrypt and decrypt text using the Vigenere cipher technique.
    /// </summary>
    public class VigenereCipher
        {

        // Method to encrypt text using the Vigenere cipher
        /// <summary>
        /// Encrypts the given text using the Vigenere cipher with the specified key.
        /// </summary>
        /// <param name="text">The text to encrypt.</param>
        /// <param name="key">The key to use for encryption. It should only contain alphabetic characters.</param>
        /// <returns>The encrypted text.</returns>
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

                        // Shift character using the key
                        char encryptedChar = (char)((character - offset + (key[keyIndex] - 'A')) % 26 + offset);
                        result += encryptedChar;

                        // Move to the next character of the key
                        keyIndex = (keyIndex + 1) % key.Length;
                    }
                    else
                    {
                        // If the character is not a letter, add it unchanged
                        result += character;
                    }
                }
                return result;
        }

        // Method to decrypt text using the Vigenere cipher
        /// <summary>
        /// Decrypts the given text using the Vigenere cipher with the specified key.
        /// </summary>
        /// <param name="text">The text to decrypt.</param>
        /// <param name="key">The key to use for decryption. It should only contain alphabetic characters.</param>
        /// <returns>The decrypted text.</returns>
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

                        // Reverse shift character using the key
                        char decryptedChar = (char)((character - offset - (key[keyIndex] - 'A') + 26) % 26 + offset);
                        result += decryptedChar;

                        // Move to the next character of the key
                        keyIndex = (keyIndex + 1) % key.Length;
                    }
                    else
                    {
                     // If the character is not a letter, add it unchanged
                     result += character;
                    }
                }
                return result;
        }

        // Method to check if the key is valid
        /// <summary>
        /// Checks if the provided key is valid. A valid key should only contain alphabetic characters.
        /// </summary>
        /// <param name="key">The key to validate.</param>
        /// <returns>True if the key is valid, otherwise false.</returns>
        public static bool IsKeyValid(string key)
        {
          foreach (char c in key)
          {
              if (!char.IsLetter(c))
                  return false;
          }
          return true;
        }
    }
}
