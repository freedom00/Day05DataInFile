using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Day05DataInFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String currentFile;
        private bool isDocModified;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void miNew_Click(object sender, RoutedEventArgs e)
        {
            if (isDocModified)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(this, "Are you sure you want to create a new file without saving the current modification?", "Operation Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (MessageBoxResult.Cancel == messageBoxResult)
                {
                    return;
                }
                else if (MessageBoxResult.No == messageBoxResult)
                {
                    doSave();
                }
            }

            tbText.Text = "";
            isDocModified = false;
            currentFile = null;
        }

        private void miOpen_Click(object sender, RoutedEventArgs e)
        {
            if (isDocModified)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(this, "Are you sure you want to open a file without saving the current modification?", "Operation Confirm", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (MessageBoxResult.Cancel == messageBoxResult)
                {
                    return;
                }
                else if (MessageBoxResult.No == messageBoxResult)
                {
                    doSave();
                }
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (true == openFileDialog.ShowDialog())
            {
                try
                {
                    tbText.Text = File.ReadAllText(openFileDialog.FileName);
                    isDocModified = false;
                    currentFile = openFileDialog.FileName;
                }
                catch (IOException ex)
                {
                    MessageBox.Show(this, ex.Message, "File Operation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void miSave_Click(object sender, RoutedEventArgs e)
        {
            doSave();
        }

        private void miSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (false == saveFileDialog.ShowDialog())
            {
                return;
            }
            currentFile = saveFileDialog.FileName;

            try
            {
                File.WriteAllText(currentFile, tbText.Text);
                isDocModified = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, ex.Message, "File Operation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void tbText_TextChanged(object sender, TextChangedEventArgs e)
        {
            isDocModified = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isDocModified)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(this, "Are you sure you want to exit without saving the current modification?", "Operation Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (MessageBoxResult.No == messageBoxResult)
                {
                    doSave();
                }
            }
        }

        private void doSave()
        {
            if (null == currentFile)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (false == saveFileDialog.ShowDialog())
                {
                    return;
                }
                currentFile = saveFileDialog.FileName;
            }

            try
            {
                File.WriteAllText(currentFile, tbText.Text);
                isDocModified = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, ex.Message, "File Operation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}