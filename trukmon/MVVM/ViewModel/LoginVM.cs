using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using trukmon.Data;
using trukmon.Model;
using WpfCours.MVVM.ViewModel;

namespace trukmon.MVVM.ViewModel
{
    public class LoginVM : BaseVM
    {
        ExerciceMonsterContext _dataContext;
        ObservableCollection<Login> possibleLogins;

        public ICommand LoginRequest { get; set; }
        public ICommand LinkRequest { get; set; }

        private string _servName;
        public string ServName
        {
            get => _servName;
            set
            {
                if (_servName != value)
                {
                    _servName = value;
                    OnPropertyChanged(nameof(ServName));
                }
            }
        }
        
        private string _username="poyo";
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        private string _password="poyo";
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public LoginVM()
        {
            _dataContext = new ExerciceMonsterContext();
            LoginRequest = new RelayCommand(LoginAttempt);
            LinkRequest = new RelayCommand(LinkAttempt);
            InitDB();
        }

        public async void LoginAttempt()
        {
            await LoginAttemptAsync();
        }
        public void LinkAttempt()
        {
            //_appSettings.ServerName = ServName;
            //var options = new DbContextOptionsBuilder<ExerciceMonsterContext>().Options;
            //_dataContext = new ExerciceMonsterContext(_appSettings, options);
            //MessageBox.Show(_appSettings.ServerName);
            MessageBox.Show("Not implemented");
        }

        private void Login(Player? player)
        {
            MainWindowVM.OnRequestVMChange.Invoke(new HomePageVM(player));
        }

        static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha256.ComputeHash(bytes);

                // Convert hash byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                {
                    builder.Append(b.ToString("x2")); // Convert each byte to a hex value
                }
                return builder.ToString();
            }
        }

        public async Task LoginAttemptAsync()
        {
            try
            {
                if (_dataContext == null)
                {
                    MessageBox.Show("_dataContext is null");
                    return;
                }

                if (string.IsNullOrEmpty(Username))
                {
                    MessageBox.Show("Username is null or empty");
                    return;
                }

                if (string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("Password is null or empty");
                    return;
                }

                if (_dataContext.Logins == null)
                {
                    MessageBox.Show("_dataContext.Logins is null");
                    return;
                }
                possibleLogins = new ObservableCollection<Login>(
                    await _dataContext.Logins
                        .Where(e => e.Username == Username)
                        .ToListAsync());

                foreach (var item in possibleLogins)
                {
                    if (item.PasswordHash.Equals(ComputeSHA256Hash(Password)))
                    {
                        MessageBox.Show("Yeee");
                        var player = _dataContext.Players.FirstOrDefault(e => e.LoginId == item.Id);
                        if (player == null)
                        {
                            player = new Player()
                            {
                                Name = Username,
                                LoginId = item.Id
                            };
                            _dataContext.Players.Add(player);
                            await _dataContext.SaveChangesAsync();
                        }
                        Login(player);
                    }
                    else
                    {
                        MessageBox.Show("Wrong Password");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login : An error occurred: {ex.Message}");
            }
        }
        public async void InitDB()
        {
            using (var db = new ExerciceMonsterContext())
            {
                db.Database.EnsureCreated();
                var possiblePoyo = new ObservableCollection<Login>(
                await _dataContext.Logins
                    .Where(e => e.Username == "poyo")
                    .ToListAsync());

                if (possiblePoyo.Count > 0)
                {
                    return;
                }
                else
                {
                    await db.Database.ExecuteSqlRawAsync("DELETE FROM PlayerMonster");
                    await db.Database.ExecuteSqlRawAsync("DELETE FROM MonsterSpell");

                    // Clear main tables
                    db.Monsters.RemoveRange(db.Monsters);
                    db.Spells.RemoveRange(db.Spells);


                    db.Logins.Add(new Login()
                    {
                        Username = "poyo",
                        PasswordHash = ComputeSHA256Hash("poyo")
                    });

                    await MonsterSet.InitMonsters(db);
                    await SpellSet.InitSpells(db);

                    // Save changes to apply deletions
                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
