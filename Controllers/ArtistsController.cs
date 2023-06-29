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
    public class ArtistsController : ControllerBase
    {
        private ApiDbContext _db;
        public ArtistsController(ApiDbContext db) {  _db = db; }

        // POST api/<SongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Artist artist)
        {
            var imageUrl = await UploadImage(artist.Image);
            artist.ImageUrl = imageUrl;
            await _db.Artists.AddAsync(artist);
            await _db.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetArtists(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 1;
            var artists = await (from artist in _db.Artists
                                  select new
                                  {
                                      Id = artist.Id,
                                      Name = artist.Name,
                                      ImageUrl = artist.ImageUrl,
                                  }).ToListAsync();
            return Ok(artists.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ArtistDetails(int artistId)
        {
            var ArtistSongs = await _db.Artists.Where(artist => artist.Id == artistId).Include(artist => artist.Songs).ToListAsync();
            return Ok(ArtistSongs);
        }

        private static async Task<string> UploadImage(IFormFile? file)
        {
            string containerName = "songscover";
            return await FileHelper.UploadToBlobStorage(containerName, file);
        }
    }
}
