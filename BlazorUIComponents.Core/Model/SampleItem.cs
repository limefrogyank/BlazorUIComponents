using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorUIComponents.Core.Model
{
    public class SampleItem
    {
        public string Id { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public string DisplayName { get; set; }
    }
}
