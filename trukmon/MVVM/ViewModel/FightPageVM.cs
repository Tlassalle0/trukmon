using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using trukmon.Data;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    public class FightPageVM : BaseVM
    {
        private DispatcherTimer _moveDelayTimer;
        private bool _isUserMoveLocked;
        private bool _isOpponentMoveInProgress;
        private Player? Player;
        private ExerciceMonsterContext db = new ExerciceMonsterContext();
        public FightEntity PlayerMonster { get; set; }

        private FightEntity _opponentMonster;
        public FightEntity OpponentMonster
        {
            get { return _opponentMonster; }
            set
            {
                if (_opponentMonster != value)
                {
                    _opponentMonster = value;
                    OnPropertyChanged(nameof(OpponentMonster));
                }
            }
        }
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }


        private string _logs = "";
        public string Logs
        {
            get => _logs;
            set
            {
                if (_logs != value)
                {
                    _logs = value;
                    OnPropertyChanged(nameof(Logs));
                }
            }
        }

        public ICommand CastSpellCommand { get; set; }

        public float HealthBoost = 1;
        public float DamageBoost = 1;

        public int MonsterCount;


        public Random random = new Random();

        public FightPageVM(int id, Player? player)
        {
            var Model = db.Monsters
              .Include(m => m.Spells) // Inclut explicitement la relation
              .FirstOrDefault(m => m.Id == id);
            PlayerMonster = new FightEntity
            {
                Id = Model.Id,
                Name = Model.Name,
                Health = Model.Health,
                MaxHealth = Model.Health,
                Spells = Model.Spells.Take(4).ToList()
            };

            MonsterCount = db.Monsters.Count();
            SetEnemy();
            CastSpellCommand = new RelayCommand<object>(CastSpell);
            Player = player;

            _moveDelayTimer = new DispatcherTimer();
            _moveDelayTimer.Interval = TimeSpan.FromSeconds(1); // Delay for 1 seconds (you can adjust this)
            _moveDelayTimer.Tick += MoveDelayTimer_Tick; // Handle the timer tick
            _isUserMoveLocked = false;
            _isOpponentMoveInProgress = false;

        }
        public void SetEnemy()
        {
            int opponentId = random.Next(1, MonsterCount + 1);
            var Model = db.Monsters
              .Include(m => m.Spells) // Include Spells
              .FirstOrDefault(m => m.Id == opponentId);

            if (Model != null)
            {
                OpponentMonster = new FightEntity
                {
                    Id = Model.Id,
                    Name = Model.Name,
                    Health = (int)(Model.Health * HealthBoost),  // HealthBoost applied here
                    MaxHealth = (int)(Model.Health * HealthBoost),
                    Spells = Model.Spells.ToList()  // Including the list of spells for the enemy
                };
            }

            _isUserMoveLocked = false;
        }

        public void CastSpell(object param)
        {
            if (_isUserMoveLocked)
            {
                return;
            }
            if (param is Spell spell)
            {
                
                OpponentMonster.Health -= spell.Damage;
                Logs = $"{PlayerMonster.Name} cast {spell.Name} dealing {spell.Damage} damage to {OpponentMonster.Name}\n"+Logs;

                _isUserMoveLocked = true;
                if (OpponentMonster.Health <= 0)
                {
                    MessageBox.Show("You win!");
                    if (random.Next(0, 2) == 0)
                    {
                        HealthBoost += 0.05f;
                    }
                    else
                    {
                        DamageBoost += 0.05f;
                    }
                    Logs= $"{PlayerMonster.Name} deafeated {OpponentMonster.Name}\n" + Logs;
                    SetEnemy();
                    PlayerMonster.Health = PlayerMonster.MaxHealth;
                    Score++;
                }
                else
                {
                    _moveDelayTimer.Start();
                }
            }
        }
        private void MoveDelayTimer_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            _moveDelayTimer.Stop();
            _isOpponentMoveInProgress = true;

            // Randomly select an opponent spell and perform their move
            int spellIndex = random.Next(0, OpponentMonster.Spells.Count);
            var spell = OpponentMonster.Spells[spellIndex];
            var damage = (int)(spell.Damage * DamageBoost);
            PlayerMonster.Health -= damage;

            // Log the opponent's move
            Logs = $"{OpponentMonster.Name} cast {spell.Name} dealing {damage} damage to {PlayerMonster.Name}\n" + Logs;

            // Check if the player is defeated
            if (PlayerMonster.Health <= 0)
            {
                MessageBox.Show("You lose!");
                MainWindowVM.OnRequestVMChange(new HomePageVM(Player));
            }

            // Enable the user to select their next move
            _isUserMoveLocked = false;
        }
    }
}
