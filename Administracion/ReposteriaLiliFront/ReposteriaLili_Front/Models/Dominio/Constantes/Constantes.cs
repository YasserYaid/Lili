﻿namespace ReposteriaLili_Front.Models.Dominio.Constantes
{
    public static class Constantes
    {
        public const string ESTADO_ORDEN_PROCESO_DE_VALIDACION = "En proceso de validacion";

        //////2000  EXITOSOS = 200 
        public const int CU01_CODIGO_LOGIN_SATISFATORIO = 2001;
        public const string CU01_MENSAJE_LOGIN_SATISFATORIO = "El usuario inicio sesion satisfactoriamente";
        public const int CU02_CODIGO_CLIENTE_REGISTRADO_SATISFATORIAMENTE = 2002;
        public const string CU02_MENSAJE_CLIENTE_REGISTRADO_SATISFATORIAMENTE = "El Cliente se registro satisfactoriamente";
        public const int CU03_CODIGO_PRODUCTOS_RECUPERADOS_SATISFATORIAMENTE = 2003;
        public const string CU03_MENSAJE_PRODUCTOS_RECUPERADOS_SATISFATORIAMENTE = "Los productos se obtuvieron satisfactoriamente";
        public const int CU04_CODIGO_REGISTRO_ORDEN_SATISFACTORIO = 2004;
        public const string CU04_MENSAJE_REGISTRO_ORDEN_SATISFACTORIO = "La orden se registro satisfactoriamente";
        public const int CU05_CODIGO_ORDENES_RECUPERADAS_SATISFATORIAMENTE = 2005;
        public const string CU05_MENSAJE_ORDENES_RECUPERADAS_SATISFATORIAMENTE = "Las ordenes se obtuvieron satisfactoriamente";
        public const int CU06_CODIGO_ORDEN_CANCELADA_SATISFATORIAMENTE = 2006;
        public const string CU06_MENSAJE_ORDEN_CANCELADA_SATISFATORIAMENTE = "La orden se cancelo satisfactoriamente";
        public const int CU07_CODIGO_ASIGNACION_REPARTIDOR_SATISFACTORIA = 2007;
        public const string CU07_MENSAJE_ASIGNACION_REPARTIDOR_SATISFACTORIA = "Se asigno el repartidor satisfactoriamente";
        public const int CU08_CODIGO_ORDENES_ASIGNADAS_RECUPERADAS_SATISFATORIAMENTE = 2008;
        public const string CU08_MENSAJE_ORDENES_ASIGNADAS_RECUPERADAS_SATISFATORIAMENTE = "Las ordenes asignadas se obtuvieron satisfactoriamente";
        public const int CU09_CODIGO_ORDEN_ACTUALIZADA_SATISFATORIAMENTE = 2009;
        public const string CU09_MENSAJE__ORDEN_ACTUALIZADA_SATISFATORIAMENTE = "La orden se actualizo satisfactoriamente";
        public const int CU10_CODIGO_EMPLEADO_REGISTRADO_SATISFATORIAMENTE = 2010;
        public const string CU10_MENSAJE_EMPLEADO_REGISTRADO_SATISFATORIAMENTE = "El empleado se registro satisfactoriamente";
        public const int CU11_CODIGO_SUCURSAL_REGISTRADA_SATISFATORIAMENTE = 2011;
        public const string CU11_MENSAJE_SUCURSAL_REGISTRADA_SATISFATORIAMENTE = "La sucursal se registro satisfactoriamente";
        public const int CU12_CODIGO_PRODUCTO_REGISTRADO_SATISFATORIAMENTE = 2012;
        public const string CU12_MENSAJE_PRODUCTO_REGISTRADO_SATISFATORIAMENTE = "El producto se registro satisfactoriamente";
        public const int CU07_CODIGO_SUCURSALES_RECUPERADAS_SATISFATORIAMENTE = 2013;
        public const string CU07_MENSAJE_SUCURSALES_RECUPERADAS_SATISFATORIAMENTE = "Las cucursales se recuperaron satisfactoriamente";
        public const int CU07_CODIGO_ORDENES_RECUPERADAS_SATISFATORIAMENTE = 2014;
        public const string CU07_MENSAJE_ORDENES_RECUPERADAS_SATISFATORIAMENTE = "Las ordenes se recuperaron satisfactoriamente";
        public const int CU07_CODIGO_REPARTIDORES_RECUPERADOS_SATISFATORIAMENTE = 2015;
        public const string CU07_MENSAJE_REPARTIDORES_RECUPERADOS_SATISFATORIAMENTE = "Los repartidores se recuperaron satisfactoriamente";

        //////4000  RESTRICCIONES = 400 ERROR DE INFORMACION ENVIADA DEL CLIENTE "BAD REQUEST"
        public const int CU01_CODIGO_LOGIN_FALLIDO = 4001;
        public const string CU01_MENSAJE_LOGIN_FALLIDO = "Telefono o contraseña incorrecto";
        public const int CU02_CU10_CODIGO_TELEFONO_REPETIDO = 4002;
        public const string CU02_CU10_MENSAJE_TELEFONO_REPETIDO = "El numero de telefono ya existe";
        public const int CU02_CU10_CODIGO_USERNAME_REPETIDO = 4003;
        public const string CU02_CU10_MENSAJE_USERNAME_REPETIDO = "El UserName ya existe";
        public const int CU02_CU10_CODIGO_CORREO_REPETIDO = 4004;
        public const string CU02_CU10_MENSAJE_CORREO_REPETIDO = "El correo ya existe";
        public const int CU03_CODIGO_LISTAR_PRODUCTOS_FALLIDO = 4005;
        public const string CU03_MENSAJE_LISTAR_PRODUCTOS_FALLIDO = "No se puede retornar productos por la falta de ifentificador de susursal";
        public const int CU03_CODIGO_NO_SE_ENCONTRARON_PRODUCTOS = 4006;
        public const string CU03_MENSAJE_NO_SE_ENCONTRARON_PRODUCTOS = "No se encotraron productos disponibles en el catalogo";
        public const int CU04_CODIGO_REGISTRO_ORDEN_FALLIDO = 4007;
        public const string CU04_MENSAJE_REGISTRO_ORDEN_FALLIDO = "La orden no se pudo registrar contacte con el sitio web";
        public const int CU05_CODIGO_LISTAR_PEDIDOS_FALLIDO = 4008;
        public const string CU05_MENSAJE_LISTAR_PEDIDOS_FALLIDO = "No se puede retornar pedidos sin identificador del cliente";
        public const int CU05_CODIGO_NO_SE_ENCONTRARON_PEDIDOS = 4009;
        public const string CU05_MENSAJE_NO_SE_ENCONTRARON_PEDIDOS = "No se encotraron pedidos disponibles para el cliente";
        public const int CU06_CODIGO_NO_SE_ENCONTRO_ORDEN = 4010;
        public const string CU06_MENSAJE__NO_SE_ENCONTRO_ORDEN = "No se pudo encontrar la ordena para cancelar";
        public const int CU07_CODIGO_NO_SE_ENCONTRO_REPARTIDOR = 4011;
        public const string CU07_MENSAJE_NO_SE_ENCONTRO_REPARTIDOR = "No se pudo localizar al repartidor seleccionado en la base de datos";
        public const int CU07_CODIGO_NO_SE_ENCONTRO_ORDEN = 4012;
        public const string CU07_MENSAJE_NO_SE_ENCONTRO_ORDEN = "No se pudo encontrar la orden en la base de datos";
        public const int CU08_CODIGO_LISTAR_PEDIDOS_FALLIDO = 4013;
        public const string CU08_MENSAJE_LISTAR_PEDIDOS_FALLIDO = "No se puede retornar pedidos sin identificador del empleado";
        public const int CU08_CODIGO_NO_SE_ENCONTRARON_PEDIDOS_ASIGNADOS = 4014;
        public const string CU08_MENSAJE_NO_SE_ENCONTRARON_PEDIDOS_ASIGNADOS = "No se encotraron pedidos asignados para el repartidor";
        public const int CU09_CODIGO_NO_SE_ACTUALIZO_LA_ORDEN = 4015;
        public const string CU09_MENSAJE_NO_SE_ACTUALIZO_LA_ORDEN = "No se pudo actualizar la orden, contacte con soporte tecnico";
        public const int CU09_CODIGO_NO_SE_SUBIERON_LAS_IMAGENES = 4016;
        public const string CU09_MENSAJE_NO_SE_SUBIERON_LAS_IMAGENES = "No se pudo subir la imagen al servidor, contacte con soporte tecnico";
        public const int CU11_CODIGO_NOMBRE_COMERCIAL_REPETIDO = 4017;
        public const string CU11_MENSAJE_NOMBRE_COMERCIAL_REPETIDO = "El nombre comercial ya existe";
        public const int CU12_CODIGO_NO_HAY_IMAGEN = 4018;
        public const string CU12_MENSAJE_NO_HAY_IMAGEN = "No se puede registrar un producto sin imagenes";
        public const int CU07_CODIGO_NO_SE_ENCONTRARON_SUCURSALES = 4019;
        public const string CU07_MENSAJE_NO_SE_ENCONTRARON_SUCURSALES = "No se encotraron sucursales registradas en la base de datos";
        public const int CU07_CODIGO_NO_SE_ENCONTRARON_ORDENES = 4020;
        public const string CU07_MENSAJE_NO_SE_ENCONTRARON_ORDENES = "No se encotraron ordenes registradas en la base de datos";
        public const int CU07_CODIGO_NO_SE_ENCONTRARON_REPARTIDORES = 4021;
        public const string CU07_MENSAJE_NO_SE_ENCONTRARON_REPARTIDORES = "No se encotraron repartidores registradas en la base de datos";

        //////5000  EXCEPCIONES - ERRORES = 500 "ERROR INTERNAL SERVER"
        public const int CODIGO_MEDIATOR_ERROR = 5000;
        public const string MENSAJE_MEDIATOR_ERROR = "Error en el MediatR";
        public const int CODIGO_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER = 5001;
        public const string MENSAJE_NO_HAY_CODIGO_CONTROL_EN_CQRS_HANDLER = "ERROR DE SERVIDOR - NO SE ASIGNO UN CODIGO DE CONTROL EN CQRS";
        public const int CODIGO_DATA_BASE_ERROR = 5002;
        public const string MENSAJE_DATA_BASE_ERROR = "Error de base de datos";
        public const int CODIGO_DB_PRIMARY_KEY_VIOLATION = 5003;
        public const string MENSAJE_DB_PRIMARY_KEY_VIOLATION = "Violacion de restriccion en llave primaria";
        public const int CU06_CODIGO_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO_ORDEN = 5004;
        public const string CU06_MENSAJE_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO_ORDEN = "No se pudo obtener la lista de productos orednados para actualizar cantidad";
        public const int CU06_CODIGO_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO = 5005;
        public const string CU06_MENSAJE_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO = "No se pudo obtener la lista de productos registrados para actualizar cantidad";
        public const int CU06_CODIGO_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO_SUCURSAL = 5006;
        public const string CU06_MENSAJE_NO_SE_RECUPERO_INFO_TABLA_PRODUCTO_SUCURSAL = "No se pudo obtener la lista de productos en las sucursales para actualizar cantidad";
    }
}
