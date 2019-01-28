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
        private Dictionary<string, string> SESSION = new Dictionary<string, string>();
        private List<Grid> list_grid = new List<Grid>();

        public MainWindow()
        {
            InitializeComponent();
            m_sql = new SQLConnection("www.maguicloud.fr", "bdd_bataillenavale", "bataille", "navale");
            list_grid.Add(Connexion);
            list_grid.Add(Game);
            list_grid.Add(Account);
        }

        private void User_id_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Connection_Click(object sender, RoutedEventArgs e)
        {
            if (!user_name.Text.Equals(" "))
            {
                if (!password.Password.ToString().Equals(" "))
                {
                    try
                    {
                        SESSION = m_sql.getDictPlayer(user_name.Text);
                        if (SESSION["password"].Equals(password.Password.ToString()))
                            change_grid(Account);
                        else
                            setErrorMessage("Erreur d'authentification !");
                    }
                    catch
                    {
                        setErrorMessage("Le joueur " + user_name.Text + " n'éxiste pas !");
                    }
                }
                else setErrorMessage("Champ identifiant vide");
            }
            else setErrorMessage("Champ mot de passe vide");
        }

        private void setErrorMessage(String msg)
        {
            Label_erreur.Content = msg;
        }

        private void Mdp_Perdu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Inscription_Click(object sender, RoutedEventArgs e)
        {

        }

        private void change_grid(Grid grid)
        {
            foreach (Grid g in list_grid)
                if (g.Equals(grid))
                    g.Visibility = Visibility.Visible;
                else
                    g.Visibility = Visibility.Hidden;
        }
    }
}
