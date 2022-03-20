using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_FinalTask.FolderWindow
{
    /// <summary>
    /// Логика взаимодействия для BlizWindow.xaml
    /// </summary>
    public partial class BlizWindow : Window
    {
        static List<ClientService> _AllClientService;

        public BlizWindow()
        {
            InitializeComponent();


            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();


        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime TodayDate = DateTime.Now;
            DateTime TommorowDate = TodayDate.AddDays(1);

            _AllClientService = Clases.BD.Data.ClientService.ToList();
            List<ClientService> OurClientServiceList = _AllClientService.Where(x => x.StartTime.Date == TodayDate.Date).ToList();
            OurClientServiceList = OurClientServiceList.Where(x => x.StartTime.Hour >= TodayDate.Hour).ToList();
            List<ClientService> TemporraaryList = _AllClientService.Where(x => x.StartTime.Date == TommorowDate.Date).ToList();
            OurClientServiceList.AddRange(TemporraaryList);
            OurClientServiceList.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            LVBliz.ItemsSource = OurClientServiceList;
            LVBliz.Items.Refresh();
        }
    }
}
