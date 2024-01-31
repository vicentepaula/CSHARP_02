using ContratoWeb.contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models.usuario
{
    public class RepositorioLongonAplicacaoADO : InLogon<DominioLogon>
    {
        private RepositorioBD bd;

        public DominioLogon capturaUsuario(string usu, string sen)
        {
            DominioLogon logon = null;            

            string url = (" select * from tb_usuario where usuario = '{0}' and senha = '{1}' ");
            using (bd = new RepositorioBD())
            {
                url = String.Format(url, usu, sen);
                var reader = bd.ExecutaComandoComRetorno(url);
                          
                
                while (reader.Read())
                {
                    usu = reader["usuario"].ToString();
                    sen = reader["senha"].ToString();

                    logon = new DominioLogon()
                    {
                        nome = usu,
                        senha = sen
                    };
                   
                }
             return logon;

            }

        }


        public int cadastrarUsuario(string usuario, string senha)
        {
            int cadastrado = 0;

            if (!existeUsu(usuario))
            {
                string url = ("insert into usuContrato_teste.TB_USUARIO(TB_USUARIO.USUARIO, TB_USUARIO.SENHA)values('{0}','{1}')");
                url = string.Format(url, usuario, senha);
                using (bd = new RepositorioBD())
                {
                    bd.ExecutaCommando(url);
                    cadastrado = 1;
                }
                                                 
            }

            return cadastrado;
        }
            

        public bool existeUsu(string user)
        {
            bool achou = false;
            string usu_banco;

            //string url1 = "select usuario from tb_usuario where usuario = '{0}'";
            string url = "select * from TB_USUARIO WHERE REGEXP_LIKE(usuario, '{0}','i')";
            url = string.Format(url, user);

            using (bd = new RepositorioBD())
            {
               var result = bd.ExecutaComandoComRetorno(url);
                      
                
                while (result.Read())
                {
                    usu_banco = result["usuario"].ToString();
                                                           
                    return  achou = CompararUsuario(user, usu_banco);
                }
                               

            }
            
            return achou;
        }

        public bool CompararUsuario(string user, string usubanco)
        {
            bool achou = false;

            if (string.Equals(user, usubanco, StringComparison.OrdinalIgnoreCase))
            {
                return achou = true;
            }

            return achou;
        }

    }

}