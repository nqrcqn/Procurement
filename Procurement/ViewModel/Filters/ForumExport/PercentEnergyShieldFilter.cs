using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Procurement.ViewModel.Filters.ForumExport
{
    public class PercentEnergyShieldFilter : OrStatFilter
    {
        public PercentEnergyShieldFilter()
            : base("% Increased Energy Shield", "Items with % increased energy shield", "% increased maximum Energy Shield", "increased Global Defences")
        { }

        public override FilterGroup Group
        {
            get { return FilterGroup.Default; }
        }
    }
}
