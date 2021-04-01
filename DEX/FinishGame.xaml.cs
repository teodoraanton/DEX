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

namespace DEX
{
    /// <summary>
    /// Interaction logic for FinishGame.xaml
    /// </summary>
    public partial class FinishGame : Window
    {
        public FinishGame()
        {
            InitializeComponent();
        }

        private void YES_Click(object sender, RoutedEventArgs e)
        {
            EntertainmentModule entertainmentModule = new EntertainmentModule();
            entertainmentModule.Show();
            entertainmentModule.Top = 0;
            entertainmentModule.Left = 0;
            this.Close();
        }

        private void NO_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.Top = 0;
            mainWindow.Left = 0;
            this.Close();
        }
    }
}
