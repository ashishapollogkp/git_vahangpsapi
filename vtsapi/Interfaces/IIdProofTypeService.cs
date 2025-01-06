using vahangpsapi.Data;
using vahangpsapi.Models;

namespace vahangpsapi.Interfaces
{
    public interface IIdProofTypeService
    {
        Task<List<IdProofTypeModel>> GetIdProofTypeList();
        Task<IdProofTypeModel> GetIdProofTypeDetail(int Id);

        Task<int> AddIdProofTypeData(IdProofTypeModel idProofTypeModel);
        Task<bool> UpdateIdProofTypeData(IdProofTypeModel idProofTypeModel);
        Task<bool> DeleteIdProofTypeData(int id);
    }
}
