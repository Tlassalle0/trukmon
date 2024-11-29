using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using trukmon.Model;

namespace trukmon.Data
{
    public class SpellSet
    {
        public async static Task AddSpellAsync(string name, int damage, string desc, ExerciceMonsterContext dataContext)
        {
            try
            {
                Spell newSpell = new Spell
                {
                    Name = name,
                    Damage = damage,
                    Description = desc
                };

                dataContext.Spells.Add(newSpell);
                await dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the spell: {ex.Message}");
            }
        }

        public async static Task InitSpells(ExerciceMonsterContext db)
        {
            try
            {
                if (db.Spells.Count() == 0)
                {
                    await AddSpellAsync("Lazyness", 1, "A lazy slap", db);
                    await AddSpellAsync("Fireball", 10, "A ball of fire", db);
                    await AddSpellAsync("Ice Shard", 5, "A shard of ice", db);
                    await AddSpellAsync("Lightning Bolt", 15, "A bolt of lightning", db);
                    await AddSpellAsync("Earthquake", 15, "Shakes the earth", db);
                    await AddSpellAsync("Solar grasp", 20, "Seize the sol", db);
                    await AddSpellAsync("Tsunami", 20, "A big wave", db);
                    await AddSpellAsync("Hostile Takeover", 15, "A wave of berserk gas", db);
                    await AddSpellAsync("Sword Dance", 10, "A dance of swords", db);
                    await AddSpellAsync("Banane à l'ananas", 5, "A fruit ? maybe 2 fruits ?", db);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while initializing the spells: {ex.Message}");
            }

            // Get all monsters in the DB
            var monsters = db.Monsters.ToList();
            foreach (var monster in monsters)
            {
               switch (monster.Name){
                    case "Tartue":
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Ice Shard").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Lazyness").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Sword Dance").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Fireball").FirstOrDefault());
                        break;
                    case "Tartouflette":
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Tsunami").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Solar Grasp").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Sword Dance").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Fireball").FirstOrDefault());
                        break;
                    case "Papaela":
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Banane à l'ananas").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Hostile Takeover").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Sword Dance").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Fireball").FirstOrDefault());
                        break;
                    case "Papapala":
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Earthquake").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Lightning Bolt").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Sword Dance").FirstOrDefault());
                        monster.Spells.Add(db.Spells.Where(s => s.Name == "Fireball").FirstOrDefault());
                        break;
                }
            }
        }
    }
}
