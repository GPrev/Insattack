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
            update();
        }


        public void update()
        {
            MapView map = this.Parent as MapView;
            Coord coord = map.CursorPos;
            List<Unit> unitList = map.SelectedUnits;
            if(coord.Equals(Coord.nowhere))
            {
                m_main.Content = "Sélectionnez une case pour afficher les unités.";
            }else{
                m_main.Content = "Liste des unités de la case (" + coord.X + ", " + coord.Y + ") :";
            }
            
            m_units.Children.Clear();
            for (int i = 0; i < unitList.Count; i++)
            {
                UnitInfo unit = new UnitInfo(unitList[i]);
                m_units.Children.Add(unit);
            }
        }

    }
}
