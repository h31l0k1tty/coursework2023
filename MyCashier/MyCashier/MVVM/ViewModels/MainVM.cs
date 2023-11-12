using MyCashier.Services;
using System.Linq;
using System.Windows;

namespace MyCashier.MVVM.ViewModels
{
    public class MainVM : ViewModelBase
    {
        public MainVM()
        {
            Navigator.NavigateEvent += UpdateViewModel; //Подписываемся на событие Navigator'а
            Navigator.Navigate(new UC_AuthorisationVM(Properties.Settings.Default.Login, Properties.Settings.Default.Password));
        }

        private Visibility areButtonsVisible = Visibility.Hidden;
        public Visibility AreButtonsVisible
        {
            get { return areButtonsVisible; }
            set
            {
                areButtonsVisible = value;
                OnPropertyChanged(nameof(AreButtonsVisible));
            }
        }

        public object CurrentViewModel //Поле, реплицирующее Navigator.CurrentView
        {
            get { return Navigator.CurrentViewModel; }
            set
            {
                OnPropertyChanged(nameof(this.CurrentViewModel)); //Оповещаем ContentControl об изменении CurrentViewModel

                if(CurrentViewModel.GetType() == typeof(UC_JournalVM) || CurrentViewModel.GetType() == typeof(UC_DebtsVM))
                    AreButtonsVisible = Visibility.Visible;
                else
                    AreButtonsVisible = Visibility.Hidden;
            }
        }



        void UpdateViewModel(object newViewModel) //Метод, принимающий новую ViewModel на замену предыдущей
        {
            CurrentViewModel = newViewModel;
        }


        private RelayCommand switchToJournal = null!;
        public RelayCommand SwitchToJournal
        {
            get
            {
                return switchToJournal ?? new RelayCommand
                    (obj => { Navigator.Navigate(new UC_JournalVM()); });
            }
        }

        private RelayCommand switchToDebts = null!;
        public RelayCommand SwitchToDebts
        {
            get
            {
                return switchToDebts ?? new RelayCommand
                    (obj => { Navigator.Navigate(new UC_DebtsVM()); });
            }
        }
    }
}
