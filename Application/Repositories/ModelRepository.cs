using System;
using System.Collections.Generic;
using Application.Database;
using Application.Models;
using MongoDB.Driver;

namespace Application.Repositories
{
    public interface IModelRepository
    {
        Model Create(Model model);
        List<Model> Get();
        Model GetModel(string id);
        void Remove(Model modelIn);
        void Remove(string id);
        void Update(string id, Model modelIn);
    }

    public class ModelRepository : IModelRepository
    {
        private readonly IDatabaseContext dbContext;
        private IMongoCollection<Model> models;

        public ModelRepository(IDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
            if (dbContext.IsConnectionOpen())
            {
                models = dbContext.Models;
            }
        }

        public List<Model> Get() => models.Find(model => true).ToList();

        public Model GetModel(string id) => models.Find<Model>(model => model.Id == id).FirstOrDefault();

        public Model Create(Model model)
        {
            models.InsertOne(model);
            return model;
        }

        public void Update(string id, Model modelIn) => models.ReplaceOne(model => model.Id == id, modelIn);

        public void Remove(Model modelIn) => models.DeleteOne(model => model.Id == modelIn.Id);

        public void Remove(string id) => models.DeleteOne(model => model.Id == id);
    }
}