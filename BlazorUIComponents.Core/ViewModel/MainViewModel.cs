using BlazorUIComponents.Core.Service;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorUIComponents.Core.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        
        //private Lazy<CourseListViewModel> courseListViewModel;

        //private ReactiveObject selectedItem;
        //private List<ReactiveObject> menuItems = new List<ReactiveObject>
        //{
        //    Splat.Locator.Current.GetServiceExt<CourseListViewModel>(),
        //    Splat.Locator.Current.GetServiceExt<CheckInViewModel>()
        //};

        //public ReactiveObject SelectedItem { get => selectedItem; set => this.RaiseAndSetIfChanged(ref selectedItem, value); }

        //public List<ReactiveObject> MenuItems => menuItems;
        private ObservableAsPropertyHelper<bool> isEnabled;
        public bool IsEnabled => isEnabled.Value;

        public ListViewDemoViewModel ListViewDemoViewModel { get; }
        public CounterViewModel CounterViewModel { get; }
        public FetchDataViewModel FetchDataViewModel { get; }


        public ReactiveCommand<object, Task> NavLinkCommand { get; }

        public MainViewModel(INavigationService navigationService) : base("Main")
        {
            this.navigationService = navigationService;

            ListViewDemoViewModel = Splat.Locator.Current.GetServiceExt<ListViewDemoViewModel>();
            CounterViewModel = Splat.Locator.Current.GetServiceExt<CounterViewModel>();
            FetchDataViewModel = Splat.Locator.Current.GetServiceExt<FetchDataViewModel>();

            //this.WhenAnyObservable(x => x.authService.IsLoggedIn).ObserveOn(RxApp.MainThreadScheduler).ToProperty(this, x => x.IsEnabled, out isEnabled);

            NavLinkCommand = ReactiveCommand.Create<object, Task>((vm) =>
            {
                return navigationService.NavigateToAsync(vm);
            });

        }



    }
}
