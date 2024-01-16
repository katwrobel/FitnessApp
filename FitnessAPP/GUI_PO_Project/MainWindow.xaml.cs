using MaterialDesignThemes.Wpf;
using PO_Project;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;

namespace GUI_PO_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ListOfUsers listOfUsers;
        private DispatcherTimer timer;

        UserTrainings userTrainings = new UserTrainings();

        // Delegat do aktualizacji interfejsu użytkownika
        private delegate void UpdateUIDelegate();

        // Delegat do odtwarzania dźwięku
        private delegate void PlaySoundDelegate();

        private UpdateUIDelegate updateUIDelegate;
        private PlaySoundDelegate playSoundDelegate;
        public MainWindow()
        {
            InitializeComponent();

            // Inicjalizacja timera
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Aktualizacja co sekundę
            timer.Tick += Timer_Tick;

            // Uruchomienie timera
            timer.Start();

            listOfUsers = ListOfUsers.OdczytXml("users.xml") ?? new ListOfUsers();

            if (listOfUsers is object)
            {
                lstUsers.ItemsSource = new ObservableCollection<User>(listOfUsers.Users);
            }

            updateUIDelegate = UpdateUIHandler;
            playSoundDelegate = PlaySound;



        }

        public string SelectedUserNameLabel
        {
            get { return (string)GetValue(SelectedUserNameLabelProperty); }
            set { SetValue(SelectedUserNameLabelProperty, value); }
        }

        public static readonly DependencyProperty SelectedUserNameLabelProperty =
        DependencyProperty.Register("SelectedUserNameLabel", typeof(string), typeof(MainWindow), new PropertyMetadata("Admin"));

        private void UpdateUIHandler()
        {
            // Aktualizuj interfejs użytkownika, na przykład odśwież listę treningów
            // lub wykonaj inne operacje związane z interfejsem
            lstUsers.Items.Refresh();
            // Wyświetl komunikat
            MessageBox.Show("Interfejs został zaktualizowany!");
        }
        private void PlaySound()
        {
            // Odtwórz dźwięk systemowy
            SystemSounds.Beep.Play();
        }

        private void btnSounds_Click(object sender, RoutedEventArgs e)
        {
            // Symulacja wywołania delegatów z poziomu innego wątku (np. gdy dane zostaną zaktualizowane)
            Task.Run(() =>
            {
                // Symulacja czasochłonnej operacji
                System.Threading.Thread.Sleep(600);

                // Wywołaj delegaty w głównym wątku, aby zaktualizować interfejs użytkownika i odtworzyć dźwięk
                Dispatcher.Invoke(updateUIDelegate);
                Dispatcher.Invoke(playSoundDelegate);
                Dispatcher.Invoke(() => ResetInputFields());
            });
        }

        private void ResetInputFields()
        {
            
            txtNameExCardio.Text = "Nazwa ćwiczenia";
            txtTime.Text = "Czas";
            txtDistance.Text = "Dystans";
            txtNameExGym.Text = "Nazwa ćwiczenia";
            txtSets.Text = "Serie";
            txtReps.Text = "Powtórzenia";
            txtKilo.Text = "Kilogramy";
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Aktualizacja czasu i daty co sekundę
            DateTime nowTime = DateTime.Now;
            lblTime.Content = nowTime.ToLongTimeString();

            DateTime nowDate = DateTime.Now;
            lblDate.Content = nowDate.ToLongDateString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            User us = new User();
            RegisterWindow rg = new RegisterWindow(us);
            bool? result = rg.ShowDialog();
            if (result == true)
            {
                listOfUsers.RegisterUser(us);
                lstUsers.ItemsSource = new
                                ObservableCollection<User>(listOfUsers.Users);
            }


        }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        private void toggleThem2(object sender, RoutedEventArgs e)  //zmiana motywu/theme
        {
            if (OperatingSystem.IsWindows()) //sprawdzenie czy system jest WIndows
            {
                ITheme theme = paletteHelper.GetTheme();

                if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
                {
                    IsDarkTheme = false;
                    theme.SetBaseTheme(Theme.Light);
                }
                else
                {
                    IsDarkTheme = true;
                    theme.SetBaseTheme(Theme.Dark);
                }
                paletteHelper.SetTheme(theme); //ustawia dla całej aplikacji
            }
        }

        private void btnexit2_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) //przesuwanie okna za pomocą lewego przycisku
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }



        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            this.lstUsers.Items.SortDescriptions.Clear();
            this.lstUsers.Items.SortDescriptions.Add(
               new System.ComponentModel.SortDescription(nameof(User.Name), System.ComponentModel.ListSortDirection.Ascending));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedIndex > -1)
            {
                foreach (var item in lstUsers.SelectedItems)
                {
                    listOfUsers.DeleteUsers(((User)item));
                }
                lstUsers.ItemsSource = new
                ObservableCollection<User>(listOfUsers.Users);
            }
        }

        private void btnAddCardioTraining_Click(object sender, RoutedEventArgs e)
        {



            if (lstUsers.SelectedIndex > -1)
            {
                if (txtNameExCardio.Text != "" || txtTime.Text != "" || txtDistance.Text != "")
                {

                    User selectedUser = (User)lstUsers.SelectedItem;

                    // Pobierz dane z interfejsu użytkownika
                    string exerciseName = txtNameExCardio.Text;
                    int time = int.Parse(txtTime.Text);
                    double distance = double.Parse(txtDistance.Text);

                    // Utwórz obiekt CardioExercise
                    CardioExercise cardioExercise = new CardioExercise(exerciseName, time, distance, selectedUser);

                    // Dodaj trening kardio do użytkownika
                    selectedUser.UserTrainings ??= new UserTrainings(selectedUser); // Ustaw, jeśli null
                    Training tr = new Training(selectedUser, DateTime.Today, EnumType.Cardio);
                    tr.AddExerciseCardio(cardioExercise);
                    selectedUser.UserTrainings.AddTraining(tr);

                    // Odśwież widok listy użytkowników
                    lstUsers.ItemsSource = new ObservableCollection<User>(listOfUsers.Users);

                    MessageBox.Show("Dodano Trening!");
                    return;
                }
                else
                {
                    MessageBox.Show("Niepoprawne dane treningu!");
                }
            }
        }

        private void btnAddGymTraining_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedIndex > -1)
            {
                if (txtNameExCardio.Text != "" || txtTime.Text != "" || txtDistance.Text != "")
                {

                    User selectedUser = (User)lstUsers.SelectedItem;

                    // Pobierz dane z interfejsu użytkownika
                    string exerciseName = txtNameExGym.Text;
                    int kilograms = int.Parse(txtKilo.Text);
                    int reps = int.Parse(txtReps.Text);
                    int sets = int.Parse(txtSets.Text);


                    // Utwórz obiekt CardioExercise
                    GymExercise gymExercise = new GymExercise(exerciseName, kilograms, reps, sets, selectedUser);

                    // Dodaj trening kardio do użytkownika
                    selectedUser.UserTrainings ??= new UserTrainings(selectedUser); // Ustaw, jeśli null
                    Training tr = new Training(selectedUser, DateTime.Today, EnumType.Strength);
                    tr.AddExerciseGym(gymExercise);
                    selectedUser.UserTrainings.AddTraining(tr);

                    // Odśwież widok listy użytkowników
                    lstUsers.ItemsSource = new ObservableCollection<User>(listOfUsers.Users);

                    MessageBox.Show("Dodano Trening!");
                    return;
                }
                else
                {
                    MessageBox.Show("Niepoprawne dane treningu!");
                }
            }
        }

        private void AllTrainingsBtn_Click(object sender, RoutedEventArgs e)
        {

            if (lstUsers.SelectedIndex > -1)
            {
                foreach (User item in lstUsers.SelectedItems)
                {
                    UserTrainings userTrainings = item.UserTrainings; // Uzyskaj UserTrainings zamiast tworzyć nowy obiekt

                    // Uzyskaj treningi dla użytkownika
                    List<Training> trainings = userTrainings?.Trainings;

                    StringBuilder stringBuilder = new StringBuilder();

                    if (trainings != null && trainings.Count > 0)
                    {
                        stringBuilder.AppendLine($"Treningi użytkownika: {item.Name} {item.LastName}");
                        foreach (Training training in trainings)
                        {
                            stringBuilder.AppendLine(training.ToString());
                        }

                        MessageBox.Show(stringBuilder.ToString(), "Treningi użytkownika", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Brak treningów dla wybranego użytkownika.", "Brak treningów", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void StatisticsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lstUsers.SelectedIndex > -1)
            {
                foreach (User item in lstUsers.SelectedItems)
                {

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($"Statystyki użytkownika: {item.Name} {item.LastName}");
                    stringBuilder.AppendLine($"BMI: {item.BMI(item.Weight, item.Height).ToString("F2")}");
                    stringBuilder.AppendLine($"Total Score: {item.TotalScore().ToString("F2")}");
                    //stringBuilder.AppendLine($"Total Calories Burned: {item.TotalCaloriesBurned().ToString("F2")} kcal");

                    MessageBox.Show(stringBuilder.ToString(), "Statystyki użytkownika", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            else
            {
                MessageBox.Show("Nie wybrano użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRanking_Click(object sender, RoutedEventArgs e)
        {
            // Tworzymy obiekt UsersRanking
            UsersRanking usersRanking = new UsersRanking();

            // Przypisujemy mu listę użytkowników wybranych w ListBox
            List<User> selectedUsers = lstUsers.SelectedItems.Cast<User>().ToList();

            // Tworzymy listę UserTrainings na podstawie wybranych User
            List<UserTrainings> selectedUserTrainings = selectedUsers
                .Select(user => new UserTrainings(user))
                .ToList();

            // Przypisujemy listę UserTrainings do usersRanking.Ranking
            usersRanking.Ranking = selectedUserTrainings;

            // Wywołujemy metodę do obliczenia i sortowania postępów użytkowników
            usersRanking.CalculateAndSortTotalProgress();

            // Teraz usersRanking.Ranking jest posortowany rankingiem użytkowników

            // Wyświetlamy ranking w oknie dialogowym
            StringBuilder rankingStringBuilder = new StringBuilder();
            foreach (var userTrainings in usersRanking.Ranking)
            {
                rankingStringBuilder.AppendLine(userTrainings.GetRankingInfo());
            }

            MessageBox.Show(rankingStringBuilder.ToString(), "Ranking");
        }

        private void btnSaveFIle_Click(object sender, RoutedEventArgs e) //zapis do pliku txt ktory bedzie na pulpicie
        {
            if (lstUsers.SelectedIndex > -1)
            {
                User selectedUser = (User)lstUsers.SelectedItem;

                if (selectedUser.UserTrainings != null && selectedUser.UserTrainings.Trainings.Count > 0)
                {
                    try
                    {
                        // Uzyskaj ścieżkę do pulpitu użytkownika
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        // Tworzymy unikalną nazwę pliku na pulpicie na podstawie danych użytkownika
                        string fileName = $"{selectedUser.Name}_{selectedUser.LastName}_Trainings.txt";

                        // Pełna ścieżka do pliku na pulpicie
                        string filePathOnDesktop = System.IO.Path.Combine(desktopPath, fileName);

                        // Utwórz lub nadpisz plik tekstowy
                        using (StreamWriter file = new StreamWriter(filePathOnDesktop))
                        {
                            file.WriteLine($"Treningi użytkownika: {selectedUser.Name} {selectedUser.LastName}");
                            file.WriteLine();

                            foreach (Training training in selectedUser.UserTrainings.Trainings)
                            {
                                file.WriteLine(training.ToString());
                                file.WriteLine();
                            }
                        }

                        // Wyświetl komunikat po zapisaniu
                        MessageBox.Show($"Treningi zostały zapisane do pliku: {filePathOnDesktop} \nLokalizacja: Pulpit", "Zapisano", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Wystąpił błąd podczas zapisu pliku: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Brak treningów dla wybranego użytkownika.", "Brak treningów", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrano użytkownika.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void lstUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstUsers.SelectedIndex > -1)
            {
                if (txtNameExCardio.Text != "" || txtTime.Text != "" || txtDistance.Text != "")
                {

                    User selectedUser = (User)lstUsers.SelectedItem;
                    SelectedUserNameLabel = $"{selectedUser.Name} {selectedUser.LastName}";
                }
            }
        }
    }
}