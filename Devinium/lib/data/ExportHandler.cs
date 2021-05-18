using System.Collections.Generic;

namespace Devinium.lib.data
{
    class ExportHandler
    {
        public static readonly Dictionary<Item, GUID> questingItems = new Dictionary<Item, GUID>();
        public static readonly Dictionary<Recipe, GUID> questingRecipes = new Dictionary<Recipe, GUID>();
        public static readonly Dictionary<Category, GUID> questingCategories = new Dictionary<Category, GUID>();
        public static readonly Dictionary<Deposit, string> questingDeposits = new Dictionary<Deposit, string>();
        public ExportHandler()
        {
            Export();
        }
        private void Export()
        {
            foreach (Item item in ImportHandler.imports.items)
            {
                questingItems[item] = GUID.Parse(item.guid);
            }

            foreach (Recipe item in ImportHandler.imports.recipes)
            {
                questingRecipes[item] = GUID.Parse(item.itemID);
            }

            foreach (Category item in ImportHandler.imports.categories)
            {
                questingCategories[item] = GUID.Parse(item.guid);
            }

            foreach (Deposit item in ImportHandler.imports.deposits)
            {
                questingDeposits[item] = item.replaced_item;
            }
        }
    }
}
