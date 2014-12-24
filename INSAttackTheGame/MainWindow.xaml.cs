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
using Wrapper;
using MapDataModel;

namespace INSAttackTheGame
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //TEMPORARY : when this goes away, delete the reference to the wrapper in TheGame project
            var wrapper = new WrapperMapGenerator();
            var map = wrapper.makeMap(10, 3, 5, 15, 10);
            m_mapView.init(map);
        }

        private void onMapViewClick(object sender, MouseButtonEventArgs e)
        {
            m_mapView.onClick(sender, e);
        }
    }
    
}
