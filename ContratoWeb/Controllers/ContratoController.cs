using ContratoWeb.Models;
using System;
using System.Web.Mvc;

namespace ContratoWeb.Controllers
{
    public class ContratoController : Controller
    {
        UsuarioAplicacao appUsuario;

        public ContratoController()
        {
            appUsuario = UsuarioAplicacaoContrutor.usuaroApADO();
        }

        // GET: Contrato
        [Authorize]
        public ActionResult Index()
        {
            var contrato = appUsuario.bllRetListaContratos();
           
            return View(contrato);

        }

        // GET: Contrato/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var contrato = appUsuario.ListarPorId(id);

            Session["saldoContratoSession"] = contrato.SALDO;

            return View(contrato);
        }

        // GET: Contrato/Create
        [Authorize]
        public ActionResult Create(int id)
        {
            return View();
        }

        // POST: Contrato/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Contrato/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var contrato = appUsuario.ListarPorId(id);

            return View(contrato);
        }

        [Authorize]
        public ActionResult Pesquisar()
        {          
            return View();
           
        }


        // POST: Contrato/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult ExecutarPesquisa(int NRO_CONTRATO)
        {   
            var contrato = appUsuario.bllRetListaContratosBancoConsinco(NRO_CONTRATO);
          
            return View(contrato);
        }

        [Authorize]
        public ActionResult Lancamento(int nrocont, int nroloja, decimal vlrcont)
        {
            DominioContrato contrato = appUsuario.retornaContratoParaEdicao(nrocont, nroloja, vlrcont);

            if (!appUsuario.existeContrato(nrocont, nroloja))
            {   
                Session["saldoContratoSession"] = vlrcont;
                contrato = appUsuario.retornaContratoParaEdicaoContratosNaoLancados(nrocont, nroloja, vlrcont);
                contrato.SALDO = vlrcont;

                return View(contrato);

            }
                      

            return View(contrato);

        }


        [HttpPost]
        [Authorize]
        public ActionResult ExecutarPesquisaPorData(DateTime dtaInicial, DateTime dtaFinal, bool checagem)
        {           
            var contrato = appUsuario.retornaContratoIndexPorData(dtaInicial, dtaFinal, checagem);

            return View(contrato);
        }

      

    }
}
