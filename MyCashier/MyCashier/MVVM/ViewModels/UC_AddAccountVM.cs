using Microsoft.EntityFrameworkCore;
using MyCashier.MVVM.Models;
using MyCashier.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCashier.MVVM.ViewModels
{
    public class UC_AddAccountVM : ViewModelBase
    {
        private char[] allowedCharacters = { '0','1','2','3','4','5','6','7','8','9' };

        private string accountName = null!;
        public string AccountName
        {
            get { return accountName; }
            set 
            { 
                accountName = value;
                OnPropertyChanged(nameof(AccountName));
            }
        }

        private string balance = null!;
        public string Balance
        {
            get { return balance; }
            set
            {
                foreach (char c in allowedCharacters) //Проверка, является ли введённый символ цифрой
                {
                    if (!String.IsNullOrEmpty(value)){
                        if (value.Last() == c)
                        {
                            balance = value;
                            OnPropertyChanged(nameof(Balance));
                            break;
                        }
                    } else { balance = ""; }
                }
            }
        }

        //private object selectedCurrency = null!;
        //public object SelectedCurrency
        //{
        //    get { return selectedCurrency; }
        //    set
        //    {
        //        if(selectedCurrency != value)
        //        {
        //            selectedCurrency = value;
        //            OnPropertyChanged(nameof(SelectedCurrency));
        //        }
        //    }
        //}


        private ObservableCollection<Currency> currencies = new(MyCashierDbContext.db.Currency.ToList());
        public ObservableCollection<Currency> Currencies
        {
            get { return currencies; }
            set
            {
                currencies = value;
                OnPropertyChanged(nameof(Currencies));
            }
        }


        //private RelayCommand addAccountCmd = null!;
        //public RelayCommand AddAccountCmd
        //{
        //    get
        //    {
        //        return addAccountCmd ?? new RelayCommand
        //            (obj =>
        //            {
        //                Account newAccount = new()
        //                {
        //                    id = default,
        //                    name = AccountName.Trim(),
        //                    balance = Convert.ToDecimal(Balance),
        //                    currencyId = SelectedCurrency.ToString().Trim(),
        //                    userId = CurrentUser.Id
        //                };
        //                MyCashierDbContext.db.Account.Add(newAccount);
        //                MyCashierDbContext.db.SaveChanges();
        //                Navigator.Navigate(new UC_JournalVM());
        //            }, 
        //            obj => !String.IsNullOrEmpty(AccountName) &&
        //                   !String.IsNullOrEmpty(Balance) &&
        //                   !String.IsNullOrEmpty(SelectedCurrency.ToString()));
        //    }
        //}

        private RelayCommand cancelCmd = null!;
        public RelayCommand CancelCmd
        {
            get 
            {
                return cancelCmd ?? new RelayCommand 
                    (obj => Navigator.Navigate(new UC_JournalVM())); 
            }
        }
    }
}
