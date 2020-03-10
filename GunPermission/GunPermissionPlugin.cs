using System;
using System.Linq;
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
        }

        public void FixedUpdate() => CheckEquips();
        
        

        //Once I figure out overriding the netcode for this I shouldn't have to do this tickbased so no more shitty performance
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
        
    }
}