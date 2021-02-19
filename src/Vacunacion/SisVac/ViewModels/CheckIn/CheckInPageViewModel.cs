using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace SisVac.ViewModels.CheckIn
{
    public class CheckInPageViewModel : BindableBase
    {
        public CheckInPageViewModel()
        {
            NextCommand = new DelegateCommand(OnNextCommandExecute);
            BackCommand = new DelegateCommand(OnBackCommandExecute);
        }
        public int PositionView { get; set; }

        public ICommand NextCommand { get; }
        public ICommand BackCommand { get; }

        private void OnNextCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    PositionView = 1;
                    break;
                case 1:
                    PositionView = 2;
                    break;
                case 2:
                    PositionView = 3;
                    break;
                case 3:
                    PositionView = 4;
                    break;
                case 4:
                    break;
            }
        }

        private void OnBackCommandExecute()
        {
            switch (PositionView)
            {
                case 0:
                    break;
                case 1:
                    PositionView = 0;
                    break;
                case 2:
                    PositionView = 1;
                    break;
                case 3:
                    PositionView = 2;
                    break;
                case 4:
                    PositionView = 3;
                    break;
            }
        }
    }
}
