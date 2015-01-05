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

namespace INSAttackTheGame
{
    /// <summary>
    /// Logique d'interaction pour PlayersDisplay.xaml
    /// </summary>
    public partial class PlayersDisplay : UserControl
    {
        public PlayersDisplay()
        {
            InitializeComponent();
        }

        public void init()
        {
            for (int i = 0; i < Context.Game.NbPlayer; i++)
            {
                PlayerInfo playerView = new PlayerInfo(Context.Game.Players[i]);
                playerView.update();
                m_players.Children.Add(playerView);
            }
            m_main.Text = "Joueur actif : " + Context.Game.ActivePlayer.toString() + "\nNombre de tours restant : " + Context.Board.NbTurns;
        }

        public void update()
        {
            foreach (PlayerInfo player in m_players.Children)
            {
                player.update();
            }
        }
    }
}
