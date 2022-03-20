using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace WPF_FinalTask
{
    public partial class ClientService
    {

        public string ServiceTitle
        {
            get
            {
                List<Service> OurSer = Clases.BD.Data.Service.ToList();
                List<Service> Ser = OurSer.Where(x => x.ID == ServiceID).ToList();
                for (int i = 0; i < Ser.Count; i++)
                {
                    if (Ser[i].ID == ServiceID)
                    {
                        return Ser[i].Title;
                    }
                }
                return "";
            }
        }
        public string FIO
        {
            get
            {
                List<Client> Ourclient = Clases.BD.Data.Client.ToList();
                return Ourclient[ClientID].FirstName + " " + Ourclient[ClientID].LastName + " " + Ourclient[ClientID].Patronymic;
            }
        }
        public string EmailTelephone
        {
            get
            {
                List<Client> Ourclient = Clases.BD.Data.Client.ToList();
                return "Почта: " + Ourclient[ClientID].Email + " " + "Телефон: " + Ourclient[ClientID].Phone;
            }
        }


        public string TimeToStart
        {
            get
            {
                DateTime TodayTime = DateTime.Now;
                TimeSpan KeepTime;           
                KeepTime = StartTime.Subtract(TodayTime);
                if (KeepTime.Days == 1)
                {
                    return "Осталось: " + (KeepTime.Hours+24) + " часов и " + KeepTime.Minutes + " минут";
                }
                return "Осталось: " + KeepTime.Hours + " часов и " + KeepTime.Minutes + " минут";
            }
        }

        public SolidColorBrush ColorTextBox
        {
            get
            {
                DateTime TodayTime = DateTime.Now;
                TimeSpan KeepTime;
                KeepTime = StartTime.Subtract(TodayTime);
                SolidColorBrush brush = new SolidColorBrush(Color.FromArgb(0,0,0,1));
                if (KeepTime.Hours == 0 && KeepTime.Days == 0)
                {
                    brush = new SolidColorBrush(Color.FromArgb(255, 1, 1, 1));
                    return System.Windows.Media.Brushes.Red;
                }
                else 
                {
                    brush = new SolidColorBrush(Color.FromArgb(0, 0, 0, 1));
                    return System.Windows.Media.Brushes.Black;
                }
            }
        }

    }
}
