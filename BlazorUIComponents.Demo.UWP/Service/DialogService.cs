using BlazorUIComponents.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorUIComponents.Demo.UWP.Service
{
    public class DialogService : IDialogService
    {      

        public Task<string> ShowSingleInputModalAsync(string title, string description, string inputHeader)
        {
            throw new NotImplementedException();
        }
    }
}
