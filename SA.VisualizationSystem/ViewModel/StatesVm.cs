using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using SA.VisualizationSystem.StatesReference;

namespace SA.VisualizationSystem.ViewModel
{
    public class StatesVm : ViewModelBase
    {
        public StatesVm()
        {
            ViewStatesList = new ObservableCollection<StateData>();
            Initialization();
        }
        private void SaveClickHandler()
        {
            try
            {
                var addStateList = ViewStatesList.Where(st => st.Id == 0).ToArray();
                var deleteStateList =
                    _prevStatesList.Where(st => ViewStatesList.All(v => st.Id != v.Id)).Select(st => st.Id).ToArray();
                var editStateList =
                    ViewStatesList.Where(st => st.Id != 0 && !st.StateEquals(_prevStatesList.First(p => p.Id == st.Id)))
                        .ToArray();

                _statesServiceClient = new StateServiceClient();
                _statesServiceClient.Open();
                _statesServiceClient.AddStates(addStateList);
                _statesServiceClient.DeleteStates(deleteStateList);
                _statesServiceClient.EditStates(editStateList);
                _statesServiceClient.Close();
                Initialization();
                MessageBox.Show("All changes were successfully accepted");
            }
            catch (Exception ex)
            {
                 MessageBox.Show(string.Format("Error : {0}",ex.Message));
            }
        }
        
        
        private void Initialization()
        {
            _statesServiceClient = new StateServiceClient();
            ViewStatesList.Clear();
            _statesServiceClient.Open();
            _prevStatesList = _statesServiceClient.GetStates().ToList();
            var states = _statesServiceClient.GetStates();
            foreach (var state in states)
            {
                ViewStatesList.Add(state);
            }
            _statesServiceClient.Close();
        }

        public ICommand SaveClick
        {
            get { return _saveClickCommand ?? (_saveClickCommand = new RelayCommand(SaveClickHandler)); }
        }

        public ObservableCollection<StateData> ViewStatesList { get; set; }


        private List<StateData> _prevStatesList;
        private RelayCommand _saveClickCommand;
        private StateServiceClient _statesServiceClient;
    }

    static class ExtensionClassStates
    {
        public static bool StateEquals(this StateData st1, StateData st2)
        {
            if (!st1.StatesNames.Equals(st2.StatesNames) || st1.CountryCurrencyUnit != st2.CountryCurrencyUnit ||
                    st1.CountryDevelopmentCoef != st2.CountryDevelopmentCoef || st1.NewsInfluenceCoef != st2.NewsInfluenceCoef ||
                    st1.LicensesExcises != st2.LicensesExcises)
                return false;
            return true;
        }
    }
}
