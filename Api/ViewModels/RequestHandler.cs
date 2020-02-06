using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.ViewModels
{
    public class RequestHandler
    {
        public RequestHandler()
        {
        }

        public string Mensagem { get; set; }
        public RequestHandler Send(string msg)
        {
            Mensagem = msg;
            return this;
        }
    }
}
