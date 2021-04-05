using System;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;
using winForms = System.Windows.Forms;
using System.IO;
using Path = System.IO.Path;

namespace RezumeReader
{
    public partial class MainWindow : Window
    {
        private Word.Application wordapp;
        public Word.Application Wordapp { get => wordapp; set => wordapp = value; }

        public MainWindow() => InitializeComponent();

        private void Window_MouseLeftButtonDown(object sender, RoutedEventArgs e) => this.DragMove();

        private void BtnClose_Click(object sender, RoutedEventArgs e) => this.DragMove();

        /*       Установка пути к папке с файлами    */
        private void BtnSPath_Click(object sender, RoutedEventArgs e)
        {
            winForms.FolderBrowserDialog folderDialog = new winForms.FolderBrowserDialog();

            folderDialog.ShowNewFolderButton = false;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;

            winForms.DialogResult result = folderDialog.ShowDialog();

            if (result == winForms.DialogResult.OK)
            {
                String sPath = folderDialog.SelectedPath;
                TbSPath.Text = sPath;
            }
        }
        /*      Установка пути к папке, в которую будут отправляться копии отсортированного     */
        private void BtnDPath_Click(object sender, RoutedEventArgs e)
        {
            winForms.FolderBrowserDialog folderDialog = new winForms.FolderBrowserDialog();

            folderDialog.ShowNewFolderButton = true;
            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;

            winForms.DialogResult result = folderDialog.ShowDialog();

            if (result == winForms.DialogResult.OK)
            {
                String dPath = folderDialog.SelectedPath;
                TbDPath.Text = dPath;
            }
        }
        /*      Начало сортировки       */
        private void BtnStartSort_Click(object sender, RoutedEventArgs e)
        {
            string sPath = TbSPath.Text;
            string dPath = TbDPath.Text;

            string[] file_list = Directory.GetFiles(sPath, "*.docx"); //получаем список docx файлов

            Wordapp = new Word.Application();

            Wordapp.Visible = false;
                        
            if (sPath != null)
            {
                
                foreach (string file_to_read in file_list)
                {
                    Wordapp.Documents.Open(file_to_read);
                    string wordtext = Wordapp.Documents.Open(file_to_read).Content.Text;
                    if (wordtext.Contains(CbGender.Text) && wordtext.Contains(CbAge.Text) && wordtext.Contains(CbEducation.Text)&&
                        wordtext.Contains(CbCitizenship.Text) && wordtext.Contains(CbScientist.Text) && wordtext.Contains(CbPost.Text)&&
                        wordtext.Contains(CbMoney.Text) && wordtext.Contains(CbExperience.Text) && wordtext.Contains(CbEnglish.Text))
                    {
                        Wordapp.Documents.Close();
                        File.Copy(file_to_read, dPath + @"\" + Path.GetFileName(file_to_read), true);
                    }
                    else Wordapp.Documents.Close();
                }
            }
            Wordapp.Documents.Save();
            Wordapp.Quit();
        }

        /* Checked события */
        private void Gender_Checked(object sender, RoutedEventArgs e) => CbGender.IsEnabled = true;
        private void Age_Checked(object sender, RoutedEventArgs e) => CbAge.IsEnabled = true;
        private void Education_Checked(object sender, RoutedEventArgs e) => CbEducation.IsEnabled = true;
        private void Citizenship_Checked(object sender, RoutedEventArgs e) => CbCitizenship.IsEnabled = true;
        private void Scientist_Checked(object sender, RoutedEventArgs e) => CbScientist.IsEnabled = true;
        private void Post_Checked(object sender, RoutedEventArgs e) => CbPost.IsEnabled = true;
        private void Money_Checked(object sender, RoutedEventArgs e) => CbMoney.IsEnabled = true;
        private void Experience_Checked(object sender, RoutedEventArgs e) => CbExperience.IsEnabled = true;
        private void English_Checked(object sender, RoutedEventArgs e) => CbEnglish.IsEnabled = true;

        /* UNhecked события */
        private void Gender_Unchecked(object sender, RoutedEventArgs e)
        {
            CbGender.IsEnabled = false;
            CbGender.Text = "Пол";
        }
        private void Age_Unchecked(object sender, RoutedEventArgs e)
        {
            CbAge.IsEnabled = false;
            CbAge.Text = "Пол";
        }
        private void Education_Unchecked(object sender, RoutedEventArgs e)
        {
            CbEducation.IsEnabled = false;
            CbEducation.Text = "Пол";
        }
        private void Citizenship_Unchecked(object sender, RoutedEventArgs e)
        {
            CbCitizenship.IsEnabled = false;
            CbCitizenship.Text = "Пол";
        }
        private void Scientist_Unchecked(object sender, RoutedEventArgs e)
        {
            CbScientist.IsEnabled = false;
            CbScientist.Text = "Пол";
        }
        private void Post_Unchecked(object sender, RoutedEventArgs e)
        {
            CbPost.IsEnabled = false;
            CbPost.Text = "Пол";
        }
        private void Money_Unchecked(object sender, RoutedEventArgs e)
        {
            CbMoney.IsEnabled = false;
            CbMoney.Text = "Пол";
        }
        private void Experience_Unchecked(object sender, RoutedEventArgs e)
        {
            CbExperience.IsEnabled = false;
            CbExperience.Text = "Пол";
        }
        private void English_Unchecked(object sender, RoutedEventArgs e)
        {
            CbEnglish.IsEnabled = false;
            CbEnglish.Text = "Пол";
        }
    }
}
