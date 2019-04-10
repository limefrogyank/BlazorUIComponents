﻿using BlazorUIComponents.Core.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorUIComponents.Core.ViewModel.ListViewDemo
{
    public class WeatherItemViewModel : ReactiveObject
    {
        private readonly WeatherForecast weatherForecast;

        public string Date => weatherForecast.Date.ToShortDateString();
        public int TemperatureC => weatherForecast.TemperatureC;
        public int TemperatureF => weatherForecast.TemperatureF;
        public string Summary => weatherForecast.Summary;

        public WeatherItemViewModel(WeatherForecast weatherForecast)
        {
            this.weatherForecast = weatherForecast;
        }
    }
}