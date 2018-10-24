using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Workers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BLogic b = new BLogic();

        public MainWindow()
        {
            InitializeComponent();

            
            dg1.ItemsSource = b.p;
            
        }

        private void bc1(object sender, RoutedEventArgs e)
        {
            itb1.Text = "Id: " + b.pi[0].id;
            itb2.Text = "Name: " + b.pi[0].name;
            itb3.Text = "Birthday: " + b.pi[0].birthday;
            itb4.Text = "Location: " + b.pi[0].location;

            indtb1.Text = "Position:" + b.q[0].position.ToString();
            indtb2.Text = "Salary:" + b.q[0].salary.ToString();
            indtb3.Text = "Quality of tasks:" + b.q[0].quality_of_tasks.ToString();
            indtb4.Text = "Teamwork:" + b.q[0].teamwork.ToString();
            indtb5.Text = "Rating:" + (b.q[0].quality_of_tasks + b.q[0].teamwork)/2 * 0.5;

            
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("D:\\MyProjects\\c#\\Workers\\17.jpg");
            bi3.EndInit();
            mainImg.Source = bi3;
        }

        private void bc2(object sender, RoutedEventArgs e)
        {
            itb1.Text = "Id: " + b.pi[1].id;
            itb2.Text = "Name: " + b.pi[1].name;
            itb3.Text = "Birthday: " + b.pi[1].birthday;
            itb4.Text = "Location: " + b.pi[1].location;

            indtb1.Text = "Position:" + b.q[1].position.ToString();
            indtb2.Text = "Salary:" + b.q[1].salary.ToString();
            indtb3.Text = "Quality of tasks:" + b.q[1].quality_of_tasks.ToString();
            indtb4.Text = "Teamwork:" + b.q[1].teamwork.ToString();
            indtb5.Text = "Rating:" + (b.q[1].quality_of_tasks + b.q[1].teamwork) / 2 * 0.5;

            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("D:\\MyProjects\\c#\\Workers\\18.jpg");
            bi3.EndInit();
            mainImg.Source = bi3;
        
    }

        private void bc3(object sender, RoutedEventArgs e)
        {
            itb1.Text = "Id: " + b.pi[2].id;
            itb2.Text = "Name: " + b.pi[2].name;
            itb3.Text = "Birthday: " + b.pi[2].birthday;
            itb4.Text = "Location: " + b.pi[2].location;

            indtb1.Text = "Position:" + b.q[2].position.ToString();
            indtb2.Text = "Salary:" + b.q[2].salary.ToString();
            indtb3.Text = "Quality of tasks:" + b.q[2].quality_of_tasks.ToString();
            indtb4.Text = "Teamwork:" + b.q[2].teamwork.ToString();
            indtb5.Text = "Rating:" + (b.q[2].quality_of_tasks + b.q[2].teamwork) / 2 * 0.5;

            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri("D:\\MyProjects\\c#\\Workers\\43.jpg");
            bi3.EndInit();
            mainImg.Source = bi3;

        }

    }
}
