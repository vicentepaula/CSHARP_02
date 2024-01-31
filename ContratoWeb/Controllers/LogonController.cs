using ContratoWeb.Models.usuario;
using System.Web.Mvc;
using System.Web.Security;

namespace ContratoWeb.Controllers
{
    public class LogonController : Controller
    {
        UsuAplicacaoLogon applogon;
                      
        public LogonController()
        {
            applogon = UsuContrutorDominioLogon.logonaplicacao();
        }
             
        [HttpPost]
        [AllowAnonymous]
        public ActionResult logonSistema(DominioLogon logon, string returnUrl)
        {   
            if (!ModelState.IsValid)
            {
                return View(logon);
            }
            var acesso = applogon.capturaUsuario(logon.nome, logon.senha);

            if (acesso != null)
            {
                    //Session["usuLogado"] = acesso.nome;

                FormsAuthentication.SetAuthCookie(acesso.nome, true);
                if (Url.IsLocalUrl(returnUrl))
                {   //Não estou redirecionando a returnUrl pois nesta aplicação é inviável
                    return RedirectToAction("../Contrato/Index");
                }
                else
                {
                    return RedirectToAction("../Contrato/Index");
                }
                                           
               
            }
            else
            {
               ModelState.AddModelError("","Login Inválido");
            }
            return View(logon);
        }
                

        [HttpGet]
        [AllowAnonymous]
        public ActionResult logonSistema(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [AllowAnonymous]
        public ActionResult sair()
        {
            System.Web.Security.FormsAuthentication.SignOut();
         
            return RedirectToAction("logonSistema");
        }


        [HttpGet]
        [Authorize]
        public ActionResult CadastroUsuario()
        {

            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult CadastroUsuario(DominioLogon usu)
        {
             var resul = applogon.cadastrarUsuario(usu.nome, usu.senha );

            if (resul == 1)
            {
          //      ViewBag.cadastroResul = "Cadastro Realizado com sucesso";
            }
            else
            {
          //      ViewBag.cadastroResul = "Falha ao realizar cadastro";
            }

            return  RedirectToAction("CadastroUsuario"); 
        }
    }
}
