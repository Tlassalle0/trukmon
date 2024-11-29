using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    public class SpellInfoVM : BaseVM
    {
        public ObservableCollection<Spell> Spells { get; set; }
        ExerciceMonsterContext _dataContext = new ExerciceMonsterContext();

        public SpellInfoVM()
        {
            var Spells = _dataContext.Spells.ToList();
        }
    }
}
