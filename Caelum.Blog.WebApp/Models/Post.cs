using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Caelum.Blog.WebApp.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Titulo { get; set; }
        [Required]
        [StringLength(250)]
        public string Resumo { get; set; }
        [Required]
        [StringLength(50)]
        public string Categoria { get; set; }
        public DateTime? DataPublicacao { get; set; }
        public bool Publicado { get; set; }
    }
}
