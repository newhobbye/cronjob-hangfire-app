using System.Text.Json.Serialization;

namespace hangfire_jobs_service.Models.Response.ViaCep
{
    public record ViaCepResponse(
        [property: JsonPropertyName("cep")] string Cep,
        [property: JsonPropertyName("logradouro")] string Logradouro,
        [property: JsonPropertyName("complemento")] string Complemento,
        [property: JsonPropertyName("unidade")] string Unidade,
        [property: JsonPropertyName("bairro")] string Bairro,
        [property: JsonPropertyName("localidade")] string Localidade,
        [property: JsonPropertyName("uf")] string Uf,
        [property: JsonPropertyName("ibge")] string Ibge,
        [property: JsonPropertyName("gia")] string Gia,
        [property: JsonPropertyName("ddd")] string Ddd,
        [property: JsonPropertyName("siafi")] string Siafi);
    
}
