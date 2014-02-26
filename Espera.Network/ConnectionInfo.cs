using System;

namespace Espera.Network
{
    public class ConnectionInfo
    {
        public ConnectionInfo(NetworkAccessPermission permission, Version serverVersion)
        {
            this.AccessPermission = permission;
            this.ServerVersion = serverVersion;
        }

        public NetworkAccessPermission AccessPermission { get; private set; }

        public Version ServerVersion { get; private set; }
    }
}