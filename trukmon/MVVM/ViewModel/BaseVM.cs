using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace trukmon.MVVM.ViewModel
{
    abstract public class BaseVM : ObservableObject
    {
        public virtual void OnShowVM() { }
        public virtual void OnHideVM() { }
    }
}
