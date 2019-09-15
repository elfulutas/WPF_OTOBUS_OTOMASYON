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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLoginApp.Classes;

namespace WpfLoginApp
{
    /// <summary>
    /// HesapBilgileri.xaml etkileşim mantığı
    /// </summary>
    public partial class HesapBilgileri : UserControl
    {
        public HesapBilgileri()
        {
            InitializeComponent();
            usernametxt.Text = loginuser.UserName;
            nametxt.Text = loginuser.Name;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=UserDB; Integrated Security=True");

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon.Open();
                }

            }
            catch
            {
                MessageBox.Show("Veritabanına bağlanamadı");
            }


            String Query = "UPDATE dbo.Users SET Username =@Username ,Name = @Name,Password = @Password WHERE Username=@Username ";
            SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.Parameters.AddWithValue("@Username", usernametxt.Text);
            sqlCmd.Parameters.AddWithValue("@Name", nametxt.Text);

            bool executeOuery = true;
          
            // if (usernametxt.Text != loginuser.UserName)
            // {

            // }
            //if(nametxt.Text != loginuser.Name)
            // {

            // }
            if (oldpasstxt.Password.Length != 0 )
            {
                if(oldpasstxt.Password == loginuser.Password)
                {
                    sqlCmd.Parameters.AddWithValue("@Password", newpasstxt.Password);
                }
                else
                {
                    executeOuery = false;
                }
            }
            else
            {
                sqlCmd.Parameters.AddWithValue("@Password", loginuser.Password);

            }
            if (executeOuery)
            {
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Değişiklikler kaydedildi.");
            }
            else
            {
                MessageBox.Show("şifreniz yanlış oldu");
            }

            
        }
    }
}
