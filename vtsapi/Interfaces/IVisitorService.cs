using vahangpsapi.Models;

namespace vahangpsapi.Interfaces
{
    public interface IVisitorService
    {
        Task<List<VisitorModel>> GetVisitorDetails(int deptId);
        Task<VisitorModel> GetVisitorDetails(long id);
        Task<long> AddVisitor(VisitorModel visitor);
        Task<long> UpdateVisitor(VisitorModel visitor); 
        Task<bool> DeleteVisitor(long id); 
    }
}
