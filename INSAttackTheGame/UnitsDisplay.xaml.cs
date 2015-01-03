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
    /// Logique d'interaction pour UnitsDisplay.xaml
    /// </summary>
    public partial class UnitsDisplay : UserControl
    {
        
        public UnitsDisplay(Game game)
        {
            InitializeComponent();
            m_unitList = new List<Unit>();
            m_game = game;
            m_main.Content = "Liste des unités";
        }

        Game m_game;

        List<Unit> m_unitList;

        public List<Unit> UnitList
        {
            get { return m_unitList; }
            set { m_unitList = value; }
        }


        public void update(Coord coord)
        {
            if (coord == Coord.nowhere)
            {
                m_units.Children.Clear();
                m_main.Content = "Sélectionnez une case pour afficher les unités qu'elle contient.";
            }
            else
            {
                m_unitList = m_game.Board.UnitTable[coord];
                m_main.Content = "Liste des unités de la case : (" + coord.X + ", " + coord.Y + ") :";
                update();
            }
        }

        public void update()
        {
            m_units.Children.Clear();
            for (int i = 0; i < m_unitList.Count; i++)
            {
                Label unitDisplay = new Label();
                
                m_units.Children.Insert(i, unitDisplay);

            }
        }

        public Unit get(Label label)
        {
            return m_unitList[m_units.Children.IndexOf(label)];
        }


    }
}
