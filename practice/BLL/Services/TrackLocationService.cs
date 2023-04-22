using Microsoft.EntityFrameworkCore;
using practice.BLL.Interfaces;
using practice.DAL.Interfaces;
using practice.DAL.Models;

namespace practice.BLL.Services
{
    public class TrackLocationService : ITrackLocationService
    {
        private readonly IUnitOfWork unitOfWork;

        public TrackLocationService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<string> GetOverallInfoOnWalks(string imei)
        {
            var result = "Testing something";

            var trackLocations = await unitOfWork.TrackLocations.GetAllRaw().ToListAsync();


            return result;
        }
    }
}
