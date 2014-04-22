namespace Espera.Network
{
    public static class NetworkConstants
    {
        /// <summary>
        /// The default port for network messages.
        /// </summary>
        public static readonly int DefaultPort = 49587;

        /// <summary>
        /// The UDP discovery message that the server sends out.
        /// </summary>
        public static readonly string DiscoveryMessage = "espera-server-discovery";

        /// <summary>
        /// The highest assignable port number.
        /// </summary>
        public static readonly int MaxPort = 65534;

        /// <summary>
        /// The lowest assignable port number.
        /// </summary>
        public static readonly int MinPort = 49152;
    }
}