using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    class SelectPageVM : BaseVM
    {
        public ExerciceMonsterContext db;
        public ICommand MonsterButton { get; set; }
        public SelectPageVM()
        {
            db = new ExerciceMonsterContext();
        }

        public void MonsterButton_Click()
        {
            MessageBox.Show("MonsterButton_Click");
        }
    }
}
