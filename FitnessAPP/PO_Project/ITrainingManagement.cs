using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO_Project
{
    /// <summary>
    /// Interfejs ITrainingManagement definiuje zestaw metod związanych z zarządzaniem treningami. 
    /// </summary>
    public interface ITrainingManagement
        {
            void AddExercise(Exercise exercise); // dodaj cwiczenie
            void StartTraining();  // Rozpoczyna trening
            string ChooseTrainingType();  // Wybiera rodzaj treningu (siłownia, bieganie, jazda na rowerze, pływanie, inne)
            void SaveTraining(Training training);  // Zapisuje informacje o treningu
            
            void DisplayTrainingInfo(Training training);  // Wyświetla informacje o treningu
        }
    
}
