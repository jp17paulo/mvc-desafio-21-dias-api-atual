using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;

namespace mvc.Controllers
{
    public class AlunosController : Controller
    {
        private readonly ILogger<AlunosController> _logger;

        public AlunosController(ILogger<AlunosController> logger)
        {
            _logger = logger;
        }

        [Route("/alunos")]
        public IActionResult Index()
        {
            return View(Aluno.Todos());
        }

        [Route("/alunos/Novo")]
        public IActionResult Novo()
        {
            return View();
        }


        [Route("/alunos/Incluir")]
        [HttpPost]
        public IActionResult Incluir(Aluno aluno)
        {
            //Console.WriteLine(aluno.Nome);
            aluno.Salvar();
            return Redirect("/alunos");
        }


        [Route("/alunos/Editar")]
        [HttpGet]
        public IActionResult Editar(int id)
        {
            var aluno = Aluno.BuscarPorId(id);
            ViewBag.Aluno = aluno;
            return View();
        }

        [Route("/alunos/Editar")]
        [HttpPost]
        public IActionResult Editar(Aluno aluno)
        {
            aluno.Salvar();
            return Redirect("/alunos");
        }

        [Route("/alunos/Excluir")]
        public void Excluir(int id)
        {
            //Console.WriteLine(aluno.Nome);
            Aluno.ApagarPorId(id);

            Response.Redirect("/alunos");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}