using MyCashier.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCashier.MVVM.ViewModels
{
    public class UC_MainVM : ViewModelBase
    {
        private RelayCommand goToAuthotisationCmd = null!;
        public RelayCommand GoToAuthotisationCmd
        {
            get
            {
                return goToAuthotisationCmd ?? new RelayCommand
                    (obj => { Navigator.Navigate(new UC_AuthorisationVM()); });
            }
        }
    }
}
