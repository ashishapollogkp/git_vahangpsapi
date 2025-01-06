using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vahangpsapi.Context;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;

namespace vahangpsapi.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly JwtContext _jwtContext;
        private readonly IMapper _mapper;
        public DeviceService(JwtContext jwtContext , IMapper mapper)
        {
            _jwtContext = jwtContext;
            _mapper = mapper;
        }


        public async Task<List<DeviceModel>> GetDeviceList(int deptId)
        {         

            var deviceData=await _jwtContext.Device_Master.Where(x=>x.DEPT_ID==deptId || deptId==1).ToListAsync();
            List<DeviceModel> listDevice = _mapper.Map<List<DeviceModel>>(deviceData);
            
            return listDevice;
        }

    }
}
