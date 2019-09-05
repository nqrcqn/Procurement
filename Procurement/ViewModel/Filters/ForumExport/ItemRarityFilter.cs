namespace Procurement.ViewModel.Filters
{
    internal class ItemRarityFilter : OrStatFilter
    {
        public override FilterGroup Group
        {
            get { return FilterGroup.MagicFind; }
        }

        public ItemRarityFilter()
            : base("Item Rarity", "Item with the Item Rarity stat", "increased Rarity", "increased Item Rarity")
        { }
    }
}
