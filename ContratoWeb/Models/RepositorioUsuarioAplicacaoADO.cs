using ContratoWeb.contrato;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ContratoWeb.Models
{
    public class RepositorioUsuarioAplicacaoADO : IRepositorio<DominioContrato>
    {
        private RepositorioBD bd;

  
        //MÉTODO CHAMADO NA PÁGINA INDICE
        public IEnumerable<DominioContrato> bllRetListaContratos() //bllRetListaContratos
        {        
            using (bd = new RepositorioBD())
            {
                string sql = "SELECT c.id_acao, c.NRO_CONTRATO, c.nroempresa, c.seqfornecedor, d.empresa,g.nomerazao, a.NOME_ACAO ,c.nro_nf, c.seqgestor, f.comprador,  C.VALOR_CONTRATO,  C.SALDO_CONTRATO, TO_CHAR(c.dta_contrato, 'dd/mm/yyyy') as DATA_CONTRATO FROM usuContrato_teste.contrato C, usuContrato_teste.acao a, CONSINCO.GE_PESSOA G, CONSINCO.max_comprador F, CONSINCODW.DIM_EMPRESA D where c.nro_contrato = a.nro_contrato and c.id_acao = a.id_acao and c.seqfornecedor = g.seqpessoa AND c.seqgestor = f.seqcomprador and c.nroempresa = d.nroempresa and c.SALDO_CONTRATO > 0 order by c.dta_contrato";
             // string sql2 = "SELECT c.NRO_CONTRATO, c.nroempresa,d.empresa,g.nomerazao,c.seqgestor, f.comprador, TO_CHAR(c.VALOR_CONTRATO,'L99G999D99') as VALOR_CONTRATO, TO_CHAR(c.SALDO_CONTRATO,'L99G999D99') as SALDO_CONTRATO, TO_CHAR(c.dta_contrato,'dd/mm/yyyy') as DATA_CONTRATO FROM usuContrato_teste.contrato C, usuContrato_teste.acao a, CONSINCO.GE_PESSOA G, CONSINCO.max_comprador F, CONSINCODW.DIM_EMPRESA D where c.nro_contrato = a.nro_contrato and c.id_acao = a.id_acao and c.seqfornecedor = g.seqpessoa AND c.seqgestor = f.seqcomprador and c.nroempresa = d.nroempresa and c.SALDO_CONTRATO > 0 order by c.dta_contrato";
                
                var retorno = bd.ExecutaComandoComRetorno(sql);

                return ReaderEmLista(retorno);
            }

        }

        //Metodo de apoio que retorna listas
        public List<DominioContrato> ReaderEmLista(Oracle.ManagedDataAccess.Client.OracleDataReader reader)
        {
            var usuarios = new List<DominioContrato>();
            while (reader.Read())
            {
                var tempoOejeto = new DominioContrato()
                {
                    ID_ACAO = int.Parse(reader["ID_ACAO"].ToString()),
                    NRO_CONTRATO = int.Parse(reader["NRO_CONTRATO"].ToString()),
                    NROEMPRESA = int.Parse(reader["NROEMPRESA"].ToString()),
                    NomeEmpresa = reader["EMPRESA"].ToString(),
                    COD_FORNECEDOR = int.Parse(reader["SEQFORNECEDOR"].ToString()), /// verificar
                    NOME_ACAO = reader["nome_acao"].ToString(),
                    NomeFonecedor = reader["NOMERAZAO"].ToString(),
                    NRO_NF = reader["NRO_NF"].ToString(),
                    Comprador = reader["COMPRADOR"].ToString(),
                    COD_GESTOR = int.Parse(reader["SEQGESTOR"].ToString()),
                    VALOR_CONTRATO = decimal.Parse(reader["VALOR_CONTRATO"].ToString()),
                    SALDO = decimal.Parse(reader["SALDO_CONTRATO"].ToString()),
                    DTA_CONTRATO = DateTime.Parse(reader["DATA_CONTRATO"].ToString())

                };

                usuarios.Add(tempoOejeto);
            }
            reader.Close();

            return usuarios;
        }


        public DominioContrato ListarPorId_CONTRATO(int id)
        {
            using (bd = new RepositorioBD())
            {
                var strQuery = string.Format("select * from acao where id_contrato ='{0}'", id);
                var retorno = bd.ExecutaComandoComRetorno(strQuery);

                return ReaderEmLista(retorno).FirstOrDefault();
            }
        }


        public DominioContrato ListarPorId(int id) // TRABALHANDO COM ESTE
        {
            using (bd = new RepositorioBD())
            {
                var strQuery = string.Format("SELECT c.id_acao, c.NRO_CONTRATO, c.nroempresa,c.seqfornecedor ,d.empresa,g.nomerazao,a.nome_acao, a.NOME_ACAO , c.nro_nf,c.seqgestor, f.comprador, c.VALOR_CONTRATO, c.SALDO_CONTRATO, TO_CHAR(c.dta_contrato, 'dd/mm/yyyy') as DATA_CONTRATO FROM usuContrato_teste.contrato C, usuContrato_teste.acao a, CONSINCO.GE_PESSOA G, CONSINCO.max_comprador F, CONSINCODW.DIM_EMPRESA D where c.nro_contrato = a.nro_contrato and c.nroempresa = a.nroempresa and c.id_acao ='{0}' and c.seqfornecedor = g.seqpessoa AND c.seqgestor = f.seqcomprador and c.nroempresa = d.nroempresa order by c.dta_contrato", id);
                var retorno = bd.ExecutaComandoComRetorno(strQuery);
                                                
                return ReaderEmLista(retorno).FirstOrDefault();
            }
        }


        //SEM USO
        //Método chamado pelo botão editar da pagina index
        public List<DominioContrato> retornaContratoParaEdicao(int id)
        {
            var usuarios = new List<DominioContrato>();

            using (bd = new RepositorioBD())
            {
                string sql = "SELECT c.id_acao, c.NRO_CONTRATO, c.nroempresa,d.empresa,g.nomerazao,c.id_acao ,a.nome_acao ,c.seqgestor, f.comprador, c.VALOR_CONTRATO, c.SALDO_CONTRATO, TO_CHAR(c.dta_contrato, 'dd/mm/yyyy') as DATA_CONTRATO FROM usuContrato_teste.contrato C, usuContrato_teste.acao a, CONSINCO.GE_PESSOA G, CONSINCO.max_comprador F, CONSINCODW.DIM_EMPRESA D where c.nro_contrato = a.nro_contrato and c.nroempresa = a.nroempresa and c.id_acao ='{0}' and c.seqfornecedor = g.seqpessoa AND c.seqgestor = f.seqcomprador and c.nroempresa = d.nroempresa order by c.dta_contrato";
                sql = String.Format(sql, id);
                var reader = bd.ExecutaComandoComRetorno(sql);

                while (reader.Read())
                {
                    var tempoOejeto = new DominioContrato()
                    {
                        ID_ACAO = int.Parse(reader["ID_ACAO"].ToString()),
                        NRO_CONTRATO = int.Parse(reader["NRO_CONTRATO"].ToString()),
                        NROEMPRESA = int.Parse(reader["NROEMPRESA"].ToString()),
                        NomeEmpresa = reader["EMPRESA"].ToString(),
                        NOME_ACAO = reader["NRO_NF"].ToString(),
                        NRO_NF = reader["NOME_ACAO"].ToString(),
                        Comprador = reader["COMPRADOR"].ToString(),
                        COD_GESTOR = int.Parse(reader["SEQGESTOR"].ToString()),
                        VALOR_CONTRATO = decimal.Parse(reader["VALOR_CONTRATO"].ToString()),
                        SALDO = decimal.Parse(reader["SALDO_CONTRATO"].ToString()),
                        DTA_CONTRATO = DateTime.Parse(reader["DATA_CONTRATO"].ToString())

                    };
                    usuarios.Add(tempoOejeto);

                }
                reader.Close();

                return usuarios;
            }

        }

        //FEITA ALTERAÇÃO PARA INCLUSÃO DO SALDO, VERIFICAR POSSIVEL ERRO NESTA QUERY
        public DominioContrato retornaContratoParaEdicao(int nrocont, int nroloja, decimal vlrcontrato)
        {
            var usuarios = new List<DominioContrato>();

            using (bd = new RepositorioBD())
            {   
                string sql = "select ID_ACAO, con.NRO_CONTRATO, con.NROEMPRESA, con.SEQFORNECEDOR, C.COMPRADOR, E.EMPRESA, con.SEQGESTOR, con.VALOR_CONTRATO, con.SALDO_CONTRATO, B.NOMERAZAO, con.DTA_CONTRATO, nac.DTAINCLUSAO, nac.DESCACORDO from CONTRATO con, CONSINCO.max_comprador C, CONSINCODW.DIM_EMPRESA E, CONSINCO.GE_PESSOA B, CONSINCO.msu_acordopromoc nac where con.NRO_CONTRATO = '{0}' AND con.NROEMPRESA = '{1}' AND con.NROEMPRESA = E.NROEMPRESA and con.SEQGESTOR = C.SEQCOMPRADOR AND con.SEQFORNECEDOR = B.SEQPESSOA and con.NRO_CONTRATO = nac.NROACORDO and con.NROEMPRESA = nac.NROEMPRESA and con.SEQGESTOR = nac.SEQCOMPRADOR";
                         

                // string sql = "select ID_ACAO, con.NRO_CONTRATO, con.NROEMPRESA, con.SEQFORNECEDOR, C.COMPRADOR, E.EMPRESA, con.SEQGESTOR, con.VALOR_CONTRATO, con.SALDO_CONTRATO, B.NOMERAZAO, con.DTA_CONTRATO from CONTRATO con, CONSINCO.max_comprador C, CONSINCODW.DIM_EMPRESA E, CONSINCO.GE_PESSOA B where con.NRO_CONTRATO = '{0}' AND con.NROEMPRESA = '{1}' AND con.NROEMPRESA = E.NROEMPRESA and con.SEQGESTOR = C.SEQCOMPRADOR AND con.SEQFORNECEDOR = B.SEQPESSOA";
                //string sql = "SELECT com.ID_ACAO, nac.NROACORDO, A.SEQFORNECEDOR, C.SEQCOMPRADOR,C.COMPRADOR , nac.nroempresa, E.EMPRESA, B.NOMERAZAO,nac.descacordo ,nac.vlracordo , com.SALDO_CONTRATO, nac.dtainclusao FROM CONSINCO.msu_acordopromoc nac, CONSINCO.MAF_FORNECEDOR A, CONSINCO.GE_PESSOA B, CONSINCO.max_comprador C, CONSINCODW.DIM_EMPRESA E, contrato com WHERE nac.nroacordo = '{0}' and A.SEQFORNECEDOR = B.SEQPESSOA and E.NROEMPRESA = '{1}' and nac.VLRACORDO = '{2}' and nac.seqfornecedor = a.seqfornecedor AND nac.seqcomprador = c.seqcomprador AND codtipoacordo in (3,4,6) order by nac.nroempresa";

                DominioContrato tempoOejeto = null;

                sql = String.Format(sql, nrocont, nroloja);
                var reader = bd.ExecutaComandoComRetorno(sql);

                while (reader.Read())
                {
                    tempoOejeto = new DominioContrato()
                    {
                        ID_ACAO = int.Parse(reader["ID_ACAO"].ToString()),
                        NRO_CONTRATO = int.Parse(reader["NRO_CONTRATO"].ToString()),
                        NROEMPRESA = int.Parse(reader["NROEMPRESA"].ToString()),
                        COD_FORNECEDOR = int.Parse(reader["SEQFORNECEDOR"].ToString()),
                        Comprador = reader["COMPRADOR"].ToString(),
                        NomeEmpresa = reader["EMPRESA"].ToString(),
                        COD_GESTOR = int.Parse(reader["SEQGESTOR"].ToString()),
                        VALOR_CONTRATO = decimal.Parse(reader["VALOR_CONTRATO"].ToString()),
                        SALDO = decimal.Parse(reader["SALDO_CONTRATO"].ToString()),
                        NomeFonecedor = reader["NOMERAZAO"].ToString(),
                        DTA_CONTRATO = DateTime.Parse(reader["dtainclusao"].ToString()),
                        NOME_ACAO = reader["descacordo"].ToString(),


                        /*
                        ID_ACAO = int.Parse(reader["ID_ACAO"].ToString()),
                        NRO_CONTRATO = int.Parse(reader["NROACORDO"].ToString()),
                        NROEMPRESA = int.Parse(reader["NROEMPRESA"].ToString()),
                        COD_FORNECEDOR = int.Parse(reader["SEQFORNECEDOR"].ToString()),
                        Comprador = reader["COMPRADOR"].ToString(),
                        NomeEmpresa = reader["EMPRESA"].ToString(),
                        COD_GESTOR = int.Parse(reader["seqcomprador"].ToString()),
                        VALOR_CONTRATO = decimal.Parse(reader["vlracordo"].ToString()),
                        SALDO = decimal.Parse(reader["SALDO_CONTRATO"].ToString()),
                        NomeFonecedor = reader["NOMERAZAO"].ToString(),
                        DTA_CONTRATO = DateTime.Parse(reader["dtainclusao"].ToString()),
                        NOME_ACAO = reader["descacordo"].ToString(),
                            
                        */
                    };
                    usuarios.Add(tempoOejeto);

                }
                reader.Close();

                return tempoOejeto;
            }

        }

        
        public DominioContrato retornaContratoParaEdicaoContratosNaoLancados(int nrocont, int nroloja, decimal vlrcontrato)
        {
            var usuarios = new List<DominioContrato>();

            using (bd = new RepositorioBD())
            {
                  string sql = "select nac.NROACORDO, nac.NROEMPRESA, nac.SEQFORNECEDOR,C.COMPRADOR, E.EMPRESA, nac.SEQCOMPRADOR, nac.VLRACORDO, B.NOMERAZAO, nac.DTAINCLUSAO, nac.DESCACORDO from CONSINCO.msu_acordopromoc nac, CONSINCO.max_comprador C, CONSINCODW.DIM_EMPRESA E, CONSINCO.GE_PESSOA B where nac.NROACORDO = '{0}' and nac.NROEMPRESA = '{1}' and nac.SEQCOMPRADOR = C.SEQCOMPRADOR and nac.NROEMPRESA = E.NROEMPRESA and nac.SEQFORNECEDOR = B.SEQPESSOA";
                //string sql = "SELECT com.ID_ACAO, nac.NROACORDO, A.SEQFORNECEDOR, C.SEQCOMPRADOR,C.COMPRADOR , nac.nroempresa, E.EMPRESA, B.NOMERAZAO,nac.descacordo ,nac.vlracordo , com.SALDO_CONTRATO, nac.dtainclusao FROM CONSINCO.msu_acordopromoc nac, CONSINCO.MAF_FORNECEDOR A, CONSINCO.GE_PESSOA B, CONSINCO.max_comprador C, CONSINCODW.DIM_EMPRESA E, contrato com WHERE nac.nroacordo = '{0}' and A.SEQFORNECEDOR = B.SEQPESSOA and E.NROEMPRESA = '{1}' and nac.VLRACORDO = '{2}' and nac.seqfornecedor = a.seqfornecedor AND nac.seqcomprador = c.seqcomprador AND codtipoacordo in (3,4,6) order by nac.nroempresa";

                DominioContrato tempoOejeto = null;

                sql = String.Format(sql, nrocont, nroloja);
                var reader = bd.ExecutaComandoComRetorno(sql);

                while (reader.Read())
                {
                    tempoOejeto = new DominioContrato()
                    {                        
                        //ID_ACAO = int.Parse(reader["ID_ACAO"].ToString()), -- NAO VAI TER ID, POIS AINDA NAO FOI LANÇADO 
                        NRO_CONTRATO = int.Parse(reader["NROACORDO"].ToString()),
                        NROEMPRESA = int.Parse(reader["NROEMPRESA"].ToString()),
                        COD_FORNECEDOR = int.Parse(reader["SEQFORNECEDOR"].ToString()),
                        Comprador = reader["COMPRADOR"].ToString(),
                        NomeEmpresa = reader["EMPRESA"].ToString(),
                        COD_GESTOR = int.Parse(reader["seqcomprador"].ToString()),
                        VALOR_CONTRATO = decimal.Parse(reader["vlracordo"].ToString()),
                        //SALDO = decimal.Parse(reader["SALDO_CONTRATO"].ToString()), --SALDO VAI SER O VALOR DO CONTRATO
                        NomeFonecedor = reader["NOMERAZAO"].ToString(),
                        DTA_CONTRATO = DateTime.Parse(reader["dtainclusao"].ToString()),
                        NOME_ACAO = reader["descacordo"].ToString(),
                            
                        
                    };
                    usuarios.Add(tempoOejeto);

                }
                reader.Close();

                return tempoOejeto;
            }

        }



        public void SalvarContrato(DominioContrato contrato)
        {
            if (existeContrato(contrato.ID_ACAO))
            {
                updateTbContrato(contrato);
            }
            else
            {
                inserirContrato(contrato);
            }


        }

        public void inserirContrato(DominioContrato contrato)
        {
            string url = ("insert into usuContrato_teste.CONTRATO(contrato.nro_contrato, contrato.seqfornecedor, contrato.nroempresa, contrato.seqgestor, contrato.valor_contrato, contrato.NRO_NF, contrato.saldo_contrato, contrato.id_acao, contrato.DTA_CONTRATO)values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}', TO_DATE('{8}', 'DD-MM-YYYY hh24:mi:ss'))");
            url = string.Format(url, contrato.NRO_CONTRATO, contrato.COD_FORNECEDOR, contrato.NROEMPRESA, contrato.COD_GESTOR, contrato.VALOR_CONTRATO, contrato.NRO_NF, contrato.SALDO, contrato.ID_ACAO, contrato.DTA_CONTRATO);

            using (bd = new RepositorioBD())
            {
                bd.ExecutaCommando(url);
            }

        }



        public void updateTbContrato(DominioContrato contrato)
        {
            var strQuery = string.Format("update CONTRATO set CONTRATO.saldo_contrato = '{0}' where CONTRATO.id_acao = '{1}'", contrato.SALDO, contrato.ID_ACAO);

            using (bd = new RepositorioBD())
            {
             // bd.ExecutaCommando(url.Replace(",", "."));
                bd.ExecutaCommando(strQuery);
            }


        }



        public IEnumerable<DominioContrato> bllRetListaContratosBancoConsinco(int NRO_CONTRATO) 
        {                        
            using (bd = new RepositorioBD())
            {
                //  var strQuery = string.Format("SELECT nac.NROACORDO, nac.nroempresa, B.NOMERAZAO,c.comprador, nac.descacordo ,nac.vlracordo ,nac.dtainclusao FROM CONSINCO.msu_acordopromoc nac, CONSINCO.MAF_FORNECEDOR A, CONSINCO.GE_PESSOA B, CONSINCO.max_comprador C WHERE nac.nroacordo = '{0}' and A.SEQFORNECEDOR = B.SEQPESSOA and nac.seqfornecedor = a.seqfornecedor AND nac.seqcomprador = c.seqcomprador AND codtipoacordo in (3,4,6) order by nac.nroempresa", NRO_CONTRATO);
                  var strQuery = string.Format("SELECT nac.NROACORDO, A.SEQFORNECEDOR, C.SEQCOMPRADOR, nac.nroempresa, B.NOMERAZAO,nac.descacordo ,nac.vlracordo , nac.dtainclusao, C.comprador FROM CONSINCO.msu_acordopromoc nac, CONSINCO.MAF_FORNECEDOR A, CONSINCO.GE_PESSOA B, CONSINCO.max_comprador C, CONSINCODW.DIM_EMPRESA E WHERE nac.nroacordo = '{0}' and A.SEQFORNECEDOR = B.SEQPESSOA and nac.NROEMPRESA = E.NROEMPRESA and nac.seqfornecedor = a.seqfornecedor AND nac.seqcomprador = c.seqcomprador AND codtipoacordo in (3,4,6) order by nac.nroempresa", NRO_CONTRATO);


                var retorno = bd.ExecutaComandoComRetorno(strQuery);
                var acao = new List<DominioContrato>();
              
                while (retorno.Read())
                {
                    var tempoOejeto = new DominioContrato()
                    {                       
                        NRO_CONTRATO = int.Parse(retorno["NROACORDO"].ToString()),
                        COD_FORNECEDOR = int.Parse(retorno["SEQFORNECEDOR"].ToString()),
                        COD_GESTOR = int.Parse(retorno["seqcomprador"].ToString()),
                        NROEMPRESA = int.Parse(retorno["NROEMPRESA"].ToString()),
                        NomeFonecedor = retorno["NOMERAZAO"].ToString(),
                        NOME_ACAO = retorno["descacordo"].ToString(),
                        VALOR_CONTRATO = decimal.Parse(retorno["vlracordo"].ToString()),
                        DTA_CONTRATO = DateTime.Parse(retorno["DTAINCLUSAO"].ToString()),
                        Comprador = retorno["COMPRADOR"].ToString()

                    };

                    acao.Add(tempoOejeto);
                }
                retorno.Close();

                return acao;
                               
            }
        }

                
        private bool existeContrato(int id_acao)
        {
            var strQuery = string.Format("select * from contrato where ID_ACAO = '{0}'", id_acao);

            bool id_acao_text_contrato = false;

            using (bd = new RepositorioBD())
            {
                var result = bd.ExecutaComandoComRetorno(strQuery);

                while (result.Read())
                {
                    
                    id_acao_text_contrato = true;

                }

                return id_acao_text_contrato;

            }

        }



        public bool existeContrato(int nrocont, int nroloja)
        {
            var strQuery = string.Format("select * from contrato where nro_contrato = '{0}' and nroempresa = '{1}'", nrocont, nroloja);

            bool id_acao_text_contrato = false;

            using (bd = new RepositorioBD())
            {
              var result = bd.ExecutaComandoComRetorno(strQuery);
                                                        
                    while (result.Read())
                    {
                      //id_acao_text_contrato = reader["ID_ACAO"].ToString();
                        id_acao_text_contrato = true;

                    }
               
                return id_acao_text_contrato;

            }

        }



        public DominioContrato retornaContratoBancoC5_Por_NROcontrato_NRO_Loja(int NRO_CONTRATO, int nroloja)
        {
            DominioContrato tempoOejeto = null;

            using (bd = new RepositorioBD())
            {   var strQuery = string.Format("select nac.nroacordo, A.SEQFORNECEDOR, C.SEQCOMPRADOR, nac.nroempresa, B.NOMERAZAO, nac.descacordo, nac.vlracordo, nac.dtainclusao from  CONSINCO.msu_acordopromoc nac, CONSINCO.MAF_FORNECEDOR A ,CONSINCO.max_comprador C, CONSINCO.GE_PESSOA B where nac.nroacordo = '{0}' and nac.nroempresa = '{1}' and nac.seqfornecedor = a.seqfornecedor AND nac.seqcomprador = c.seqcomprador and a.seqfornecedor = b.seqpessoa AND codtipoacordo in (3,4,6)", NRO_CONTRATO, nroloja);
             // var strQuery = string.Format("SELECT nac.NROACORDO, A.SEQFORNECEDOR, C.SEQCOMPRADOR, nac.nroempresa, B.NOMERAZAO,nac.descacordo ,nac.vlracordo , nac.dtainclusao FROM CONSINCO.msu_acordopromoc nac, CONSINCO.MAF_FORNECEDOR A, CONSINCO.GE_PESSOA B, CONSINCO.max_comprador C, CONSINCODW.DIM_EMPRESA E WHERE nac.nroacordo = '{0}' and A.SEQFORNECEDOR = B.SEQPESSOA and nac.NROEMPRESA = '{1}' and nac.seqfornecedor = a.seqfornecedor AND nac.seqcomprador = c.seqcomprador AND codtipoacordo in (3,4,6) order by nac.nroempresa", NRO_CONTRATO, nroloja);


                var retorno = bd.ExecutaComandoComRetorno(strQuery);
                
                while (retorno.Read())
                {
                     tempoOejeto = new DominioContrato()
                     {
                        NRO_CONTRATO = int.Parse(retorno["NROACORDO"].ToString()),
                        COD_FORNECEDOR = int.Parse(retorno["SEQFORNECEDOR"].ToString()),
                        COD_GESTOR = int.Parse(retorno["seqcomprador"].ToString()),
                        NROEMPRESA = int.Parse(retorno["NROEMPRESA"].ToString()),
                        NomeFonecedor = retorno["NOMERAZAO"].ToString(),
                        NOME_ACAO = retorno["descacordo"].ToString(),
                        VALOR_CONTRATO = decimal.Parse(retorno["vlracordo"].ToString()),
                        DTA_CONTRATO = DateTime.Parse(retorno["DTAINCLUSAO"].ToString())
                                               
                    };

                   
                }
                retorno.Close();

                return tempoOejeto;

            }
        }
        
        //SEM USO
        public OracleDataReader bllRetornaLojas()
        {
            string sql = "select nroempresa, empresa from CONSINCO.dim_empresa";

            var acao = new List<DominioAcao>();
            DominioAcao tempoOejeto = null;

            using (bd = new RepositorioBD())
            {
                var retorno = bd.ExecutaComandoComRetorno(sql);

                while (retorno.Read())
                {
                    tempoOejeto = new DominioAcao()
                    {
                        NROEMPRESA = int.Parse(retorno["NROEMPRESA"].ToString()),
                        NomeEmpresa = retorno["EMPRESA"].ToString()

                    };

                    acao.Add(tempoOejeto);

                }
                retorno.Close();

            }

            return null;
        }


        public IEnumerable<DominioContrato> retornaContratoIndexPorData(DateTime dataInicial, DateTime dataFinal, bool controle)
        {
         //   string dta1 = dataInicial.ToString().Substring(0, dataInicial.ToString().IndexOf(" "));
         //   string dta2 = dataFinal.ToString().Substring(0, dataFinal.ToString().IndexOf(" "));
            string sql = null;

            var info1 = new DominioContrato()
            {
                dtainicial = dataInicial,
                dtaFinal = dataFinal
            };
           
            if (controle)
            {
               sql = "SELECT c.id_acao, c.NRO_CONTRATO, c.nroempresa, c.seqfornecedor, d.empresa,g.nomerazao, a.NOME_ACAO ,c.nro_nf, c.seqgestor, f.comprador,  C.VALOR_CONTRATO,  C.SALDO_CONTRATO, TO_CHAR(c.dta_contrato, 'DD/MM/YYYY') as DATA_CONTRATO FROM usuContrato_teste.contrato C, usuContrato_teste.acao a, CONSINCO.GE_PESSOA G, CONSINCO.max_comprador F, CONSINCODW.DIM_EMPRESA D where c.DTA_CONTRATO >= to_date('"+info1.dtainicial+"','DD-MM-YYYY hh24:mi:ss') AND c.DTA_CONTRATO < to_date('"+info1.dtaFinal+"','DD-MM-YYYY hh24:mi:ss') + 1 and c.nro_contrato = a.nro_contrato and c.id_acao = a.id_acao and c.seqfornecedor = g.seqpessoa AND c.seqgestor = f.seqcomprador and c.nroempresa = d.nroempresa order by c.dta_contrato";

            }
            else
            {
                     sql = "SELECT c.id_acao, c.NRO_CONTRATO, c.nroempresa, c.seqfornecedor, d.empresa,g.nomerazao, a.NOME_ACAO ,c.nro_nf, c.seqgestor, f.comprador,  C.VALOR_CONTRATO,  C.SALDO_CONTRATO, TO_CHAR(c.dta_contrato, 'DD/MM/YYYY') as DATA_CONTRATO FROM usuContrato_teste.contrato C, usuContrato_teste.acao a, CONSINCO.GE_PESSOA G, CONSINCO.max_comprador F, CONSINCODW.DIM_EMPRESA D where c.DTA_CONTRATO >= to_date('" + info1.dtainicial + "','DD-MM-YYYY hh24:mi:ss') AND c.DTA_CONTRATO < to_date('" + info1.dtaFinal + "','DD-MM-YYYY hh24:mi:ss') + 1 and c.nro_contrato = a.nro_contrato and c.id_acao = a.id_acao and c.seqfornecedor = g.seqpessoa AND c.seqgestor = f.seqcomprador and c.nroempresa = d.nroempresa and c.SALDO_CONTRATO > 0 order by c.dta_contrato";

                //   sql = "SELECT c.id_acao, c.NRO_CONTRATO, c.nroempresa, c.seqfornecedor, d.empresa,g.nomerazao, a.NOME_ACAO ,c.nro_nf, c.seqgestor, f.comprador,  C.VALOR_CONTRATO,  C.SALDO_CONTRATO, TO_CHAR(c.dta_contrato, 'DD/MM/YYYY') as DATA_CONTRATO FROM usuContrato_teste.contrato C, usuContrato_teste.acao a, CONSINCO.GE_PESSOA G, CONSINCO.max_comprador F, CONSINCODW.DIM_EMPRESA D where c.DTA_CONTRATO between to_date('" + info1.dtainicial + "','DD-MM-YYYY hh24:mi:ss') AND to_date('" + info1.dtaFinal + "','DD-MM-YYYY hh24:mi:ss') and c.nro_contrato = a.nro_contrato and c.id_acao = a.id_acao and c.seqfornecedor = g.seqpessoa AND c.seqgestor = f.seqcomprador and c.nroempresa = d.nroempresa and c.SALDO_CONTRATO > 0 order by c.dta_contrato";

            }


            using (bd = new RepositorioBD())
            {
                var retorno = bd.ExecutaComandoComRetorno(sql);
                return ReaderEmLista(retorno);
            }


        }


        //###############################################################################################################################################################################


        public string retornaNomeEmpresa(int nroEmpresa)
        {
            var strQuery = string.Format("select nroempresa, empresa from CONSINCO.dim_empresa", nroEmpresa);

            string nomeEmpresa = null;

            using (bd = new RepositorioBD())
            {
                var retorno = bd.ExecutaComandoComRetorno(strQuery);


                while (retorno.Read())
                {
                    nomeEmpresa = retorno["EMPRESA"].ToString();

                }

            }

            return nomeEmpresa;

        }

        public OracleDataReader retornaContratoIndex()
        {
            throw new NotImplementedException();
        }


        public OracleDataReader carregaContratoSemId_Index(int nrocont, int nroempr, decimal saldo)
        {
            throw new NotImplementedException();
        }

        public OracleDataReader carregaTelaContratoPag_load()
        {
            throw new NotImplementedException();
        }

        public void deletarDadosTbTemp()
        {
            throw new NotImplementedException();
        }

        public bool existeContrato(DominioContrato contrato) // TESTAR ESSE MÉTODO ISOLADAMENTE
        {

            string url = ("select * from contrato where nro_contrato =" + contrato.NRO_CONTRATO + " and NROEMPRESA = " + contrato.NROEMPRESA + " and seqgestor =" + contrato.COD_GESTOR);
            bool id_acao_text_contrato = false;
                                      
            using (bd = new RepositorioBD())
            {
                OracleDataReader reader = bd.ExecutaComandoComRetorno(url);
                                                       
                while (reader.Read())
                {
                    //id_acao_text_contrato = reader["ID_ACAO"].ToString();
                    id_acao_text_contrato = true;

                }
                    
                return id_acao_text_contrato;
            }             


        }
               

        public void inserirTbTempContrato()
        {
            throw new NotImplementedException();
        }


       
        public OracleDataReader retornaContratoSemIdAcao()
        {
            throw new NotImplementedException();
        }
        /*
        public string retornaNomeEmpresa(int nroEmpresa)
        {
            throw new NotImplementedException();
        }
        */
        public string retornaNomeFornecedor()
        {
            throw new NotImplementedException();
        }

        public string retornaSaldoContrado()
        {
            throw new NotImplementedException();
        }

        public bool retornaValoresTbTempContrato()
        {
            throw new NotImplementedException();
        }

        public void retornaValorTotalContrato()
        {
            throw new NotImplementedException();
        }

       
      
    }
}