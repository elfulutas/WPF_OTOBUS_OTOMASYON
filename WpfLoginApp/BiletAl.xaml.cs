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
    /// BiletAl.xaml etkileşim mantığı
    /// </summary>
    public partial class BiletAl : UserControl
    {
        public BiletAl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = UserDB; Integrated Security = True");
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String neredenquery = "SELECT DISTINCT nereden FROM dbo.Seferler";
                SqlCommand cmdnereden = new SqlCommand(neredenquery, con);
                cmdnereden.CommandType = CommandType.Text;
                SqlDataReader drnereden = cmdnereden.ExecuteReader();
                while (drnereden.Read())
                {
                    neredencmb.Items.Add(drnereden["nereden"]);
                }

            }
            catch
            {
                MessageBox.Show("BAĞLANTI HATASI");
            }
        }

        private void Neredencmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source = localhost\SQLEXPRESS; Initial Catalog = UserDB; Integrated Security = True");
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                String nereyequery = "SELECT DISTINCT nereye FROM dbo.Seferler WHERE nereden=@comboItem";

                SqlCommand cmdnereye = new SqlCommand(nereyequery, con);

                cmdnereye.CommandType = CommandType.Text;
                cmdnereye.Parameters.AddWithValue("@comboItem", neredencmb.SelectedItem.ToString());


                SqlDataReader drnereye = cmdnereye.ExecuteReader();

                nereyecmb.Items.Clear();
                while (drnereye.Read())
                {
                    nereyecmb.Items.Add(drnereye["nereye"]);
                }

            }
            catch
            {
                MessageBox.Show("BAĞLANTI HATASI");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {//sefer listele
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
            String query = "SELECT COUNT(*) FROM Seferler WHERE nereden =@nereden AND tarih = @tarih and nereye = @nereye  ";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@nereden", neredencmb.SelectedItem);
            cmd.Parameters.AddWithValue("@nereye", nereyecmb.SelectedItem);
            cmd.Parameters.AddWithValue("@tarih", sefertarihi.SelectedDate);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count >= 1)
            {

                dataGrid.Visibility = Visibility;
                SqlCommand sefercmd = new SqlCommand("Select nereden, nereye, tarih, saat from dbo.Seferler where nereden=@nereden and nereye=@nereye and tarih = @tarih", sqlCon);

                sefercmd.Parameters.AddWithValue("@nereden", neredencmb.SelectedItem);
                sefercmd.Parameters.AddWithValue("@nereye", nereyecmb.SelectedItem);
                sefercmd.Parameters.AddWithValue("@tarih", sefertarihi.SelectedDate);
                SqlDataAdapter adapter = new SqlDataAdapter(sefercmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                cmd.Dispose();
                adapter.Dispose();
                dataGrid.ItemsSource = dt.DefaultView;

            }
            else
            {
                MessageBox.Show("Uygun sefer yok");

            }

        }



        private void BtnSatinAl_Click(object sender, RoutedEventArgs e)
        {
            koltuk.Visibility = Visibility;

            
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        void sorgula()
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

            string query = "SELECT koltuk_no from Bilet where sefer_id=@sefer_id";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@sefer_id", Seferler.sefer_id);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Seferler.koltuk_no = Convert.ToInt32(dr["koltuk_no"]);
                if (Seferler.koltuk_no == 1)
                {
                    b1.Background = new SolidColorBrush(Colors.Red);
                    b1.IsEnabled = false;
                }
                if (Seferler.koltuk_no == 2)
                {
                    b2.Background = new SolidColorBrush(Colors.Red);
                    b2.IsEnabled = false;
                }

            }
            dr.Close();
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=UserDB; Integrated Security=True");


            bool execQueryControl = true;
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
            String query = "SELECT sefer_id FROM Seferler WHERE nereden =@nereden AND tarih = @tarih and nereye = @nereye  ";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@nereden", neredencmb.SelectedItem);
            cmd.Parameters.AddWithValue("@nereye", nereyecmb.SelectedItem);
            cmd.Parameters.AddWithValue("@tarih", sefertarihi.SelectedDate);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Seferler.sefer_id = Convert.ToInt32(dr["sefer_id"]);
            }

            dr.Close();

            if (execQueryControl)
            {
                String Query = "INSERT INTO dbo.Bilet(Username,sefer_id,koltuk_no) VALUES(@Username, @sefer_id,@koltuk_no) ";
                SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Username", loginuser.UserName);
                sqlCmd.Parameters.AddWithValue("@sefer_id", Seferler.sefer_id);
                sqlCmd.Parameters.AddWithValue("@koltuk_no", b1.Content);
                int isExecuted = sqlCmd.ExecuteNonQuery();

                if (isExecuted < 1)
                {
                    MessageBox.Show("HATA");
                }
                else
                {

                    MessageBox.Show("Bilet Alım işlemi tamamlandı.");
                    b1.Background = new SolidColorBrush(Colors.Red);
                }

            }

        }

        private void B2_Click(object sender, RoutedEventArgs e)
        {


            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=UserDB; Integrated Security=True");


            bool execQueryControl = true;
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
            String query = "SELECT sefer_id FROM Seferler WHERE nereden =@nereden AND tarih = @tarih and nereye = @nereye  ";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@nereden", neredencmb.SelectedItem);
            cmd.Parameters.AddWithValue("@nereye", nereyecmb.SelectedItem);
            cmd.Parameters.AddWithValue("@tarih", sefertarihi.SelectedDate);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Seferler.sefer_id = Convert.ToInt32(dr["sefer_id"]);
            }

            dr.Close();

            if (execQueryControl)
            {
                String Query = "INSERT INTO dbo.Bilet(Username,sefer_id,koltuk_no) VALUES(@Username, @sefer_id,@koltuk_no) ";
                SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Username", loginuser.UserName);
                sqlCmd.Parameters.AddWithValue("@sefer_id", Seferler.sefer_id);
                sqlCmd.Parameters.AddWithValue("@koltuk_no", b2.Content);
                int isExecuted = sqlCmd.ExecuteNonQuery();

                if (isExecuted < 1)
                {
                    MessageBox.Show("HATA");
                }
                else
                {

                    MessageBox.Show("Bilet Alım işlemi tamamlandı.");
                    b2.Background = new SolidColorBrush(Colors.Red);
                }

            }
        }

        private void B3_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost\SQLEXPRESS; Initial Catalog=UserDB; Integrated Security=True");


            bool execQueryControl = true;
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
            String query = "SELECT sefer_id FROM Seferler WHERE nereden =@nereden AND tarih = @tarih and nereye = @nereye  ";
            SqlCommand cmd = new SqlCommand(query, sqlCon);
            cmd.Parameters.AddWithValue("@nereden", neredencmb.SelectedItem);
            cmd.Parameters.AddWithValue("@nereye", nereyecmb.SelectedItem);
            cmd.Parameters.AddWithValue("@tarih", sefertarihi.SelectedDate);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Seferler.sefer_id = Convert.ToInt32(dr["sefer_id"]);
            }

            dr.Close();

            if (execQueryControl)
            {
                String Query = "INSERT INTO dbo.Bilet(Username,sefer_id,koltuk_no) VALUES(@Username, @sefer_id,@koltuk_no) ";
                SqlCommand sqlCmd = new SqlCommand(Query, sqlCon);

                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.Parameters.AddWithValue("@Username", loginuser.UserName);
                sqlCmd.Parameters.AddWithValue("@sefer_id", Seferler.sefer_id);
                sqlCmd.Parameters.AddWithValue("@koltuk_no", b3.Content);
                int isExecuted = sqlCmd.ExecuteNonQuery();

                if (isExecuted < 1)
                {
                    MessageBox.Show("HATA");
                }
                else
                {

                    MessageBox.Show("Bilet Alım işlemi tamamlandı.");
                    b3.Background = new SolidColorBrush(Colors.Red);
                }

            }

        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            sorgula();
        }
    }
}

