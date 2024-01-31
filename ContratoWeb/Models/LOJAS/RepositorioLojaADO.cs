using ContratoWeb.contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models.LOJAS
{
    public class RepositorioLojaADO : INLoja<DominioLoja>
    {
        private RepositorioBD bd;

        public List<DominioLoja> bllRetornaLojas()
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

            return acao;
        }

    }
}