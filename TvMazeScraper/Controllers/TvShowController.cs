using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TvMazeScraper.ResponseModels;
using MongoDB.Driver;
using System.Net;
using TvMazeService.Models;
using TvMazeService;

namespace TvMazeScraper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TvShowController : ControllerBase
    {
        private ApplicationService _applicationService;
        //private readonly TvShowService _tvShowService;

        public TvShowController(ApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // GET api/tvshow/id
        [HttpGet("{id}")]
        public ActionResult<TvShowModel> Get(int id)
        {
            return _applicationService.GetById(id);
        }

        // GET api/tvshow
        [HttpGet]
        public IEnumerable<TvShowModel> Get()
        {
            return _applicationService.GetAll();
        }

        // GET api/tvshow/pageSize/pageNumber
        [HttpGet]
        [Route("{pageSize}/{pageNumber}")]
        public ActionResult<IEnumerable<TvShowModel>> GetPage(int pageSize, int pageNumber)
        {
            try
            {
                return new ActionResult<IEnumerable<TvShowModel>>(_applicationService.GetPage(pageSize, pageNumber));
            }
            catch
            {

                return StatusCode((int)HttpStatusCode.BadRequest);
            }
        }


        // POST api/tvshow
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string resultMessage = string.Empty;

            try
            {
                HttpResponseMessage result;
                List<TvShowModel> tvShowModelList = new List<TvShowModel>();
                List<CastModel> castModelList = new List<CastModel>();

                using (var client = new HttpClient())
                {
                    result = await client.GetAsync("http://api.tvmaze.com/shows");
                    if (result.IsSuccessStatusCode)
                    {
                        var tvShowResult = await result.Content.ReadAsStringAsync();
                        List<TvShowResponse> tvShowResponseList = JsonConvert.DeserializeObject<List<TvShowResponse>>(tvShowResult);

                        foreach (var tvShowResponse in tvShowResponseList)
                        {
                            string castUrl = string.Format("http://api.tvmaze.com/shows/{0}/cast", tvShowResponse.id);
                            result = await client.GetAsync(castUrl);

                            TvShowModel tvShowModel = new TvShowModel();
                            tvShowModel.Id = tvShowResponse.id;
                            tvShowModel.Name = tvShowResponse.name;

                            if (result.IsSuccessStatusCode)
                            {
                                var castResult = await result.Content.ReadAsStringAsync();
                                List<CastResponse> castResponseList = JsonConvert.DeserializeObject<List<CastResponse>>(castResult);

                                castModelList = new List<CastModel>();

                                foreach (var castResponse in castResponseList)
                                {
                                    CastModel castModel = new CastModel();
                                    castModel.Id = castResponse.Person.id;
                                    castModel.Name = castResponse.Person.name;
                                    castModel.Birthday = castResponse.Person.birthday;
                                    castModelList.Add(castModel);
                                }

                                tvShowModel.Cast = castModelList.OrderByDescending(x => x.Birthday).ToList();
                            }

                            tvShowModelList.Add(tvShowModel);
                        }
                    }
                }

                _applicationService.InsertTvShowCollection(tvShowModelList);
                resultMessage = "Success";
            }
            catch (Exception ex)
            {
                resultMessage = ex.Message;
            }

            return Ok(resultMessage);
        }

    }
}
