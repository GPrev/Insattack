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
            //ImageBrush backgroundPicture = new ImageBrush();
            //backgroundPicture.ImageSource =
            //    new BitmapImage(
            //        new Uri(@"pack://application:,,/Resources/textures/parcho12.jpg")
            //    );
            //Background = backgroundPicture;
        }

        //out of date
        //public void update()
        //{
        //    if (Context.isGameValid())
        //    {
        //        Coord coord = Context.CursorPos;
        //        List<Unit> unitList = Context.SelectedUnitsList;
        //        int selectedUnit = -1;
        //        //m_units = new ListBox();
        //        List<UnitInfo> units = new List<UnitInfo>();
        //        if (coord.Equals(Coord.nowhere))
        //        {
        //            m_main.Text = "Sélectionnez une case pour afficher les unités.";
        //        }
        //        else
        //        {
        //            m_main.Text = "Liste des unités de la case (" + coord.X + ", " + coord.Y + ") :";
        //        }

        //        //m_units.Items.Clear();
        //        for (int i = 0; i < unitList.Count; i++)
        //        {
        //            UnitInfo unit = new UnitInfo(unitList[i]);
        //            //unit.MouseLeftButtonDown += new MouseButtonEventHandler(onClick);
        //            if ((unitList[i] == Context.SelectedUnit) && (unitList[i] != null)) selectedUnit = i;//unit.select();
        //            units.Add(unit);
        //        }
        //        m_units = new ListBox();
        //        m_grid.Children.Add(m_units);
        //        m_units.ItemsSource = units;
        //        m_units.SelectionChanged += new SelectionChangedEventHandler(m_units_SelectionChanged);
        //        if (selectedUnit != -1) m_units.SelectedIndex = selectedUnit;
        //    }
        //}

        public void update()
        {
            if (Context.isGameValid())
            {
                Coord coord = Context.CursorPos;
                List<Unit> unitList = Context.SelectedUnitsList;
                int selectedUnit = -1;

                List<UnitInfo> units = new List<UnitInfo>();
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
                    if ((unitList[i] == Context.SelectedUnit) && (unitList[i] != null)) selectedUnit = i;//unit.select();
                    units.Add(unit);
                }
                m_units.ItemsSource = units;
                if (selectedUnit != -1) m_units.SelectedIndex = selectedUnit;
                m_units.Background = this.Background;
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

        private void m_units_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resetColor();
            if(m_units.SelectedItem != null) {((UnitInfo) m_units.SelectedItem).select();}
            //((UnitInfo)sender).select();
        }

    }
}
