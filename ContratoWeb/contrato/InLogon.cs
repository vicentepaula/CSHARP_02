using ContratoWeb.Models.usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContratoWeb.contrato
{
    public interface InLogon<T> where T : class
    {
        DominioLogon capturaUsuario(string usu, string sen);
        int cadastrarUsuario(string usuario, string senha);
        bool existeUsu(string user);
        bool CompararUsuario(string user, string usubanco);

    }
}
