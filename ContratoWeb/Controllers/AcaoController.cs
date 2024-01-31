using ContratoWeb.Models;
using ContratoWeb.Models.LOJAS;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ContratoWeb.Controllers
{
    public class AcaoController : Controller
    {       
        UsuarioACAOAplicacao appAcao;
        
        public AcaoController()
        {
            appAcao = UsuarioAcaoConstrutor.usuAcaoApp();

        }


        //GET: Acao/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            var acao = appAcao.ListarPorId(id);

            return View(acao);
        }


        //GET: Acao
        [Authorize]
        public ActionResult Create(int id)
        {   
            var dta = DateTime.Now;
            var acao = appAcao.ListarPorId(id);

            Session["IdAcao_tbContrato"] = id;

            var usappLoja = UsuarioLojaConstrutor.ExceUsauaioLoja();
            ViewBag.ListaLojas = new SelectList(usappLoja.bllRetornaLojas(), "empresa", "empresa");

            
            var acaoRetorno = new DominioAcao()
            {
                NRO_CONTRATO = acao.NRO_CONTRATO,
                NROEMPRESA = acao.NROEMPRESA,
                DTA_ACAO = dta,
                SALDO_CONTRATO = (decimal)Session["saldoContratoSession"]

            };
            acao = null;
            return View(acaoRetorno);
        }


        

        private DominioContrato dadosContrato(int id)
        {
            var appUsuApli = UsuarioAplicacaoContrutor.usuaroApADO();
            var contrato = appUsuApli.ListarPorId(id);
           
            return contrato;
        }

        [Authorize]
        public ActionResult ListaAcoes(int NR)
        {    var acao = appAcao.ListaAcaoPorNROcontratoNroLoja(NR);
           
             return View(acao);
        }


        //POST: Contrato/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(DominioAcao acao)
        {
            var appContrato = UsuarioAplicacaoContrutor.usuaroApADO();

            int id = (int)Session["IdAcao_tbContrato"];
            decimal vlrAcao = acao.VALOR_ACAO;
            string valorRequestNomeLoja = Request["ListaLojas"];
            Session.Remove("IdAcao_tbContrato");
           // Session["IdAcao_tbContrato"] = null;
                      

            DominioContrato contrato = dadosContrato(id);


            if (acao != null)
            {
                int id_contrato = appAcao.InsereRetornarIDContrato(acao.NRO_CONTRATO);
                acao.ID_CONTRATO = id_contrato;
                acao.NomeEmpresa = valorRequestNomeLoja;

           //     (decimal)Session["saldoContratoSession"]

                if (acao.VALOR_ACAO > 0 && acao.VALOR_ACAO <= acao.SALDO_CONTRATO)
                {
                    appAcao.Salvar(acao);
                }
                else
                {   // Essa linha vai ser alterada, redirecionar para a pagina de lançamento com uma mensagem de erro
                    return Redirect("~/Contrato/Pesquisar");
                }
               

                DominioAcao acaoInserida = appAcao.ListarPorId_Contrato(id_contrato);
                var dta = DateTime.Now;

                if (vlrAcao > 0 && vlrAcao <= contrato.SALDO)
                {
                    contrato.SALDO = contrato.SALDO - vlrAcao;

                    var tempoOejeto = new DominioContrato()
                    {
                        ID_ACAO = id,
                             
                        SALDO = contrato.SALDO,
                     

                    };
                    appContrato.SalvarContrato(tempoOejeto);

                }

            }

            return Redirect("~/Contrato/Index");

        }

        [Authorize]
        public ActionResult LancaContratoNovo(int nrocont, int nroloja, decimal saldo , decimal vlrcont)
        {
            var dta = DateTime.Now;
            DominioAcao dominioAcao = null;
          //Session["saldoContratoSession"] = saldo;

            var appUsuApli = UsuarioAplicacaoContrutor.usuaroApADO();
                                                                                                        
            var usappLoja = UsuarioLojaConstrutor.ExceUsauaioLoja();

            ViewBag.ListaLojas = new SelectList(usappLoja.bllRetornaLojas(), "empresa", "empresa");

            if (!appUsuApli.existeContrato(nrocont, nroloja))
            {
                //CONTRATOS QUE NÃO EXISTEM NA TABELA CONTRATO DO BANCO BD_CONTRATO
                dominioAcao = new DominioAcao()
                {
                    NRO_CONTRATO = nrocont,
                    NROEMPRESA = nroloja,
                 //   DTA_ACAO = dta,
                    SALDO_CONTRATO = vlrcont
                };

            }
            else
            {   //CONTRATOS QUE JÁ EXISTEM NA TABELA CONTRATO DO BANCO BD_CONTRATO
                  DominioContrato contrato =  appUsuApli.retornaContratoParaEdicao(nrocont, nroloja, vlrcont);
                  return RedirectToAction("../Contrato/Details", new { id = contrato.ID_ACAO});
                             
            }
                              

            return View(dominioAcao);
        }

      
        [HttpPost]
        [Authorize]
        public ActionResult CriarContrato(DominioAcao acao)
        {
            var dta = DateTime.Now;
            DominioContrato contrato = null;
            int nrolojaorigemcontrato = acao.NROEMPRESA; //Loja de origem do contrato na banco da c5

            var appUsuApli = UsuarioAplicacaoContrutor.usuaroApADO();

            if (!appUsuApli.existeContrato(acao.NRO_CONTRATO, acao.NROEMPRESA))
            {
                string valorRequestNomeLoja = Request["ListaLojas"];
                decimal valorSaldoSession = (decimal)Session["saldoContratoSession"];

                Session.Remove("ListaLojas");
                Session.Remove("saldoContratoSession");

               // Session["ListaLojas"] = null;
               // Session["saldoContratoSession"] = null;


               //string numeroLojaString = valorRequestNomeLoja.Substring(0, valorRequestNomeLoja.IndexOf("-"));

                int id_contrato = appAcao.InsereRetornarIDContrato(acao.NRO_CONTRATO);
                acao.ID_CONTRATO = id_contrato;

            
                acao.NomeEmpresa = valorRequestNomeLoja;


                if (acao.VALOR_ACAO > 0 && acao.VALOR_ACAO <= acao.SALDO_CONTRATO)
                {
                    appAcao.Salvar(acao);
                }
                else
                {   // Essa linha vai ser alterada, redirecionar para a pagina de lançamento com uma mensagem de erro
                    return Redirect("~/Contrato/Pesquisar");
                }
             

                DominioAcao acaoRecuperada =  appAcao.ListarPorId_Contrato(id_contrato);
                DominioContrato contr = appUsuApli.retornaContratoBancoC5_Por_NROcontrato_NRO_Loja(acaoRecuperada.NRO_CONTRATO, nrolojaorigemcontrato);

                contrato = new DominioContrato()
                {
                    NRO_CONTRATO = acaoRecuperada.NRO_CONTRATO,
                    COD_FORNECEDOR = contr.COD_FORNECEDOR,
                    NROEMPRESA = acaoRecuperada.NROEMPRESA,
                    COD_GESTOR = contr.COD_GESTOR,
                    VALOR_CONTRATO = contr.VALOR_CONTRATO,
                    NRO_NF = acaoRecuperada.NRO_NF,
                    SALDO = valorSaldoSession - acao.VALOR_ACAO,
                    ID_ACAO = acaoRecuperada.ID_ACAO,
                    DTA_CONTRATO = dta,
                };

                appUsuApli.SalvarContrato(contrato);
            };

            return Redirect("~/Contrato/Index");

        }



    }


    
}
