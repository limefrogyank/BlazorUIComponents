using BlazorUIComponents.Core.Model;
using BlazorUIComponents.Core.Service;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorUIComponents.Core.ViewModel
{
    public class FetchDataViewModel : ViewModelBase
    {
        private readonly WeatherForecastService weatherForecastService;

        SourceList<WeatherForecast> forecastList = new SourceList<WeatherForecast>();

        public IObservableCollection<WeatherForecast> WeatherForecastItems { get; private set; } = new ObservableCollectionExtended<WeatherForecast>();

        public FetchDataViewModel(WeatherForecastService weatherForecastService) : base("Fetch Data")
        {
            this.weatherForecastService = weatherForecastService;

            forecastList.Connect().Bind(WeatherForecastItems).Subscribe();

            Initialize();
        }

        private async void Initialize()
        {
            var weatherItems = await weatherForecastService.GetForecastAsync(DateTime.Now);
            forecastList.AddRange(weatherItems);
        }
    }
}
