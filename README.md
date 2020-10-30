# TvMazeAPI
.Net Core WebAPI with MongoDB

TvMazeScraper contains TvMaze.Scraper.Tests, TvMazeScraper that is .Net Core WebAPI and TvMazeService.

TvMazeScraper

HttpGet 
 1)  api/tvshow which is Get action in TvShowController and it requests all TvShowModel data from MongoDb.
  
   Example : https://localhost:44336/api/tvshow
 
 2) api/tvshow/{id} which is another Get action in TvShowController and it requests only one TvShowModel from MongoDb.

 Example : https://localhost:44336/api/tvshow/1
 
 3) api/tvshow/{pageSize}/{pageNumber} which is another Get action in TvShowController and it requests some data depending on pageSize and pageNumber parameters from MongoDb.

 Example : https://localhost:44336/api/tvshow/10/1
 
 
HttpPost
 
 4) api/tvshow which is Post action in TvShowController and it sends TvShowModel data to MongoDB
 
  Example : https://localhost:44336/api/tvshow with Post
  
  When it sends data,it is using http://api.tvmaze.com/show api to get TvShows.
  When it sends data to MongoDB,it is also using  http://api.tvmaze.com/shows/{tvShovip}/cast to get Casts who play in TvShow.
  
   TvShowModel is being created by using TvShows and Casts to be able to create new Json data format.
