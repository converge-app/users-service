using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Application.Helpers;
using Application.Models.Entities;
using Application.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    public interface IUserService
    {
        User Create(User user);
        void Update(User userParam);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Create(User user)
        {
            if (_userRepository.GetByEmail(user.Email) != null)
                throw new Exception("Email is already taken");

            return _userRepository.Create(user);
        }

        public void Update(User userParam)
        {
            var user = _userRepository.GetById(userParam.Id) ??
                       throw new ArgumentNullException("_userRepository.GetById(userParam.Id)");

            if (userParam.Email != user.Email)
                if (_userRepository.GetByEmail(userParam.Email) != null)
                    throw new Exception("Email was already taken");

            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Email = userParam.Email;

            _userRepository.Update(user.Id, user);
        }
    }
}