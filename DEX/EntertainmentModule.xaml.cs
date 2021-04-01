using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for EntertainmentModule.xaml
    /// </summary>
    public partial class EntertainmentModule : Window
    {
        int correctAnswers = 0;
        List<string> randomWords;
        int indexWord = 0;
        string[] randomChoice = { "Image", "Description" };
        public EntertainmentModule()
        {
            InitializeComponent();

            TheWord.PreviewKeyDown += EnterClicked;
            this.PreviewKeyDown += EscClicked;

            Random random = new Random();

            string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";
            string[] lines = File.ReadAllLines(path);

            randomWords = new List<string>();

            int index = 0;
            while(index < 5)
            {
                int dex = random.Next(lines.Length);
                if(randomWords.Contains(lines[dex]) == false)
                {
                    randomWords.Add(lines[dex]);
                    index++;
                }
            }
            Game();
        }

        void EscClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow menu = new MainWindow();
                menu.Show();
                this.Close();
                e.Handled = true;
            }
        }

        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string[] line = randomWords[indexWord].Split('#');

                if(TheWord.Text == line[1])
                {
                    CorrectOrWrong.Source = new BitmapImage(new Uri("C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/CORRECT.jpg"));
                    CorrectOrWrong.Visibility = Visibility.Visible;
                    correctAnswers++;
                }
                else
                {
                    CorrectOrWrong.Source = new BitmapImage(new Uri("C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/Wrong.jpg"));
                    CorrectOrWrong.Visibility = Visibility.Visible;
                    TheCorrectAnswerIs.Visibility = Visibility.Visible;
                    CorrectAnswer.Text = line[1];
                    CorrectAnswer.Visibility = Visibility.Visible;
                }
            }
        }

        private void NextOrFinish_Click(object sender, RoutedEventArgs e)
        {
            indexWord++;
            if (indexWord == 4)
            {
                NextOrFinish.Content = "Finish";
            }
            if (indexWord < 5)
            {
                TheWord.Text = "";
                Description.Visibility = Visibility.Collapsed;
                ImageToGuess.Visibility = Visibility.Collapsed;
                CorrectAnswer.Visibility = Visibility.Collapsed;
                CorrectOrWrong.Visibility = Visibility.Collapsed;
                TheCorrectAnswerIs.Visibility = Visibility.Collapsed;
                Game();
            }
            else
            {
                FinishGame finish = new FinishGame();
                finish.Points.Text = correctAnswers.ToString();

                if (correctAnswers == 0 || correctAnswers == 1)
                {
                    finish.BackgroundImage.Source = new BitmapImage(new Uri("C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/BAD.jpg"));
                }
                else
                {
                    if (correctAnswers == 2 || correctAnswers == 3 || correctAnswers == 4)
                    {
                        finish.BackgroundImage.Source = new BitmapImage(new Uri("C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/GOOD.jpg"));
                    }
                    else
                    {
                        finish.BackgroundImage.Source = new BitmapImage(new Uri("C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/CONGRATULATIONS.jpg"));
                    }
                }

                finish.Show();
                finish.Top = 0;
                finish.Left = 0;
                this.Close();
            }
        }

        public void Game()
        {
            string[] line = randomWords[indexWord].Split('#');

            Random random = new Random();
            int ImageOrDescription = random.Next(randomChoice.Length);

            if (line[4] == "C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/FirstImage.jpg" || line[4] == "file:///C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/FirstImage.jpg")
            {
                Description.Text = line[2];
                Description.Visibility = Visibility.Visible;
            }
            else
            {
                if(randomChoice[ImageOrDescription] == "Description")
                {
                    Description.Text = line[2];
                    Description.Visibility = Visibility.Visible;
                }
                else
                {
                    ImageToGuess.Source = new BitmapImage(new Uri(line[4]));
                    ImageToGuess.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
