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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using WpfLoginApp.Classes;

namespace WpfLoginApp
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static class
      User
        {
            public static string Username { get; set; }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = UserDB; Integrated Security = True" );
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String query = "SELECT COUNT(*) FROM dbo.Users WHERE Username=@Username AND Password=@Password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;
                //addwithvalue sorgumuzdaki hangi parametreye hangi değerin ekleneceğini belirtir.
                cmd.Parameters.AddWithValue("@Username", usertxt.Text);
                cmd.Parameters.AddWithValue("@Password", pswdtxt.Password);
                //komutumuzu çalıştırma kısmı
                int count = Convert.ToInt32(cmd.ExecuteScalar()); //tek değer dönderdiğini bildiğim için executescalar kullanıldı.
                //convert.toınıt32 fonksiyonu ile bu değeri int çevirdik.
                if (count == 1)
                {
                    string selectName = "Select Name,Username,Password  FROM dbo.users WHERE Username =@Username";
                    SqlCommand Namecmd = new SqlCommand(selectName, con);

                    Namecmd.CommandType = CommandType.Text;
                    Namecmd.Parameters.AddWithValue("@Username", usertxt.Text);

                    SqlDataReader dr = Namecmd.ExecuteReader();
                    while(dr.Read())
                    {
                        loginuser.Name = dr["Name"].ToString();
                        loginuser.UserName = dr["Username"].ToString();
                        loginuser.Password = dr["Password"].ToString();
                    }

                    AfterLogin newwindow = new AfterLogin();
                    newwindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı ya da şifre hatalı");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                
            }
            finally
            {
                con.Close();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Signup newwindow = new Signup();
            newwindow.Show();
            this.Close();
        }
    }
}
