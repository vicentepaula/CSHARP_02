namespace ContratoWeb.Models
{
    public class UsuarioAplicacaoContrutor
    {
        public static UsuarioAplicacao usuaroApADO()
        {
            return new UsuarioAplicacao(new RepositorioUsuarioAplicacaoADO());
        }
    }
}