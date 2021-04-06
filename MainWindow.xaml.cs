using System;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;
using winForms = System.Windows.Forms;
using System.IO;
using Path = System.IO.Path;
using System.Diagnostics;

namespace RezumeReader
{
    public partial class MainWindow : Window
    {
        private Word.Application wordapp;
        public Word.Application Wordapp { get => wordapp; set => wordapp = value; }

        public MainWindow() => InitializeComponent();

        private void Window_MouseLeftButtonDown(object sender, RoutedEventArgs e) => this.DragMove();

        private void BtnClose_Click(object sender, RoutedEventArgs e) => this.Close();

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

            Wordapp = new Word.Application();

            Wordapp.Visible = false;

            try
            {
                string[] file_list = Directory.GetFiles(sPath, "*.docx"); //получаем список docx файлов


                if (sPath.Length != 0 && dPath.Length != 0)
                {

                    foreach (string file_to_read in file_list)
                    {
                        Wordapp.Application.Visible = false;
                        Wordapp.Documents.Open(file_to_read);
                        string wordtext = Wordapp.Documents.Open(file_to_read).Content.Text;
                        
                        if (wordtext.Contains("Шаблон") == false &&
                            (wordtext.IndexOf(CbGender.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                            (wordtext.IndexOf(CbEducation.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                            (wordtext.IndexOf(CbCitizenship.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                            (wordtext.IndexOf(CbScientist.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                            (wordtext.IndexOf(CbPost.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                            (wordtext.IndexOf(CbEnglish.Text, StringComparison.OrdinalIgnoreCase) >= 0))
                            File.Copy(file_to_read, dPath + @"\" + Path.GetFileName(file_to_read), true);

                    }
                    Process.Start("explorer", dPath);
                }
                else
                {
                    ExceptionDialog exceptionDialog = new ExceptionDialog();
                    exceptionDialog.ShowDialog();
                }
            }
            catch(Exception)
            {
                ExceptionDialog exceptionDialog = new ExceptionDialog();
                exceptionDialog.ShowDialog();
            }
            Wordapp.Quit(true);
        }

        /* Checked события */
        private void Gender_Checked(object sender, RoutedEventArgs e) => CbGender.IsEnabled = true;
        private void Education_Checked(object sender, RoutedEventArgs e) => CbEducation.IsEnabled = true;
        private void Citizenship_Checked(object sender, RoutedEventArgs e) => CbCitizenship.IsEnabled = true;
        private void Scientist_Checked(object sender, RoutedEventArgs e) => CbScientist.IsEnabled = true;
        private void Post_Checked(object sender, RoutedEventArgs e) => CbPost.IsEnabled = true;
        private void English_Checked(object sender, RoutedEventArgs e) => CbEnglish.IsEnabled = true;

        /* UNhecked события */
        private void Gender_Unchecked(object sender, RoutedEventArgs e)
        {
            CbGender.IsEnabled = false;
            CbGender.Text = "Пол";
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
        private void English_Unchecked(object sender, RoutedEventArgs e)
        {
            CbEnglish.IsEnabled = false;
            CbEnglish.Text = "Пол";
        }
    }
}
