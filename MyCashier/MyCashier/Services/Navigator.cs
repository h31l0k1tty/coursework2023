using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCashier.Services
{
    public static class Navigator
    {
        private static object currentViewModel = null!;
        public static object CurrentViewModel { get { return currentViewModel; } }

        public delegate void NavigateDelegate(object ViewModel);
        public static event NavigateDelegate? NavigateEvent;

        /* Navigate вызывает NavigateEvent,
            туда подцепляется новая ViewModel из метода MainVM.UpdateViewModel(),
                который в свою очередь был подписан на NavigateEvent в MainVM */
        public static void Navigate(object ViewModel)
        {
            currentViewModel = ViewModel;
            NavigateEvent?.Invoke(currentViewModel);
        }
    }
}
