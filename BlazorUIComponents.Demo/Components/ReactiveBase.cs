using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUIComponents.Demo.Components
{
    public abstract class ReactiveBase : ComponentBase
    {
        public void RegisterViewModel(object viewModel)
        {
            if (viewModel is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)viewModel).PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Invoke(() => StateHasChanged());

        }
    }

    public abstract class ReactiveLayoutBase : LayoutComponentBase
    {
        public void RegisterViewModel(object viewModel)
        {
            if (viewModel is INotifyPropertyChanged)
            {
                ((INotifyPropertyChanged)viewModel).PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Invoke(() => StateHasChanged());

        }
    }
}
