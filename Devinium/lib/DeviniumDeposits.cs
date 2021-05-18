using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using System.Collections.Generic;
using static Devinium.lib.data.ExportHandler;
using Devinium.lib.data;

namespace Devinium.lib
{
    class DeviniumDeposits
    {
        public void InitDeposits()
        {
            depositsurface = Resources.FindObjectsOfTypeAll<DepositLocationSurface>();
            depositunderground = Resources.FindObjectsOfTypeAll<DepositLocationUnderground>();

            foreach (KeyValuePair<Deposit, string> dict in questingDeposits)
            {
                CreateDeposit(dict.Key.underground, dict.Key.percent_replace, dict.Key.output_name, dict.Key.yields[0], dict.Key.yields[1], dict.Key.replaced_item);
            }

            Debug.Log("[Devinium | Deposits]: Deposits Loaded...");
        }
        public void CreateDeposit(bool Underground, int PercentageToReplace, string outputname, float minyield, float maxyield, string ItemToReplace)
        {

            if (Underground)
            {
                foreach (DepositLocationUnderground underground in depositunderground)
                {
                    if (Random.Range(0, 100) <= PercentageToReplace)
                    {
                        if ((ItemToReplace != null && underground.Ore == GetItem(ItemToReplace)) || ItemToReplace == null)
                        {
                            underground.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(underground, GetItem(outputname));
                        }
                    }
                }
            }
            if (!Underground)
            {
                foreach (DepositLocationSurface surface in depositsurface)
                {
                    if (Random.Range(0, 100) <= PercentageToReplace)
                    {
                        if ((ItemToReplace != null && surface.Ore == GetItem(ItemToReplace)) || ItemToReplace == null)
                        {
                            surface.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(surface, GetItem(outputname));
                        }
                    }
                }
            }
            Debug.Log("[Devinium | Deposits]: Deposit Replacing " + ItemToReplace + " has been replaced with " + outputname);
        }
        private ItemDefinition GetItem(string itemname)
        {
            ItemDefinition item = GameResources.Instance.Items.FirstOrDefault(s => s.name == itemname);
            if (item == null)
            {
                Debug.Log("ERROR: [Devinium | Deposits]: Item is null, name: " + itemname + ". Replacing with NullItem");

                Debug.LogError("[Devinium | Deposits]: Item is null, name: " + itemname + ". Replacing with NullItem");
                return GameResources.Instance.Items.FirstOrDefault(s => s.name == "NullItem");
            }
            return item;

        }
        private DepositLocationSurface[] depositsurface;
        private DepositLocationUnderground[] depositunderground;
        private static readonly FieldInfo OreField = typeof(DepositLocation).GetField("m_ore", BindingFlags.NonPublic | BindingFlags.Instance);

    }
}