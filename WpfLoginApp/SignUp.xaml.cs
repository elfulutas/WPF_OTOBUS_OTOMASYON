using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace WpfLoginApp
{
    /// <summary>
    /// Signup.xaml etkileşim mantığı
    /// </summary>
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=UserDB; Integrated Security=True");

            bool execQueryControl = true;




            if (UserNameTextBox.Text.Length == 0)
            {
                UserNameValidate.Content = "Must filled";
                execQueryControl = false;
            }

            else
            {
                try
                {
                    if (sqlCon.State == ConnectionState.Closed)
                    {
                        sqlCon.Open();
                    }

                    String checkUsernameQuery = "SELECT COUNT(*) FROM dbo.Users WHERE Username=@Username";

                    // Bir SQL sorgusu oluşturuyoruz. 
                    //Parametre olarak hangi sorguyu çalıştıracağımız ve hangi bağlantı ile çalıştıracağımızı gönderiyoruz.
                    SqlCommand sqlCmd = new SqlCommand(checkUsernameQuery, sqlCon);

                    sqlCmd.CommandType = CommandType.Text;

                    sqlCmd.Parameters.AddWithValue("@Username", UserNameTextBox.Text);


                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        execQueryControl = false;
                        UserNameValidate.Content = "Username exist";
                    }

                }
                catch
                {
                    MessageBox.Show("Veri tabanı bağlantısında hata oldu.");
                }

            }
            if (NameTextBox.Text.Length == 0)
            {
                execQueryControl = false;
                NameValidate.Content = "Must filled";
            }

            if (PasswordTextBox.Password.Length == 0)
            {
                PasswordValidate.Content = "Must filled";
                execQueryControl = false;
            }

            if (PasswordAgainTextBox.Password.Length == 0)
            {
                PasswordAgainValidate.Content = "Must filled";
                execQueryControl = false;
            }
            else
            {
                if (PasswordAgainTextBox.Password != PasswordTextBox.Password)
                {
                    execQueryControl = false;
                    PasswordAgainValidate.Content = "Passwords not equal";
                }
            }

            if (execQueryControl)
            {
                String insertQuery = "INSERT INTO dbo.Users VALUES(@Username, @Name, @Password)";

                SqlCommand sqlCmd = new SqlCommand(insertQuery, sqlCon);

                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Username", UserNameTextBox.Text);
                sqlCmd.Parameters.AddWithValue("@Name", NameTextBox.Text);
                sqlCmd.Parameters.AddWithValue("@Password", PasswordTextBox.Password);

                int isExecuted = sqlCmd.ExecuteNonQuery();

                if (isExecuted < 1)
                {
                    MessageBox.Show("Veritabanı bağlantı hatası");
                }
                else
                {
                    MessageBox.Show("Kullanıcı kaydedildi");
                    MainWindow mWindow = new MainWindow();
                    mWindow.Show();
                    this.Close();
                }
            }
        }

        private void UserNameTextChanged(object sender, TextChangedEventArgs e)
        {
            UserNameValidate.Content = "";
        }

        private void NameTextChanged(object sender, TextChangedEventArgs e)
        {
            NameValidate.Content = "";
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordValidate.Content = "";
        }

        private void PasswordAgainChanged(object sender, RoutedEventArgs e)
        {
            PasswordAgainValidate.Content = "";
        }
    }
}
