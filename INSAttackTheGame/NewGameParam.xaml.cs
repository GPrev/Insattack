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
using System.Windows.Shapes;
using INSAttack;
using MapDataModel;

namespace INSAttackTheGame
{
    /// <summary>
    /// Logique d'interaction pour NewGameParam.xaml
    /// </summary>
    public partial class NewGameParam : Window
    {

        private List<Department> m_depts;
        private NewGameBuilder m_gameBuilder;

        public NewGameParam()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            m_depts = new List<Department>();
            m_player1.setDefault("INFO");
            m_player2.setDefault("EII");
        }

        public GameBuilder Builder
        {
            get { return m_gameBuilder; }
        }

        private List<Department> getDepartments()
        {
            m_depts = new List<Department>();
            m_depts.Add(m_player1.Department);
            m_depts.Add(m_player2.Department);
            return m_depts;
        }

        private BoardStrategy getBoardCreator()
        {
            if (m_boards.SelectedIndex == 0) return new DemoBoardStrategy(m_depts);
            if (m_boards.SelectedIndex == 1) return new SmallBoardStrategy(m_depts);
            if (m_boards.SelectedIndex == 2) return new NormalBoardStrategy(m_depts);
            return null;
        }


        private void onNew(object sender, RoutedEventArgs e)
        {
            m_gameBuilder = new NewGameBuilder();
            m_gameBuilder.Departments = getDepartments();
            m_gameBuilder.BoardCreator = getBoardCreator();
            
            this.Close();
        }

        private void m_buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
