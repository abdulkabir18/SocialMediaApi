namespace Application.Interfaces.External
{
    public interface ICurrentDeviceService
    {
        string GetClientIp();
        Task<IpLocationResponse?> GetLocationAsync(string ip);
    }
}
