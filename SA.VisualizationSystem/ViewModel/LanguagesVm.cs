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
using SA.VisualizationSystem.LanguagesReference;
using SA.VisualizationSystem.StatesReference;

namespace SA.VisualizationSystem.ViewModel
{
    public class LanguagesVm : ViewModelBase
    {
        public LanguagesVm()
        {
            ViewLanguagesList = new ObservableCollection<LanguageData>();
            Initialization();
        }

        private void SaveClickHandler()
        {
            try
            {
                var addLanguageList = ViewLanguagesList.Where(l => l.Id == 0).ToArray();
                var deleteLanguageList =
                    _prevLanguagesList.Where(l => ViewLanguagesList.All(v => l.Id != v.Id)).Select(l => l.Id).ToArray();
                var editLanguageList =
                    ViewLanguagesList.Where(l => l.Id != 0 && !l.LanguageEquals(_prevLanguagesList.First(p => p.Id == l.Id))).ToArray();

                _languageServiceClient = new LanguageServiceClient();
                _languageServiceClient.Open();
                _languageServiceClient.AddLanguages(addLanguageList);
                _languageServiceClient.DeleteLanguages(deleteLanguageList);
                _languageServiceClient.EditLanguages(editLanguageList);
                _languageServiceClient.Close();
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
            _languageServiceClient = new LanguageServiceClient();
            ViewLanguagesList.Clear();
            _languageServiceClient.Open();
            _prevLanguagesList = _languageServiceClient.GetLanguagesList().ToList();
            var languages = _languageServiceClient.GetLanguagesList();
            foreach (var language in languages)
            {
                ViewLanguagesList.Add(language);
            }
            _languageServiceClient.Close();
        }

        public ICommand SaveClick
        {
            get { return _saveClickCommand ?? (_saveClickCommand = new RelayCommand(SaveClickHandler)); }
        }

        public ObservableCollection<LanguageData> ViewLanguagesList { get; set; }

        private List<LanguageData> _prevLanguagesList;
        private RelayCommand _saveClickCommand;
        private LanguageServiceClient _languageServiceClient;
    }

    static class ExtensionClassLanguages
    {
        public static bool LanguageEquals(this LanguageData l1, LanguageData l2)
        {
            if (!l1.LanguageName.Equals(l2.LanguageName))
                return false;
            return true;
        }
    }
}
