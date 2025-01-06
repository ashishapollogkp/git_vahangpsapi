using vahangpsapi.Models;

namespace vahangpsapi.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDonationData> GetDashboardCountData(int roleId, string userName);
    }
}
