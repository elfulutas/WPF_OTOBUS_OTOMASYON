using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfLoginApp.Classes
{
    public class UygulamaKontrol
    {
        public static void AddUserControlToGrid(Grid gridobj, UserControl ucobj)
        {
            if(gridobj.Children.Count > 0)
                gridobj.Children.Clear();
          
            gridobj.Children.Add(ucobj);
            
        }
    }
}
