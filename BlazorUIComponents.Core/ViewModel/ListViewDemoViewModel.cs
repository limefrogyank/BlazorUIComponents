using BlazorUIComponents.Core.Model;
using BlazorUIComponents.Core.Service;
using BlazorUIComponents.Core.ViewModel.ListViewDemo;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace BlazorUIComponents.Core.ViewModel
{
    public class ListViewDemoViewModel : ViewModelBase
    {
        private readonly WeatherForecastService weatherForecastService;

        private SourceList<WeatherForecast> weatherItemList = new SourceList<WeatherForecast>();
        
        public IObservableCollection<WeatherItemViewModel> WeatherItemViewModels { get; private set; } = new ObservableCollectionExtended<WeatherItemViewModel>();

        public ReactiveCommand<Action, Task> AddWeatherItemCommand { get; }
        public ReactiveCommand<Action<WeatherItemViewModel>, Task> ShowWeatherDialogCommand { get; }
        public ReactiveCommand<WeatherItemViewModel, Task> WeatherItemClickCommand { get; }

        

        public ListViewDemoViewModel(WeatherForecastService weatherForecastService) : base("ListView")
        {
            this.weatherForecastService = weatherForecastService;

            weatherItemList.Connect()
                .Transform(item => new WeatherItemViewModel(item))
                .Bind(WeatherItemViewModels)
                .Subscribe();

            WeatherItemClickCommand = ReactiveCommand.Create< WeatherItemViewModel,Task>(item =>
            {
                Debug.WriteLine($"Weather Forecast clicked: {item.Summary}");
                return Task.CompletedTask;
            });

            Initialize();
        }

        private async void Initialize()
        {
            var items = await weatherForecastService.GetForecastAsync(DateTime.Now);
            weatherItemList.AddRange(items);
        }
    }
}
