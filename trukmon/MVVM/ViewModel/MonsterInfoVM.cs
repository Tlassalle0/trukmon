using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    internal class MonsterInfoVM
    {
        public ObservableCollection<MonsterSpellInfoVM> Monsters { get; set; }
        ExerciceMonsterContext _dataContext = new ExerciceMonsterContext();
        
    }
}
