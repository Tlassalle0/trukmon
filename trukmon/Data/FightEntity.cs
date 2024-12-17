using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trukmon.Model;

namespace trukmon.Data
{
    public class FightEntity : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private int health;
        private int maxHealth;
        private List<Spell> spells;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public int Health
        {
            get => health;
            set { health = value; OnPropertyChanged(nameof(Health)); }
        }

        public int MaxHealth
        {
            get => maxHealth;
            set { maxHealth = value; OnPropertyChanged(nameof(MaxHealth)); }
        }

        public List<Spell> Spells
        {
            get => spells;
            set { spells = value; OnPropertyChanged(nameof(Spells)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
