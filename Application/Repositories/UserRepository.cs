using System.Collections.Generic;
using Application.Database;
using Application.Models.Entities;
using MongoDB.Driver;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        List<User> Get();
        User GetById(string id);
        User GetByUsername(string username);
        User Create(User user);
        void Update(string id, User userIn);
        void Remove(User userIn);
        void Remove(string id);
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

        public User GetById(string id)
        {
            return _users.Find<User>(user => user.Id == id).FirstOrDefault();
        }

        public User GetByUsername(string username)
        {
            return _users.Find<User>(user => user.Email == username).FirstOrDefault();
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn)
        {
            _users.ReplaceOne(user => user.Id == id, userIn);
        }

        public void Remove(User userIn)
        {
            _users.DeleteOne(user => user.Id == userIn.Id);
        }

        public void Remove(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }
    }
}