using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BlazorUIComponents.Core.ViewModel
{
    public class CommandBarDemoViewModel:ReactiveObject
    {
        private string message;

        public ReactiveCommand<string,Task> AnyCommand { get; }
        public string Message { get => message; set => this.RaiseAndSetIfChanged(ref message, value); }

        public CommandBarDemoViewModel()
        {
            AnyCommand = ReactiveCommand.Create< string, Task>( (param) =>
            {
                Message = param;
                return Task.CompletedTask;
            });
        }
    }
}
