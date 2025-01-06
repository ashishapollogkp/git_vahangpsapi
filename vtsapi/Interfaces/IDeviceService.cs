using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vahangpsapi.Models;
namespace vahangpsapi.Interfaces
{
   public interface IDeviceService
    {
        Task<List<DeviceModel>> GetDeviceList(int deptId);
        //Task<DeviceModel> GetDeviceDetail(long id);
        //Task<long> AddDevice(DeviceModel visitor);
        //Task<long> UpdateDevice(DeviceModel visitor);
        //Task<bool> DeleteDevice(long id);
    }
}
