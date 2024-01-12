namespace apibronco.bronco.com.br.Entity
{
    public enum EnumTipoAcesso
    {
        Admin = 1, 
        Funcionario  = 2
    }

    public static class Permissoes
    {
        public const string Administrador = "Administrador";
        public const string Funcionario = "Funcionario";
    }
}
