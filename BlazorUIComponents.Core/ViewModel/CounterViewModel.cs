using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BlazorUIComponents.Core.ViewModel
{
    public class CounterViewModel : ViewModelBase
    {
        private int count = 0;

        public int Count { get => count; set => this.RaiseAndSetIfChanged(ref count, value); }

        public ReactiveCommand<Unit, Task> IncrementCountCommand { get; }


        public CounterViewModel() : base("Counter")
        {

            IncrementCountCommand = ReactiveCommand.Create<Task>(() =>
            {
                Count += 1;
                return Task.CompletedTask;
            });

        }
    }
}
