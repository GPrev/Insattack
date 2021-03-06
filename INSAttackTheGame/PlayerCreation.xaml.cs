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
    /// Logique d'interaction pour PlayerCreation.xaml
    /// </summary>
    public partial class PlayerCreation : UserControl
    {
        public PlayerCreation()
        {
            InitializeComponent();
        }
        public PlayerCreation(int i)
        {
            InitializeComponent();
            m_main.Text = "Joueur : " + i;
        }

        public String PlayerName
        {
            get { return m_playerName.Text; }
        }

        //Return the department create with the parameters chosen by the user
        public Department Department
        {
            get
            {
                Player player = new Player(m_playerName.Text, getDept());
                return getDepartment(player);
            }
        }

        //Create the department
        private Department getDepartment(Player player)
        {
            if (m_departChoice.SelectedIndex == 0) return new INFO(player);
            if (m_departChoice.SelectedIndex == 1) return new EII(player);
            if (m_departChoice.SelectedIndex == 2) return new SRC(player);
            if (m_departChoice.SelectedIndex == 3) return new SGM(player);
            if (m_departChoice.SelectedIndex == 4) return new GMA(player);
            if (m_departChoice.SelectedIndex == 5) return new INSAttack.GC(player);
            return new INFO(player); //default case
        }

        private Dept getDept()
        {
            if (m_departChoice.SelectedIndex == 1) return Dept.EII;
            if (m_departChoice.SelectedIndex == 2) return Dept.SRC;
            if (m_departChoice.SelectedIndex == 3) return Dept.SGM;
            if (m_departChoice.SelectedIndex == 4) return Dept.GMA;
            if (m_departChoice.SelectedIndex == 5) return Dept.GC;
            return Dept.INFO; //default case
        }

        //Set the default department for the player
        public void setDefault(String department)
        {
            if (department.Equals("INFO")) m_departChoice.SelectedIndex = 0;
            if (department.Equals("EII")) m_departChoice.SelectedIndex = 1;
            if (department.Equals("SRC")) m_departChoice.SelectedIndex = 2;
            if (department.Equals("SGM")) m_departChoice.SelectedIndex = 3;
            if (department.Equals("GMA")) m_departChoice.SelectedIndex = 4;
            if (department.Equals("GC")) m_departChoice.SelectedIndex = 5;
        }

        //set the number of the player
        public void setPlayerNumber(int i)
        {
            m_main.Text = "Joueur " + i + " : ";
            m_playerName.Text = "Joueur " + i;
        }

    }
}
