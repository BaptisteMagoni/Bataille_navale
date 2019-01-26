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

namespace Bataille_navale
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private SQLConnection m_sql;
        private List<string> player = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            m_sql = new SQLConnection("www.maguicloud.fr", "bdd_bataillenavale", "bataille", "navale");
            player = m_sql.getDictPlayer("ZasTax");
            foreach(string info in player)
            {
                Console.WriteLine(info);
            }
        }

        private void User_id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Connection_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Mdp_Perdu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Inscription_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
