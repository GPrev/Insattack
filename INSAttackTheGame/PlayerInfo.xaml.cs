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
using MapDataModel;

namespace INSAttackTheGame
{
    /// <summary>
    /// Logique d'interaction pour PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : UserControl
    {

        private Player m_player;

        public Player Player
        {
            get { return m_player; }
            set { m_player = value; }
        }

        public string PlayerName
        {
            get { return m_player.toString(); }
        }
        public int NbUnits
        {
            get { return Context.Game.countUnits(m_player); }
        }
        public int NbPoints
        {
            get { return Context.Game.getPoints(m_player); }
        }
        public PlayerInfo()
        {
            InitializeComponent();
        }

        public PlayerInfo(Player player)
        {
            InitializeComponent();
            m_player = player;
            update();

            BitmapFrame img = null;
            switch(player.Dept)
            {
                case Dept.EII:
                    img = BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Units/EII.png"));
                    break;
                case Dept.SRC:
                    img = BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Units/SRC.png"));
                    break;
                case Dept.GMA:
                    img = BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Units/GMA.png"));
                    break;
                case Dept.SGM:
                    img = BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Units/SGM.png"));
                    break;
                case Dept.GC:
                    img = BitmapFrame.Create(new Uri(@"pack://application:,,/Resources/Units/GC.png"));
                    break;
                default: //INFO
                    break; //the picture is already there
            }
            if(img != null)
                m_playerFace.Source = img;
        }

        public void update()
        {
            m_playerName.Content = PlayerName;
            m_nbPoint.Content = "Points : " + NbPoints;
            m_nbUnits.Content = "Nb Units : " + NbUnits;
        }
    }
}
