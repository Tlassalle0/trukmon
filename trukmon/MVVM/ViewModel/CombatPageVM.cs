using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    class CombatPageVM
    {
        public bool InFight = false;
        public List<Monster> monsters { get; set; }
        public List<Monster> starters { get; set; }

        ExerciceMonsterContext _dataContext;

        public CombatPageVM()
        {
            _dataContext = new ExerciceMonsterContext();
            monsters = _dataContext.Monsters.ToList();
            starters = _dataContext.Monsters.Where(m=>m.Name=="Tartue" || m.Name=="Papaela").ToList();

        }
    }
}
