using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using trukmon.Data;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    class SelectPageVM : BaseVM
    {
        public ObservableCollection<MonsterSpellInfoVM> Monsters { get; set; }
        private ExerciceMonsterContext db;
        private Player? Player;
        public ICommand MonsterButton { get; set; }
        public SelectPageVM(Player? player)
        {
            db = new ExerciceMonsterContext();
            LoadMonsters();
            MonsterButton = new RelayCommand<int>(MonsterButton_Click);
            Player = player;
        }

        private void LoadMonsters()
        {
            Monsters = new ObservableCollection<MonsterSpellInfoVM>(
                db.Monsters
                    .Select(monster => new MonsterSpellInfoVM
                    {
                        MonsterName = monster.Name,
                        Id = monster.Id,
                        Health = monster.Health.ToString()
                    }).ToList()
            );
        }
        public void MonsterButton_Click(int id)
        {
            StartRun(id);
        }

        public void StartRun(int id)
        {
            MainWindowVM.OnRequestVMChange(new FightPageVM(id, Player));
        }
    }
}
