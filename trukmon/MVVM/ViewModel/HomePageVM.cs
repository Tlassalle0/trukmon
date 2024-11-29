using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    public class HomePageVM : BaseVM
    {
        public string Username;
        public string HomeText { get; set; } = "Default";
        public HomePageVM() { }
        public HomePageVM(Player? player)
        {
            if (player!= null)
            {
                Username = player.Name;
                HomeText = "Welcome " + Username;
            }
            else
            {
                HomeText = "You don't have an account";
            }
        }
    }
}
