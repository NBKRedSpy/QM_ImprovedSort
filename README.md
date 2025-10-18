# Quasimorph Improved Sort

![Items in descending cost order](media/thumbnail.png)
This mod was previously called "Cost Sort".

It improves the game's item sorting so that the best items appear first in your inventory. Items are still grouped by type (such as weapons, backpacks, augments, etc.), but within each group, sorting prioritizes the most valuable items. The sort order considers factors like item cost (highest first), durability, remaining uses, spoilage time, and more.

By default, items are also grouped by manufacturer when relevant. For example, all energy weapons from the same manufacturer will be grouped together. You can disable this grouping if you prefer sorting primarily by price, making the most expensive items appear at the top regardless of manufacturer. See the [Sort Order](#sort-order) section below for full details.

For even more advanced inventory management, check out the [Sort Excess Mod](https://steamcommunity.com/sharedfiles/filedetails/?id=3481043805), which builds on this mod to help you avoid scrolling through pages of duplicate items.

# Sort Order

|Criteria|Added By Mod|Description|
|--|--|--|
|Item type||The types of items such as helmet, ranged weapon, melee, etc.|
|Slots item takes||If the item takes one or two slots.
|Item Manufacturer||(This can be disabled) The manufacture of the item.  Ex: SBN, Realware, etc.|
|Cost (descending)|x|The listed price of the item.|
|Chip lock status|x|Locked chips are first|
|Is Modified|x|If the item is a modified version.  Indicated in game by the M icon.|
|Item id||The internal identifier of an item.  Ex: rags.  This is identical to the game's existing sort.|
|Stack Count|x|The number of items in the stack.  Descending.|
|Durability|x|The amount of durability the item has.  Descending.|
|Remaining Uses|x|For items that have multiple uses, the number of remaining uses.  Descending.|
|Spoilage Time|x|The remaining amount of time before an item spoils.  Ex: shards. Descending.|
|Amount of Ammo|x|The amount of ammo that a weapon contains.  Descending.|

# Configuration

## MCM
This mod supports the Mod Configuration Menu.  The options can be set in the game's "Mods" button on the main screen.
Alternatively, the settings can be changed in the config file directly.

## Config File

The configuration file will be created on the first game run and can be found at `%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\MiniMapMoveCamera\config.json`.

|Name|Default|Description|
|--|--|--|
|GroupByManufacture|true|Disable this option to sort items by price, placing the most expensive items first. This is often useful because the most expensive item is usually the best. If enabled, items will be grouped by type, similar to the game's default sorting (e.g., energy weapons grouped together).|

# Buy Me a Coffee
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.
Thanks!

# Credits
* Special thanks to Crynano for his excellent Mod Configuration Menu. 

# Source Code
Source code is available on GitHub https://github.com/NBKRedSpy/QM_ImprovedSort

# Change Log

## 1.3.0 
* Multi Version Support for current release.

## 1.2.0 
* Fixed sort being non deterministic.  (Pressing sort multiple times would still move items around).
* Added the option to primarily sort by cost instead of manufacture grouping.
* Added MCM