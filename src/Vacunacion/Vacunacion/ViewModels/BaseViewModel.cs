using System;
using PropertyChanged;
namespace Vacunacion.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel
    {
        public string Title { get; set; }

        public bool IsBusy { get; set; }
    }
}
