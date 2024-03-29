﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Models.ImageManagement
{
    public class FirebaseSettings
    {
        public FirebaseSettings(string? email, string? password, string? rutaRaiz, string? rutaImagenPerfil, string? rutaImagenProducto, string rutaImagenCodigoBarra, string? api_key)
        {
            Email = email;
            Password = password;
            RutaRaiz = rutaRaiz;
            RutaImagenPerfil = rutaImagenPerfil;
            RutaImagenProducto = rutaImagenProducto;
            RutaImagenCodigoBarra = rutaImagenCodigoBarra;
            Api_key = api_key;
        }

        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RutaRaiz { get; set; }
        public string? RutaImagenPerfil { get; set; }
        public string? RutaImagenProducto { get; set; }
        public string? RutaImagenCodigoBarra { get; set; }
        public string? Api_key { get; set;}
    }
}
