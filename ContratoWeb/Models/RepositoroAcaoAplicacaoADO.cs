using ContratoWeb.contrato;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContratoWeb.Models
{
    public class RepositoroAcaoAplicacaoADO : IAcaoRepositorio<DominioAcao>
    {
        private RepositorioBD bd;


        public DominioAcao ListarPorId(int id)
        {
            using (bd = new RepositorioBD())
            {
                var strQuery = string.Format("select * from acao where id_acao ='{0}'", id);
                var retorno = bd.ExecutaComandoComRetorno(strQuery);

                return ReaderEmLista(retorno).FirstOrDefault();
            }
        }

        
        public DominioAcao ListarPorId_Contrato(int id_contrato) 
        {
            using (bd = new RepositorioBD())
            {
                var strQuery = string.Format("select * from acao where id_contrato ='{0}'", id_contrato);
                var retorno = bd.ExecutaComandoComRetorno(strQuery);

                return ReaderEmLista(retorno).FirstOrDefault();
            }
        }


        //Metodo de apoio que retorna listas
        public List<DominioAcao> ReaderEmLista(Oracle.ManagedDataAccess.Client.OracleDataReader reader)
        {
            var acao = new List<DominioAcao>();
            while (reader.Read())
            {
                var tempoOejeto = new DominioAcao()
                {
                    ID_ACAO = int.Parse(reader["ID_ACAO"].ToString()),
                    NRO_CONTRATO = int.Parse(reader["NRO_CONTRATO"].ToString()),
                    NROEMPRESA = int.Parse(reader["NROEMPRESA"].ToString()),
                    NomeEmpresa = reader["NOME_LOJA"].ToString(),
                    NOME_ACAO = reader["nome_acao"].ToString(),
                    NomeFonecedorAcao = reader["FORNECEDOR_ACAO"].ToString(),
                    DTA_ACAO = DateTime.Parse(reader["DTA_ACAO"].ToString()),
                    OBSERVACAO = reader["OBSERVACAO"].ToString(),
                    VALOR_ACAO = decimal.Parse(reader["VALOR_ACAO"].ToString()),
                    NRO_NF = reader["NRO_NF"].ToString()
                };

                acao.Add(tempoOejeto);
            }
            reader.Close();

            return acao;
        }


        public void Salvar(DominioAcao acao)
        {
            Insert(acao);
            if (existeContrato(acao.NRO_CONTRATO, acao.NROEMPRESA))
            {

           //     Insert(acao);
                /*
                 * 
                }
                else
                {
                    Alterar(acao);

                }
                */
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


        private void Insert(DominioAcao acao)
        {
            
            string url = ("insert into ACAO(NOME_ACAO, DTA_ACAO, VALOR_ACAO, NRO_CONTRATO, NROEMPRESA, FORNECEDOR_ACAO, OBSERVACAO, NOME_LOJA, ID_CONTRATO, NRO_NF)values('{0}',TO_DATE('{1}', 'DD-MM-YYYY hh24:mi:ss'),'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')");
            url = string.Format(url, acao.NOME_ACAO, acao.DTA_ACAO, acao.VALOR_ACAO, acao.NRO_CONTRATO, acao.NROEMPRESA, acao.NomeFonecedorAcao, acao.OBSERVACAO, acao.NomeEmpresa, acao.ID_CONTRATO, acao.NRO_NF);
            using (bd = new RepositorioBD())
            {
                bd.ExecutaCommando(url);
            }
                       
        }

        private void Alterar(DominioAcao acao)
        {

        }

        public int InsereRetornarIDContrato(int contrato)
        {
            string url = ("insert into SEQUENCIA_CONTRATO(NRO_CONTRATO)values('{0}')");
            url = string.Format(url, contrato);
            using (bd = new RepositorioBD())
            {
                bd.ExecutaCommando(url);
            }

            DominioIDcontrato info = retornaIDcontratoPorNumeroContrato(contrato);
            Excluir_TB_SEQUENCIAcONTRATO(contrato);

            return info.ID_CONTRATO;
        }


        private DominioIDcontrato retornaIDcontratoPorNumeroContrato(int contrato)
        {
            DominioIDcontrato tempoOejeto = null;
            using (bd = new RepositorioBD())
            {
                var strQuery = string.Format("select * from SEQUENCIA_CONTRATO where NRO_CONTRATO ='{0}'", contrato);
                var reader = bd.ExecutaComandoComRetorno(strQuery);

                while (reader.Read())
                {
                    tempoOejeto = new DominioIDcontrato()
                    {
                        ID_CONTRATO = int.Parse(reader["ID_CONTRATO"].ToString())
                        
                    };
          
                }
                reader.Close();


                return tempoOejeto;
            }

           
        }


        public void Excluir_TB_SEQUENCIAcONTRATO(int contrato)
        {

            using (bd = new RepositorioBD())
            {
                var strQuery = string.Format("DELETE  FROM SEQUENCIA_CONTRATO WHERE NRO_CONTRATO = {0} ", contrato);
                bd.ExecutaCommando(strQuery);
            }

        }


        public IEnumerable<DominioAcao> ListaAcaoPorNROcontratoNroLoja(int ID)
        {
            using (bd = new RepositorioBD())
            {
                var strQuery = string.Format(" select * from acao where nro_contrato ='{0}' ", ID);
                var retorno = bd.ExecutaComandoComRetorno(strQuery);

                var acao = new List<DominioAcao>();
                while (retorno.Read())
                {
                    var tempoOejeto = new DominioAcao()
                    {                            
                        ID_ACAO = int.Parse(retorno["ID_ACAO"].ToString()),
                        NRO_CONTRATO = int.Parse(retorno["NRO_CONTRATO"].ToString()),
                        NROEMPRESA = int.Parse(retorno["NROEMPRESA"].ToString()),
                        NomeEmpresa = retorno["NOME_LOJA"].ToString(),
                        NOME_ACAO = retorno["nome_acao"].ToString(),
                        NomeFonecedorAcao = retorno["FORNECEDOR_ACAO"].ToString(),
                        DTA_ACAO = DateTime.Parse(retorno["DTA_ACAO"].ToString()),
                        OBSERVACAO = retorno["OBSERVACAO"].ToString(),
                        VALOR_ACAO = decimal.Parse(retorno["VALOR_ACAO"].ToString()),
                        ID_CONTRATO = int.Parse(retorno["ID_CONTRATO"].ToString()),
                        NRO_NF = retorno["NRO_NF"].ToString(),
                       
                    };

                    acao.Add(tempoOejeto);
                }
                retorno.Close();

                return acao;
               
            }
        }


        public List<DominioAcao> bllRetornaLojas()
        {
            string sql = "select nroempresa, empresa from CONSINCO.dim_empresa";

            var acao = new List<DominioLoja>();
            DominioLoja tempoOejeto = null;

            using (bd = new RepositorioBD())
            {
                var retorno = bd.ExecutaComandoComRetorno(sql);

                while (retorno.Read())
                {
                    tempoOejeto = new DominioLoja()
                    {
                        nroempresa = int.Parse(retorno["NROEMPRESA"].ToString()),
                        empresa = retorno["empresa"].ToString()

                    };

                    acao.Add(tempoOejeto);

                }
                retorno.Close();

            }

            return null;
        }



        //#################################################################################################################################################################




    }



}