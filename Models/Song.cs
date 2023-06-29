﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicApi.Models
{
    public class Song
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public int? Duration { get; set; }
        public DateTime? UploadedDate { get; set; }
        public bool IsFeatured { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? AudioFile { get; set; }
        public string? AudioUrl { get; set; }
        public int? ArtistId { get; set; }
        public int? AlbumId { get; set;}
    }
}
