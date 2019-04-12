using BlazorUIComponents.Core.Service;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorUIComponents.Demo.Service
{
    public class DialogService : IDialogService
    {
        private Func<string, string, string, Task<string>> showModalFunc;

        public void Register(Func<string, string, string, Task<string>> showModalFunc)
        {
            this.showModalFunc = showModalFunc;
        }

        public async Task<string> ShowSingleInputModalAsync(string title, string description, string inputHeader)
        {
            var result = await showModalFunc.Invoke(title, description, inputHeader);
            return result;

        }
    }
}
