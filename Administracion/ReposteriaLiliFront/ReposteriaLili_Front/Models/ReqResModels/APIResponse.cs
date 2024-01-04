using System.Net;
using System.Net.Sockets;

namespace ReposteriaLili_Front.Models.ReqResModels
{
    public class APIResponse
    {
        public HttpStatusCode? StatusCode { get; set; } = null;
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
            StatusCode = null;
        }
    }
}
