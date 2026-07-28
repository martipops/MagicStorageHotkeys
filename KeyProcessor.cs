using HelpfulHotkeys;
using MagicStorage;
using MagicStorage.Common.Global;
using MagicStorage.Common.Systems;
using MagicStorage.Items;
using MagicStorage.UI;
using MagicStorage.UI.Input;
using MagicStorage.UI.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.UI;


namespace MagicStorageHotkeys
{
    internal class KeyProcessor : ModPlayer
    {

        public static ModKeybind OpenCraftingAccessKey;
        public static ModKeybind OpenStorageAccessKey;
        public static ModKeybind FocusSearchBarKey;
        public static ModKeybind ClearSearchBarKey;

        public static Mod MagicStorageMod;

        private static List<int> craftingAccessItemTypes;
        private static List<int> storageAccessItemTypes;

        public override void Load()
        {
            if (Main.dedServ)
                return;

            MagicStorageMod = ModLoader.GetMod("MagicStorage");

            craftingAccessItemTypes = new List<int> {
                ModContent.ItemType<PortableCraftingAccess>(),
                ModContent.ItemType<PortableCraftingAccessHM>(),
                ModContent.ItemType<PortableCraftingAccessPreHM>()
            };

            storageAccessItemTypes = new List<int> {
                ModContent.ItemType<PortableAccess>(),
                ModContent.ItemType<PortableAccessHM>(),
                ModContent.ItemType<PortableAccessPreHM>()
            };

            OpenCraftingAccessKey = KeybindLoader.RegisterKeybind(Mod, "OpenCraftingAccess", Keys.CapsLock);
            OpenStorageAccessKey = KeybindLoader.RegisterKeybind(Mod, "OpenStorageAccess", Keys.OemTilde);
            FocusSearchBarKey = KeybindLoader.RegisterKeybind(Mod, "FocusSearchBar", Keys.C);
            ClearSearchBarKey = KeybindLoader.RegisterKeybind(Mod, "ClearSearchBar", Keys.Z);

        }

        public override void Unload()
        {
            craftingAccessItemTypes = null;
            storageAccessItemTypes = null;
        }



        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (OpenCraftingAccessKey.JustPressed)
                FindAndUseItem(ref craftingAccessItemTypes);
            if (OpenStorageAccessKey.JustPressed)
                FindAndUseItem(ref storageAccessItemTypes);
            if (FocusSearchBarKey.JustPressed)
                FocusSearchBar();
            if (ClearSearchBarKey.JustPressed)
                ClearSearchBar();
                
        }

        public void FindAndUseItem(ref List<int> netids)
        {
            int item = Player.FindItem(netids);
            if (item != -1)
            {
                Player.GetModPlayer<HelpfulHotkeysPlayer>().QuickUseItemAt(item);
            }
        }

        public void FocusSearchBar()
        {
            NewUISearchBar bar = MagicUI.craftingUI.GetDefaultPage<BaseStorageUIAccessPage>().searchBar;
            NewUISearchBar bar2 = MagicUI.storageUI.GetDefaultPage<BaseStorageUIAccessPage>().searchBar;
            bar.State.Focus();
            bar2.State.Focus();
        }

        public void ClearSearchBar()
        {
            NewUISearchBar bar = MagicUI.craftingUI.GetDefaultPage<BaseStorageUIAccessPage>().searchBar;
            NewUISearchBar bar2 = MagicUI.storageUI.GetDefaultPage<BaseStorageUIAccessPage>().searchBar;
            bar.State.Clear();
            bar2.State.Clear();
        }
    }
}