using Caelum.Blog.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Caelum.Blog.WebApp.Data;


namespace Caelum.Blog.WebApp.Controllers
{
    public class PostsController : Controller
    {
       
        public ViewResult Index()
        {
            var lista = new PostDAO().Listar();
            return View(lista);
        }

        public ViewResult Novo()
        {
            return View(new Post());
        }

        [HttpPost]
        public IActionResult Inclusao(Post novoPost)
        {
            

            if (ModelState.IsValid)
            {
                var dao = new PostDAO();
                dao.Incluir(novoPost);
                return RedirectToAction("Index");
            }
            return View("Novo", novoPost);

        }

       

        public IActionResult Editar (int id)
        {
            var dao = new PostDAO();
            var post = dao.BuscaPorId(id);
            return View(post);
        }

        [HttpPost]

        public IActionResult Alteracao(Post post)

        {
            if (ModelState.IsValid)
            {
                var dao = new PostDAO();
                dao.Atualizar(post);
                return RedirectToAction("Index");
            }
            return View("Editar", post);
        }
        

        public IActionResult Excluir (int id)
        {
            var dao = new PostDAO();
            var post = dao.BuscaPorId(id);
            dao.Apagar(post);
            return RedirectToAction("Index");
        }

        public IActionResult Categoria([Bind(Prefix ="id")] string cat)
        {
            var dao = new PostDAO();
            var posts = dao.BuscaPorCategoria(cat);
            return View("Index",posts);
        }

        public IActionResult PublicaPost(int id)
        {
            var dao = new PostDAO();
            var post = dao.BuscaPorId(id);

            post.DataPublicacao = DateTime.Today;
            post.Publicado = true;

            dao.Atualizar(post);

            return RedirectToAction("Index");
        }
    }
}
