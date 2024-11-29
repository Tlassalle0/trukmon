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
    /// Logique d'interaction pour MonsterInfoPage.xaml
    /// </summary>
    public partial class MonsterInfoPage : UserControl
    {
        public ObservableCollection<MonsterSpellInfoVM> Monsters { get; set; }

        private ExerciceMonsterContext _dataContext;


        public MonsterInfoPage()
        {
            InitializeComponent(); // Ensure components are initialized first
            _dataContext = new ExerciceMonsterContext(); // Initialize context
            LoadMonsters(); // Load data after UI is ready
            DataContext = this; // Set DataContext
        }

        private void LoadMonsters()
        {
            Monsters = new ObservableCollection<MonsterSpellInfoVM>(
                _dataContext.Monsters
                    .Select(monster => new MonsterSpellInfoVM
                    {
                        MonsterName = monster.Name,
                        Moves = monster.Spells.Select(spell => spell.Name).Take(4).ToList() // Limit to 4 moves
                    }).ToList()
            );
        }
    }
}
