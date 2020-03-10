using System.Collections.Generic;
using Rocket.API;

namespace GunPermission
{
    public class GunPermissionConfiguration : IRocketPluginConfiguration
    {

        public List<string> Permissions;
        public List<ushort> BlacklistedGunIds;
        
        public void LoadDefaults()
        {
            Permissions = new List<string>();
            BlacklistedGunIds = new List<ushort>();
        }
    }
}