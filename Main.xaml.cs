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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_FinalTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Clases.FrameUpdate.frame = Fmain;
            Clases.BD.Data = new Entities();
            Fmain.Navigate(new Frames.Servises());
            TBOXAuto.Text = "0000";
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (TBOXAuto.Text == Class.Auto.PassAdmin && Class.Auto.ChangeUser>0)
            {
                Class.Auto.ChangeUser++;
                BBliz.Visibility = Visibility.Visible;
            }
            else if (TBOXAuto.Text == Class.Auto.PassUser && Class.Auto.ChangeUser < 0)
            {
                Class.Auto.ChangeUser++;
                BBliz.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Не правильный код", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            Fmain.Navigate(new Frames.Servises());
        }

        private void BBliz_Click(object sender, RoutedEventArgs e)
        {
            FolderWindow.BlizWindow BW = new FolderWindow.BlizWindow();
            BW.Show();
        }
    }
}
