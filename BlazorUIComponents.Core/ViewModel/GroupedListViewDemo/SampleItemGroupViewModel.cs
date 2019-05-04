using BlazorUIComponents.Core.Model;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace BlazorUIComponents.Core.ViewModel.GroupedListViewDemo
{
    public class SampleItemGroupViewModel : ObservableCollectionExtended<ListViewDemo.SampleItemViewModel>, IDisposable
    {
        private readonly IGroup<SampleItem, string, string> sampleItemGroup;
        private readonly CompositeDisposable _cleanUp;

        public string Key => sampleItemGroup.Key;

        //public IObservableCollection<SampleItem> SampleItems { get; private set; } = new ObservableCollectionExtended<SampleItem>();

        public SampleItemGroupViewModel(IGroup<SampleItem, string, string> sampleItemGroup)
        {
            this.sampleItemGroup = sampleItemGroup;

            var dataLoader = sampleItemGroup.Cache.Connect()
                //.ObserveOn(RxApp.MainThreadScheduler)
                .Transform(x=>new ListViewDemo.SampleItemViewModel(x))
                .Bind(this)
                .Subscribe();

            _cleanUp = new CompositeDisposable(dataLoader);
        }

        public void Dispose()
        {
            _cleanUp.Dispose();
        }
    }
}
