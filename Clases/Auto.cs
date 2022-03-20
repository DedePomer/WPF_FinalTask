using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FinalTask.Class
{
    static class Auto
    {
        static public int TypeUser = 1;
        static public string PassAdmin = "0000";
        static public string PassUser = "1111";

        public static int ChangeUser
        {
            get
            {
                return TypeUser;
            }
            set
            {
                TypeUser *= -1;
            }
        }
    }
}
