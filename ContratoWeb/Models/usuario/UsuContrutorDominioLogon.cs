namespace ContratoWeb.Models.usuario
{
    public class UsuContrutorDominioLogon
    {
        public static UsuAplicacaoLogon logonaplicacao()
        {
            return new UsuAplicacaoLogon (new RepositorioLongonAplicacaoADO());
        }

    }
}