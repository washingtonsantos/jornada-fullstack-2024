using System.Text.Json.Serialization;

namespace Fina.Core.Responses;

public class Response<TData>
{
    private readonly int _code = Configuration.DefaultStatusCode;

    [JsonConstructor]
    public Response() => _code = Configuration.DefaultStatusCode;

    public Response(TData? data,
        int code = Configuration.DefaultStatusCode,
        string? message = null)
    {
        Data = data;
        _code = code;
        Mensagem = message;
    }

    public TData? Data { get; set; }
    public string? Mensagem { get; set; }

    [JsonIgnore]
    public bool Sucesso => _code is >= 200 and <= 299;
}
