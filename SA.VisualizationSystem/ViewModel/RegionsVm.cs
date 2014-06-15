using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SA.VisualizationSystem.RegionsReference;
using SA.VisualizationSystem.StatesReference;

namespace SA.VisualizationSystem.ViewModel
{
    class RegionsVm : ViewModelBase
    {
        public RegionsVm()
        {
            ViewRegionsList = new ObservableCollection<RegionData>();
            StatesNames = new ObservableCollection<string>();
            Initialization();
            FindClickHandler();
        }
        private void Initialization()
        {
            StatesNames.Clear();
            _statesServiceClient = new StateServiceClient();
            _statesList = new List<StateData>();
            _statesServiceClient.Open();
            _statesList = _statesServiceClient.GetStatesByLanguage(1).ToList();
            foreach (var state in _statesList)
            {
                StatesNames.Add(state.StatesNames);
            }
            CurrentStateName = StatesNames.First();
            _statesServiceClient.Close();
        }
        private void SaveClickHandler()
        {
            try
            {
                var addRegionList = ViewRegionsList.Where(rg => rg.Id == 0).ToList();
                addRegionList.ForEach(rg => rg.StateId = _statesList.Find(st => st.StatesNames == CurrentStateName).Id); // add state id to each ereocrd
                var deleteRegionList =
                    _prevRegionsList.Where(rg => ViewRegionsList.All(v => rg.Id != v.Id)).Select(rg => rg.Id).ToArray();
                var editRegionList =
                    ViewRegionsList.Where(rg => rg.Id != 0 && !rg.StateEquals(_prevRegionsList.First(p => p.Id == rg.Id)))
                        .ToArray();

                _regionServiceClient = new RegionServiceClient();
                _regionServiceClient.Open();
                _regionServiceClient.AddRegions(addRegionList.ToArray());
                _regionServiceClient.DeleteRegions(deleteRegionList);
                _regionServiceClient.EditRegions(editRegionList);
                _regionServiceClient.Close();
                Initialization();
                MessageBox.Show("All changes were successfully accepted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Eroor : {0}", ex.Message));
            }
        }

        private void FindClickHandler()
        {
            if(CurrentStateName == null)
                return;
            ViewRegionsList.Clear();
            _regionServiceClient = new RegionServiceClient();
            _regionServiceClient.Open();
            _prevRegionsList = _regionServiceClient.GetRegions(_statesList.First(st=>st.StatesNames == CurrentStateName).Id).ToList();
            var regions = _regionServiceClient.GetRegions(_statesList.First(st => st.StatesNames == CurrentStateName).Id).ToList();
            foreach (var region in regions)
            {
                ViewRegionsList.Add(region);
            }
            _statesServiceClient.Close();
        }




        public ICommand SaveClick
        {
            get { return _saveClickCommand ?? (_saveClickCommand = new RelayCommand(SaveClickHandler)); }
        }

        public static ObservableCollection<string> StatesNames { get; set; }

        private string _currentStateName;
        public string CurrentStateName {
            get { return _currentStateName; }
            set
            {
                _currentStateName = value;
                FindClickHandler();
                RaisePropertyChanged("CurrentStateName");
            }
        }

        public ObservableCollection<RegionData> ViewRegionsList { get; set; }

        private List<StateData> _statesList;
        private List<RegionData> _prevRegionsList;
        private RelayCommand _saveClickCommand;
        private StateServiceClient _statesServiceClient;
        private RegionServiceClient _regionServiceClient;

    }

    static class ExtensionClassRegions
    {
        public static bool StateEquals(this RegionData rg1, RegionData rg2)
        {
            if (!rg1.RegionsNames.Equals(rg2.RegionsNames) || rg1.ProfitTax != rg2.ProfitTax ||
                    rg1.GrossProfitTax != rg2.GrossProfitTax || rg1.Industry != rg2.Industry ||
                    rg1.Cx != rg2.Cx || rg1.ServicesSector != rg2.ServicesSector || rg1.Trade != rg2.Trade || rg1.Tourism != rg2.Tourism)
                return false;
            return true;
        }
    }
}
