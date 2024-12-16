using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using trukmon.Model;
using trukmon.MVVM.ViewModel;

namespace trukmon.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour SelectPage.xaml
    /// </summary>
    public partial class SelectPage : UserControl
    {
        public ObservableCollection<MonsterSpellInfoVM> Monsters { get; set; }
        private ExerciceMonsterContext _dataContext;
        public SelectPage()
        {
            InitializeComponent();
            _dataContext = new ExerciceMonsterContext();
            LoadMonsters();
            DataContext = this;
        }
        private void LoadMonsters()
        {
            Monsters = new ObservableCollection<MonsterSpellInfoVM>(
                _dataContext.Monsters
                    .Select(monster => new MonsterSpellInfoVM
                    {
                        MonsterName = monster.Name,
                        Health = monster.Health.ToString()
                    }).ToList()
            );
        }

        public void StartRun(int id)
        {
            MainWindowVM.OnRequestVMChange(new CombatPageVM(id));
        }
    }
}
