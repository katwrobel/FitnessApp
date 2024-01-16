﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PO_Project
{
    public enum EnumGender { Female, Male }
    public class User : ICloneable, IEquatable<User>, IComparable<User> //abstrakcyjna?? 
    {
       
        /// <summary>
        /// Klasa User reprezentuje użytkownika, przechowując jego dane: imię, nazwisko, nick, wagę, wzrost, płeć oraz unikalny nick. 
        /// </summary>


        string name;
        string lastName;
        string nick;
        double weight;
        double height;
        EnumGender gender;
        string login;
        string password;
        //lista do przechowywania użytkowników , możliwe połączenie z bazą
         
         static List<string> usedNicks = new List<string>();


        /// <summary>
        /// Właściwość pobiera lub ustawia imię użytkownika, imię musi zawierać tylko litery i mieć co najmniej 3 znaki.
        /// </summary>

        public string Name { get => name;
            set
            {
                // Wyrażenie regularne sprawdzające, czy imię zawiera tylko litery
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$") || value.Length < 3)
                {
                    throw new WrongData();
                }

                name = value;
            }
        }

        /// <summary>
        /// Właściwość pobiera lub ustawia nazwisko użytkownika, nazwisko musi zawierać tylko litery i mieć co najmniej 3 znaki.
        /// </summary>
        public string LastName { get => lastName;
            set
            {
                // Wyrażenie regularne sprawdzające, czy nazwisko zawiera tylko litery
                if (!Regex.IsMatch(value, @"^[a-zA-Z]+$")|| value.Length < 3)
                {
                    throw new WrongData();
                }

                lastName = value;
            }
        }


        /// <summary>
        /// Właściwość pobiera lub ustawia nick użytkownika.
        /// </summary>
        public string Nick { get => nick; set => nick = value; }

        public UserTrainings UserTrainings { get; set; } = new UserTrainings(); 

        /// <summary>
        /// Właściwość pobiera lub ustawia wagę użytkownika, waga musi mieścić się w zakresie od 30 do 300 kg.
        /// </summary>
        public double Weight
        {
            get => weight;
            set
            {
                if (value < 30 || value > 300)
                {
                    throw new WrongData();
                   
                }
                weight = value;
            }
        }

        /// <summary>
        /// Właściwość pobiera lub ustawia wzrost użytkownika, wzrost musi mieścić się w zakresie od 1.0 do 2.5 metra.
        /// </summary>
        public double Height
        {
            get => height;
            set
            {
                if (value < 1.0 || value > 2.5)
                    throw new WrongData();
                height = value;
            }
        }

        /// <summary>
        /// Właściwość pobiera lub ustawia płeć użytkownika.
        /// </summary>
        public EnumGender Gender { get => gender; set => gender = value; }

        /// <summary>
        /// Właściwość pobiera lub ustawia listę wszystkich użytkowników.
        /// </summary>
    

        /// <summary>
        /// Właściwość pobiera lub ustawia listę używanych nicków.
        /// </summary>
        public static List<string> UsedNicks { get => usedNicks; set => usedNicks = value; }
        public string Password { get => password;
            set => password = value; }     // dodac ograniczenia - min 8 znakow, musi zawierac tylko litery i cyfry
        public string Login { get => login; set => login = value; }

        /// <summary>
        /// Metoda GenerateNickname generuje każdemu użytkownikowi unikalny nick - bierze 3 pierwsze litery imienia oraz 3 pierwsze litery nazwiska użytkownika. 
        /// Jeśli już istnieje osoba z takim nickiem, dodaje na koniec nicku kolejny numer. 
        /// Po wygenerowaniu unikalnego nicku dodaje go do listy UsedNicks. 
        /// </summary>
        /// <returns></returns>
         
       


        public string GenerateNickname()
        {
            string nameLower = Name.ToLower();
            string lastNameLower = LastName.ToLower();


            int suffix = 1;

            do
            {
                Nick = $"{nameLower.Substring(0, 3)}{lastNameLower.Substring(0, 3)}";


                if (usedNicks.Contains(Nick))
                {
                    Nick += suffix;
                    suffix++;
                }
            } while (usedNicks.Contains(Nick));

            usedNicks.Add(Nick);
            return Nick;
        }




        /// <summary>
        /// Konstruktor nieparametryczny, tworzy obiekt użytkownika.
        /// </summary>
        public User() 
        {
           // Nick = GenerateNickname();
            //Users.Add(this);
            //UsedNicks.Add(Nick);
        }


   
        /// <summary>
        /// Konstruktor parametryczny dodaje użytkownika do listy Users, każdemu użytkownikowi generuje unikalny nick oraz dodaje ten nick do listy używanych nicków.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastName"></param>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <param name="gender"></param>
        public User(string name, string lastName, double weight, double height, EnumGender gender, string password, string login)
        {
            Name = name;
            LastName = lastName;
            Weight = weight;
            Height = height;
            Gender = gender;
            Password = password;
            Login = login;
            GenerateNickname();
        }

        /// <summary>
        /// Metoda BMI oblicza BMI użytkownika. 
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public double BMI(double weight, double height) => weight / (height*height); //metoda do oblicznia BMI na podstawie waga wzrost

        public double TotalScore()
        {
            return UserTrainings?.CalculateTotalProgress() ?? 0;
        }

        public double TotalCaloriesBurned()
        {
            return UserTrainings?.CalculateTotalCaloriesBurned() ?? 0;
        }

        /// <summary>
        /// Metoda wypisuje dane użytkownika - jego imię, nazwisko, nick oraz obliczone BMI. 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            this.nick = GenerateNickname();
            //switch do BMI 
            return $"{Name} {LastName} {Nick} ";
        }

        public int CompareTo(User? other)
        {
           if(other == null) { return -1; }
           int cmp = LastName.CompareTo(other.LastName);
           if(cmp != 0) {  return cmp; }
           return Name.CompareTo(other.Name);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(User? other) // porównowanie po nicku 
        {
            if (other == null) { return false; }
            return Name.Equals(other.Name);
        }



        //automatyczne tworzenie Nick-u jako klucza do słownika , przy logowaniu
        //METODY : logowanie poprzez nick,  rejestracja
        // zapisanie nick-ów do bazy 
    }
}
