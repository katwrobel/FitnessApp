using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PO_Project
{
    [Serializable]
    /// <summary>
    /// Klasa ListOfUsers reprezentuje listę użytkowników w systemie. 
    /// </summary>
    public class ListOfUsers : ICloneable
    {
         List<User> users;

        /// <summary>
        /// Właściwość pobiera lub ustawia listę użytkowników. 
        /// </summary>
        public  List<User> Users { get => users; set => users = value; }

        /// <summary>
        /// Konstruktor nieparametryczny, inicjalizuje nowy obiekt ListOfUsers z pustą listą użytkowników.
        /// </summary>
        public ListOfUsers()
        {
            Users = new();
        }

        //sprawdzanie czy istnieje taki uzytkownik

        public bool AuthenticateUser(string login, string password)
        {
            User? user = Users.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                // Użytkownik o podanym loginie i haśle istnieje, więc zwróć komunikat o udanym logowaniu
                return false;//$"Zalogowano pomyślnie jako {user.GenerateNickname()}";
            }
            else
            {
                // Użytkownik o podanym loginie i haśle nie istnieje, więc zwróć komunikat o błędzie logowania
                return true;//"Niepoprawny login lub hasło.";
            }
        }

        public void RegisterUser(User newUser) //logika
        {
            

                // Dodaj nowego użytkownika
                Users.Add(newUser);
            

        }

        /// <summary>
        /// Metoda GenerateNickname generuje unikalny nick dla podanego użytkownika, korzystając z metody GenerateNickname z klasy User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateNickname(User user)
        {
            user.GenerateNickname();  // Wywołaj metodę z klasy User
            return user.Nick;  // Zwróć nowy nick
        }
        // czy to jest potrzebne tu??????

        public void SortUsers()
        {

            Users.Sort();

        }
        public void DeleteUsers(User us)
        {
            if (us is not null)
            {
                Users.Remove(us);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Uzytkownicy: ");
            foreach (User user in Users)
            {
                sb.AppendLine(user.ToString());
            }
            return sb.ToString();
        }

        public void ZapiszXML(string nazwa)
        {
            using StreamWriter sw = new(nazwa);
            XmlSerializer xs = new(typeof(ListOfUsers));
            xs.Serialize(sw, this);
        }
        public static ListOfUsers? OdczytXml(string nazwa)
        {
            if (!File.Exists(nazwa))
            {
                return null;
            }
            using StreamReader sw = new(nazwa);
            XmlSerializer xs = new(typeof(ListOfUsers));
            return xs.Deserialize(sw) as ListOfUsers;
        }

        public object Clone() //klonowanie glebokie
        {
            ListOfUsers listOfUsers = new ListOfUsers();
            listOfUsers = (ListOfUsers)this.MemberwiseClone();
            listOfUsers.Users = new List<User>();
            foreach (User us in this.Users)
                listOfUsers.RegisterUser((User)us.Clone());

            return listOfUsers;
        }
    }
}
