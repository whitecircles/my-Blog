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

namespace VideoPlayer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog myDialog = new OpenFileDialog();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            myDialog.ShowDialog();
            FileInfo fileInfo = new FileInfo(myDialog.FileName);
            me1.Source = new Uri(fileInfo.FullName);
            
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (me1.Source != null)
            {
                me1.Play();
            }

            
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            me1.Pause();
        }
    }

  
}
