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
    /// Interaction logic for WordSearchModule.xaml
    /// </summary>
    public partial class WordSearchModule : Window
    {
        List<string> wordsList;
        List<string> categoryList;
        public WordSearchModule()
        {
            InitializeComponent();

            SearchWord.PreviewKeyDown += EnterClicked;
            this.PreviewKeyDown += EscClicked;

            wordsList = new List<string>();
            categoryList = new List<string>();

            string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";
            string[] lines = File.ReadAllLines(path);

            int indexFile = 0;
            while (indexFile < lines.Length)
            {
                string[] dexWords = lines[indexFile].Split('#');
                wordsList.Add(dexWords[1]);
                categoryList.Add(dexWords[3]);
                if (SearchCategory.Items.Contains(dexWords[3]) == false)
                {
                    SearchCategory.Items.Add(dexWords[3]);
                }
                indexFile++;
            }

            SearchWord.TextChanged += new TextChangedEventHandler(SearchWord_TextChanged);
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
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
                MainWindow menu = new MainWindow();
                menu.Show();
                this.Close();
                e.Handled = true;
            }
        }

        private void SearchWord_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typedString = SearchWord.Text;
            string category = SearchCategory.Text;

            List<string> autoList = new List<string>();

            if(category == "")
            {
                foreach (string item in wordsList)
                {
                    if (!string.IsNullOrEmpty(SearchWord.Text))
                    {
                        if (item.StartsWith(typedString))
                        {
                            autoList.Add(item);
                        }
                    }
                }
            }
            else
            {
                int index = 0;
                while(index < wordsList.Count)
                {
                    if(categoryList[index] == category)
                    {
                        if (!string.IsNullOrEmpty(SearchWord.Text))
                        {
                            if (wordsList[index].StartsWith(typedString))
                            {
                                autoList.Add(wordsList[index]);
                            }
                        }
                    }
                    index++;
                }
            }

            if (autoList.Count > 0)
            {
                Suggestion.ItemsSource = autoList;
                Suggestion.Visibility = Visibility.Visible;
            }
            else
            {
                Suggestion.Visibility = Visibility.Collapsed;
                Suggestion.ItemsSource = null;
            }
        }

        private void Suggestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Suggestion.ItemsSource != null)
            {
                Suggestion.Visibility = Visibility.Collapsed;
                SearchWord.TextChanged -= new TextChangedEventHandler(SearchWord_TextChanged);
                if (Suggestion.SelectedIndex != -1)
                {
                    SearchWord.Text = Suggestion.SelectedItem.ToString();
                }
                SearchWord.TextChanged += new TextChangedEventHandler(SearchWord_TextChanged);
            }
        }

        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                string word = SearchWord.Text;
                if (wordsList.Contains(word))
                {
                    string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";
                    string[] lines = File.ReadAllLines(path);

                    int indexFile = 0;
                    while (indexFile < lines.Length)
                    {
                        string[] dexWords = lines[indexFile].Split('#');
                        if (dexWords[1] == word)
                        {
                            Word.Text = word;
                            Description.Text = dexWords[2];
                            Category.Text = dexWords[3];
                            Image.Source = new BitmapImage(new Uri(dexWords[4]));
                            break;
                        }
                        indexFile++;
                    }
                    SearchCategory.SelectedIndex = -1;
                    SearchWord.Clear();
                }
                else
                {
                    MessageBox.Show("The word is not in dex!");
                }
                e.Handled = true;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string word = SearchWord.Text;
            if(wordsList.Contains(word))
            {
                string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";
                string[] lines = File.ReadAllLines(path);

                int indexFile = 0;
                while (indexFile < lines.Length)
                {
                    string[] dexWords = lines[indexFile].Split('#');
                    if (dexWords[1] == word)
                    {
                        Word.Text = word;
                        Description.Text = dexWords[2];
                        Category.Text = dexWords[3];
                        Image.Source = new BitmapImage(new Uri(dexWords[4]));
                        break;
                    }
                    indexFile++;
                }
                SearchCategory.SelectedIndex = -1;
                SearchWord.Clear();
            }
            else
            {
                MessageBox.Show("The word is not in dex!");
            }
        }
    }
}
