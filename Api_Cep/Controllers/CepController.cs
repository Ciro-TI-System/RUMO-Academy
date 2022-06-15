using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiCep.Controllers
{
    [ApiController]
    public class CepController : ControllerBase
    {
        [HttpGet("v1/ConsultarCep")]
        public IActionResult ConsultarCep(string cep)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(cep))
                    throw new InvalidOperationException("O cep deve ser informado.");

                cep = cep.Replace("-", "");
                if (cep.Length != 8)
                    throw new InvalidOperationException("Formato de cep inválido.");

                var retorno = Conexoes.ConsumoApi.Get<Contratos.ViaCep.Response>("https://viacep.com.br/ws/" + cep + "/json/");
                return StatusCode(200, retorno);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                var log = new Contratos.LogAplicacao.Request()
                {
                    DataHora = DateTime.Now,
                    MensagemErro = ex.Message,
                    NomeAplicacao = "ApiCep",
                    NomeMaquina = Environment.MachineName,
                    Usuario = Environment.UserName,
                    RastreioErro = ex.StackTrace
                };
                Conexoes.ConsumoApi.Post<object>("https://logaplicacao.aiur.com.br/v1/Logs", log);
                return StatusCode(500);
            }
        }
    }
}
