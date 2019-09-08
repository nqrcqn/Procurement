﻿using POEApi.Model;
using System.Linq;

namespace Procurement.ViewModel.Filters
{
    public class UserSearchFilter : IFilter
    {
        public FilterGroup Group
        {
            get { return FilterGroup.Default; }
        }

        private string filter;
        public UserSearchFilter(string filter)
        {
            this.filter = filter;
        }
        public bool CanFormCategory
        {
            get { return false; }
        }

        public string Keyword
        {
            get { return "User search"; }
        }

        public string Help
        {
            get { return "Matches user search on name/typeline, geartype, mods and texts"; }
        }

        public bool Applicable(Item item)
        {
            if (string.IsNullOrEmpty(filter))
                return false;

            if (containsMatchedCosmeticMod(item) || isMatchedGear(item))
                return true;

            string[] words = filter.ToLowerInvariant().Split(' ');

            int matchedcount = 0;

            foreach (var splitword in words)
            {
                var word = splitword;

                bool matched = false;
                bool dontmatch = false;

                if (word.StartsWith("!"))
                {
                    dontmatch = true;
                    word = word.Remove(0, 1);
                }

                if (item.TypeLine.ToLowerInvariant().Contains(word) || item.Name.ToLowerInvariant().Contains(word))
                    matched = true;

                if (item.Explicitmods != null && !matched)
                    foreach (var mod in item.Explicitmods)
                        if (mod.ToLowerInvariant().Contains(word))
                            matched = true;

                if (item.Implicitmods != null && !matched)
                    foreach (var mod in item.Implicitmods)
                        if (mod.ToLowerInvariant().Contains(word))
                            matched = true;

                if (item.FracturedMods != null && !matched)
                    foreach (var mod in item.FracturedMods)
                        if (mod.ToLowerInvariant().Contains(word))
                            matched = true;

                if (item.CraftedMods != null && !matched)
                    foreach (var mod in item.CraftedMods)
                        if (mod.ToLowerInvariant().Contains(word))
                            matched = true;

                if (item.EnchantMods != null && !matched)
                    foreach (var mod in item.EnchantMods)
                        if (mod.ToLowerInvariant().Contains(word))
                            matched = true;

                if (item.FlavourText != null && !matched)
                    foreach (var text in item.FlavourText)
                        if (text.ToLowerInvariant().Contains(word))
                            matched = true;

                if (item.DescrText != null && !matched)
                    if (item.DescrText.ToLowerInvariant().Contains(word))
                        matched = true;

                if (item.SecDescrText != null && !matched)
                    if (item.SecDescrText.ToLowerInvariant().Contains(word))
                        matched = true;

                if (item.ProphecyText != null && !matched)
                    if (item.ProphecyText.ToLowerInvariant().Contains(word))
                        matched = true;

                if ((item is Map || item is Gear) && !matched)
                {
                    string rarity = null;

                    var gear1 = item as Gear;

                    if (item is Map
                        || (gear1 != null
                        && !gear1.GearType.Equals(GearType.Breachstone)
                        && !gear1.GearType.Equals(GearType.DivinationCard)
                        && !gear1.TypeLine.StartsWith("Sacrifice at ")
                        && !gear1.TypeLine.StartsWith("Mortal ")
                        && !gear1.TypeLine.StartsWith("Fragment of the ")
                        && !gear1.TypeLine.EndsWith(" Key")))
                    {
                        if (item.Rarity == Rarity.Normal)
                            rarity = "normal";
                        else if (item.Rarity == Rarity.Magic)
                            rarity = "magic";
                        else if (item.Rarity == Rarity.Rare)
                            rarity = "rare";
                        else if (item.Rarity == Rarity.Unique)
                            rarity = "unique";
                        else if (item.Rarity == Rarity.Relic)
                            rarity = "relic";

                        if (rarity.Contains(word))
                            matched = true;
                    }
                }
                
                if (!matched)
                {
                    string text = null;
                    if (item.EnchantMods != null && item.EnchantMods.Count() > 0)
                    {
                        text = "enchanted";
                        if (text.Contains(word))
                            matched = true;
                    }
                    if (item.CraftedMods != null && item.CraftedMods.Count() > 0 && !matched)
                    {
                        text = "crafted";
                        if (text.Contains(word))
                            matched = true;
                    }
                    if (item.Fractured && !matched)
                    {
                        text = "fractured";
                        if (text.Contains(word))
                            matched = true;
                    }
                    if (item.Corrupted && !matched)
                    {
                        text = "corrupted";
                        if (text.Contains(word))
                            matched = true;
                    }
                    if (!item.Identified && !matched)
                    {
                        text = "unidentified";
                        if (text.Contains(word))
                            matched = true;
                    }
                    if (item.ItemLevel > 0 && !matched)
                    {
                        text = item.ItemLevel.ToString();
                        if (text.Contains(word))
                            matched = true;
                    }
                    if (item.StackSize > 0 && !matched)
                    {
                        text = item.StackSize.ToString();
                        if (text.Contains(word))
                            matched = true;
                    }
                    if (item.MaxStackSize > 0 && !matched)
                    {
                        text = item.MaxStackSize.ToString();
                        if (text.Contains(word))
                            matched = true;
                    }
                }

                if (word.StartsWith("tier:") && !matched)
                {
                    int tier;
                    bool greaterthan = false;
                    bool lessthan = false;
                    word = word.Remove(0, 5);
                    if (word.EndsWith("+"))
                    {
                        word = word.Remove(word.Length - 1);
                        greaterthan = true;
                    }
                    else if (word.EndsWith("-"))
                    {
                        word = word.Remove(word.Length - 1);
                        lessthan = true;
                    }
                    
                    int.TryParse(word, out tier);
                    if (tier >= 1 && tier <= 16)
                    {
                        var map = item as Map;
                        if (map != null)
                            if (greaterthan && tier <= map.MapTier)
                                matched = true;
                            else if (lessthan && tier >= map.MapTier)
                                matched = true;
                            else if (tier == map.MapTier)
                                matched = true;
                    }
                }

                if (word.StartsWith("ilvl:") && item.ItemLevel > 0 && !matched)
                {
                    int ilvl;
                    bool greaterthan = false;
                    bool lessthan = false;
                    word = word.Remove(0, 5);
                    if (word.EndsWith("+"))
                    {
                        word = word.Remove(word.Length - 1);
                        greaterthan = true;
                    }
                    else if (word.EndsWith("-"))
                    {
                        word = word.Remove(word.Length - 1);
                        lessthan = true;
                    }
                    int.TryParse(word, out ilvl);
                    if (ilvl >= 1 && ilvl <= 100)
                    {
                        if (greaterthan && ilvl <= item.ItemLevel)
                            matched = true;
                        else if (lessthan && ilvl >= item.ItemLevel)
                            matched = true;
                        else if (ilvl == item.ItemLevel)
                            matched = true;
                    }
                }

                if ((matched && !dontmatch) || (!matched && dontmatch))
                    matchedcount++;
            }

            if (words.Count() == matchedcount)
                return true;

            var gear = item as Gear;

            if (gear != null && gear.SocketedItems.Any(x => Applicable(x)))
                return true;

            return false;
        }

        private bool containsMatchedCosmeticMod(Item item)
        {
            return item.Microtransactions.Any(x => x.ToLowerInvariant().Contains(filter.ToLowerInvariant()));
        }

        private bool isMatchedGear(Item item)
        {
            Gear gear = item as Gear;

            if (gear == null)
                return false;

            return gear.GearType.ToString().ToLowerInvariant().Contains(filter.ToLowerInvariant());
        }
    }
}
