using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContratoWeb.Models.LOJAS
{
    public class UsuarioLojaConstrutor
    {
        public static UsuarioLoja ExceUsauaioLoja()
        {  
            
            return new  UsuarioLoja(new RepositorioLojaADO());
        }

    }
}