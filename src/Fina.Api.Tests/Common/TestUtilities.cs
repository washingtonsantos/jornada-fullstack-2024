namespace Fina.Api.Tests.Common;

/// <summary>
/// Utilitários comuns para testes.
/// Centraliza funcionalidades reutilizáveis entre diferentes testes.
/// </summary>
public static class TestUtilities
{
    /// <summary>
    /// Gera um identificador único para testes.
    /// Útil para criar dados de teste únicos.
    /// </summary>
    public static string GenerateUniqueId()
    {
        return Guid.NewGuid().ToString("N")[..8];
    }

    /// <summary>
    /// Cria um email de teste único.
    /// </summary>
    public static string GenerateTestEmail()
    {
        return $"test_{GenerateUniqueId()}@example.com";
    }

    /// <summary>
    /// Cria um timestamp de teste.
    /// </summary>
    public static DateTime GenerateTestTimestamp()
    {
        return DateTime.UtcNow;
    }

    /// <summary>
    /// Valida se uma string é um JSON válido.
    /// </summary>
    public static bool IsValidJson(string json)
    {
        try
        {
            System.Text.Json.JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Delay para testes assíncronos (útil para simular operações assíncronas).
    /// </summary>
    public static async Task DelayAsync(int milliseconds = 100)
    {
        await Task.Delay(milliseconds);
    }
}
