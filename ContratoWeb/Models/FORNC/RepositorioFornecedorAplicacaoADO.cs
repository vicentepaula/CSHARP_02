using ContratoWeb.contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models.FORNC
{
    public class RepositorioFornecedorAplicacaoADO : INfornecedor<DominioFornecedor>
    {
        private RepositorioBD bd;
               
        public List<DominioFornecedor> RetornaListaForcedores()
        {
           string sql = "select p.seqpessoa, p.NOMERAZAO from consinco.ge_PESSOA p WHERE ROWNUM <= 100";
           

            var forn = new List<DominioFornecedor>();
            DominioFornecedor tempoOejeto = null;

            using (bd = new RepositorioBD())
            {
                var retorno = bd.ExecutaComandoComRetorno(sql);

                while (retorno.Read())
                {
                    tempoOejeto = new DominioFornecedor()
                    {
                        SEQPESSOA = int.Parse(retorno["seqpessoa"].ToString()),
                        NOMERAZAO = retorno["NOMERAZAO"].ToString()

                    };

                    forn.Add(tempoOejeto);

                }
                retorno.Close();

            }

            return forn;
        }





    }


}