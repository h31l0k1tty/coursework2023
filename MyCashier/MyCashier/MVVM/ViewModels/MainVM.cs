using MyCashier.Services;
using System.Linq;

namespace MyCashier.MVVM.ViewModels
{
    public class MainVM : ViewModelBase
    { 
        public object CurrentViewModel //Поле, реплицирующее Navigator.CurrentView
        {
            get { return Navigator.CurrentViewModel; }
            set
            {
                OnPropertyChanged(nameof(this.CurrentViewModel)); //Оповещаем ContentControl об изменении CurrentViewModel
            }
        }

        void UpdateViewModel(object newViewModel) //Метод, принимающий новую ViewModel на замену предыдущей
        {
            CurrentViewModel = newViewModel;
        }

        public MainVM()
        {
            Navigator.NavigateEvent += UpdateViewModel; //Подписываемся на событие Navigator'а

            Navigator.Navigate(new UC_AuthorisationVM(Properties.Settings.Default.Login, Properties.Settings.Default.Password));
        }
    }
}
