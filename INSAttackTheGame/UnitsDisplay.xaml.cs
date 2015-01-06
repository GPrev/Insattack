using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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

        public UnitsDisplay()
        {
            InitializeComponent();
        }


        public void update()
        {
            if (Context.isGameValid()) 
            { 
                Coord coord = Context.CursorPos;
                List<Unit> unitList = Context.SelectedUnitsList;
                if (coord.Equals(Coord.nowhere))
                {
                    m_main.Text = "Sélectionnez une case pour afficher les unités.";
                }
                else
                {
                    m_main.Text = "Liste des unités de la case (" + coord.X + ", " + coord.Y + ") :";
                }

                m_units.Children.Clear();
                for (int i = 0; i < unitList.Count; i++)
                {
                    UnitInfo unit = new UnitInfo(unitList[i]);
                    unit.MouseLeftButtonDown += new MouseButtonEventHandler(onClick);
                    if ((unitList[i] == Context.SelectedUnit) && (unitList[i] != null)) unit.select();
                    m_units.Children.Add(unit);
                }
            }
        }


        public void onClick(object sender, MouseButtonEventArgs e)
        {
            resetColor();
            ((UnitInfo) sender).select();
        }



        public void resetColor()
        {
            foreach (UnitInfo unitInfo in m_units.Children)
            {
                unitInfo.Border.BorderBrush = Brushes.Black;
            }
        }

    }
}
