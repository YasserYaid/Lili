using Microsoft.AspNetCore.Http;
using System.Net;

namespace ReposteriaLili_API.Core.Application.ReqResModels
{
    public class DatabaseResponse
    {
        public bool isExitoso { get; set; } = false;
        public int? ControlCode { get; set; }
        public string? ControlMessage { get; set; } = null;
        public List<string>? ErrorMessages { get; set; } = null;
        public object? Resultado { get; set; } = null;
        public void LimpiarValores()
        {
            isExitoso = false;
            ControlCode = null;
            ControlMessage = null;
            ErrorMessages = null;
            Resultado = null;
        }
    }
}
