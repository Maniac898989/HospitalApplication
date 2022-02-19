using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectHealth.Data.Data;
using ProjectHealth.Models;
using ProjectHealth.Models.WebModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHealth.Logic.Auth
{
    public class AuthLogic : IAuthLogic
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _iconfiguration;

        public AuthLogic(ApplicationDbContext applicationDbContext, IConfiguration iconfiguration)
        {
            _applicationDbContext = applicationDbContext;
            _iconfiguration = iconfiguration;
        }

        public async Task<Result> Register(Registration loginObject)
        {
            try
            {
                var res = new Result();
                //call passwordhashhmetthod
                CreatePasswordhash(loginObject.Password, out byte[] PasswordHash, out byte[] PasswordSalt);

                //write to database
                AccessTable at = new AccessTable
                {
                    Firstname = loginObject.Firstname,
                    Lastname = loginObject.Lastname,
                    Email = loginObject.UserName,
                    Phone = loginObject.Phone,
                    DateCreated = DateTime.Now,
                    Passwordhash = PasswordHash,
                    PasswordSalt = PasswordSalt
                };

                await _applicationDbContext.AddAsync(at);
                await _applicationDbContext.SaveChangesAsync();

                res.Message = "Registration Successful";
                res.IsSuccessful = true;

                return res;
            }
            catch (Exception ex)
            {

                return new Result { Message = ex.Message, IsSuccessful = false };
            }
        }



        public async Task<Result> Login(Login login)
        {
            var res = new Result();
            try
            {
                
                //convert the key to bytes and call db to compare
                var User = _applicationDbContext.AccessTable.Where(x => x.Email == login.Username).FirstOrDefault();
                if (User == null)
                {
                    res.Message = "user not found";
                    res.IsSuccessful = false;
                    return res;
                }
                if (!Verifypassword(login.Password, User.Passwordhash, User.PasswordSalt))
                {
                    res.Message = "password incorrect";
                    res.IsSuccessful = false;
                    return res;
                }

                var token = CreateToken(User);
                if (token != null)
                {
                    res.Message = "Sucessfully logged in";
                    res.IsSuccessful = true;
                    res.ReturnedCode = token;
                    return res;
                }

                return res;
            }
            catch (Exception ex)
            {
               return new Result { IsSuccessful = false, Message = ex.Message.ToString()};
            }
           
        }

        private bool Verifypassword(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var Computedhash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Computedhash.SequenceEqual(PasswordHash);
            }
        }

        private void CreatePasswordhash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password.Trim()));
            }
        }

        private string CreateToken(AccessTable accessTable)
        {
            try
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, accessTable.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration.GetSection("AppSettings:Token").Value));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(5), signingCredentials: cred);

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
