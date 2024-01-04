using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.BranchesClassic
{
    public class BranchVM
    {
        public int? Id { get; set; }
        public string? Calle { get; set; }
        public string? Ciudad { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Colonia { get; set; }
        public string? DiaInicial { get; set; }
        public string? DiaFinal { get; set; }
        public string? Estado { get; set; }
        public string? HoraInicial { get; set; }
        public string? HoraFinal { get; set; }
        public string? Latitud { get; set; }
        public string? Longitud { get; set; }
        public string? Municipio { get; set; }
        public string? NombreComercial { get; set; }
        public string? NumeroDepartamento { get; set; }
        public string? Pais { get; set; }
    }
}
