using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    public class HomePageVM : BaseVM
    {
        public string Username;
        public string HomeText { get; set; } = "Default";
        private Player? Player;

        public ICommand LogoutRequest { get; set; }
        public ICommand StartRequest { get; set; }
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
            LogoutRequest = new RelayCommand(Logout);
            Player = player;
            StartRequest = new RelayCommand(Start);
        }

        public void Logout()
        {
            MainWindowVM.OnRequestVMChange(new LoginVM());
        }
        public void Start()
        {
            MainWindowVM.OnRequestVMChange(new SelectPageVM(Player));
        }
    }
}
