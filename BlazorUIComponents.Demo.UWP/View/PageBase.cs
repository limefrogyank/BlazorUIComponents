using BlazorUIComponents.Core.ViewModel;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace BlazorUIComponents.Demo.UWP.View
{
    public abstract class PageBase<VM> : Page, IViewFor<VM>, INotifyPropertyChanged where VM : ViewModelBase
    {
        private VM viewModel;
        public VM ViewModel { get => viewModel; set { if (viewModel != value) { viewModel = value; RaisePropertyChanged(); } } }
        object IViewFor.ViewModel { get => ViewModel; set => ViewModel = (VM)value; }

        protected void RaisePropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
