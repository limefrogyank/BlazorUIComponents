using BlazorUIComponents.Core;
using BlazorUIComponents.Core.Service;
using BlazorUIComponents.Core.ViewModel;
using BlazorUIComponents.Demo.UWP.Service;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorUIComponents.Demo.UWP
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public AppBootstrapper()
        {
            //Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            Locator.CurrentMutable.Register<MainViewModel>();
            
            Locator.CurrentMutable.Register<ListViewDemoViewModel>();
            Locator.CurrentMutable.Register<CounterViewModel>();
            Locator.CurrentMutable.Register<FetchDataViewModel>();

            //Only for ViewModelViewHost usage
            //Locator.CurrentMutable.Register<IViewFor<LoginViewModel>>(() => new LoginView());


            Locator.CurrentMutable.RegisterConstant(new DialogService(), typeof(IDialogService));
            Locator.CurrentMutable.RegisterConstant(new NavigationService(), typeof(INavigationService));
            Locator.CurrentMutable.RegisterConstant(new WeatherForecastService(), typeof(WeatherForecastService));

            ////Not using routing with UWP
            //Router
            //    .NavigateAndReset
            //    .Execute(Locator.Current.GetService<CourseListViewModel>())
            //    .Subscribe();
        }

        public RoutingState Router { get; protected set; }


    }
}