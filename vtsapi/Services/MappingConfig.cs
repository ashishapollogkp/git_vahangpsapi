using AutoMapper;
using vahangpsapi.Data;
using vahangpsapi.Models;

namespace vahangpsapi.Services
{
    public class MappingConfig: Profile
    {
       
        public MappingConfig()
        {
            CreateMap<IdProofType, IdProofTypeModel>().ReverseMap();
            CreateMap<DeviceMaster, DeviceModel>().ReverseMap();

        }
    }
}
