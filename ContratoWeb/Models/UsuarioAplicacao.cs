using ContratoWeb.contrato;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace ContratoWeb.Models
{
    public class UsuarioAplicacao
    {
        private readonly IRepositorio<DominioContrato> repositorio;

        public UsuarioAplicacao(IRepositorio<DominioContrato> irep)
        {
            repositorio = irep;
        }

        public IEnumerable<DominioContrato> bllRetornaLojas(string sql)
        {
            return repositorio.bllRetListaContratos();
        }

        //Lista contratos do banco contrato
        public IEnumerable<DominioContrato> bllRetListaContratos()
        {
            return repositorio.bllRetListaContratos();
        }

        public List<DominioContrato> retornaContratoParaEdicao(int id)
        {
            return repositorio.retornaContratoParaEdicao(id);
        }

        public DominioContrato ListarPorId(int id)
        {
            return repositorio.ListarPorId(id);
        }

        public void SalvarContrato(DominioContrato contrato)
        {
            repositorio.SalvarContrato(contrato);
        }

        public DominioContrato ListarPorId_CONTRATO(int id_contrato)
        {
            return repositorio.ListarPorId_CONTRATO(id_contrato);
        }

        public IEnumerable<DominioContrato> bllRetListaContratosBancoConsinco(int NRO_CONTRATO)
        {
            return repositorio.bllRetListaContratosBancoConsinco(NRO_CONTRATO);
        }

        public DominioContrato retornaContratoParaEdicao(int nrocont, int nroloja, decimal vlrcontrato)
        {
            return repositorio.retornaContratoParaEdicao(nrocont, nroloja, vlrcontrato);
        }

        public bool existeContrato(int nrocont, int nroloja)
        {
            return repositorio.existeContrato(nrocont, nroloja);
        }

        public DominioContrato retornaContratoBancoC5_Por_NROcontrato_NRO_Loja(int NRO_CONTRATO, int nroloja)
        {
            return repositorio.retornaContratoBancoC5_Por_NROcontrato_NRO_Loja(NRO_CONTRATO, nroloja);
        }

        public DominioContrato retornaContratoParaEdicaoContratosNaoLancados(int nrocont, int nroloja, decimal vlrcontrato)
        {
            return repositorio.retornaContratoParaEdicaoContratosNaoLancados(nrocont, nroloja, vlrcontrato);
        }

        public IEnumerable<DominioContrato> retornaContratoIndexPorData(DateTime dataInicial, DateTime dataFinal, bool controle)
        {
            return repositorio.retornaContratoIndexPorData(dataInicial, dataFinal, controle);
        }

    }

}