using System;
using DevIO.Api.Controllers;
using DevIO.Business.Intefaces;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DevIO.Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/teste")]
    public class TesteController : MainController
    {
        private readonly ILogger _logger; // não é necessario resolver na injecao de dependencia, o aspnet ja usa internamente

        public TesteController(INotificador notificador, IUser appUser, ILogger<TesteController> logger) : base(notificador, appUser)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Valor()
        {

            // testando erros com elmah

            // nesse caso a execption não é vista pelo almah, ela interrompe o processo da api 
            //throw new Exception("Error");
            
            // o erro da divisao abaixo é capturado pelo elamh usando metodo Ship
            //try
            //{
            //    var i = 0;
            //    var result = 42 / i;
            //}
            //catch (DivideByZeroException e)
            //{
            //    e.Ship(HttpContext);
            //}

            // esses logs não aparecem para o gerenciador de log elmah. ele mesmo pega os errors de acesso da aplicação, sem a necessidade de config adicionais
            _logger.LogTrace("Log de Trace"); // por ex: hora inicio e hora fim (dev), mas esta desabilitado, usar o information que aceita qualquer coisa
            _logger.LogDebug("Log de Debug"); // informforçoes de debug (dev)
            _logger.LogInformation("Log de Informação"); // nada de importante, mas q queira registrar
            _logger.LogWarning("Log de Aviso"); // warnings, não sendo erros, mas não deveriam acontecer
            _logger.LogError("Log de Erro");
            _logger.LogCritical("Log de Problema Critico"); // falha critica, que afeta a saude da api

            return "Sou a V2";
        }
    }
}