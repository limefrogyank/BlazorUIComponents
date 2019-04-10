using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorUIComponents.Core.ViewModel
{
    public class ViewModelBase : ReactiveObject, IRoutableViewModel, ISupportsActivation
    {
        public ViewModelActivator Activator { get; } = new ViewModelActivator();

        public string UrlPathSegment { get; }
        public string Icon { get; }

        public IScreen HostScreen { get; }

        public ViewModelBase(string title, string icon = "")
        {
            UrlPathSegment = title;
            Icon = icon;
            HostScreen = Locator.Current.GetService<IScreen>();
        }
    }
}
