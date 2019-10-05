using System.Collections.Generic;

namespace POEApi.Model
{
    public class Hybrid
    {
        public bool IsVaalGem { get; set; }
        public List<Property> Properties { get; set; }
        public List<string> ExplicitMods { get; set; }
        public string SecDescrText { get; set; }

        internal Hybrid(JSONProxy.Hybrid proxy)
        {
            IsVaalGem = proxy.IsVaalGem;
            Properties = proxy.Properties;
            ExplicitMods = proxy.ExplicitMods;
            SecDescrText = proxy.SecDescrText;
        }
    }
}
