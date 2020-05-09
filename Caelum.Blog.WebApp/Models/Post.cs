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


        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(50, MinimumLength = 3)]
        public string Titulo { get; set; }

        [StringLength(250)]
        [Required]
        public string Resumo { get; set; }

        [StringLength(50)]
        [Required]
        public string Categoria { get; set; }
        
        public DateTime? DataPublicacao { get; set; }
        public bool Publicado { get; set; }
    }
}
