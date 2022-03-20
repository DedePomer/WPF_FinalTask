using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_FinalTask.FolderWindow
{
    
    public partial class SubClientWindow : Window
    {
        List<Service> _SubClientList = new List<Service>();
        List<ClientService> _ClientSrv = Clases.BD.Data.ClientService.ToList();
        List<Client> _ClientList  = Clases.BD.Data.Client.ToList();
        bool _TimeIsTrue = false;
        DateTime DateSub;

        public SubClientWindow()
        {
            InitializeComponent();
        }

        public SubClientWindow(List<Service> services)
        {
            InitializeComponent();
            _SubClientList = services;
            TBLOCKTitle.Text = _SubClientList[0].Title;
            TBLOCKDuration.Text = _SubClientList[0].DurationInSeconds / 60 + " мин.";
            string FIO ="";
            for (int i = 0; i < _ClientList.Count; i++)
            {
                FIO += _ClientList[i].FirstName+" ";
                FIO += _ClientList[i].LastName + " ";
                FIO += _ClientList[i].Patronymic;
                CBClients.Items.Add(FIO);
                FIO = "";
            }
        }

        public bool ItsTime(string time)
        {
            string[]timeMas = time.Split(':');

             if (timeMas[0][0] != '2' && timeMas[1][0] != '6')
             {
                return true; 
             }

            if (timeMas[0][0] == '2' && Convert.ToInt32(timeMas[0][1]+"") <= 3 && timeMas[1][0] != '6')
            {
                return true;
            }
            else if (timeMas[1][0] == '6' && Convert.ToInt32(timeMas[1][1]+"") == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int[] RarseDate(string str)
        {
            string[] strMass = str.Split('.');
            int[] IntMas = new int[3];
            for (int i = 0; i < 3; i++)
            {
                if (i != 2)
                {
                    IntMas[i] = Convert.ToInt32(strMass[i]);
                }
                else 
                {
                    strMass[i]=strMass[i].Substring(0, 4);
                    IntMas[i] = Convert.ToInt32(strMass[i]);
                }
            }
            return IntMas;
        }

        private void TBOXStartTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            var OnlyNumberAndSign = new Regex(@"[0-2][0-9]:[0-6][0-9]$");
            var NoSpecSign = new Regex(@"[^!@#$%^&*()_+-]");
            var NoBukv = new Regex(@"[^А-я]");

            if (OnlyNumberAndSign.IsMatch(TBOXStartTime.Text) && NoSpecSign.IsMatch(TBOXStartTime.Text) && NoBukv.IsMatch(TBOXStartTime.Text))
            {
                if (ItsTime(TBOXStartTime.Text))
                {
                    TBOXStartTime.BorderBrush = Brushes.Green;
                    _TimeIsTrue = true;
                    string SelectedDateStr = DPdate.SelectedDate.ToString();
                    int[] OurDate = RarseDate(SelectedDateStr);
                    DateSub = new DateTime(OurDate[2], OurDate[1], OurDate[0]);
                    string[] strOurTime = TBOXStartTime.Text.Split(':');
                    int[] OurTime = new int[] { Convert.ToInt32(strOurTime[0]), Convert.ToInt32(strOurTime[1]) };
                    DateSub = DateSub.AddHours(OurTime[0]);
                    DateSub = DateSub.AddMinutes(OurTime[1]);
                    
                    DateTime OurData = DateSub.AddMinutes(_SubClientList[0].DurationInSeconds / 60);
                    TBLOCKTimeEnd.Text = "Време окончания: " + OurData.TimeOfDay.ToString();
                }
                else
                {
                    TBOXStartTime.BorderBrush = Brushes.Red;
                    _TimeIsTrue = false;
                    TBLOCKTimeEnd.Text = "";
                }
            }
            else
            {
                TBOXStartTime.BorderBrush = Brushes.Red;
                _TimeIsTrue = false;
                TBLOCKTimeEnd.Text = "";
            }
        }

        private void Bsave_Click(object sender, RoutedEventArgs e)
        {
            if (CBClients.SelectedIndex!=-1)
            {
                if (DPdate.SelectedDate!=null)
                {
                    if (_TimeIsTrue)
                    {
                        ClientService AddClientSrvice = new ClientService()
                        {
                            ClientID = CBClients.SelectedIndex + 1,
                            ServiceID = _SubClientList[0].ID,
                            StartTime = DateSub
                        };
                        Clases.BD.Data.ClientService.Add(AddClientSrvice);
                        Clases.BD.Data.SaveChanges();
                        MessageBox.Show("Запись добавленна", "Добавление", MessageBoxButton.OK);
                        this.Close();
                    }
                }
                
            }
        }
    }
}
