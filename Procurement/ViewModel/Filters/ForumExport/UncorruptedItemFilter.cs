﻿using POEApi.Model;

namespace Procurement.ViewModel.Filters.ForumExport
{
    public class UncorruptedItemFilter : IFilter
    {
        public bool CanFormCategory
        {
            get
            {
                return false;
            }
        }

        public string Keyword
        {
            get
            {
                return "Uncorrupted Items";
            }
        }

        public string Help
        {
            get
            {
                return "Uncorrupted Items";
            }
        }

        public FilterGroup Group
        {
            get
            {
                return FilterGroup.Default;
            }
        }

        public bool Applicable(Item item)
        {
            if (item.TypeLine.Contains(" Flask") || item.TypeLine.Contains("Sacrifice at ") || item.TypeLine.Contains("Mortal ") || item.TypeLine.Contains(" Key") || item.TypeLine.Contains("Fragment of the ") || item.TypeLine.Contains(" Breachstone"))
                return false;

            if ((item is Map || item is Gem) && !item.Corrupted)
                return true;

            Gear gear = item as Gear;
            return gear != null && !gear.Corrupted && !(gear.MaxStackSize > 0);
        }
    }
}
