using System;
using System.Collections.Generic;
using Application.Database;
using Application.Models.Entities;
using MongoDB.Driver;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        List<User> Get();
        User GetById(Guid id);
        User GetByEmail(string email);
        User Create(User user);
        void Update(Guid id, User userIn);
        void Remove(User userIn);
        void Remove(Guid id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseContext dbContext;
        private IMongoCollection<User> _users;

        public UserRepository(IDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
            if (dbContext.IsConnectionOpen()) _users = dbContext.Users;
        }

        public List<User> Get()
        {
            return _users.Find(user => true).ToList();
        }

        public User GetById(Guid id)
        {
            return _users.Find<User>(user => user.Id == id).FirstOrDefault();
        }

        public User GetByEmail(string email)
        {
            return _users.Find<User>(user => user.Email == email).FirstOrDefault();
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(Guid id, User userIn)
        {
            _users.ReplaceOne(user => user.Id == id, userIn);
        }

        public void Remove(User userIn)
        {
            _users.DeleteOne(user => user.Id == userIn.Id);
        }

        public void Remove(Guid id)
        {
            _users.DeleteOne(user => user.Id == id);
        }
    }
}