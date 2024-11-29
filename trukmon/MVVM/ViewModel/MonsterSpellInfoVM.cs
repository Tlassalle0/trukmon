using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trukmon.MVVM.ViewModel
{
    public class MonsterSpellInfoVM
    {
        public string MonsterName { get; set; }
        public List<string> Moves { get; set; } = new List<string>();
    }
}
