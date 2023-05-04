using DevIO.UI.Site.Data.Context;
using DevIO.UI.Site.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.UI.Site.Controllers
{
    public class AlunosController : Controller
    {
        private readonly MeuDbContext _context;

        public AlunosController(MeuDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var aluno = new Aluno
            {
                Nome = "Homer Simpson",
                DataNascimento = new DateTime(1960, 05, 15),
                Email = "homer@simpsons.com"
            };
            _context.Alunos.Add(aluno);
            _context.SaveChanges();
            return View(aluno);
        }
    }
}
