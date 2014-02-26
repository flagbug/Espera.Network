using System;

namespace Espera.Network
{
    public class ConnectionInfo
    {
        public NetworkAccessPermission AccessPermission { get; set; }

        public Version ServerVersion { get; set; }
    }
}