using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Xamarin.Forms.PlatformConfiguration;

namespace DEX
{
    /// <summary>
    /// Interaction logic for AdministrativeModule.xaml
    /// </summary>
    public partial class AdministrativeModule : Window
    {
        public AdministrativeModule()
        {
            InitializeComponent();

            this.PreviewKeyDown += EscClicked;

            string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";
            var lines = File.ReadAllLines(path);
            if (lines.Length == 0)
            {
                int idWord = 1;
                ID.Text = idWord.ToString();
            }
            else
            {
                var lastLine = lines.Last();
                string[] last = lastLine.Split('#');

                int idWord = int.Parse(last[0]) + 1;
                ID.Text = idWord.ToString();
            }

            List<string> comboBox = new List<string>();
            comboBox = (DataContext as Dex).ReadFile();

            int j = 0;
            while (j < comboBox.Count)
            {
                if(Category.Items.Contains(comboBox[j]) == false)
                {
                    Category.Items.Add(comboBox[j]);
                }
                j++;
            }
        }

        int idWord = 1;
        string pathImage = null;

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";
            var lines = File.ReadAllLines(path);
            var lastLine = lines.Last();
            string[] last = lastLine.Split('#');

            idWord = int.Parse(last[0]) + 1;
            ID.Text = idWord.ToString();

            if (pathImage == null)
            {
                pathImage = "C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/FirstImage.jpg";
            }

            string category = "";

            if (NewCategory.Text != "" && Category.Items.Contains(NewCategory.Text) == false)
            {
                category = NewCategory.Text;
                //Category.Items.Add(category);
            }
            else
            {
                if (NewCategory.Text != "" && Category.Items.Contains(NewCategory.Text) == true)
                {
                    category = NewCategory.Text;
                    MessageBox.Show("This category already exists in the list!");
                }
                else
                {
                    if (NewCategory.Text == "")
                    {
                        category = Category.Text;
                    }
                }
            }


            string fileTXT = idWord.ToString() + "#" + Word.Text + "#" + Description.Text + "#" + category + "#" + pathImage;

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(fileTXT);
            }

            (DataContext as Dex).DexItems.Add(new Words()
            {
                Id = idWord.ToString(),
                Word = Word.Text,
                Description = Description.Text,
                Category = category,
                Path = pathImage
            });

            Image.Source = new BitmapImage(new Uri("C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/FirstImage.jpg"));
            idWord++;
            pathImage = null;
            ID.Text = idWord.ToString();

            Word.Clear();
            Description.Clear();
            NewCategory.Clear();
            Category.SelectedIndex = -1;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            pathImage = Image.Source.ToString();

            string idWordTXT = ID.Text;

            string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";

            (DataContext as Dex).DexItems.Remove(
                (DataContext as Dex).DexItems.Where(i => i.Id == ID.Text).Single());

            RemoveAndModifyFile file = new RemoveAndModifyFile();
            file.RemoveItem(idWordTXT);

            Word.Clear();
            Description.Clear();
            NewCategory.Clear();
            Category.Items.Clear();

            List<string> comboBox = new List<string>();
            (DataContext as Dex).DexItems.Clear();
            comboBox = (DataContext as Dex).ReadFile();

            int j = 0;
            while (j < comboBox.Count)
            {
                if (Category.Items.Contains(comboBox[j]) == false)
                {
                    Category.Items.Add(comboBox[j]);
                }
                j++;
            }

            var lines = File.ReadAllLines(path);
            var lastLine = lines.Last();
            string[] last = lastLine.Split('#');

            idWord = int.Parse(last[0]) + 1;
            ID.Text = idWord.ToString();

            Image.Source = new BitmapImage(new Uri("C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/FirstImage.jpg"));
            pathImage = null;

        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            pathImage = Image.Source.ToString();

            string newText = ID.Text + "#" + Word.Text + "#" + Description.Text + "#" + Category.Text + "#" + pathImage;
            string idWordTXT = ID.Text;

            string path = "D:\\FACULTATE\\ANUL 2\\Semestrul 2\\Medii Vizuale de Programare\\Tema 1\\DEX\\dictionary.txt";

            var found = (DataContext as Dex).DexItems.FirstOrDefault(index => index.Id == idWordTXT);
            int number = (DataContext as Dex).DexItems.IndexOf(found);

            (DataContext as Dex).DexItems[number] = new Words()
            {
                Id = ID.Text,
                Category = Category.Text,
                Description = Description.Text,
                Word = Word.Text,
                Path = pathImage
            };

            RemoveAndModifyFile file = new RemoveAndModifyFile();
            file.ModifyItem(idWordTXT, newText);

            Word.Clear();
            Description.Clear();
            NewCategory.Clear();
            Category.Items.Clear();

            List<string> comboBox = new List<string>();
            (DataContext as Dex).DexItems.Clear();
            comboBox = (DataContext as Dex).ReadFile();

            int j = 0;
            while (j < comboBox.Count)
            {
                if (Category.Items.Contains(comboBox[j]) == false)
                {
                    Category.Items.Add(comboBox[j]);
                }
                j++;
            }

            var lines = File.ReadAllLines(path);
            var lastLine = lines.Last();
            string[] last = lastLine.Split('#');

            idWord = int.Parse(last[0]) + 1;
            ID.Text = idWord.ToString();

            Image.Source = new BitmapImage(new Uri("C:/Users/LAPTOP-HP-TEODORA/Desktop/PhotosDEX/FirstImage.jpg"));
            pathImage = null;
        }

        private void SearchImage_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = false;
            CommonFileDialogResult result = dialog.ShowDialog();
            pathImage = dialog.FileName;
            var uriSource = new Uri(pathImage);
            Image.Source = new BitmapImage(uriSource);
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow menu = new MainWindow();
            menu.Show();
            this.Close();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
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

        private void ListOfID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int number = ListOfID.SelectedIndex;
            if (ListOfID.SelectedIndex > -1)
            {
                ID.Text = (ListOfID.SelectedItem as Words).Id;
                Word.Text = (ListOfID.SelectedItem as Words).Word;
                Category.Text = (ListOfID.SelectedItem as Words).Category;
                Description.Text = (ListOfID.SelectedItem as Words).Description;
                Image.Source = new BitmapImage(new Uri((ListOfID.SelectedItem as Words).Path));
                pathImage = null;
            }
        }
    }
}
