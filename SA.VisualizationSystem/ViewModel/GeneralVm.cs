using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SA.VisualizationSystem.ViewModel
{
    public class GeneralVm : ViewModelBase
    {
        public GeneralVm()
        {
            ListCollection = new ObservableCollection<Data>
            {
                new Data()
                {
                    Name = "sda",
                    Surname = "fe2wfew"
                },
                new Data()
                {
                    Name = "sda2",
                    Surname = "fe2wfew"
                }
            };
        }

        public ObservableCollection<Data> ListCollection { get; set; }
    }

    public class Data
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
