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
using SA.VisualizationSystem.ActionsReference;
using SA.VisualizationSystem.BusinessesReference;
using SA.VisualizationSystem.RegionsReference;
using SA.VisualizationSystem.StatesReference;

namespace SA.VisualizationSystem.ViewModel
{
    class ActionsVm : ViewModelBase
    {
        public ActionsVm()
        {
            StatesNames = new ObservableCollection<string>();
            RegionsNames = new ObservableCollection<string>();
            BusinessesNames = new ObservableCollection<string>();
            ViewActionsList = new ObservableCollection<ActionData>();
            Initialization();
        }
        private void Initialization()
        {
            StatesNames.Clear();
            _statesServiceClient = new StateServiceClient();
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
                var addActionList = ViewActionsList.Where(a => a.Id == 0).ToList();
                addActionList.ForEach(a => a.BusinessId = _businessesList.Find(b=> b.BusinessesNames == CurrentBusinessName).Id); // add business id to each record
                var deleteActionList = _prevActionList.Where(pr => ViewActionsList.All(v => pr.Id != v.Id)).Select(a => a.Id).ToArray();
                var editActionsList = ViewActionsList.Where(a => a.Id != 0 && !a.ActionEquals(_prevActionList.First(p => p.Id == a.Id))).ToArray();

                _actionServiceClient = new ActionServiceClient();
                _actionServiceClient.Open();
                _actionServiceClient.AddActions(addActionList.ToArray());
                _actionServiceClient.DeleteActions(deleteActionList);
                _actionServiceClient.EditActions(editActionsList);
                _actionServiceClient.Close();
                Initialization();
                MessageBox.Show("All changes were successfully accepted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error : {0}", ex.Message));
            }
        }

        private void FindClickHandler()
        {
            if(CurrentBusinessName == null)
                return;
            ViewActionsList.Clear();
            _actionServiceClient = new ActionServiceClient();
            _actionServiceClient.Open();
            _prevActionList = _actionServiceClient.GetActions(_businessesList.First(b => b.BusinessesNames == CurrentBusinessName).Id).ToList();
            var actions = _actionServiceClient.GetActions(_businessesList.First(b => b.BusinessesNames == CurrentBusinessName).Id).ToList();
            foreach (var action in actions)
            {
                ViewActionsList.Add(action);
            }
            _actionServiceClient.Close();
        }

        private void CurrentStateChangedHandler()
        {
            if(CurrentStateName == null)
                return;
            RegionsNames.Clear();
            _regionServiceClient = new RegionServiceClient();
            _regionServiceClient.Open();
            _regionsList = _regionServiceClient.GetRegionsByLanguage(1,
                _statesList.Find(st => st.StatesNames == CurrentStateName).Id).ToList();
            if (_regionsList.Count == 0)
            {
                ViewActionsList.Clear();
                return;
            }
            foreach (var region in _regionsList)
            {
                RegionsNames.Add(region.RegionsNames);
            }
            CurrentRegionName = RegionsNames.First();
            _regionServiceClient.Close();
        }

        private void CurrentRegionChangedHandler()
        {
            if (CurrentRegionName == null)
                return;
            BusinessesNames.Clear();
            _businessServiceClient = new BusinessServiceClient();
            _businessServiceClient.Open();
            _businessesList = _businessServiceClient.GetBusinessesByLanguage(1,
                _regionsList.Find(rg => rg.RegionsNames == CurrentRegionName).Id).ToList();
            if (_businessesList.Count == 0)
            {
                ViewActionsList.Clear();
                return;
            }
            foreach (var business in _businessesList)
            {
                BusinessesNames.Add(business.BusinessesNames);
            }
            CurrentBusinessName = BusinessesNames.First();
            _businessServiceClient.Close();
        }


        public ICommand SaveClick
        {
            get { return _saveClickCommand ?? (_saveClickCommand = new RelayCommand(SaveClickHandler)); }
        }


        public static ObservableCollection<string> StatesNames { get; set; }

        public static ObservableCollection<string> RegionsNames { get; set; }

        public static ObservableCollection<string> BusinessesNames { get; set; }


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
                CurrentRegionChangedHandler();
                RaisePropertyChanged("CurrentRegionName");
            }
        }

        private string _currentBusinessName;
        public string CurrentBusinessName
        {
            get { return _currentBusinessName; }
            set
            {
                _currentBusinessName = value;
                FindClickHandler();
                RaisePropertyChanged("CurrentBusinessName");
            }
        }

        public ObservableCollection<ActionData> ViewActionsList { get; set; }



        private List<StateData> _statesList;
        private List<RegionData> _regionsList;
        private List<BusinessData> _businessesList;

        private List<ActionData> _prevActionList;

        private RelayCommand _saveClickCommand;

        private StateServiceClient _statesServiceClient;
        private RegionServiceClient _regionServiceClient;
        private BusinessServiceClient _businessServiceClient;
        private ActionServiceClient _actionServiceClient;
    }

    static class ExtensionClassActions
    {
        public static bool ActionEquals(this ActionData a1, ActionData a2)
        {
            if (!a1.Descriptions.Equals(a2.Descriptions))
                return false;
            return true;
        }
    }
}
