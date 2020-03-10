using System;
using System.Linq;
using System.Reflection;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace GunPermission
{
    public class GunPermissionPlugin : RocketPlugin<GunPermissionConfiguration>
    {

        public static GunPermissionPlugin Instance;

        public override void LoadPlugin()
        {
            Instance = this;
            MethodInfo originalMethod = typeof(PlayerEquipment).GetMethod("askEquip", BindingFlags.Instance | BindingFlags.Public);
            MethodInfo newMethod =
                typeof(OverrideMethods).GetMethod("askEquip", BindingFlags.Static | BindingFlags.Public);
            RedirectionHelper.RedirectCalls(originalMethod, newMethod);
        }

        
        //Scrapped code
        /*
        public void FixedUpdate() => CheckEquips();
        public static void CheckEquips()
        {
            foreach (SteamPlayer pla in Provider.clients)
            {
                UnturnedPlayer rPlayer = UnturnedPlayer.FromSteamPlayer(pla);
                if ((rPlayer.Player.equipment.useable as UseableGun) == null || !rPlayer.Player.equipment.isEquipped)
                    continue;

                bool isAllowed = false;
                
                foreach (var permission in Instance.Configuration.Instance.Permissions.Where(permission => rPlayer.HasPermission(permission)))
                    isAllowed = true;

                if (Instance.Configuration.Instance.BlacklistedGunIds.Contains(
                    ((ItemGunAsset) rPlayer.Player.equipment.asset).id))
                    isAllowed = false;
                        
                
                if (isAllowed)
                    return;
                
                rPlayer.Player.equipment.dequip();
                
            }
        }
        */

        public static bool CanEquip(UnturnedPlayer rPlayer)
        {
            bool isAllowed = false;
                
            foreach (var permission in Instance.Configuration.Instance.Permissions.Where(permission => rPlayer.HasPermission(permission)))
                isAllowed = true;

            if (Instance.Configuration.Instance.BlacklistedGunIds.Contains(
                ((ItemGunAsset) rPlayer.Player.equipment.asset).id))
                isAllowed = false;

            return isAllowed;
        }
        
    }
}