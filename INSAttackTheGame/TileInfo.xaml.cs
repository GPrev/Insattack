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

namespace INSAttackTheGame
{
    /// <summary>
    /// Logique d'interaction pour TileInfo.xaml
    /// </summary>
    public partial class TileInfo : UserControl
    {
        public TileInfo()
        {
            InitializeComponent();
        }

        public void update()
        {
            if (Context.isGameValid() && Context.CursorPos != null)
            {
                m_coord.Content = "Case : (" + Context.CursorPos.X + ", " + Context.CursorPos.Y + ")";
                m_infos.Text = Context.Map.TileTable[Context.CursorPos].ToString();
            }
        }
    }
}
