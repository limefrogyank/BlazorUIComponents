using BlazorUIComponents.Core.Service;
using BlazorUIComponents.Core.ViewModel;
using BlazorUIComponents.Demo.UWP.View;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace BlazorUIComponents.Demo.UWP.Service
{
    public class NavigationService : INavigationService
    {
        private readonly Subject<object> navigated = new Subject<object>();
        Frame navFrame;

        Dictionary<Type, Type> viewKeys = new Dictionary<Type, Type>
        {
            { typeof(ListViewDemoViewModel), typeof(ListViewDemoView) },
             { typeof(CounterViewModel), typeof(CounterView) },
              { typeof(FetchDataViewModel), typeof(FetchDataView) }
        };

        public NavigationService()
        {
            var systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.BackRequested += NavigateBackHandler;
        }

        public void Initialize(object initializationItem)
        {
            if (navFrame == null)
                navFrame = (Frame)initializationItem;
        }

        private void NavigateBackHandler(object sender, BackRequestedEventArgs e)
        {
            if (navFrame?.CanGoBack != true) return;
            var instance = navFrame.BackStack.Last().Parameter;

            var oldpage = (Page)navFrame.Content ?? throw new NullReferenceException();
            if (oldpage.DataContext is IDisposable)
                (oldpage.DataContext as IDisposable).Dispose();
            oldpage.DataContext = null;
            navFrame.GoBack();
            navFrame.ForwardStack.Clear();
            e.Handled = true;

            var page = (Page)navFrame.Content ?? throw new NullReferenceException();
            var type = instance.GetType();

            if (page is IViewFor)
            {
                ((IViewFor)page).ViewModel = instance;
            }
            page.DataContext = instance;
            RaiseNavigated(instance);
        }


        public IObservable<object> Navigated => navigated.AsObservable();

        public object CurrentViewModel { get; private set; }

        public Task GoBackAsync()
        {
            if (navFrame?.CanGoBack != true) return Task.CompletedTask;
            var instance = navFrame.BackStack.Last().Parameter;
            navFrame.GoBack();
            navFrame.ForwardStack.Clear();

            var page = (Page)navFrame.Content ?? throw new NullReferenceException();
            var type = instance.GetType();
            if (page is IViewFor)
                ((IViewFor)page).ViewModel = instance;
            page.DataContext = instance;
            RaiseNavigated(instance);

            return Task.CompletedTask;
        }

        public Task NavigateToAsync(object viewModel)
        {
            var viewModelType = viewModel.GetType();

            navFrame.Navigate(viewKeys[viewModelType], viewModel, new DrillInNavigationTransitionInfo());

            var page = (Page)navFrame.Content ?? throw new NullReferenceException();

            if (page is IViewFor)
                ((IViewFor)page).ViewModel = viewModel;
            page.DataContext = viewModel;

            RaiseNavigated(viewModel);

            return Task.CompletedTask;
        }

        private void RaiseNavigated(object viewModel)
        {
            var systemNavigationManager = SystemNavigationManager.GetForCurrentView();
            systemNavigationManager.AppViewBackButtonVisibility = navFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

            CurrentViewModel = viewModel;
            navigated.OnNext(viewModel);
        }


    }
}
