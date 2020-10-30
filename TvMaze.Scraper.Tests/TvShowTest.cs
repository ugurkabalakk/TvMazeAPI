using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Moq;
using TvMazeService;
using TvMazeService.Models;
using Xunit;

namespace TvMaze.Scraper.Tests
{

    public class TvShowTest
    {
        private Mock<IMongoCollection<TvShowModel>> _mockCollection;
        private TvShowModel _tvShowModel;
        private CastModel _castModel;
        private List<CastModel> _castList;
        private ApplicationService _applicationService = new ApplicationService(
                "mongodb+srv://user:BmnEF3z8CK48n3PL@cluster0.tsbuw.azure.mongodb.net/TvMaze?retryWrites=true&w=majority",
                "TvMaze", "TvShow");

        public TvShowTest()
        {
            _castList = new List<CastModel>();
            _castModel = new CastModel { Id = 7, Name = "Mackenzie Lintz", Birthday = "1996-11-22" };
            _castList.Add(_castModel);
            _castModel = new CastModel { Id = 5, Name = "Colin Ford", Birthday = "1996-09-12" };
            _castList.Add(_castModel);

            _tvShowModel = new TvShowModel
            {
                Id = 1,
                Name = "Under the Dome",
                Cast = _castList
            };
            _mockCollection = new Mock<IMongoCollection<TvShowModel>>();
            _mockCollection.Object.InsertOne(_tvShowModel);

        }

        [Fact]
        public void WhenGetAllTvShows_GetTvShowsNotNull()
        {

            IEnumerable <TvShowModel> tvShowModelList = _applicationService.GetAll();
            tvShowModelList.Should().NotBeNull();
        }

        [Fact]
        public void WhenOneTvShowGet_GetTvShow_Valid_Success()
        {
            TvShowModel tvShowModel = _applicationService.GetById(1);

            tvShowModel.Should().NotBeNull();

            tvShowModel.Name.Should().Be(_tvShowModel.Name);

        }

        [Fact]
        public void WhenSetTvShowPageCount_GetItemCount_Valid_Success()
        {
            IEnumerable<TvShowModel> tvShowModelList = _applicationService.GetPage(10,1);
            tvShowModelList.Should().NotBeNull().
                            And.HaveCount(10);

        }

    }
}
