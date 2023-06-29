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
    public class AlbumsController : ControllerBase
    {
        private ApiDbContext _db;
        public AlbumsController(ApiDbContext db) { _db = db; }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Album album)
        {
            var imageUrl = await UploadImage(album.Image);
            album.ImageUrl = imageUrl;
            await _db.Albums.AddAsync(album);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await (from album in _db.Albums
                                 select new
                                 {
                                     Id = album.Id,
                                     Name = album.Name,
                                     ImageUrl = album.ImageUrl,
                                 }).ToListAsync();
            return Ok(albums);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> AlbumDetails(int albumId)
        {
            var albumDetails = await _db.Albums.Where(album => album.Id == albumId).Include(album => album.Songs).ToListAsync();
            return Ok(albumDetails);
        }

        private static async Task<string> UploadImage(IFormFile? file)
        {
            string containerName = "songscover";
            return await FileHelper.UploadToBlobStorage(containerName, file);
        }
    }
}
