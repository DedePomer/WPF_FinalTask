using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF_FinalTask
{
    public partial class Service
    {
        public string PhotoPath
        {

            get
            {
                //string s = "..\" + MainImagePath;
                return "..\\Res\\" + MainImagePath;
            }
        }

        public string Dis
        {
            get
            {
                string number;
                if (Discount > 0)
                {
                    number = "*скидка " + Convert.ToString(Discount * 100) + "%";
                }
                else
                {
                    number = "";
                }
                return number;

            }
        }

        public string CrossedOutPrice
        {
            get
            {
                if (Discount > 0)
                {
                    return (int)Cost + " ";
                }
                else
                {
                    return "";
                }
            }
        }

        public string Price
        { get
            {
                string s = " за " + (DurationInSeconds / 60) + " мин.";
                if (Discount > 0)
                {
                    int a = Convert.ToInt32((int)Cost * (1 - Discount));// переменная в которой хранится цена со скидкой
                    return Convert.ToString(a) + " рублей" + s;
                }
                else
                {
                    return "" + (int)Cost + " рублей" + s;
                }
            }
        }

        public SolidColorBrush ColorCell
        {
            get 
            {
                if (Discount > 0)
                {
                    return Brushes.LightGoldenrodYellow;
                }
                else
                {
                    return Brushes.White;
                }
            }
        }





    }
}
