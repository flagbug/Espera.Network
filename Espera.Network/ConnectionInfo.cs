using System;

namespace Espera.Network
{
    public class ConnectionInfo
    {
        public ConnectionInfo(NetworkAccessPermission permission, Version serverVersion, ResponseInfo responseInfo)
        {
            if (responseInfo == null)
                throw new ArgumentNullException("responseInfo");

            this.AccessPermission = permission;
            this.ServerVersion = serverVersion;
            this.ResponseInfo = responseInfo;
        }

        public NetworkAccessPermission AccessPermission { get; private set; }

        public ResponseInfo ResponseInfo { get; private set; }

        public Version ServerVersion { get; private set; }
    }
}