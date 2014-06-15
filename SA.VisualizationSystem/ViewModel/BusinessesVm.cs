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
using SA.VisualizationSystem.BusinessesReference;
using SA.VisualizationSystem.RegionsReference;
using SA.VisualizationSystem.StatesReference;
using Language = SA.VisualizationSystem.StatesReference.Language;

namespace SA.VisualizationSystem.ViewModel
{
    class BusinessesVm : ViewModelBase
    {
        public BusinessesVm()
        {
            StatesNames = new ObservableCollection<string>();
            RegionsNames = new ObservableCollection<string>();
            ViewBusinessList = new ObservableCollection<BusinessData>();
            Initialization();
        }
        private void Initialization()
        {
            StatesNames.Clear();
            _statesServiceClient = new StateServiceClient();
            _statesServiceClient.Open();
            _statesList = _statesServiceClient.GetStatesByLanguage(Language.English).ToList();
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
                var addBusinessList = ViewBusinessList.Where(rg => rg.Id == 0).ToList();
                addBusinessList.ForEach(b => b.RegionId = _regionsList.Find(rg => rg.RegionsNames == CurrentRegionName).Id); // add state id to each ereocrd
                var deleteBusinessList =
                    _prevBusinessList.Where(pr => ViewBusinessList.All(v => pr.Id != v.Id)).Select(b => b.Id).ToArray();
                var editBusinessList =
                    ViewBusinessList.Where(b => b.Id != 0 && !b.BusinessEquals(_prevBusinessList.First(p => p.Id == b.Id)))
                        .ToArray();

                _businessServiceClient = new BusinessServiceClient();
                _businessServiceClient.Open();
                _businessServiceClient.AddBusinesses(addBusinessList.ToArray());
                _businessServiceClient.DeleteBusinesses(deleteBusinessList);
                _businessServiceClient.EditBusinesses(editBusinessList);
                _businessServiceClient.Close();
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
            if(CurrentRegionName == null)
                return;
            ViewBusinessList.Clear();
            _businessServiceClient = new BusinessServiceClient();
            _businessServiceClient.Open();
            _prevBusinessList = _businessServiceClient.GetBusinesses(_regionsList.First(rg => rg.RegionsNames == CurrentRegionName).Id).ToList();
            var businesses = _businessServiceClient.GetBusinesses(_regionsList.First(rg => rg.RegionsNames == CurrentRegionName).Id).ToList();
            foreach (var business in businesses)
            {
                ViewBusinessList.Add(business);
            }
            _statesServiceClient.Close();
        }

        private void CurrentStateChangedHandler()
        {
            RegionsNames.Clear();
            _regionServiceClient = new RegionServiceClient();
            _regionServiceClient.Open();
            _regionsList = _regionServiceClient.GetRegionsByLanguage(RegionsReference.Language.English,
                _statesList.Find(st => st.StatesNames == CurrentStateName).Id).ToList();
            if (_regionsList.Count == 0)
            {
                ViewBusinessList.Clear();
                return;
            }
            foreach (var region in _regionsList)
            {
                RegionsNames.Add(region.RegionsNames);
            }
            CurrentRegionName = RegionsNames.First();
            _regionServiceClient.Close();
        }


        public ICommand SaveClick
        {
            get { return _saveClickCommand ?? (_saveClickCommand = new RelayCommand(SaveClickHandler)); }
        }


        public static ObservableCollection<string> StatesNames { get; set; }

        public static ObservableCollection<string> RegionsNames { get; set; }


        private string _currentStateName;
        public string CurrentStateName {
            get
            {
                return _currentStateName;
            }
            set
            {
                _currentStateName = value;
                CurrentStateChangedHandler();
                RaisePropertyChanged("CurrentStateName");
            }
        }

        private string _currentRegionName;
        public string CurrentRegionName {
            get { return _currentRegionName; }
            set
            {
                _currentRegionName = value;
                FindClickHandler();
                RaisePropertyChanged("CurrentRegionName");
            }
        }

        public ObservableCollection<BusinessData> ViewBusinessList { get; set; }



        private List<StateData> _statesList;
        private List<RegionData> _regionsList;

        private List<BusinessData> _prevBusinessList;

        private RelayCommand _saveClickCommand;

        private StateServiceClient _statesServiceClient;
        private RegionServiceClient _regionServiceClient;
        private BusinessServiceClient _businessServiceClient;

    }

    static class ExtensionClassBusinesses
    {
        public static bool BusinessEquals(this BusinessData b1, BusinessData b2)
        {
            if (!b1.BusinessesNames.Equals(b2.BusinessesNames) || !b1.Descriptions.Equals(b2.Descriptions) ||!b1.Addresses.Equals(b2.Addresses))
                return false;
            return true;
        }
    }
}
