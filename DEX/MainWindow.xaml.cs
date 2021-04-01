using System.IO;
using System.Windows;
using System.Windows.Input;

namespace DEX
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";
            File.OpenWrite(path);

            InitializeComponent();

            this.PreviewKeyDown += EscClicked;

            this.Top = 0;
            this.Left = 0;
        }

        private void Administrative_Click(object sender, RoutedEventArgs e)
        {
            AdministrativeModule administrativeModule = new AdministrativeModule();
            administrativeModule.Show();
            administrativeModule.Top = 0;
            administrativeModule.Left = 0;
            this.Close();
        }

        private void WordSearch_Click(object sender, RoutedEventArgs e)
        {
            WordSearchModule wordSearchModule = new WordSearchModule();
            wordSearchModule.Show();
            wordSearchModule.Top = 0;
            wordSearchModule.Left = 0;
            this.Close();
        }

        private void Entertainment_Click(object sender, RoutedEventArgs e)
        {
            EntertainmentModule entertainmentModule = new EntertainmentModule();
            entertainmentModule.Show();
            entertainmentModule.Top = 0;
            entertainmentModule.Left = 0;
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void EscClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }
    }


}
