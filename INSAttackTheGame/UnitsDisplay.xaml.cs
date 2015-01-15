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
        private List<UnitInfo> m_unitsList;
        private bool m_visibility;//true if the unit list has to be display
        public UnitsDisplay()
        {
            InitializeComponent();
            m_visibility = false;
            m_unitsList = new List<UnitInfo>();
        }


        public void update()
        {
            if (Context.isGameValid())
            {
                Coord coord = Context.CursorPos;
                List<Unit> unitList = Context.SelectedUnitsList;
                int selectedUnit = -1;

                m_unitsList = new List<UnitInfo>();
                if (coord.Equals(Coord.nowhere))
                {
                    m_main.Text = "Sélectionnez une case pour afficher les unités.";
                }
                else
                {
                    m_main.Text = "Liste des unités de la case (" + coord.X + ", " + coord.Y + ") :";
                }

                for (int i = 0; i < unitList.Count; i++)
                {
                    UnitInfo unit = new UnitInfo(unitList[i]);
                    if ((unitList[i] == Context.SelectedUnit) && (unitList[i] != null)) selectedUnit = i;
                    m_unitsList.Add(unit);
                }
                m_units.ItemsSource = m_unitsList;
                if (selectedUnit != -1) m_units.SelectedIndex = selectedUnit;
                m_units.Background = this.Background;
                if (unitList.Count == 0)
                {
                    m_units.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    if(m_visibility) m_units.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                m_units.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public void onClick(object sender, MouseButtonEventArgs e)
        {
            resetColor();
            ((UnitInfo) sender).select();
        }



        public void resetColor()
        {
            foreach (UnitInfo unitInfo in m_units.Items)
            {
                unitInfo.Border.BorderBrush = Brushes.Black;
            }
        }

        public void setUIVisibility(bool visible)
        {
            m_visibility = visible;
            if (!visible) Visibility = System.Windows.Visibility.Hidden;
            else Visibility = System.Windows.Visibility.Visible;
        }

        private void m_units_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetColor();
            if(m_units.SelectedItem != null) {((UnitInfo) m_units.SelectedItem).select();}
            //((UnitInfo)sender).select();
        }

    }
}
