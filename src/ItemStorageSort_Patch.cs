using HarmonyLib;
using MGSC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QM_ImprovedSort
{
    /// <summary>
    /// Sorts the items using the custom logic.
    /// </summary>
    [HarmonyPatch(typeof(ItemStorage), nameof(ItemStorage.SortingComparsion))]
    public static class ItemStorageSort_Patch
    {
        public static bool Prefix(BasePickupItem x, BasePickupItem y, ref int __result)
        {
            //Note:  
            //   The existing game uses a very simple sort.  There is a sort priority, which groups
            // the items into items defined by the game.  EG:  Helmets, Armors, etc.
            // This sort is invoked inside of those groups.
            //
            //   The default sort uses the number of slots that an item takes, then by the item's id string.
            // The id's have a natural prefix (such as "army_" or "trucker_") and custom items will have a 
            // suffix of "_custom".  
            // This provides a natural type of prefix grouping since it is a simple compare of the item's string id.
            //
            //   Since this mod's sort is more complex, it attempts to retain the grouping by trying to use the prefix
            // and suffix.

            __result = CompareItems(x, y);
            return false;
        }

        public static int CompareItems(BasePickupItem x, BasePickupItem y)
        {

            //Note - The game has a built in grouping for the items.  This sort is provided the data within those groups.
            //  Ex:  Helmets, Armors, etc.

            int result;


            //--Default game sort by slot size.
            result = x.InventoryWidthSize.CompareTo(y.InventoryWidthSize) * -1;
            if (result != 0) return result;


            //Put chips at the top
            result = CompareUnlockedData(x, y);
            if (result != 0) return result;


            //--Sort by "Prefix".  The id's generally have a prefix such as "army_" or "trucker_".
            //  However, that is not always the case.  For example, watermelon does not have a prefix.
            //  Since the game's default sort is by id, this is the closest we can get to grouping similar items.
            //
            //  The purpose of this is to group similar items together, and still allow sorting by cost, durability, etc.
            if (Plugin.Config.GroupByManufacture)
            {
                result = GetPrefix(x.Id).CompareTo(GetPrefix(y.Id));
                if (result != 0) return result;
            }


            //--Cost sort
            //  They should all be PickupItems, but just in case.
            PickupItem piX = x as PickupItem;
            PickupItem piY = y as PickupItem;
            
            float xPrice = ((ItemRecord)(piX?._records?.FirstOrDefault()))?.Price ?? 0f;
            float yPrice = ((ItemRecord)(piY?._records?.FirstOrDefault()))?.Price ?? 0f;
            result = xPrice.CompareTo(yPrice) * -1;
            if (result != 0) return result;



            //finish here:
            //result = CompareUnlockedData(x, y);
            //if (result != 0) return result;


            //--Modified versions first
            result = CompareIdsWithModified(x, y);
            if (result != 0) return result;

            //--Number of items in the stack.
            result = x.StackCount.CompareTo(y.StackCount) * -1;
            if (result != 0) return result;

            //--Remaining durability
            float xPercent = x.Comp<BreakableItemComponent>()?.CurrentPercent ?? 0;
            float yPercent = y.Comp<BreakableItemComponent>()?.CurrentPercent ?? 0;

            result = xPercent.CompareTo(yPercent) * -1;
            if (result != 0) return result;

            //--Remaining uses.
            result = x.HasFullUsages.CompareTo(y.HasFullUsages) * -1;
            if (result != 0) return result;

            //--Spoilage time
            DateTime xExpireDate = x.Comp<ExpireComponent>()?.ExpireDate ?? DateTime.MinValue;
            DateTime yExpireDate = y.Comp<ExpireComponent>()?.ExpireDate ?? DateTime.MinValue;

            result = xExpireDate.CompareTo(yExpireDate) * -1;
            if (result != 0) return result;

            //--Ammo count.  Mostly because I tend to unload my ammo early in the game,
            //and it doesn't hurt anything to order by the amount of ammo loaded.

            WeaponComponent xWeapon = x.Comp<WeaponComponent>();
            WeaponComponent yWeapon = y.Comp<WeaponComponent>();

            if (xWeapon != null && yWeapon != null)
            {
                int xAmmo = xWeapon.CurrentAmmo;
                int yAmmo = yWeapon.CurrentAmmo;
                result = xAmmo.CompareTo(yAmmo) * -1;
                if (result != 0) return result;
            }

            return 0;


        }

        /// <summary>
        /// If either item is a data disk, compare their unlock status and order the locked items first.
        /// In this case, locked means the player has not yet unlocked the data disk's content.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int CompareUnlockedData(BasePickupItem x, BasePickupItem y)
        {
            // Extract DataDiskComponent from both items
            var xDataDisk = x.Comp<DatadiskComponent>();
            var yDataDisk = y.Comp<DatadiskComponent>();
            
            // If neither item has a DataDiskComponent, they are equal in this comparison
            if (xDataDisk == null && yDataDisk == null)
                return 0;
            
            // If only one has a DataDiskComponent, prioritize the one without (non-data-disk items first)
            if (xDataDisk == null)
                return -1;
            if (yDataDisk == null)
                return 1;
            
            // Extract DataDiskRecord from both items
            var xRecord = x as PickupItem;
            var yRecord = y as PickupItem;
            
            var xDataDiskRecord = xRecord?._records?.FirstOrDefault() as DatadiskRecord;
            var yDataDiskRecord = yRecord?._records?.FirstOrDefault() as DatadiskRecord;

            // Check unlock status using MGSC.ItemInteractionSystem.IsAlreadyUnlockedDatadisk

            var mercenaries = Plugin.State.Get<Mercenaries>();
            var magnumCargo = Plugin.State.Get<MagnumCargo>();  

            bool xUnlocked = xDataDiskRecord != null && MGSC.ItemInteractionSystem.IsAlreadyUnlockedDatadisk(xDataDiskRecord, xDataDisk,
                mercenaries, magnumCargo);
            bool yUnlocked = yDataDiskRecord != null && MGSC.ItemInteractionSystem.IsAlreadyUnlockedDatadisk(yDataDiskRecord, yDataDisk,
                mercenaries, magnumCargo);

            // Compare unlock status - locked items first (false < true, so multiply by -1 to reverse)
            return xUnlocked.CompareTo(yUnlocked); //Locked first.
        }

        /// <summary>
        /// Used to remove the _custom suffix from the id to determine if two items are the same base item.
        /// </summary>
        private static Regex CustomBaseRegEx = new Regex(@"_custom$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Matches the item's "prefix" such as "army_" or "trucker_".  This is used to group the items.
        /// Technically text before the first _ may not actually be a prefix, but close enough.
        /// </summary>
        private static Regex ItemPrefixRegEx = new Regex(@"^(.+?_).*$", RegexOptions.IgnoreCase);

        /// <summary>
        /// Gets the prefix if available.  Ex:  "army_" from "army_marksman_1"
        /// </summary>
        /// <remarks>Note - the prefix extraction is not perfect as the first text with an _
        /// is not necessarily the prefix.  However, this is technically how the game's
        /// default id sort works anyway.
        ///</remarks>
        /// <param name="id">The item's id</param>
        /// <param name="prefix">The prefix that was extracted.</param>
        /// <returns>True if text that is most likely a prefix was extracted.</returns>
        private static string GetPrefix(string id)
        {
            Match match = ItemPrefixRegEx.Match(id);
            return match.Success ? match.Groups[1].Value : ""; 
        }


        /// <summary>
        /// Compares the ids of two items, taking into account the modified item suffix.
        /// Identical items will be sorted by the modified item first.
        /// </summary>
        /// <param name="x">The first item to compare</param>
        /// <param name="y">The second item to compare.</param>
        /// <returns></returns>
        private static int CompareIdsWithModified(BasePickupItem x, BasePickupItem y)
        {

            if (!x.IsModifiedItem && !y.IsModifiedItem)
            {
                return x.Id.CompareTo(y.Id);   
            }

            string xBaseId = x.IsModifiedItem ? CustomBaseRegEx.Replace(x.Id, "") : x.Id;
            string yBaseId = y.IsModifiedItem ? CustomBaseRegEx.Replace(y.Id, "") : y.Id;

            //Modified, but not the same items.
            int compare = xBaseId.CompareTo(yBaseId);

            if (compare == 0)
            {
                return x.IsModifiedItem.CompareTo(y.IsModifiedItem) * -1;
            }
            else
            {
                return x.Id.CompareTo(y.Id);
            }

            
        }
    }
}
