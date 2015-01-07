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

namespace INSAttackTheGame
{
    /// <summary>
    /// Logique d'interaction pour UnitInfo.xaml
    /// </summary>
    public partial class UnitInfo : UserControl
    {
        public UnitInfo(Unit unit)
        {
            InitializeComponent();
            m_unit = unit;
            update();
        }

        Unit m_unit;
        public Unit Unit
        {
            get { return m_unit; }
            //set { m_unit = value; }
        }

        public Border Border
        {
            get { return m_border; }
        }

        public void update()
        {
            m_nameLabel.Content = "Propriétaire : Joueur " + m_unit.Player.Id;
            m_Life.Content = "Vie : " + m_unit.Life + "/" + m_unit.MaxLife;
            m_Movement.Content = "Mouvement : " + m_unit.Movement + "/" + m_unit.MaxMovement;
            m_Attack.Content = "Attaque : " + m_unit.Attack;
            m_Defense.Content = "Defense : " + m_unit.Defense;
        }


        public void select()
        {
            Context.SelectedUnit = m_unit;
            m_border.BorderBrush = Brushes.Red;
        }
    }
}
