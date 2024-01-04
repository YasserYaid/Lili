using ReposteriaLili_Front.Models.Constantes;
using static ReposteriaLili_Front.Models.Constantes.OpcionesAPI;

namespace ReposteriaLili_Front.Models.ReqResModels
{
    public class APIRequest
    {
        public OperacionHTTP OperacionHTTP { get; set; } = OperacionHTTP.GET;

        public string? ApiUrlDestino { get; set; }

        public object? Data { get; set; }
    }
}
