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
    /// BiletIptal.xaml etkileşim mantığı
    /// </summary>
    public partial class BiletIptal : UserControl
    {
        public BiletIptal()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=localhost\SQLExpress;Initial Catalog=UserDB;
                                                     Integrated Security=True");
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                string joinquery = "SELECT * FROM dbo.Bilet JOIN dbo.Seferler ON dbo.Bilet.sefer_id = dbo.Seferler.sefer_id Where dbo.Bilet.UserName=@UserName  ";
                SqlCommand select_cmd = new SqlCommand(joinquery, sqlcon);
                select_cmd.CommandType = CommandType.Text;
                select_cmd.Parameters.AddWithValue("@UserName", loginuser.UserName);

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(select_cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    adapter.Dispose();
                    seferListeleGrid.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            finally
            {
                sqlcon.Close();
            }
        }

        private void Bilet_İptal(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=localhost\SQLExpress;Initial Catalog=UserDB;
                                                     Integrated Security=True");
            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();

                    DataRowView dataRowView = (DataRowView)((Button)e.Source).DataContext;

                    Bilet.UserName = Convert.ToString(dataRowView[0]);
                    Bilet.sefer_id = Convert.ToInt32(dataRowView[1]);
                    Bilet.koltuk_no = Convert.ToInt32(dataRowView[2]);

                    String deletequery = "DELETE FROM dbo.Bilet WHERE koltuk_no=@koltuk_no AND UserName=@UserName ";
                    SqlCommand select_cmd = new SqlCommand(deletequery, sqlcon);
                    select_cmd.CommandType = CommandType.Text;
                    select_cmd.Parameters.AddWithValue("@koltuk_no", Bilet.koltuk_no);
                    select_cmd.Parameters.AddWithValue("@UserName", Bilet.UserName);
                    int count = select_cmd.ExecuteNonQuery();
                    MessageBox.Show("Bilet iptal edildi.");
                    Seferler.kontenjan = Seferler.kontenjan + 1;
                    if (count == 1)
                    {

                        string selectquery = "SELECT * FROM dbo.Bilet JOIN dbo.Seferler ON dbo.Bilet.sefer_id = dbo.Seferler.sefer_id Where dbo.Bilet.UserName=@UserName  ";
                        SqlCommand selectcmd = new SqlCommand(selectquery, sqlcon);
                        selectcmd.CommandType = CommandType.Text;
                        selectcmd.Parameters.AddWithValue("@UserName", loginuser.UserName);

                        //select_cmd.ExecuteNonQuery();
                        try
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(selectcmd);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            adapter.Dispose();
                            seferListeleGrid.ItemsSource = dt.DefaultView;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}