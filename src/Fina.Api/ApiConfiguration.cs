namespace Fina.Api
{
    public static class ApiConfiguration
    {
        public static Guid UsuarioId = Guid.NewGuid();
        public static string ConnectionString { get; set; } = string.Empty;
        public static string CorsPolicyName = "finapolicy";
    }
}
