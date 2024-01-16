using PO_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI_PO_Project
{
    /// <summary>
    /// Logika interakcji dla klasy RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        User user = new User();
        public RegisterWindow()
        {
            InitializeComponent();
        }

       public RegisterWindow(User us):this()
        {
            user = us;
        }

        private void btnZapisz_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (NameTextBox.Text != "" && SurnameTextBox.Text != "")
                {
                    user.Name = NameTextBox.Text;
                    user.LastName = SurnameTextBox.Text;
                    user.Weight = double.Parse(WeighTextBox.Text); // Upewnij się, że wartość jest poprawna
                    user.Height = double.Parse(Heigh_TextBox.Text); // Upewnij się, że wartość jest poprawna
                    user.Gender = GetSelectedGender();


                    DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Proszę wprowadzić poprawne dane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Podano niepoprawny format danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //try
            //{


            //    User newUser = new User
            //    {
            //        Name = NameTextBox.Text,
            //        LastName = SurnameTextBox.Text,
            //        Weight = double.Parse(WeighTextBox.Text), // Upewnij się, że wartość jest poprawna
            //        Height = double.Parse(Heigh_TextBox.Text), // Upewnij się, że wartość jest poprawna
            //        Gender = GetSelectedGender(),// Tutaj określ płeć w zależności od wyboru RadioButton
            //        Password = txtNewPassoword.Text, // Tutaj uzyskaj hasło z TextBox
            //        Login = txtNewLogin.Text
            //    };
            //    string registrationResult = listOfUsers.RegisterUser(newUser);

            //    if (string.IsNullOrEmpty(registrationResult))
            //    {
            //        MessageBox.Show($"Rejestracja zakończona pomyślnie.\n Wygenerowany dane:\n Nick: {newUser.GenerateNickname()} \n Login: {newUser.Login} \n Hasło: {newUser.Password}", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
            //        StartWindow startWindow = new StartWindow();
            //        startWindow.Show();
            //        this.Close();

            //    }
            //    else
            //    {
            //        MessageBox.Show(registrationResult, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }

            //        // Wywołaj metodę GenerateNickname i wyświetl nick w MessageBox
            //        // MessageBox.Show($"Wygenerowany nick: {newUser.GenerateNickname()}", "Informacja", MessageBoxButton.OK);
            //}

            //catch (FormatException)
            //{
            //    MessageBox.Show("Podano niepoprawny format danych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

        }


        private EnumGender GetSelectedGender()
        {
            if (RBFemale.IsChecked == true)
            {
                return EnumGender.Female;
            }
            else if (RBMale.IsChecked == true)
            {
                return EnumGender.Male;
            }
            else
            {
                // Jeśli żaden RadioButton nie jest zaznaczony, możesz ustawić domyślną wartość lub podjąć inne działania.
                return EnumGender.Female; // Domyślnie zakładam, że jeśli nie wybrano, to jest to kobieta.
            }
        }

        private void btnExit22_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
