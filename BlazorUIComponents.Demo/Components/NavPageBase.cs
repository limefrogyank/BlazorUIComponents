using BlazorUIComponents.Core;
using BlazorUIComponents.Core.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using Microsoft.JSInterop;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace BlazorUIComponents.Demo.Components
{
    public class NavPageBase<T> : ReactiveBase where T : class
    {
        protected T Vm;

        [Inject] IUriHelper uriHelper { get; set; }
        [Inject] IJSRuntime jSRuntime { get; set; }

        public NavPageBase()
        {


        }

        protected override Task OnInitAsync()
        {

            var navigationService = Locator.Current.GetServiceExt<INavigationService>();

            navigationService.Navigated
                .SkipWhile(vm => !(vm is T))
                .Take(1)
                .Subscribe((vm) =>
            {
                if (vm == null)
                {
                    // probably refreshed browser or navigated by typing url ... 
                    // Need to 
                    // EITHER navigate to home 
                    jSRuntime.InvokeAsync<object>("resetToBasePage", uriHelper.GetBaseUri());
                    return;
                    // OR create logic to parse url and create viewmodel and initialize from scratch... this is a lot of extra work for Blazor to get ViewModel first nav
                    // it's a shame because Blazor does this automatically...when you're using view first navigation.

                }
                Vm = (T)vm;
                Invoke(() => StateHasChanged());
            }, (onError) => Debug.WriteLine(onError.Message));

            return base.OnInitAsync();
        }
    }
}
