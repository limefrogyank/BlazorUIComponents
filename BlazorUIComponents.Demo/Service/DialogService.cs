using BlazorUIComponents.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUIComponents.Demo.Service
{
    public class DialogService : IDialogService
    {
        public Task HideModalAsync(Action hideModalAction)
        {
            hideModalAction.Invoke();
            return Task.CompletedTask;
        }

        public Task ShowModalAsync(Action showModalAction)
        {
            showModalAction.Invoke();
            return Task.CompletedTask;
        }
    }
}
