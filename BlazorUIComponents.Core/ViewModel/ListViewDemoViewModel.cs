using BlazorUIComponents.Core.Model;
using BlazorUIComponents.Core.Service;
using BlazorUIComponents.Core.ViewModel.ListViewDemo;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace BlazorUIComponents.Core.ViewModel
{
    public class ListViewDemoViewModel : ViewModelBase
    {
        private readonly IDialogService dialogService;
        private readonly WeatherForecastService weatherForecastService;

        private SourceCache<SampleItem, string> sampleItemCache = new SourceCache<SampleItem, string>(x=>x.Id);
        private object selectedItem;

        public IObservableCollection<SampleItemViewModel> SampleItemViewModels { get; private set; } = new ObservableCollectionExtended<SampleItemViewModel>();

        public object SelectedItem { get => selectedItem; set => this.RaiseAndSetIfChanged(ref selectedItem, value); }

        public ReactiveCommand<Unit, Task> AddSampleItemCommand { get; }
        public ReactiveCommand<SampleItemViewModel, Task> SampleItemClickCommand { get; }

        

        public ListViewDemoViewModel() : base("ListView")
        {
            this.dialogService = Locator.Current.GetService<IDialogService>();
            this.weatherForecastService = Locator.Current.GetService<WeatherForecastService>();

            sampleItemCache.Connect()
                .Transform(item => new SampleItemViewModel(item))
                .Bind(SampleItemViewModels)
                .Subscribe();

            AddSampleItemCommand = ReactiveCommand.Create<Task>(async () =>
            {
                var result = await dialogService.ShowSingleInputModalAsync("Add Sample Item", "Give it a name.  Everything else will be done automatically.", "Display Name");
                if (result != null)
                {
                    var sampleItem = new SampleItem() { DisplayName = result, DateAdded = DateTimeOffset.Now, Id = Guid.NewGuid().ToString() };
                    sampleItemCache.AddOrUpdate(sampleItem);
                }
            });

            SampleItemClickCommand = ReactiveCommand.Create<SampleItemViewModel, Task>(item =>
            {
                Debug.WriteLine($"Sample Item clicked: {item.DisplayName}");
                return Task.CompletedTask;
            });

            Initialize();
        }

        private async void Initialize()
        {
            //var items = await weatherForecastService.GetForecastAsync(DateTime.Now);
            List<SampleItem> items = new List<SampleItem>();
            items.Add(new SampleItem() { Id = Guid.NewGuid().ToString(), DateAdded = DateTimeOffset.Now, DisplayName = "First" });
            items.Add(new SampleItem() { Id = Guid.NewGuid().ToString(), DateAdded = DateTimeOffset.Now, DisplayName = "Second" });
            items.Add(new SampleItem() { Id = Guid.NewGuid().ToString(), DateAdded = DateTimeOffset.Now, DisplayName = "Third" });
            sampleItemCache.AddOrUpdate(items);
        }
    }
}
