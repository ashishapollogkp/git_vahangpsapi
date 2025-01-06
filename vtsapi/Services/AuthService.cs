using vahangpsapi.Context;
using vahangpsapi.Interfaces;
using vahangpsapi.Models;
using vahangpsapi.RequestModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;

namespace vahangpsapi.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtContext _jwtContext;
        private readonly IConfiguration _configuration;
        protected APIResponse _response;
        public AuthService(JwtContext jwtContext, IConfiguration configuration)
        {
            _jwtContext = jwtContext;
            _configuration = configuration;
            _response = new();
        }

        public async Task<APIResponse> Login(LoginRequest loginRequest)
        {

            string profile_image_path = "#";
            try
            {
                if (loginRequest.UserName != null && loginRequest.Password != null)
                {
                    CryptorEngine engine = new CryptorEngine();

                    string pass = CryptorEngine.Encrypt(loginRequest.Password, true);

                    if (await _jwtContext.EmployeeMaster.SingleOrDefaultAsync(s => s.UserName == loginRequest.UserName) != null)
                    {
                        var emp = await _jwtContext.EmployeeMaster.SingleOrDefaultAsync(s => s.UserName == loginRequest.UserName && s.EmpPassword == loginRequest.Password && s.EmpStatus == 1);
                        if (emp != null)
                        {
                            //string pass1 = CryptorEngine.Decrypt(emp.EncPassword, true);
                            var emp_role =await _jwtContext.RoleMaster.Where(x => x.RoleId == emp.RoleId).Select(x => x.RoleName).FirstOrDefaultAsync();
                            if (emp.RoleId == 1)
                            {
                                profile_image_path = "#";
                            }
                            if (emp.RoleId == 6)
                            {
                                profile_image_path = "http://103.109.7.173:7602/Uploads/Profile/" + emp.profile_image_path;
                            }
                            else
                            {

                                profile_image_path = _jwtContext.EmployeeMaster.Where(x => x.fk_manufacturer_id == emp.EmpId).Select(x => x.profile_image_path).FirstOrDefault();

                                if (profile_image_path == null)
                                {
                                    profile_image_path = "#";
                                }
                                else
                                {
                                    profile_image_path = "http://103.109.7.173:7602/Uploads/Profile/" + profile_image_path;

                                }
                            }


                            
                            var claims = new[] {
                                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                                new Claim("Id", emp.EmpId.ToString()),
                                new Claim("UserName", emp.UserName),
                                new Claim("Email", emp.Email),
                                new Claim("RoleName", emp_role),
                                new Claim("ProfileImage", profile_image_path),
                                new Claim("RoleId", emp.RoleId.ToString())
                            };

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.Now.AddDays(15),
                                signingCredentials: signIn
                            );

                            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

                            _response.Result = jwtToken;
                            _response.StatusCode = HttpStatusCode.OK;
                            _response.IsSuccess = true;
                            _response.ActionResponse = "success";
                        }
                        else
                        {
                            
                            _response.StatusCode = HttpStatusCode.Unauthorized;
                            _response.IsSuccess = false;
                            _response.ActionResponse = "Unauthorized user!";
                        }
                    }
                    else
                    {
                       // throw new Exception("");

                        _response.StatusCode = HttpStatusCode.NoContent;
                        _response.IsSuccess = false;
                        _response.ActionResponse = "User name or password is incorrect!";
                    }
                }
                else
                {
                    //throw new Exception("");

                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = false;
                    _response.ActionResponse = "User name and password are not null!";
                }
            }
            catch (Exception ex)
            {
                throw  new Exception(ex.Message);
            }

            return _response;
        }

        public async Task<UserModel> GetUserBasedOnLogin(string userName)
        {
            try
            {
                UserModel usr = new UserModel();

                var emp = await _jwtContext.EmployeeMaster.SingleOrDefaultAsync(s => s.UserName == userName);
                if (emp != null)
                {
                    usr.EmpId = emp.EmpId;
                    usr.Name = emp.FirstName + " " + emp.LastName;
                    usr.Email = emp.Email;
                    usr.Role = emp.RoleId.ToString();
                    usr.DeptId = emp.DeptId;
                    usr.UserName = emp.UserName;
                    return usr;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
