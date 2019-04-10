using System;
using System.Threading.Tasks;

namespace BlazorUIComponents.Core.Service
{
    public interface INavigationService
    {
        Task NavigateToAsync(object viewModel);
        Task GoBackAsync();
        IObservable<object> Navigated { get; }
        object CurrentViewModel { get; }
    }
}