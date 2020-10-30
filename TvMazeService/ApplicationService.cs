using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using TvMazeService.Models;

namespace TvMazeService
{
   public class ApplicationService
    {
        private readonly IMongoCollection<TvShowModel> _tvShows;

        public ApplicationService(string connectionString, string databaseName, string collectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _tvShows = database.GetCollection<TvShowModel>(collectionName);
        }
        
        public TvShowModel GetById(int id)
        {
            TvShowModel tvShowModel = _tvShows.Find(tv => tv.Id == id).FirstOrDefault();
            return tvShowModel;
        }

        public IEnumerable<TvShowModel> GetAll()
        {
            var data = _tvShows.Find(tv => true);
            return data.ToList();
        }
       
        public IEnumerable<TvShowModel> GetPage(int pageSize, int pageNumber)
        {
            if (pageNumber < 1)
            {
                throw new Exception();
            }

            if (pageSize < 1)
            {
                throw new Exception();
            }
            var data = _tvShows.Find(tv => true);
            return data.Skip(pageSize * (pageNumber - 1)).Limit(pageSize).ToList();
        }


        public void InsertTvShowCollection(List<TvShowModel> tvShowModelList)
        {
            _tvShows.InsertMany(tvShowModelList);
        }

    }
}
