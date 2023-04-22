using practice.DAL.Models;

namespace practice.BLL.Interfaces
{
    public interface ITrackLocationService
    {
        Task<string> GetOverallInfoOnWalks(string imei);
    }
}
