using BlazorUIComponents.Core.Model;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorUIComponents.Core.ViewModel.ListViewDemo
{
    public class SampleItemViewModel : ReactiveObject
    {
        private readonly SampleItem sampleItem;

        public string Id => sampleItem.Id;
        public string Date => sampleItem.DateAdded.ToLocalTime().ToString("T");
        public string Category => sampleItem.Category;
        public string DisplayName => sampleItem.DisplayName;

        public SampleItemViewModel(SampleItem sampleItem)
        {
            this.sampleItem = sampleItem;
        }
    }
}
