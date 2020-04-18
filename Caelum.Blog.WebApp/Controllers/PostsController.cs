using Caelum.Blog.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Caelum.Blog.WebApp.Controllers
{
    public class PostsController : Controller
    {
        IList<Post> lista = new List<Post>();

        public PostsController()
        {
        }

        public ViewResult Index()
        {
            IList<Post> lista = new List<Post>();

            // ADO.NET
            var stringCnx = "Server=(localdb)\\MSSQLLocalDB;Database=BlogCaelum;Trusted_Connection=true";
            
            // conexão com o BD
            IDbConnection conexaoBD = new SqlConnection(stringCnx);
            conexaoBD.Open();

            // consulta: select * from Posts
            IDbCommand select = conexaoBD.CreateCommand();
            select.CommandText = "select * from Posts";

            // resultado
            IDataReader leitor = select.ExecuteReader();

            while (leitor.Read())
            {
                Post post = new Post();
                post.Id = Convert.ToInt32(leitor["Id"]);
                post.Titulo = Convert.ToString(leitor["Titulo"]);
                post.Resumo = Convert.ToString(leitor["Resumo"]);
                post.Categoria = Convert.ToString(leitor["Categoria"]);
                lista.Add(post);
            }

            leitor.Close();
            conexaoBD.Close();

            return View(lista);
        }

        public ViewResult Novo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Inclusao(Post novoPost)
        {
            // ADO.NET
            var stringCnx = "Server=(localdb)\\MSSQLLocalDB;Database=BlogCaelum;Trusted_Connection=true";

            // conexão com o BD
            IDbConnection conexaoBD = new SqlConnection(stringCnx);
            conexaoBD.Open();

            // consulta: select * from Posts
            IDbCommand insert = conexaoBD.CreateCommand();
            insert.CommandText = $"insert into Posts (titulo, resumo, categoria) values ('{novoPost.Titulo}', '{novoPost.Resumo}', '{novoPost.Categoria}')";

            insert.ExecuteNonQuery();

            conexaoBD.Close();

            return RedirectToAction("Index");
        }
    }
}
