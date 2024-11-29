using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trukmon.Model;

namespace trukmon.MVVM.ViewModel
{
    class RunPageVM : BaseVM
    {
        static public Action<BaseVM> StageChangeRequest;
        private BaseVM _currentVM;
        public BaseVM CurrentVM
        {
            get => _currentVM;
            set
            {
                SetProperty(ref _currentVM, value);
                OnPropertyChanged(nameof(CurrentVM));
            }
        }
        public RunPageVM()
        {
            MainWindowVM.OnRequestVMChange += StageChange;
            MainWindowVM.OnRequestVMChange?.Invoke(new LoginVM());
        }
        public void StageChange(BaseVM newVM)
        {
            CurrentVM = newVM;
        }
    }
}
