[h1]Quasimorph Improved Sort[/h1]


This mod was previously called "Cost Sort".
It improves the game's item sorting so that the best items appear first in your inventory. Items are still grouped by type (such as weapons, backpacks, augments, etc.), but within each group, sorting prioritizes the most valuable items. The sort order considers factors like item cost (highest first), durability, remaining uses, spoilage time, and more.

By default, items are also grouped by manufacturer when relevant. For example, all energy weapons from the same manufacturer will be grouped together. You can disable this grouping if you prefer sorting primarily by price, making the most expensive items appear at the top regardless of manufacturer. See the Sort Order section below for full details.

For even more advanced inventory management, check out the [url=https://steamcommunity.com/sharedfiles/filedetails/?id=3481043805]Sort Excess Mod[/url], which builds on this mod to help you avoid scrolling through pages of duplicate items.

[h1]Sort Order[/h1]
[table]
[tr]
[td]Criteria
[/td]
[td]Added By Mod
[/td]
[td]Description
[/td]
[/tr]
[tr]
[td]Item type
[/td]
[td]
[/td]
[td]The types of items such as helmet, ranged weapon, melee, etc.
[/td]
[/tr]
[tr]
[td]Slots item takes
[/td]
[td]
[/td]
[td]If the item takes one or two slots.
[/td]
[/tr]
[tr]
[td]Item Manufacturer
[/td]
[td]
[/td]
[td](This can be disabled) The manufacture of the item.  Ex: SBN, Realware, etc.
[/td]
[/tr]
[tr]
[td]Cost (descending)
[/td]
[td]x
[/td]
[td]The listed price of the item.
[/td]
[/tr]
[tr]
[td]Is Modified
[/td]
[td]x
[/td]
[td]If the item is a modified version.  Indicated in game by the M icon.
[/td]
[/tr]
[tr]
[td]Item id
[/td]
[td]
[/td]
[td]The internal identifier of an item.  Ex: rags.  This is identical to the game's existing sort.
[/td]
[/tr]
[tr]
[td]Stack Count
[/td]
[td]x
[/td]
[td]The number of items in the stack.  Descending.
[/td]
[/tr]
[tr]
[td]Durability
[/td]
[td]x
[/td]
[td]The amount of durability the item has.  Descending.
[/td]
[/tr]
[tr]
[td]Remaining Uses
[/td]
[td]x
[/td]
[td]For items that have multiple uses, the number of remaining uses.  Descending.
[/td]
[/tr]
[tr]
[td]Spoilage Time
[/td]
[td]x
[/td]
[td]The remaining amount of time before an item spoils.  Ex: shards. Descending.
[/td]
[/tr]
[tr]
[td]Amount of Ammo
[/td]
[td]x
[/td]
[td]The amount of ammo that a weapon contains.  Descending.
[/td]
[/tr]
[/table]

[h1]Configuration[/h1]

[h2]MCM[/h2]

This mod supports the Mod Configuration Menu.  The options can be set in the game's "Mods" button on the main screen.
Alternatively, the settings can be changed in the config file directly.

[h2]Config File[/h2]

The configuration file will be created on the first game run and can be found at [i]%AppData%\..\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\MiniMapMoveCamera\config.json[/i].
[table]
[tr]
[td]Name
[/td]
[td]Default
[/td]
[td]Description
[/td]
[/tr]
[tr]
[td]GroupByManufacture
[/td]
[td]true
[/td]
[td]Disable this option to sort items by price, placing the most expensive items first. This is often useful because the most expensive item is usually the best. If enabled, items will be grouped by type, similar to the game's default sorting (e.g., energy weapons grouped together).
[/td]
[/tr]
[/table]

[h1]Buy Me a Coffee[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Credits[/h1]
[list]
[*]Special thanks to Crynano for his excellent Mod Configuration Menu.
[/list]

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM_ImprovedSort

[h1]Change Log[/h1]

[h2]1.2.0[/h2]
[list]
[*]Fixed sort being non deterministic.  (Pressing sort multiple times would still move items around).
[*]Added the option to primarily sort by cost instead of manufacture grouping.
[*]Added MCM
[/list]
