using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Project
{
    public class UsersRanking 
    {
        /// <summary>
        /// Klasa reprezentuje ranking użytkowników na podstawie ich treningów. 
        /// </summary>
        List<UserTrainings> ranking = new List <UserTrainings> ();

        /// <summary>
        /// Właściwość pobiera lub ustawia listę obiektów UserTrainings. 
    /// </summary>
        public List<UserTrainings> Ranking { get => ranking; set => ranking = value; }


        /// <summary>
        /// Konstruktor nieparametryczny, inicjalizuje nowy obiekt UsersRanking z pustą listą 
        /// </summary>
        public UsersRanking()
        {
            ranking = new List <UserTrainings> ();
        }
        public void CalculateAndSortTotalProgress()
        {
            // Sortuj ranking na podstawie interfejsu IComparable
            Ranking.Sort();
        }


        public void AddUsTrainings(UserTrainings c)
        {
            ranking.Add(c);
        }
        /// <summary>
        /// Metoda Sort umożliwia sortowanie rankingu użytkowników na podstawie wyniku ze wszystkich ćwiczeń.
        /// Sortowanie odbywa się na podstawie domyślnych kryteriów porównawczych określonych w klasie UserTrainings. 
        /// </summary>

        // tworzy ranking wszystkich uzytkownikow na podstawie CALOSCIOWEGO score ze wszystkich cwiczen

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

           foreach(UserTrainings c in ranking)
            {
                sb.AppendLine(c.GetRankingInfo().ToString());
            }
           return sb.ToString();
        }
    }
}
