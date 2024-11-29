using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using trukmon.Model;

namespace trukmon.Data
{
    public class MonsterSet
    {
        public async static Task AddMonsterAsync(string name, int health, ExerciceMonsterContext dataContext)
        {
            try
            {
                Monster newMonster = new Monster
                {
                    Name = name,
                    Health = health,
                };

                dataContext.Monsters.Add(newMonster);
                await dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the monster: {ex.Message}");
            }
        }

        public async static Task InitMonsters(ExerciceMonsterContext context)
        {
            await MonsterSet.AddMonsterAsync("Tartue", 100, context);
            await MonsterSet.AddMonsterAsync("Tartouflette", 120, context);
            await MonsterSet.AddMonsterAsync("Papaela", 90, context);
            await MonsterSet.AddMonsterAsync("Papapala", 130, context);
        }
    }
}
