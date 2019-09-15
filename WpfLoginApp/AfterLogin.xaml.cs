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
using WpfLoginApp.Classes;

namespace WpfLoginApp
{
    /// <summary>
    /// AfterLogin.xaml etkileşim mantığı
    /// </summary>
    public partial class AfterLogin : Window
    {
        public AfterLogin()
        {
            InitializeComponent();
        }

        public static object Controls { get; internal set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            welcomeuser.Content = "Hoşgeldiniz " + loginuser.Name;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //UygulamaKontrol.AddUserControlToGrid(usercontrol, new HesapBilgileri());
            if(usercontrol.Children.Count > 0)
            {
                usercontrol.Children.Clear();
            }
            usercontrol.Children.Add(new HesapBilgileri());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //UygulamaKontrol.AddUserControlToGrid(usercontrol, new DenemeUser());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {//kapatma butonu
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {//max butonu
            if(WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
;            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {//min butonu
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {//bilet al
            if (usercontrol.Children.Count > 0)
            {
                usercontrol.Children.Clear();
            }
            usercontrol.Children.Add(new BiletAl());
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            UygulamaKontrol.AddUserControlToGrid(usercontrol, new BiletIptal());
        }
    }
}
