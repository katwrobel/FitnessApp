using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using MaterialDesignThemes.Wpf;
using PO_Project;

namespace GUI_PO_Project
{
    /// <summary>
    /// Logika interakcji dla klasy StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        ListOfUsers listOfUsers = new ListOfUsers();
        public StartWindow()
        {
            InitializeComponent();
           
        }

        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper(); 
        private void toggleTheme(object sender, RoutedEventArgs e) //przycisk do zmiany kolorów aplikacji
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

        private void exitApp(object sender, RoutedEventArgs e) //przycisk do wyłączenia aplikacji
        {
            System.Windows.Application.Current.Shutdown(); 
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) //przesuwanie okna za pomocą lewego przycisku
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }


        private void LoginBtn_Click(object sender, RoutedEventArgs e) //Przycisk do logowania
        {
            string enteredUsername = txtUsername.Text;
           string enteredPassword = txtPassword.Password;
       
           if (string.IsNullOrEmpty(enteredUsername) || string.IsNullOrEmpty(enteredPassword))     //sprawdzenie czy pola sa puste
            {
               MessageBox.Show("Niepoprawny login lub hasło");
               return;
           }


            // bool isValidCredentials = listOfUsers.AuthenticateUser(enteredUsername, enteredPassword);

            if (enteredUsername == "admin" && enteredPassword == "admin") 
           {
                
               MessageBox.Show("Zalogowano pomyślnie!");

               //  przekierować użytkownika do głównego okna aplikacji 
               MainWindow mainWindow = new MainWindow();
               mainWindow.Show();
               this.Close(); 
           }
           else
           {
               MessageBox.Show("Niepoprawny login lub hasło");
           }

        }
                 //napisać poniższą metodę
      

        //private void signupBtn_Click(object sender, RoutedEventArgs e) //przycisk utwórz konto
        //{


        //    ////przekierowanie użytkownika do okna rejestracji
        //    //RegisterWindow registerWindow = new RegisterWindow();
        //    //registerWindow.Show();
        //    //this.Close();
        //}
    }   



    //    private void playSimpleSound()
    //    {
    //        SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
    //        simpleSound.Play();
    //    }

    //    private void LoginButton_Click(object sender, RoutedEventArgs e) //przycisk logowania
    //    {
    //        SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PO_Project.TrainingDbContext;Integrated Security=True;Connect Timeout=30;Encrypt=False");
    //        SqlDataAdapter sqa = new SqlDataAdapter("Select count(*) from Login where Nick = '" + TxBLogin.Text + "' and Password = '" + PsbPassword.Password + "'", con);
    //        DataTable dt = new DataTable();
    //        sqa.Fill(dt);

    //        if (dt.Rows[0][0].ToString() == "1")
    //        {
    //            this.Hide();
    //            MainWindow mainWindow = new MainWindow();
    //            mainWindow.Show();
    //        }
    //        else
    //        {
    //            MessageBox.Show("Błąd logowania. Spróbuj ponownie");
    //        }




    //    private void RegisterButton_Click(object sender, RoutedEventArgs e)
    //    {
    //       // System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\mywavfile.wav"); //opcja2
    //        //player.Play();

    //        System.Media.SystemSounds.Asterisk.Play(); //Sprawdzić dźwięk przycisku //opcja1


    //        //using (var soundPlayer = new SoundPlayer(@"c:\Windows\Media\chimes.wav"))// opcja3
    //        //{
    //        //    soundPlayer.Play(); // can also use soundPlayer.PlaySync()
    //        //}

    //        
    //    }

    //    private void TxBLogin_TextChanged(object sender, TextChangedEventArgs e)
    //    {

    //    }

}

    //Xaml

    //< Window.Resources >
    //    < Style TargetType = "Button" x: Key = "RoundButton" >
    //        < Style.Resources >
    //            < Style TargetType = "Border" >
    //                < Setter Property = "CornerRadius" Value = "5" />
    //            </ Style >
    //        </ Style.Resources >
    //    </ Style >
    //</ Window.Resources >

    //< Grid Background = "#2A2F4F" >
    //    < Border BorderBrush = "#E5BEEC" BorderThickness = "10" HorizontalAlignment = "Center" Height = "745" Margin = "0,-1,0,0" VerticalAlignment = "Center" Width = "350" >
    //        < PasswordBox x: Name = "PsbPassword" HorizontalAlignment = "Center" Height = "50" Margin = "0,333,0,0" VerticalAlignment = "Top" Width = "250"
    //            BorderBrush = "#0078AA" BorderThickness = "1" FontSize = "20" VerticalContentAlignment = "Center" />
    //    </ Border >



    //    < TextBlock x: Name = "Witaj_text" HorizontalAlignment = "Center" Margin = "0,71,0,0" TextWrapping = "Wrap" VerticalAlignment = "Top" FontSize = "72" Height = "97" FontFamily = "Sitka Small" Foreground = "#FDE2F3" >< Run Language = "pl-pl" Text = "Witaj!" />< LineBreak />< Run Language = "pl-pl" /></ TextBlock >
    //    < Button x: Name = "LoginButton" Content = "Zaloguj się" HorizontalAlignment = "Center" Height = "55" Margin = "0,463,0,0"
    //                VerticalAlignment = "Top" Width = "250" Background = "#917FB3" FontSize = "29" Foreground = "#FDE2F3"
    //                BorderBrush = "#0078AA" BorderThickness = "2"
    //                Style = "{StaticResource RoundButton}" FontFamily = "Times New Roman" Click = "LoginButton_Click" />
    //    < Button x: Name = "RegisterButton" Content = "Utwórz konto"  HorizontalAlignment = "Center" Height = "55" Margin = "0,530,0,0"
    //            VerticalAlignment = "Top" Width = "250" Background = "#917FB3" FontSize = "25" Foreground = "#FDE2F3"
    //            BorderBrush = "#0078AA" BorderThickness = "2"
    //            Style = "{StaticResource RoundButton}" FontFamily = "Times New Roman" Click = "RegisterButton_Click" />
    //    < TextBox x: Name = "TxBLogin" HorizontalAlignment = "Center" Height = "43" Margin = "0,230,0,0" TextWrapping = "Wrap" VerticalAlignment = "Top" Width = "250"
    //            BorderBrush = "#0078AA" BorderThickness = "1" FontSize = "20" VerticalContentAlignment = "Center" TextChanged = "TxBLogin_TextChanged" />
    //    < Label x: Name = "lbLogin" Content = "Login" HorizontalAlignment = "Left" Height = "50" Margin = "50,180,0,0" VerticalAlignment = "Top" Width = "250" FontSize = "25" Foreground = "#FDE2F3" />
    //    < Label x: Name = "lbPassword" Content = "Hasło" HorizontalAlignment = "Center" Height = "50" Margin = "0,278,0,0" VerticalAlignment = "Top" Width = "250" FontSize = "25" Foreground = "#FDE2F3" />



    //</ Grid >