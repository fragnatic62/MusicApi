using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Helpers;
using MusicApi.Models;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private ApiDbContext _db;
        public SongsController(ApiDbContext db) { _db = db; }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Song song)
        {
            var imageUrl = await UploadImage(song.Image);
            var audioUrl = await UploadAudioFile(song.AudioFile);
            song.ImageUrl = imageUrl;
            song.AudioUrl = audioUrl;
            song.UploadedDate = DateTime.Now;
            await _db.Songs.AddAsync(song);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs(int? pageNumber, int? pageSize) // pageNumber = 1, pageSize=5
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 2;
            var songs = await (from song in _db.Songs
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   Duration = song.Duration,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl,
                               }).ToListAsync();
            return Ok(songs.Skip((currentPageNumber-1) * currentPageSize).Take(currentPageSize));
            // pageNumber(1) - 1 = 0 then * 5 = skip(0), take(pageSize(5))
            // pageNumber(2) - 1 = 1 then * 5 = skip(5), take(pageSize(5))
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FeaturedSongs()
        {
            var songs = await (from song in _db.Songs
                               where song.IsFeatured == true
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   Duration = song.Duration,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl,
                               }).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewSongs()
        {
            var songs = await (from song in _db.Songs
                               orderby song.UploadedDate descending
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   Duration = song.Duration,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl,
                               }).Take(10).ToListAsync();
            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchSongs(string query)
        {
            var songs = await (from song in _db.Songs
                               where song.Title.StartsWith(query)
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   Duration = song.Duration,
                                   ImageUrl = song.ImageUrl,
                                   AudioUrl = song.AudioUrl,
                               }).Take(10).ToListAsync();
            return Ok(songs);
        }

        private static async Task<string> UploadImage(IFormFile? file)
        {
            string containerName = "songscover";
            return await FileHelper.UploadToBlobStorage(containerName, file);
        }

        private static async Task<string> UploadAudioFile(IFormFile? file)
        {
            string containerName = "audiofiles";
            return await FileHelper.UploadToBlobStorage(containerName, file);
        }
    }
}
