using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Project
{
    public class UserTrainings : IUserTrainingManagement,  IComparable<UserTrainings>
    {
        /// <summary>
        /// Klasa reprezentuje zestaw treningów przypisanych do danego użytkownika. 
        /// </summary>
        List<Training> trainings = new List<Training>();
        User user;

        /// <summary>
        /// Właściwość pobiera lub ustawia listę treningów przypisanych do danego użytkownika.
        /// </summary>
        public List<Training> Trainings { get => trainings; set => trainings = value; }

        /// <summary>
        /// Właściwość pobiera lub ustawia obiekt User reprezentujący użytkownika, do którego przypisane są treningi.
        /// </summary>
        public User User { get => user; set => user = value; }

        /// <summary>
        /// Konstruktor nieparametryczny, inicjalizuje nowy obiekt UserTrainings z pustą listą treningów.
        /// </summary>
        public UserTrainings()
        {
            Trainings = new List<Training>();
        }

        /// <summary>
        /// Konstruktor parametryczny, inicjalizuje nowy obiekt UserTrainings z przypisanym użytkownikiem.
        /// </summary>
        /// <param name="user"></param>
        public UserTrainings(User user) : this()
        {
            User = user;
        }

        /// <summary>
        /// Metoda AddTraining dodaje trening do listy treningów użytkownika.
        /// </summary>
        /// <param name="training"></param>
        public void AddTraining(Training training)
        {

            Trainings.Add(training);
            training.User = User;

        }

        /// <summary>
        /// Metoda RemoveTraining usuwa trening z listy treningów użytkownika.
        /// </summary>
        /// <param name="training"></param>
        public void RemoveTraining(Training training)
        {
            Trainings.Remove(training);
        }

        /// <summary>
        /// Metoda CalculateTotalProgress oblicza całkowity postęp użytkownika na podstawie wyników ćwiczeń we wszystkich treningach.
        /// </summary>
        /// <returns></returns>
        public double CalculateTotalProgress()
        {
            // Oblicz sumę postępów we wszystkich ćwiczeniach
            double totalProgress = 0;
            foreach (var training in Trainings)
            {
                foreach (var exercise in training.Exercises)
                {
                    
                    {
                        totalProgress += exercise.ExerciseScore();
                    }
                }
            }
            return totalProgress;
        }


      

        /// <summary>
        /// Metoda CalculateTotalProgressByName oblicza całkowity postęp użytkownika w określonym ćwiczeniu na podstawie wyników z tego ćwiczenia we wszystkich treningach.
        /// </summary>
        /// <param name="exerciseName"></param>
        /// <returns></returns>
        public double CalculateTotalProgressByName(string exerciseName)
        {
            // Oblicz sumę postępów we wszystkich ćwiczeniach
            double thisScore = 0;
            foreach (var training in Trainings)
            {
                foreach (var exercise in training.Exercises)
                {
                    if (exercise.ExerciseName == exerciseName)
                    {
                        thisScore += exercise.ExerciseScore();
                    }
                }
            }
            return thisScore;
        }



        /// <summary>
        /// Metoda CompareExerciseProgress porównuje postęp użytkownika w określonym ćwiczeniu z postępem innego użytkownika w tym samym ćwiczeniu.
        /// </summary>
        /// <param name="otherUserTrainings"></param>
        /// <param name="exerciseName"></param>
        public void CompareExerciseProgress(UserTrainings otherUserTrainings, string exerciseName)
        {
            double thisUserProgress = CalculateTotalProgressByName(exerciseName);
            double otherUserProgress = otherUserTrainings.CalculateTotalProgressByName(exerciseName);

            // Porównaj sumy postępów dla danego ćwiczenia
            int result = thisUserProgress.CompareTo(otherUserProgress);

            // Wynik porównania
            if (result > 0)
            {
                Console.WriteLine($"Użytkownik {this.User.Nick} ma większą sumę postępów niż użytkownik {otherUserTrainings.User.Nick} w ćwiczeniu {exerciseName}");
            }
            else if (result < 0)
            {
                Console.WriteLine($"Użytkownik {this.User.Nick} ma mniejszą sumę postępów niż użytkownik {otherUserTrainings.User.Nick} w ćwiczeniu {exerciseName}");
            }
            else
            {
                Console.WriteLine($"Sumy postępów dla ćwiczenia {exerciseName} są równe.");
            }
        }

        //metoda porownuje progres dla konkretnego cwiczenia miedzy 2 uzytkownikami 


        public double CalculateTotalCaloriesBurned()
        {
            
            double totalCaloriesBurned = 0;

            foreach (var training in Trainings)
            {
                foreach (var exercise in training.Exercises)
                {
                    if (exercise is CardioExercise cardioExercise)
                    {
                       
                        // Jeśli ćwiczenie to CardioExercise, dodaj spalone kalorie
                        totalCaloriesBurned += cardioExercise.CalculateCaloriesBurned(cardioExercise.Time, cardioExercise.Distance, User);
                    }
                }
            }

            return totalCaloriesBurned;
        }

        public List<Training> GetTrainingsForUser(User user)
        {
            // Filtruj treningi tylko dla danego użytkownika
            return Trainings.Where(training => training.User == user).ToList();
        }


        /// <summary>
        /// Metoda ToString zwraca informacje o wszystkich treningach.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var it in trainings)
            {
                stringBuilder.Append(it.ToString());
            }
            return stringBuilder.ToString();
        }
        

        /// <summary>
        /// Metoda Compare implementuje interfejs IComparer - porównuje postęp dwóch użytkowników na podstawie całkowitego wyniku treningów.
        /// </summary>
        /// <param name="userTraining1"></param>
        /// <param name="userTrainings2"></param>
        /// <returns></returns>
        //public int Compare(UserTrainings userTrainings1, UserTrainings userTrainings2)
        //{
        //    double progress1 = userTrainings1.CalculateTotalProgress();
        //    double progress2 = userTrainings2.CalculateTotalProgress();

        //    // Porównaj sumy postępów dla danego ćwiczenia
        //    return progress1.CompareTo(progress2); // Zmieniono kolejność, aby sortować malejąco
        //}

        public int CompareTo(UserTrainings? other)
        {
            // Porównaj na podstawie total progress
            return -CalculateTotalProgress().CompareTo(other.CalculateTotalProgress());
        }
        // metoda potrzebna do stworzenia rankingu calosciowego score dla wszystkich uzytkownikow


        public  string GetRankingInfo()
        {
            
            return $"{User.Name} {User.LastName} ({User.Nick}) {CalculateTotalProgress().ToString("F2")}";
        }

    }     
}
