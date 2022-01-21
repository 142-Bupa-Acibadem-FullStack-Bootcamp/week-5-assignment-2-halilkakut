using Northwind.BusinessLayer.Base;
using Northwind.DataAccess.Abstract;
using Northwind.Entity.DataTransferObject;
using Northwind.Entity.Models;
using Northwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Entity.IBase;
using Northwind.Entity.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Northwind.BusinessLayer
{
    public class UserManager : BusinessLogicBase<User, DtoUser>, IUserService
    {
        public readonly IUserRepository _userRepository;
        IConfiguration _configuration;
        private readonly IMD5Service _md5Service;

        public UserManager(IServiceProvider service, IConfiguration configuration, IMD5Service mD5Service) : base(service)
        {
            _userRepository = service.GetService<IUserRepository>();
            this._configuration = configuration;
            _md5Service = mD5Service;
        }

        public IResponse<DtoUserToken> Login(DtoLogin login)
        {
            var user = _userRepository.Login(ObjectMapper.Mapper.Map<User>(login));

            if (user != null)
            {
                var dtoUser = ObjectMapper.Mapper.Map<DtoLoginUser>(user);
                var token = new TokenManager(_configuration).CreateAccessToken(dtoUser);

                var userToken = new DtoUserToken()
                {
                    DtoLoginUser = dtoUser,
                    AccessToken = token
                };

                return new Response<DtoUserToken>()
                {
                    Message = "Token is Success!",
                    StatusCode = StatusCodes.Status200OK,
                    Data = userToken
                };
            }
            else
            {
                return new Response<DtoUserToken>()
                {
                    Message = "UserCode or Password is wrong!",
                    StatusCode = StatusCodes.Status406NotAcceptable,
                    Data = null
                };
            }
        }

        public IResponse<DtoRegister> Register(DtoRegister user,bool saveChanges = true)
        {
            try
            {
                var resolvedResult = "";
                
                var encryptPassword = _md5Service.Encrypt(user.Password);

                var reg = new DtoUser
                {
                    //UserID = user.UserID,
                    UserCode = user.UserCode,
                    UserName = user.UserName,
                    UserLastName = user.UserLastName,
                    Password = encryptPassword,
                };
                var TResult = _userRepository.Register(ObjectMapper.Mapper.Map<User>(reg));
                resolvedResult = String.Join(',', TResult.GetType().GetProperties().Select(x => $" - {x.Name} : {x.GetValue(TResult) ?? ""} - "));
                if (saveChanges)
                    Save();
                return new Response<DtoRegister>
                {
                    StatusCode = StatusCodes.Status201Created,
                    Message = "Succuess",
                    Data = ObjectMapper.Mapper.Map<User, DtoRegister>(TResult)
                };
            }
            catch (Exception ex)
            {
                return new Response<DtoRegister>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }
    }
}
