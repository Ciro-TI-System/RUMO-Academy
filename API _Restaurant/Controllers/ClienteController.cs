using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Caelum.Stella.CSharp.Validation;

namespace RestauranteAPI.Controllers
{

    [ApiController]
    public class ClienteController : ControllerBase
    {
        //Conexão
        private readonly Conexoes.SqlServer _sql;
        public ClienteController()
        {
            _sql = new Conexoes.SqlServer();
        }

        //CREATE Cliente
        [HttpPost("v1/Cliente")]
        public void InserirCliente(Model.Cliente cliente)

        
        {
            _sql.InserirCliente(cliente);
           
        }
        


        //UPDATE Cliente
        [HttpPut("v1/Cliente")]
        public void AtualizarCliente(Model.Cliente cliente)
        {
            _sql.AtualizarCliente(cliente);
        }

        //DELETE Cliente
        [HttpDelete("v1/Cliente")]
        public void DeletarCliente(Model.Cliente cliente)
        {
            _sql.DeletarCliente(cliente);
        }

        //SELECT * Cliente
        [HttpGet("v1/Cliente")]
        public List<Model.Cliente> ListarClientes()
        {
            return _sql.ListarClientes();
        }

        //SELECT Cliente
        [HttpGet("v1/Cliente/{cpf}")]
        public Model.Cliente ListarClientes(string cpf)
        {
            return _sql.SelecionarCliente(cpf);
        }
    }
}
