namespace Espera.Network
{
    public enum ResponseStatus
    {
        Success = 0,
        Unauthorized = 1,
        MalformedRequest = 2,
        NotFound = 3,
        NotSupported = 4,
        Rejected = 5,
        Fatal = 6,
        WrongPassword = 7
    }
}