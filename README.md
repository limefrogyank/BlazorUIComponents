# BlazorUIComponents
At attempt to make Blazor components that mimic UWP/Xamarin controls, follow MVVM (ViewModel nav) guidelines, and use the ReactiveUI framework.

Blazor client-side using WASM will NOT work with ReactiveUI due to Mono's port for Web Assembly being single-threaded.  For now, you can only use Razor Components (server-side Blazor using SignalR) with ReactiveUI.

# Things to consider
1. Navigation is half-finished.  Refreshes and direct links will not work.  They just make the app go back to the first page (or crash in my live demo).  But everything else works.  
2. Since I am trying to do Viewmodel-based navigation, using links (\<a href=''\>\</a\>) are not going to work.  I couldn't use Bootstrap's nice navigation bar since it depends on those links.  I had to use buttons instead.  
3. Do NOT use AspNetCore dependency injection for your services and viewmodels.  You will likely need access to IJSRuntime and IUriHelper and if you try to pull those into your viewmodels using dependency-injection from AspNetCore's default container, you'll get the server-side version of each.  And the IJSRuntime won't work.  You want the remote or client version.  So put all your registrations for Splat into the App.razor file.  The client-side has the correct IJSRuntime implementation.

# Dependencies
1.  ReactiveUI - a hacked version that allows UI stuff in a plain .NET Standard project.  (Don't use it for regular platforms.)
2.  DotNetCore v3.0.0-preview3  (required for Razor Components (aka Server-side Blazor)
3.  PreRenderComponent (available via NuGet or https://github.com/SQL-MisterMagoo/PreRenderComponent) - this detects when you're currently prerendering on the server so you don't try to make UI specific calls.  Only required for the initial page load.


# To use
1.  All of your pages that require navigation should inherit `NavPageBase<>` with the viewmodel for the page as the generic parameter.
2.  The `NavigationService` requires a mapping from ViewModel to the path that Blazor needs to find the correct view.
3.  For views that don't require navigation (they are part of a larger page), you can inherit `ReactiveBase` instead and manually call `RegisterViewModel(Vm)` after you pull in the corrent viewmodel from Splat manually.  Sorry, no `ViewModelViewHost` yet.  Something like this:
```
    private LoginViewModel Vm;

    protected override async Task OnInitAsync()
    {
        Vm = Locator.Current.GetService<LoginViewModel>();
        this.RegisterViewModel(Vm);
        await base.OnInitAsync();
    }
```
As you can see, properties are automatically updated on the view with a `StateHasChanged` call.  You can use `DynamicData` for your lists. 

BTW, this included version of ReactiveUI is hacked and taken directly from here:
https://github.com/Nethereum/ReactiveUI/tree/7ec6ef58dd469d90bce6ead32504630d15f03bc5
Do NOT use it for UWP/Android/iOS/Xamarin.... it's only for Blazor that is technically dotnet framework, but still requires all of the UI stuff.

(Sorry, I suck at git and had to remove all the bindings.)
