namespace Fina.Core
{
    public static class Configuration
    {
        public const int DefaultStatusCode = 200;
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 25;

        public static string BackendURL { get;set;} = "http://localhost:5178";
        public static string FrontendURL { get; set;} = "http://localhost:5173";
    }
}
