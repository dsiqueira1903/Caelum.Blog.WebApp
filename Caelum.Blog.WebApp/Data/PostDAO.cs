using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caelum.Blog.WebApp.Models;

namespace Caelum.Blog.WebApp.Data
{
    public class PostDAO
    {
        public void Incluir(Post novoPost)
        {
            BlogContext ctx = new BlogContext();
            ctx.Posts.Add(novoPost);
            ctx.SaveChanges();
        }

        public void Atualizar(Post post)
        {
            BlogContext ctx = new BlogContext();
            ctx.Posts.Update(post);
            ctx.SaveChanges();
        }

        public void Apagar(Post post)
        {
            BlogContext ctx = new BlogContext();
            ctx.Posts.Remove(post);
            ctx.SaveChanges();
        }


        

        public IList<Post> Listar()
        {
            BlogContext ctx = new BlogContext();
            return ctx.Posts.ToList();
        }

        public Post BuscaPorId(int id)
        {
            BlogContext ctx = new BlogContext();
            return ctx.Posts.Find(id);
        }

        public IList<Post> BuscaPorCategoria(string cat)
        {
            BlogContext ctx = new BlogContext();
            return ctx.Posts.Where(p => p.Categoria.Contains(cat)).ToList();
        }

        public void Publica(int id)
        {
            using (BlogContext contexto = new BlogContext())
            {
                Post post = contexto.Posts.Find(id);
                post.DataPublicacao = DateTime.Today;
                contexto.SaveChanges();
            }
        }

    }
}
