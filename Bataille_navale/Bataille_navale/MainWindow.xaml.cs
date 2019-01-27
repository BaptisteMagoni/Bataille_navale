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
                        {
                            Console.WriteLine("Ok !");
                            change_grid(Account);
                            BitmapImage bi3 = new BitmapImage();
                            bi3.BeginInit();
                            bi3.UriSource = new Uri(SESSION["profil"], UriKind.Relative);
                            bi3.EndInit();
                            image_profil.Stretch = Stretch.Fill;
                            image_profil.ImageSource = bi3;
                        }
                        else
                        {
                            Label_erreur.Content = "Erreur d'authentification !";
                        }
                    }
                    catch
                    {
                        Label_erreur.Content = "Le joueur " + user_name.Text + " n'éxiste pas !";
                    }
                }
                else Label_erreur.Content = "Champ identifiant vide";
            }
            else Label_erreur.Content = "Champ mot de passe vide";
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
