using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace trukmon.MVVM.ViewModel
{
    public class LoginVM : BaseVM
    {
        ExerciceMonsterContext _dataContext;
        ObservableCollection<Login> possibleLogins;

        public ICommand LoginRequest { get; set; }
        public ICommand LinkRequest { get; set; }

        public ICommand RegisterRequest { get; set; }
        public ICommand BypassRequest { get; set; }

        private string _serverName;
        public string ServerName
        {
            get => _serverName;
            set
            {
                if (_serverName != value)
                {
                    _serverName = value;
                    OnPropertyChanged(nameof(ServerName));
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

        private string _regusername = "jean";
        public string RegUsername
        {
            get => _regusername;
            set
            {
                if (_regusername != value)
                {
                    _regusername = value;
                    OnPropertyChanged(nameof(RegUsername));
                }
            }
        }

        private string _regpassword = "lassalle";
        public string RegPassword
        {
            get => _regpassword;
            set
            {
                if (_regpassword != value)
                {
                    _regpassword = value;
                    OnPropertyChanged(nameof(RegPassword));
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
            RegisterRequest = new RelayCommand(RegisterAttempt);
            BypassRequest = new RelayCommand(Bypass);
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
            string path = "../../../DB.txt";
            if (File.Exists(path))
            {
                File.WriteAllText(path, ServerName);
                InitDB();
            }
            else
            {
                MessageBox.Show("File not found");
            }
            _dataContext = new ExerciceMonsterContext();
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
            try
            {
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
                    await _dataContext.Database.ExecuteSqlRawAsync("DELETE FROM PlayerMonster");
                    await _dataContext.Database.ExecuteSqlRawAsync("DELETE FROM MonsterSpell");

                // Clear main tables
                _dataContext.Monsters.RemoveRange(_dataContext.Monsters);
                _dataContext.Spells.RemoveRange(_dataContext.Spells);


                _dataContext.Logins.Add(new Login()
                    {
                        Username = "poyo",
                        PasswordHash = ComputeSHA256Hash("poyo")
                    });

                    await MonsterSet.InitMonsters(_dataContext);
                    await SpellSet.InitSpells(_dataContext);

                    // Save changes to apply deletions
                    await _dataContext.SaveChangesAsync();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while initializing the database: {ex.Message} Try updating the server name");
            }
        }
        public async void RegisterAttempt()
        {
            if (_dataContext == null)
            {
                MessageBox.Show("_dataContext is null");
                return;
            }

            if (string.IsNullOrEmpty(RegUsername))
            {
                MessageBox.Show("Username is null or empty");
                return;
            }

            if (string.IsNullOrEmpty(RegPassword))
            {
                MessageBox.Show("Password is null or empty");
                return;
            }
            var notUsed = new ObservableCollection<Login>(
                await _dataContext.Logins
                    .Where(e => e.Username == RegUsername)
                    .ToListAsync()
                    );
            if (notUsed.Count > 0)
            {
                MessageBox.Show("Username already taken");
                return;
            }
            else
            {
                MessageBox.Show("Username available");
                await _dataContext.Logins.AddAsync(new Login()
                {
                    Username = RegUsername,
                    PasswordHash = ComputeSHA256Hash(RegPassword)
                });
            }

            _dataContext.SaveChanges();
        }

        public void Bypass()
        {
            Login(null);
        }
    }
}
