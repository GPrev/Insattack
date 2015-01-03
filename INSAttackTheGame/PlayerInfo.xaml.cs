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
using INSAttack;

namespace INSAttackTheGame
{
    /// <summary>
    /// Logique d'interaction pour PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : UserControl
    {
        private Game m_game;

        public Game Game
        {
            get { return m_game; }
            set { m_game = value; }
        }

        private Player m_player;

        public Player Player
        {
            get { return m_player; }
            set { m_player = value; }
        }

        public string PlayerName
        {
            get { return m_player.Id.ToString(); }
            //set {}
        }
        public int NbUnits
        {
            get { return m_game.countUnits(m_player); }
            //set {}
        }
        public int NbPoints
        {
            get { return m_game.points(m_player); }
            //set {}
        }
        public PlayerInfo()
        {
            InitializeComponent();
        }

        public void update()
        {
            m_nameLabel.Content = PlayerName;
            m_nbPoint.Content = "Points : " + NbPoints;
            m_nbUnits.Content = "Nb Units : " + NbUnits;
        }
    }
}
