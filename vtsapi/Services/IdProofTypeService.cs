using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;

namespace vahangpsapi.Services
{
    public class IdProofTypeService : IIdProofTypeService
    {
        private readonly JwtContext _jwtContext;
        private readonly IMapper _mapper;

        public IdProofTypeService(JwtContext jwtContext, IMapper mapper)
        {
            _jwtContext = jwtContext;
            _mapper = mapper;

        }


        public async Task<int> AddIdProofTypeData(IdProofTypeModel idProofTypeModel)
        {

            var checkdata = _jwtContext.IdProofTypes.Where(x => x.IdType == idProofTypeModel.IdType).Count();
            if (checkdata == 0)
            {

                IdProofType villa = _mapper.Map<IdProofType>(idProofTypeModel);
                villa.OrderNo = 0;
                villa.IPStatus = 0;
                var visitMas = await _jwtContext.IdProofTypes.AddAsync(villa);
                await _jwtContext.SaveChangesAsync();
                return visitMas.Entity.Id;
            }
            else { return 0; }
        }

        public async Task<bool> DeleteIdProofTypeData(int id)
        {
            try
            {
                var visit = await _jwtContext.IdProofTypes.SingleOrDefaultAsync(x => x.Id == id);
                if (visit == null)
                    throw new Exception("User not found!");
                else
                {
                    _jwtContext.IdProofTypes.Remove(visit);
                    await _jwtContext.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<IdProofTypeModel> GetIdProofTypeDetail(int Id)
        {
            var idproofdata = await _jwtContext.IdProofTypes.Where(x => x.Id == Id).FirstOrDefaultAsync();
            IdProofTypeModel list = _mapper.Map<IdProofTypeModel>(idproofdata);
            return list;
        }

        public async Task<List<IdProofTypeModel>> GetIdProofTypeList()
        {
            var idproofdata = await _jwtContext.IdProofTypes.ToListAsync();
            List<IdProofTypeModel> list = _mapper.Map<List<IdProofTypeModel>>(idproofdata);
            return list;
        }    

        public async Task<bool> UpdateIdProofTypeData(IdProofTypeModel idProofTypeModel)
        {
            try
            {
                IdProofType updatedata = await _jwtContext.IdProofTypes.SingleOrDefaultAsync(x => x.Id == idProofTypeModel.Id);

                if (updatedata == null)
                    throw new Exception("User not found!");
                else
                {

                    updatedata.IdType = idProofTypeModel.IdType;

                    _jwtContext.IdProofTypes.Update(updatedata);
                    await _jwtContext.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
