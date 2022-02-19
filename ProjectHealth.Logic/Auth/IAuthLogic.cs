using ProjectHealth.Models.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHealth.Logic.Auth
{
    public interface IAuthLogic
    {
        Task<Result> Register(Login loginObject);
        Task<Result> Login(Login loginObject);
    }
}
