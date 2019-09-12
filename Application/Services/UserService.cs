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
        User Authenticate(string username, string password);
        User Create(User user, string password);
        void Update(User userParam, string password = null);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _userRepository.GetByUsername(username) ??
                       throw new ArgumentNullException("_userRepository.GetByUsername(username)");

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) // TODO: set proper rules
                throw new Exception("Password is required");

            if (_userRepository.GetByUsername(user.Username) != null)
                throw new Exception("Username is already taken");

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return _userRepository.Create(user);
        }

        public void Update(User userParam, string password = null)
        {
            var user = _userRepository.GetById(userParam.Id) ??
                       throw new ArgumentNullException("_userRepository.GetById(userParam.Id)");

            if (userParam.Username != user.Username)
                if (_userRepository.GetByUsername(userParam.Username) != null)
                    throw new Exception("Username was already taken");

            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _userRepository.Update(user.Id, user);
        }

        private static void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is missing");

            try
            {
                passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
                passwordHash = BCrypt.Net.BCrypt.HashPassword(password, passwordSalt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is null or whitespace");
            if (string.IsNullOrWhiteSpace(storedHash))
                throw new Exception("Stored hash is null or whitespace");
            if (string.IsNullOrWhiteSpace(storedSalt))
                throw new Exception("Stored salt is null or whitespace");

            try
            {
                return BCrypt.Net.BCrypt.Verify(password, storedHash);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}