using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;

namespace GunPermission
{
    public class OverrideMethods
    {
        public static void askEquip(CSteamID steamID, byte page, byte x, byte y, byte[] hash)
        {
            UnturnedPlayer player = UnturnedPlayer.FromCSteamID(steamID);
            
            if (Player.instance.channel.checkOwner(steamID) && Provider.isServer && GunPermissionPlugin.CanEquip(player))
                player.Player.equipment.tryEquip(page, x, y, hash);
            
        }
    }
}