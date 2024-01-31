using ContratoWeb.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;


namespace ContratoWeb.contrato
{
    public interface IRepositorio<T>where T: class
    {

        OracleDataReader bllRetornaLojas();
        IEnumerable<DominioContrato> bllRetListaContratosBancoConsinco(int nrocontrato);
        IEnumerable<DominioContrato> bllRetListaContratos();
        DominioContrato ListarPorId(int id);
        void SalvarContrato(DominioContrato contrato);
        void inserirContrato(DominioContrato contrato); //REMOVER ESSA ENTRADA
        void updateTbContrato(DominioContrato contrato); // REMOVER ESSA ENTRADA
        bool existeContrato(DominioContrato contrato); //VERIFICAR SE ESTA ENTRADA VAI SER REMOVIDA
        List<DominioContrato> retornaContratoParaEdicao(int id);//
        DominioContrato retornaContratoParaEdicao(int nrocont, int nroloja, decimal vlrcontrato);
        OracleDataReader retornaContratoSemIdAcao();
        string retornaSaldoContrado();
        OracleDataReader retornaContratoIndex();
        void retornaValorTotalContrato();
        IEnumerable<DominioContrato> retornaContratoIndexPorData(DateTime dataInicial, DateTime dataFinal, bool controle);
        OracleDataReader carregaContratoSemId_Index(int nrocont, int nroempr, decimal saldo);
        OracleDataReader carregaTelaContratoPag_load();
        void inserirTbTempContrato();
        bool retornaValoresTbTempContrato();
        void deletarDadosTbTemp();
        string retornaNomeFornecedor();
        string retornaNomeEmpresa(int nroEmpresa);
        DominioContrato ListarPorId_CONTRATO(int id); // sem uso
        bool existeContrato(int nrocont, int nroloja);
        DominioContrato retornaContratoBancoC5_Por_NROcontrato_NRO_Loja(int NRO_CONTRATO, int nroloja);
        DominioContrato retornaContratoParaEdicaoContratosNaoLancados(int nrocont, int nroloja, decimal vlrcontrato);



    }
}
