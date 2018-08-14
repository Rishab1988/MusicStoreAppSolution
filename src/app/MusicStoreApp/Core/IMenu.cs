using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStoreApp.Core
{
    public class AppMenuComponentParameters
    {
        public string Parameter { get; set; }
        public object Value { get; set; }
    }

    public class AppMenuComponentUrl
    {
        public AppMenuComponentUrl()
        {
            Parameters = new List<AppMenuComponentParameters>();
        }
        public string Controller { get; set; }
        public string Action { get; set; }
        public List<AppMenuComponentParameters> Parameters { get; set; }
    }

    public class AppMenuComponent
    {
        public AppMenuComponent()
        {
            MenuComponents = new List<AppMenuComponent>();
            MenuComponentUrl = new AppMenuComponentUrl();
        }
        public string DisplayText { get; set; }
        public AppMenuComponentUrl MenuComponentUrl { get; set; }
        public List<AppMenuComponent> MenuComponents { get; set; } 
    }
}
