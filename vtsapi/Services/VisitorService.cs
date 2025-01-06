using Microsoft.EntityFrameworkCore;
using vahangpsapi.Context;
using vahangpsapi.Data;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;

namespace vahangpsapi.Services
{
    public class VisitorService:IVisitorService
    {
        private readonly JwtContext _jwtContext;
        public VisitorService(JwtContext jwtContext) 
        { 
            _jwtContext = jwtContext;
        }

        public async Task<long> AddVisitor(VisitorModel visitor)
        {
            visitor.PhotoId = "";
            visitor.IdDoc1 = "";
            visitor.IdDoc2 = "";
            if (visitor.PhotoData != null && visitor.PhotoData.Length > 0)
                visitor.PhotoId = SaveImage(visitor.PhotoData, "PH");

            if (visitor.IdDocData1 != null && visitor.IdDocData1.Length > 0)
                visitor.IdDoc1 = SaveImage(visitor.IdDocData1, "DC1");

            if (visitor.IdDocData2 != null && visitor.IdDocData2.Length > 0)
                visitor.IdDoc2 = SaveImage(visitor.IdDocData2, "DC2");

            var visitorM = new VisitorMaster()
            {
                FirstName = visitor.FirstName,
                LastName = visitor.LastName,
                DeptId = visitor.DeptId,
                Address1 = visitor.Address1,
                Address2 = visitor.Address2,
                Address3 = visitor.Address3,
                CatId = visitor.CatId,
                Email = visitor.Email,
                PhotoId = visitor.PhotoId,
                ContactNo = visitor.ContactNo,
                EmpIdToMeet = visitor.EmpIdToMeet,
                IMEINo = visitor.IMEINo,
                RFId = visitor.RFId,
                IdTYPE1 = visitor.IdTYPE1,
                IdNumber1 = visitor.IdNumber1,
                IdDoc1 = visitor.IdDoc1,
                IdTYPE2 = visitor.IdTYPE2,
                IdNumber2 = visitor.IdNumber2,
                IdDoc2 = visitor.IdDoc2,
                ValidFrom = visitor.ValidFrom,
                ValidTill = visitor.ValidTill,
                VisitorPurpose = visitor.VisitorPurposeId,
                IsVisitComplete = 0,
                VisitRemark = visitor.VisitRemark,
                CreatedBy = visitor.CreatedBy,
                CreatedOn = DateTime.Now
            };
            var visitMas = await _jwtContext.VisitorMaster.AddAsync(visitorM);
            await _jwtContext.SaveChangesAsync();

            var visitorsStatus = new VisitorsStatus()
            { 
                VisitorId= visitMas.Entity.VisitorId,
                VisitDate = DateTime.Now.Date,
                InTime = DateTime.Now,
                VisitorStatus = 0
            };
            
            await _jwtContext.VisitorStatus.AddAsync(visitorsStatus);
            await _jwtContext.SaveChangesAsync();

            return visitMas.Entity.VisitorId;
        }

        public async Task<bool> DeleteVisitor(long id) 
        {
            try
            {
                var visit = await _jwtContext.VisitorMaster.SingleOrDefaultAsync(x => x.VisitorId == id);
                if (visit == null)
                    throw new Exception("User not found!");
                else
                {
                    _jwtContext.VisitorMaster.Remove(visit);
                    await _jwtContext.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<VisitorModel>> GetVisitorDetails(int deptId)
        {
            
            List<VisitorModel> listVisitor = null;

            listVisitor = await (from d in _jwtContext.VisitorMaster
                                 join dep in _jwtContext.DepartmentMaster on d.DeptId equals dep.DeptId 
                                 join cat in _jwtContext.Categories on d.CatId equals cat.CatId
                                 join vp in _jwtContext.VisitorPurpose on d.VisitorPurpose equals vp.VPId into visitpur
                                 from vp1 in visitpur.DefaultIfEmpty()
                                 join emp in _jwtContext.EmployeeMaster on d.EmpIdToMeet equals emp.EmpId into empDef
                                 from emp1 in empDef.DefaultIfEmpty() 
                                 where (deptId==1 || d.DeptId == deptId)
                          select new VisitorModel
                          {
                              VisitorId = d.VisitorId,
                              FirstName = d.FirstName,
                              LastName = d.LastName,
                              DeptId = d.DeptId,
                              DeptName = dep.DeptName,
                              Address1 = d.Address1,
                              Address2 = d.Address2,
                              Address3 = d.Address3,
                              CatId = d.CatId,
                              CatName = cat.CatName,
                              Email = d.Email,
                              PhotoId = d.PhotoId != "" ? @$"Uploads\VisitorImg\" + d.PhotoId : "",
                              ContactNo = d.ContactNo,
                              EmpNameToMeet = emp1.FirstName + " " + emp1.LastName,
                              IMEINo = d.IMEINo,
                              //RFId = d.RFId,
                              IdTYPE1 = d.IdTYPE1,
                              IdNumber1 = d.IdNumber1,
                              IdDoc1 = d.IdDoc1 != "" ? @$"Uploads\VisitorImg\" + d.IdDoc1 : "",
                              IdTYPE2 = d.IdTYPE2,
                              IdNumber2 = d.IdNumber2,
                              IdDoc2 = d.IdDoc2 != "" ? @$"Uploads\VisitorImg\" + d.IdDoc2 : "",
                              ValidFrom = d.ValidFrom,
                              ValidTill = d.ValidTill,
                              VisitorPurposeText = vp1.VPName,
                              IsVisitComplete = d.IsVisitComplete,
                              VisitRemark = d.VisitRemark,
                              CreatedBy = d.CreatedBy
                          }).ToListAsync();

            return listVisitor;
        }

        public async Task<VisitorModel> GetVisitorDetails(long id)
        {
            var visitMas = await _jwtContext.VisitorMaster.SingleOrDefaultAsync(x => x.VisitorId == id);
            VisitorModel visitor = new VisitorModel()  
            {
                VisitorId = visitMas.VisitorId,
                FirstName = visitMas.FirstName,
                LastName = visitMas.LastName,
                DeptId = visitMas.DeptId,
                Address1 = visitMas.Address1,
                Address2 = visitMas.Address2,
                Address3 = visitMas.Address3,
                CatId = visitMas.CatId,
                Email = visitMas.Email,
                PhotoId = visitMas.PhotoId != "" ? @$"Uploads\VisitorImg\" + visitMas.PhotoId : "",
                ContactNo = visitMas.ContactNo,
                EmpIdToMeet = visitMas.EmpIdToMeet,
                IMEINo = visitMas.IMEINo,
                RFId = visitMas.RFId,
                IdTYPE1 = visitMas.IdTYPE1,
                IdNumber1 = visitMas.IdNumber1,
                IdDoc1 = visitMas.IdDoc1 != "" ? @$"Uploads\VisitorImg\" + visitMas.IdDoc1 : "",
                IdTYPE2 = visitMas.IdTYPE2,
                IdNumber2 = visitMas.IdNumber2,
                IdDoc2 = visitMas.IdDoc2 != "" ? @$"Uploads\VisitorImg\" + visitMas.IdDoc2 : "",
                ValidFrom = visitMas.ValidFrom,
                ValidTill = visitMas.ValidTill,
                VisitorPurposeId = visitMas.VisitorPurpose,
                VisitRemark = visitMas.VisitRemark,
                CreatedBy = visitMas.CreatedBy
            };
            return visitor;
        }

        public async Task<long> UpdateVisitor(VisitorModel visitor)
        {
            visitor.PhotoId = "";
            visitor.IdDoc1 = "";
            visitor.IdDoc2 = "";
            if (visitor.PhotoData != null && visitor.PhotoData.Length > 0)
                visitor.PhotoId = SaveImage(visitor.PhotoData, "PH");
            if (visitor.IdDocData1 != null && visitor.IdDocData1.Length > 0)
                visitor.IdDoc1 = SaveImage(visitor.IdDocData1, "DC1");

            if (visitor.IdDocData2 != null && visitor.IdDocData2.Length > 0)
                visitor.IdDoc2 = SaveImage(visitor.IdDocData2, "DC2");

            var visitorM = new VisitorMaster()
            {
                VisitorId = visitor.VisitorId,
                FirstName = visitor.FirstName,
                LastName = visitor.LastName,
                DeptId = visitor.DeptId,
                Address1 = visitor.Address1,
                Address2 = visitor.Address2,
                Address3 = visitor.Address3,
                CatId = visitor.CatId,
                Email = visitor.Email,
                PhotoId = visitor.PhotoId,
                ContactNo = visitor.ContactNo,
                EmpIdToMeet = visitor.EmpIdToMeet,
                IMEINo = visitor.IMEINo,
                RFId = visitor.RFId,
                IdTYPE1 = visitor.IdTYPE1,
                IdNumber1 = visitor.IdNumber1,
                IdDoc1 = visitor.IdDoc1,
                IdTYPE2 = visitor.IdTYPE2,
                IdNumber2 = visitor.IdNumber2,
                IdDoc2 = visitor.IdDoc2,
                ValidFrom = visitor.ValidFrom,
                ValidTill = visitor.ValidTill,
                VisitorPurpose = visitor.VisitorPurposeId,
                IsVisitComplete = 0,
                VisitRemark = visitor.VisitRemark,
                CreatedBy = visitor.CreatedBy,
                CreatedOn = DateTime.Now
            };

            var updVisitor = _jwtContext.VisitorMaster.Update(visitorM);
            await _jwtContext.SaveChangesAsync();

            var visitorsStatusUpd = await _jwtContext.VisitorStatus.SingleOrDefaultAsync
                (
                    x => x.VisitorId == visitorM.VisitorId 
                    && x.InTime == visitorM.ValidFrom
                    && x.VisitorStatus == 0
                );  

            if (visitorsStatusUpd != null)
            {
                visitorsStatusUpd.Id = visitorsStatusUpd.Id;
                visitorsStatusUpd.VisitorId = visitorM.VisitorId;
                visitorsStatusUpd.VisitDate = Convert.ToDateTime(visitorM.ValidFrom).Date;
                visitorsStatusUpd.InTime = Convert.ToDateTime(visitorM.ValidFrom); //visitorM.ValidFrom != "" ? visitorM.ValidFrom : DateTime.Now;
                visitorsStatusUpd.VisitorStatus = 0;
                _jwtContext.VisitorStatus.Update(visitorsStatusUpd);
                await _jwtContext.SaveChangesAsync();
            }
            else 
            {
                var visitorsStatusAdd = new VisitorsStatus()
                {
                    VisitorId = updVisitor.Entity.VisitorId,
                    VisitDate = DateTime.Now.Date,
                    InTime = updVisitor.Entity.ValidFrom,
                    VisitorStatus = 0
                };

                await _jwtContext.VisitorStatus.AddAsync(visitorsStatusAdd);
                await _jwtContext.SaveChangesAsync();
            }

            return updVisitor.Entity.VisitorId;

        }

        private string SaveImage(byte[] ImageData, string ImageType)
        {
            try
            {
                string fileMapPath = Path.Combine(Environment.CurrentDirectory, @$"Uploads\VisitorImg\");

                //Create directory if it doesn't exist
                if (!Directory.Exists(fileMapPath))
                    Directory.CreateDirectory(fileMapPath);

                string imageName = ImageType + "_" + Guid.NewGuid().ToString() + ".jpg";
                string imgPath = Path.Combine(fileMapPath, imageName);
                System.IO.File.WriteAllBytes(imgPath, ImageData);
                return Path.GetFileName(imgPath);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
