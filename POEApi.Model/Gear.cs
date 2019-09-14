using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POEApi.Infrastructure;

namespace POEApi.Model
{
    public class Gear : Item
    {
        public List<Socket> Sockets { get; set; }
        public List<SocketableItem> SocketedItems { get; set; }
        public GearType GearType { get; set; }
        public string BaseType { get; set; }

        public override bool IsGear => true;

        public Gear(JSONProxy.Item item) : base(item)
        {
            Sockets = GetSockets(item);
            Explicitmods = item.ExplicitMods;
            SocketedItems = GetSocketedItems(item);
            Implicitmods = item.ImplicitMods;
            ItemType = Model.ItemType.Gear;
            GearType = GearTypeFactory.GetType(this);
            BaseType = GearTypeFactory.GetBaseType(this);
        }

        private List<Socket> GetSockets(JSONProxy.Item item) =>
            item.Sockets == null ? new List<Socket>() : item.Sockets.Select(proxy => new Socket(proxy)).ToList();

        private List<SocketableItem> GetSocketedItems(JSONProxy.Item item) =>
            item.SocketedItems == null ? new List<SocketableItem>() :
            item.SocketedItems.Select(proxy => (SocketableItem)ItemFactory.Get(proxy)).ToList();

        public bool IsLinked(int links)
        {
            return Sockets.GroupBy(s => s.Group).Any(g => g.Count() == links);
        }

        public int NumberOfSockets()
        {
            return Sockets.Count();
        }

        protected override Dictionary<string, string> DescriptiveNameComponents
        {
            get
            {
                // TODO: Reduce code duplication between this class's implementation and AbyssJewel's (they both
                // have a "Rarity" property that works the same way, but do not inherit it from the same parent class).
                var components = base.DescriptiveNameComponents;
                if (Rarity != Rarity.Normal)
                {
                    if (!Identified)
                    {
                        components["name"] = string.Format("Unidentified {0} {1}", Rarity, TypeLine);
                    }
                    else if (this.Rarity != Rarity.Magic)
                    {
                        string quotedName = string.IsNullOrWhiteSpace(Name) ? string.Empty :
                            string.Format("\"{0}\", ", Name);
                        components["name"] = string.Format("{0}{1} {2}", quotedName, Rarity, TypeLine);
                    }
                }

                return components;
            }
        }

        private string GetSpecialItemType()
        {
            if (Elder)
            {
                return "Elder Item";
            }

            if (Shaper)
            {
                return "Shaper Item";
            }

            if (Fractured)
            {
                return "Fractured Item";
            }

            if (Synthesised)
            {
                return "Synthesised Item";
            }

            return null;
        }
    }
}
