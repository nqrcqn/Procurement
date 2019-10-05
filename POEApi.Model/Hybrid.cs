using System.Collections.Generic;

namespace POEApi.Model
{
    public class Hybrid
    {
        public bool IsVaalGem { get; set; }
        public List<Property> VaalProperties { get; set; }
        public List<string> VaalExplicitMods { get; set; }
        public string VaalSecDescrText { get; set; }

        internal Hybrid(JSONProxy.Hybrid proxy)
        {
            IsVaalGem = proxy.IsVaalGem;
            VaalProperties = proxy.Properties;
            VaalExplicitMods = proxy.ExplicitMods;
            VaalSecDescrText = proxy.SecDescrText;
        }
    }
}
