using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SA.VisualizationSystem.Services;

namespace SA.VisualizationSystem.ViewModel
{
    public class MainVm : ViewModelBase
    {
        public MainVm()
        {
        }

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }

        
        public ICommand MenuClick
        {
            get { return _viewMenuCommand ?? (_viewMenuCommand = new RelayCommand<object>(MenuViewControl)); }
        }

        private void MenuViewControl(object t)
        {
            var menuStr = t as string;

            if (menuStr.Equals("GeneralView", StringComparison.InvariantCulture))
                CurrentView = IoC.Resolve<GeneralVm>();
        }

        private object _currentView;
        private RelayCommand<object> _viewMenuCommand;
    }
}
