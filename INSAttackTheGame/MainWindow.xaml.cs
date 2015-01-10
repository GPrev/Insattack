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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NewGameParam m_parameters;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //NewGameBuilder builder = new NewGameBuilder();
            //builder.Departments.Add(new INFO(new Player()));
            //builder.Departments.Add(new EII(new Player()));
            //m_mapView.init(builder);
            m_mapView.init();
            m_unitsDisplay.update();
            m_playerDisplay.init();
        }

        private void onMapViewClick(object sender, MouseButtonEventArgs e)
        {
            m_mapView.onClick(sender, e);
            m_unitsDisplay.update();
            m_playerDisplay.update();
            m_tile.update();
        }

        private void onMapViewRClick(object sender, MouseButtonEventArgs e)
        {
            m_mapView.onRClick(sender, e);
            m_unitsDisplay.update();
            m_playerDisplay.update();
            m_tile.update();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                //Up-left directionnal key
                case Key.A:
                case Key.NumPad7:
                    goUpLeft();
                    m_unitsDisplay.update();
                    m_tile.update();
                    break;
                //Up-left directionnal key
                case Key.Z:
                case Key.NumPad8:
                    goUp();
                    m_unitsDisplay.update();
                    m_tile.update();
                    break;
                //Up-left directionnal key
                case Key.E:
                case Key.NumPad9:
                    goUpRight();
                    m_unitsDisplay.update();
                    m_tile.update();
                    break;
                //Up-left directionnal key
                case Key.Q:
                case Key.NumPad4:
                    goDownLeft();
                    m_unitsDisplay.update();
                    m_tile.update();
                    break;
                //Up-left directionnal key
                case Key.S:
                case Key.NumPad5:
                    goDown();
                    m_unitsDisplay.update();
                    m_tile.update();
                    break;
                //Up-left directionnal key
                case Key.D:
                case Key.NumPad6:
                    goDownRight();
                    m_unitsDisplay.update();
                    m_tile.update();
                    break;

                //Pass a turn for the unit
                case Key.Space:
                    if(Context.SelectedUnit != null)
                    {
                        Context.Game.passTurn(Context.SelectedUnit);
                        m_unitsDisplay.update();
                    }
                    break;

                //Pass a turn
                case Key.Enter:
                    Context.Game.endOfTurn();
                    m_unitsDisplay.update();
                    m_playerDisplay.update();
                    break;
            }
        }

        public void goUpLeft()
        {
            if (Context.CursorPos.X % 2 == 0) // "even" tile (see reference picture)
                moveUnit(-1, 0);
            else // "odd" tile
                moveUnit(-1, -1);
        }

        public void goUp()
        {
            moveUnit(0, -1);
        }

        public void goUpRight()
        {
            if (Context.CursorPos.X % 2 == 0) // "even" tile (see reference picture)
                moveUnit(1, 0);
            else // "odd" tile
                moveUnit(1, -1);
        }

        public void goDownLeft()
        {
            if (Context.CursorPos.X % 2 == 0) // "even" tile (see reference picture)
                moveUnit(-1, 1);
            else // "odd" tile
                moveUnit(-1, 0);
        }

        public void goDown()
        {
            moveUnit(0, 1);
        }

        public void goDownRight()
        {
            if (Context.CursorPos.X % 2 == 0) // "even" tile (see reference picture)
                moveUnit(1, 1);
            else // "odd" tile
                moveUnit(1, 0);
        }

        private void moveUnit(int dx, int dy)
        {
            if (Context.Map.isValid(Context.CursorPos)) //if the cursor is on a valid tile
            {
                Coord newPos = Context.CursorPos.copy();
                newPos.X += dx;
                newPos.Y += dy;
                if (Context.Map.isValid(newPos)) //if the cursor is to be moved on a valid tile
                {
                    m_mapView.moveSelUnit(newPos);
                    m_mapView.InvalidateVisual(); //refreshes the display
                }
            }
        }

        private void m_buttonEndOfTurn_Click(object sender, RoutedEventArgs e)
        {
            Context.Game.endOfTurn();
            m_unitsDisplay.update();
            m_playerDisplay.update();
        }

        private void onNew(object sender, RoutedEventArgs e)
        { 
            m_parameters = new NewGameParam();
            m_parameters.m_buttonNewGame.Click += new RoutedEventHandler(createGame);
            m_parameters.ShowDialog();

        }

        private void createGame(object sender, RoutedEventArgs e)
        {

            GameBuilder builder = m_parameters.Builder;
            if (builder != null)
            {
                if (m_parameters.m_player1.PlayerName.Equals(m_parameters.m_player2.PlayerName))
                {
                    MessageBox.Show("Chaque joueur doit avoir un nom différent.");
                }
                else
                {
                    changeMap(builder);
                    m_playerDisplay.init();
                    m_unitsDisplay.update();
                    m_buttonEndOfTurn.Visibility = System.Windows.Visibility.Visible;
                    m_unitsDisplay.Visibility = System.Windows.Visibility.Visible;
                    m_parameters.Close();
                }
            }
        }

        private void changeMap(GameBuilder builder)
        {
            Context.changeGame(builder);
            Context.CursorPos = Coord.nowhere;
            m_mapView.InvalidateMeasure();
            m_mapView.InvalidateVisual();
        }

        private void onQuickLoad(object sender, RoutedEventArgs e)
        {
            GameLoader loader = new GameLoader();
            changeMap(loader);
            m_playerDisplay.init();
            m_unitsDisplay.update();
            m_buttonEndOfTurn.Visibility = System.Windows.Visibility.Visible;
            m_unitsDisplay.Visibility = System.Windows.Visibility.Visible;
        }

        private void onQuickSave(object sender, RoutedEventArgs e)
        {
            Context.Game.save();
        }

        private void onLoad(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                GameLoader loader = new GameLoader();
                loader.SaveName = filename;
                changeMap(loader);
                m_playerDisplay.init();
                m_unitsDisplay.update();
                m_buttonEndOfTurn.Visibility = System.Windows.Visibility.Visible;
                m_unitsDisplay.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void onSave(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML Files (*.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                Context.Game.save(filename);
            }
        }

        private void onExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
