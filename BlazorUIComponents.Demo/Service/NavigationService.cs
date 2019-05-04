using BlazorUIComponents.Core;
using BlazorUIComponents.Core.Service;
using BlazorUIComponents.Core.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace BlazorUIComponents.Demo.Service
{
    public class NavigationService : INavigationService
    {
        private IJSRuntime jsRuntime;
        private IUriHelper uriHelper;

        private BehaviorSubject<object> navigated = new BehaviorSubject<object>(null);

        Dictionary<Type, Func<object, string>> pathGenerators = new Dictionary<Type, Func<object, string>>
        {
            //{ typeof(RollCallViewModel), (vm) => $"rollcall/{((RollCallViewModel)vm).RollCall.Id}" },
            //{ typeof(FetchDataViewModel), (vm) => $"course/{((fetchdata)vm).Course.Id}" },
            { typeof(FetchDataViewModel), (vm) => $"fetchdata" },
            { typeof(CounterViewModel), (vm) => $"counter" },
            { typeof(ListViewDemoViewModel), (vm) => $"listview" },
            { typeof(GroupedListViewDemoViewModel), (vm) => $"groupedlistview" }
        };

        string GeneratePath(object vm)
        {
            var func = pathGenerators[vm.GetType()];
            return func(vm);
        }
        bool VerifyPath(string path, object vm)
        {
            var func = pathGenerators[vm.GetType()];
            var genPath = func(vm);
            if (path == genPath)
                return true;
            else
                return false;
        }

        public NavigationService()
        {
            jsRuntime = Splat.Locator.Current.GetServiceExt<IJSRuntime>();
            uriHelper = Splat.Locator.Current.GetServiceExt<IUriHelper>();

            this.uriHelper.OnLocationChanged += UriHelper_OnLocationChanged;
        }

        bool calledNavigateTo = false;
        public IObservable<object> Navigated => navigated.AsObservable();

        Stack<object> BackStack = new Stack<object>();
        Stack<object> ForwardStack = new Stack<object>();
        public object CurrentViewModel { get; private set; }

        private void UriHelper_OnLocationChanged(object sender, string e)
        {
            if (calledNavigateTo)
            {
                Debug.WriteLine("navigated via link");

                calledNavigateTo = false;
                navigated.OnNext(CurrentViewModel);
            }
            else  //pressed back or forward (or maybe typed in url)
            {
                var relativeUri = e.Substring(uriHelper.GetBaseUri().Length);
                object vm = null;
                //check backstack
                if (BackStack.TryPeek(out vm))
                {
                    if (VerifyPath(relativeUri, vm))
                    {
                        Debug.WriteLine("Went back");
                        ForwardStack.Push(CurrentViewModel); //store current vm
                        vm = BackStack.Pop();
                        CurrentViewModel = vm;
                        navigated.OnNext(CurrentViewModel);
                        return;
                    }
                }
                //check forward stack
                if (ForwardStack.TryPeek(out vm))
                {
                    if (VerifyPath(relativeUri, vm))
                    {
                        Debug.WriteLine("Went forward");
                        BackStack.Push(CurrentViewModel); //store current vm
                        vm = ForwardStack.Pop();
                        CurrentViewModel = vm;
                        navigated.OnNext(CurrentViewModel);
                        return;
                    }
                }

            }
        }


        public Task GoBackAsync()
        {
            throw new NotImplementedException();
        }

        public Task NavigateToAsync(object viewModel)
        {
            calledNavigateTo = true;

            BackStack.Push(CurrentViewModel);
            ForwardStack.Clear();
            CurrentViewModel = viewModel;

            var path = GeneratePath(viewModel);
            uriHelper.NavigateTo(path);
            return Task.CompletedTask;
        }

        public void Initialize(object initializationItem)
        {
            throw new NotImplementedException();
        }


    }
}
