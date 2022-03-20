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

namespace WPF_FinalTask.Frames
{
    /// <summary>
    /// Логика взаимодействия для Servises.xaml
    /// </summary>
    public partial class Servises : Page
    {
        List<Service> StartFilter = Clases.BD.Data.Service.ToList();
        List<Service> OrderFilter;

        
        public Servises()
        {
            InitializeComponent();
            LVService.ItemsSource = StartFilter;
            OrderFilter = StartFilter;
            CBSort.SelectedIndex = 0;
            CBFilt.SelectedIndex = 0;
            Clases.UpdateListClass.myListView = LVService;
        }

        private void CBSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sort();
            LVService.Items.Refresh();
        }

        private int sort()
        {
            switch (CBSort.SelectedIndex)
            {
                case 0:
                    OrderFilter.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    return 0;

                case 1:
                    OrderFilter.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    OrderFilter.Reverse();
                    return 1;

                case -1:
                    return -1;

                default:
                    return -2;
            }
        }

        private void BLoaded(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (Class.Auto.TypeUser<0)
            {
                button.Visibility = Visibility.Visible;
            }
            else    
            {
                button.Visibility = Visibility.Hidden;
            }
        }

        private void CBColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
            LVService.ItemsSource = OrderFilter;
            LVService.Items.Refresh();
        }

        private void Filter()
        {
            int index = CBFilt.SelectedIndex;
            switch(index)
            {
                case 1:
                    OrderFilter = StartFilter.Where(x => x.Discount >= 0 && x.Discount < 0.05).ToList();
                    break;
                case 2:
                    OrderFilter = StartFilter.Where(x => x.Discount >= 0.05 && x.Discount < 0.15).ToList();
                    break;
                case 3:
                    OrderFilter = StartFilter.Where(x => x.Discount >= 0.15 && x.Discount < 0.30).ToList();
                    break;
                case 4:
                    OrderFilter = StartFilter.Where(x => x.Discount >= 0.30 && x.Discount < 0.70).ToList();
                    break;
                case 5:
                    OrderFilter = StartFilter.Where(x => x.Discount >= 0.70 && x.Discount < 1).ToList();
                    break;
                case 0:
                    OrderFilter = StartFilter;
                    break;
            }
                       
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int ind = Convert.ToInt32(button.Uid);
            Service Delete = Clases.BD.Data.Service.FirstOrDefault(y => y.ID == ind);
            Clases.BD.Data.Service.Remove(Delete);
            Clases.BD.Data.SaveChanges();
            Clases.FrameUpdate.frame.Navigate(new Servises());
            MessageBox.Show("Запись удалена", "", MessageBoxButton.OK);
        }

        private void LVService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LVService.SelectedIndex != -1)
            {
                BSubscribe.Visibility = Visibility.Visible;
            }
            else
            {
                BSubscribe.Visibility = Visibility.Collapsed;
            }

        }

        private void BSubscribe_Click(object sender, RoutedEventArgs e)
        {
            List<Service> OurService = new List<Service>();
            foreach (Service item in LVService.SelectedItems)
            {
                OurService.Add(item);
            }
            FolderWindow.SubClientWindow subClientWindow = new FolderWindow.SubClientWindow(OurService);
            subClientWindow.Show();
        }
    }
}
