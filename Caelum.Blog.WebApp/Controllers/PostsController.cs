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

        private IActionResult ViewForm(Post model)
        {
            return View("Form", model);
        }
        [HttpGet]
        public IActionResult Novo()
        {
            return ViewForm(new Post());
        }

        [HttpPost]
        public IActionResult Novo(Post novoPost)
        {
            

            if (ModelState.IsValid)
            {
                var dao = new PostDAO();
                dao.Incluir(novoPost);
                return RedirectToAction("Index");
            }
            return ViewForm(novoPost);

        }

       
        [HttpGet]
        public IActionResult Editar (int id)
        {
            var dao = new PostDAO();
            var post = dao.BuscaPorId(id);
            return ViewForm(post);
        }

        [HttpPost]

        public IActionResult Editar(Post post)

        {
            if (ModelState.IsValid)
            {
                var dao = new PostDAO();
                dao.Atualizar(post);
                return RedirectToAction("Index");
            }
            return ViewForm(post);
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

        public IActionResult CategoriaAutocomplete(string term)
        {
            var dao = new PostDAO();
            var categorias = dao.Listar()
                .Where(p => p.Categoria.Contains(term))
                .Select(p => p.Categoria)
                .Distinct();
            return Json(categorias);

        }
    }
}
