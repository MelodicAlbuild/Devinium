using Newtonsoft.Json;
using System.IO;

namespace Devinium.lib.data
{
    public class ImportHandler
    {
        public ImportHandler()
        {
            Import();
        }
        public static Rootobject imports;
        private static readonly string path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "/appdata/locallow/volcanoid/volcanoids/mods/resources/JSON/main.json";
        private void Import()
        {
            var root = JsonConvert.DeserializeObject<Rootobject>(File.ReadAllText(path));
            imports = root;
            new ExportHandler();
        }
    }

    public class Rootobject
    {
        public Item[] items { get; set; }
        public Deposit[] deposits { get; set; }
        public Recipe[] recipes { get; set; }
        public Category[] categories { get; set; }
    }

    public class Item
    {
        public string item_name { get; set; }
        public int stack_size { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string guid { get; set; }
        public string base_item { get; set; }
        public string icon_path { get; set; }
    }

    public class Deposit
    {
        public bool underground { get; set; }
        public int percent_replace { get; set; }
        public string output_name { get; set; }
        public int[] yields { get; set; }
        public string replaced_item { get; set; }
    }

    public class Category
    {
        public string category_type { get; set; }
        public string name { get; set; }
        public string guid { get; set; }
    }

    public class Recipe
    {
        public string recipe_name { get; set; }
        public int input_amount { get; set; }
        public Input[] inputs { get; set; }
        public Output[] output { get; set; }
        public string base_recipe { get; set; }
        public string itemID { get; set; }
        public string[] required_items { get; set; }
        public string recipe_category { get; set; }
    }

    public class Input
    {
        public string input_name { get; set; }
        public int input_amount { get; set; }
    }

    public class Output
    {
        public string output_name { get; set; }
        public int output_amount { get; set; }
    }
}