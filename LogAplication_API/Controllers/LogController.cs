using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LogAplicationAPI.Controllers
{
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly Conexoes.SqlServer _sql;
        public LogController()
        {
            _sql = new Conexoes.SqlServer();
        }

        [HttpPost("v1/LogAplicacao")]
        public void InserirLogAplicacao(Models.LogAplicacao log)
        {
            _sql.InserirLogAplicacao(log);
        }

    }
}
