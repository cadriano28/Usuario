/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Usuario.Models;

namespace Usuario.Controllers
{
    public class UsersController : Controller
    {
        private readonly Contexto _context;

        public UsersController(Contexto context)
        {
            _context = context;
        }

        // Ação para exibir o formulário de login
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult DetailsAdm()
        {
            return View();
        }

        // Ação para exibir a página de administrador
        public async Task<IActionResult> Adm()
        {
            // Verifica se o usuário está logado
            if (HttpContext.Session.GetString("USERLOGADO") != null)
            {
                // Obtém o ID do usuário da sessão
                string userId = HttpContext.Session.GetString("USERLOGADO");

                // Verifica se o ID é válido
                if (!string.IsNullOrEmpty(userId))
                {
                    // Obtém informações do usuário logado do banco de dados
                    var pessoaLogada = await _context.Users.FirstOrDefaultAsync(m => m.UserId == int.Parse(userId));

                    if (pessoaLogada != null)
                    {
                        // Exclui o usuário logado da lista de usuários
                        var users = await _context.Users.Where(u => u.UserId != pessoaLogada.UserId).ToListAsync();

                        return View(users);
                    }
                }
            }
            else
            {
                // Redireciona para a página de login se o usuário não estiver logado
                return RedirectToAction("Login", "Users");
            }

            // Retorna um problema se algo der errado
            return Problem("Ocorreu um erro inesperado.");
        }

        // Ação para processar o login do usuário
        [HttpPost]
        public async Task<IActionResult> Login(string UserLogin, string UserSenha, string UserNivel)
        {
            string usuario = UserLogin;
            string senha = UserSenha;
            string nivel = UserNivel;

            // Busca o usuário no banco de dados pelo nome de usuário
            var pessoa = await _context.Users.FirstOrDefaultAsync(m => m.UserLogin == usuario);

            // Verifica se o usuário foi encontrado e se a senha está correta
            if (pessoa != null && pessoa.UserSenha == senha && pessoa.UserNivel == "Administrativo")
            {
                // Define o ID do usuário na sessão
                HttpContext.Session.SetString("USERLOGADO", pessoa.UserId.ToString());

                // Redireciona para a página de administrador após o login
                return RedirectToAction("Adm", "Users");
            }
            else if (pessoa != null && pessoa.UserSenha == senha)
            {
                // Define o ID do usuário na sessão
                HttpContext.Session.SetString("USERLOGADO", pessoa.UserId.ToString());

                // Redireciona para a página inicial após o login
                return RedirectToAction("Index", "Users");
            }
            // Redireciona para a página de login se o login falhar
            return RedirectToAction("Login", "Users");
        }

        // Método para escrever um cookie
        public IActionResult WriteCookie(string value)
        {
            Response.Cookies.Append("LOGADO", value, new Microsoft.AspNetCore.Http.CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(7)
            });
            return Ok("Cookie gravado com sucesso!");
        }

        // Ação para exibir a página inicial
        public async Task<IActionResult> Index()
        {
            // Verifica se o usuário está logado
            if (HttpContext.Session.GetString("USERLOGADO") != null)
            {
                string userId = HttpContext.Session.GetString("USERLOGADO");

                if (!string.IsNullOrEmpty(userId))
                {
                    // Obtém informações do usuário logado do banco de dados
                    var pessoaLogada = await _context.Users.FirstOrDefaultAsync(m => m.UserId == int.Parse(userId));

                    if (pessoaLogada != null)
                    {
                        // Exibe os detalhes do usuário logado
                        return View(new List<Users> { pessoaLogada });
                    }
                }
            }
            else
            {
                // Redireciona para a página de login se o usuário não estiver logado
                return RedirectToAction("Login", "Users");
            }
            // Redireciona para a página de login se o usuário não estiver logado
            return RedirectToAction("Login", "Users");
        }

        // Ação para exibir o formulário de criação de usuário
        public IActionResult Create()
        {
            return View();
        }

        // Ação para exibir os detalhes de um usuário
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // Ação para processar o formulário de criação de usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserNome,UserCpf,UserTelefone,UserLogin,UserSenha,UserNivel,UserStatus")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        [HttpGet]
        // Ação para exibir o formulário de edição de usuário
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditAdm(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // Ação para processar o formulário de edição de usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdm(int id, [Bind("UserId,UserNome,UserCpf,UserTelefone,UserLogin,UserSenha,UserNivel,UserStatus")] Users users)
        {
            if (id != users.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Adm));
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserNome,UserCpf,UserTelefone,UserLogin,UserSenha,UserNivel,UserStatus")] Users users)
        {
            if (id != users.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }


        // Ação para exibir a página de confirmação de exclusão de usuário
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // Ação para processar a exclusão de usuário
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("O conjunto de entidades 'Contexto.Users' é nulo.");
            }
            var users = await _context.Users.FindAsync(id);
            if (users != null)
            {
                _context.Users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método para verificar se um usuário existe
        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}*/


//NOVO CÓDIGO ABAIXO:

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Usuario.Models;

namespace Usuario.Controllers
{
    public class UsersController : Controller
    {
        private readonly Contexto _context;

        public UsersController(Contexto context)
        {
            _context = context;
        }

        // Ação para exibir o formulário de login
        public IActionResult Login()
        {
            return View();
        }

        // Ação para processar o login do usuário
        [HttpPost]
        public async Task<IActionResult> Login(string UserLogin, string UserSenha)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserLogin == UserLogin && u.UserSenha == UserSenha);
            if (user != null)
            {
                // Define o ID do usuário na sessão
                HttpContext.Session.SetString("USERLOGADO", user.UserId.ToString());
                HttpContext.Session.SetString("USERNIVEL", user.UserNivel.ToString());

                if (user.UserNivel == NivelAcesso.Administrador)
                {
                    // Redireciona para a página de administrador após o login
                    return RedirectToAction("Adm", "Users");
                }
                else
                {
                    // Redireciona para a página inicial após o login
                    return RedirectToAction("Index", "Users");
                }
            }
            // Redireciona para a página de login se o login falhar
            return RedirectToAction("Login", "Users");
        }

        // Ação para exibir a página de administrador
        public async Task<IActionResult> Adm()
        {
            if (HttpContext.Session.GetString("USERLOGADO") != null && HttpContext.Session.GetString("USERNIVEL") == NivelAcesso.Administrador)
            {
                var users = await _context.Users.ToListAsync();
                return View(users);
            }
            else
            {
                // Redireciona para a página de login se o usuário não estiver logado ou não for administrador
                return RedirectToAction("Login", "Users");
            }
        }

        // Ação para exibir a página inicial
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("USERLOGADO") != null)
            {
                string userId = HttpContext.Session.GetString("USERLOGADO");
                var user = _context.Users.FirstOrDefault(u => u.UserId.ToString() == userId);

                if (user != null)
                {
                    return View(user);
                }
            }
            // Redireciona para a página de login se o usuário não estiver logado
            return RedirectToAction("Login", "Users");
        }

        // Ação para processar o logout do usuário
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Users");
        }
    }
}

