﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
        }
        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            NewGameBuilder builder = new NewGameBuilder();
            builder.Departments.Add(new INFO(new Player()));
            builder.Departments.Add(new EII(new Player()));
            m_mapView.init(builder);
            m_unitsDisplay.update();
            m_playerDisplay.init();
        }

        private void onMapViewClick(object sender, MouseButtonEventArgs e)
        {
            m_mapView.onClick(sender, e);
            m_unitsDisplay.update();
            m_playerDisplay.update();
        }

        private void onMapViewRClick(object sender, MouseButtonEventArgs e)
        {
            m_mapView.onRClick(sender, e);
            m_unitsDisplay.update();
            m_playerDisplay.update();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                //Up-left directionnal key
                case Key.A:
                case Key.NumPad7:
                    m_mapView.goUpLeft();
                    m_unitsDisplay.update();
                    break;
                //Up-left directionnal key
                case Key.Z:
                case Key.NumPad8:
                    m_mapView.goUp();
                    m_unitsDisplay.update();
                    break;
                //Up-left directionnal key
                case Key.E:
                case Key.NumPad9:
                    m_mapView.goUpRight();
                    m_unitsDisplay.update();
                    break;
                //Up-left directionnal key
                case Key.Q:
                case Key.NumPad4:
                    m_mapView.goDownLeft();
                    m_unitsDisplay.update();
                    break;
                //Up-left directionnal key
                case Key.S:
                case Key.NumPad5:
                    m_mapView.goDown();
                    m_unitsDisplay.update();
                    break;
                //Up-left directionnal key
                case Key.D:
                case Key.NumPad6:
                    m_mapView.goDownRight();
                    m_unitsDisplay.update();
                    break;

                //Pass a turn
                case Key.Space:
                    if(Context.SelectedUnit != null)
                    {
                        Context.Game.passTurn(Context.SelectedUnit);
                        m_unitsDisplay.update();
                    }
                    break;
            }
        }

        private void m_buttonEndOfTurn_Click(object sender, RoutedEventArgs e)
        {
            Context.Game.endOfTurn();
            m_unitsDisplay.update();
            m_playerDisplay.update();
        }

    }
    
}
