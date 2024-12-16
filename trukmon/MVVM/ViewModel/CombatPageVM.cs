using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    class CombatPageVM : BaseVM
    {
        public bool InFight = false;
        public List<Monster> monsters { get; set; }

        public List<Spell> spells { get; set; }

        private Monster currentMonster;
        private List<Spell> currentMonsterSpells;

        ExerciceMonsterContext db;


        public CombatPageVM()
        {
            db = new ExerciceMonsterContext();
            monsters=db.Monsters.ToList();
            spells = db.Spells.ToList();
        }

        public CombatPageVM(int idMonster)
        {
            db = new ExerciceMonsterContext();
            monsters = db.Monsters.ToList();
            spells = db.Spells.ToList();
            currentMonster = db.Monsters.Find(idMonster);
            currentMonsterSpells = currentMonster.Spells.ToList();
        }

    }
}
