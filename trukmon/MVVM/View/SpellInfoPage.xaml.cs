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
    /// Logique d'interaction pour SpellInfoPage.xaml
    /// </summary>
    public partial class SpellInfoPage : UserControl
    {

        public ObservableCollection<Spell> Spells { get; set; }

        private ExerciceMonsterContext _dataContext;

        public SpellInfoPage()
        {
            InitializeComponent();
            _dataContext = new ExerciceMonsterContext();
            LoadSpells();
            DataContext = this; // Set DataContext
        }

        public void LoadSpells()
        {
            Spells = new ObservableCollection<Spell>(
                _dataContext.Spells
                    .Select(spell => new Spell
                    {
                        Name = spell.Name,
                        Description = spell.Description,
                        Damage = spell.Damage,
                    }).ToList()
                );
        }
    }
}
