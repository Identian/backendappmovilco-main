namespace BackendAppMovilCOTest.Capas.Compartida
{
  public static class ConstantesCompartidasFacturacion
  {
    public const string NitExistente = "111111111111111111";
    public const string NitPlataformaErronea = "888888888888888";
    public const string NitInexistente = "999999999999999999";
    public const string NitINexistenteGestionAccesoAppMovil = "1234567891011";

    public const string IdEmpresaExistente = "1111111111";
    public const string IdEmpresaExistenteActivoAppInvalido = "3333333333";
    public const int IdEmpresaExistenteInt = 1111111111;
    public const string IdEmpresaInexistente = "2147483647";
    public const string IdEmpresaInvalida = "64676878";
    public const int IdEmpresaInvalidaInt = 64676878;
    public const int IdenterpriseInt = 465464523;
    public const string Identerprise = "465464523";
    public const string IdEmpresaInvalidaGestionAccesoApp = "3425435";

    public const string IdClienteExistente = "1111111111";
    public const string IdClienteInexistente = "2147483647";
    public const string IdClienteDestinatarioNull = "9999999999";
    public const string IdClienteNotificarNo = "8888888888";

    public const string IdProductoExistente = "1111111111";
    public const string IdProductoInexistente = "2147483647";

    public const string TokenEmpresaValido = "111111111111111111";
    public const string TokenEmpresaInvalido = "999999999999999999";
    public const string TokenClaveInvalido = "999999999999999999";

    public const string BearerTokenValido = "eyJleHAiOjE2Nzg0NTczOTMsImlhdCI6MTY3ODQ1Mzc5MywiaXNzIjoiOTAwMzkwMTI2IiwiY29udGV4dCI6eyJ1c2VyIjp7I";
    public const string BearerTokenTimeOutRedisCache = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJleHAiOjE3MTUyOTA3NTksImlhdCI6MTcxNTIwNDM1OSwiaXNzIjoiOTAwMzkwMTI2IiwiY29udGV4dCI6eyJ1c2VyIjp7ImVudGVycHJpc2VUb2tlbiI6ImU3ZmQyMGM4YmI4MzcxNGI2NzhjMTU0MDVjOTg3NGQ5ZTJjZGMyNjUiLCJlbnRlcHJpc2VJZCI6Mjg2NiwiZW50ZXJwcmlzZU5pdCI6IjkwMDM5MDEyNiIsImVudGVycHJpc2";

    public const string BearerTokenValidoUsuarioNoEncontrado = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImN0eSI6IkpXVCJ9.eyJleHAiOjE2ODY4NDgyODYsImlhdCI6MTY4Njg0NDY4NiwiaXNzIjoiOTAwMzkwMTI2IiwiY29udGV4dCI6eyJ1c2VyIjp7ImVudGVycHJpc2VUb2tlbiI6ImQ0ODg3MTczODQ3NmY5MDU3NTQ4OGFkYzg0OGQ0MDJlODRlNmZkNTUiLCJlbnRlcHJpc2VJZCI6MTAwLCJlbnRlcnByaXNlTml0IjoiOTAwMzkwMTI2IiwiZW50ZXJwcmlzZXNjaGVtZWlkIjoiMzEiLCJlbnZpcm9tZW50IjowfX19.CkDFMXKerFb5YLV4WmokTA47xgjl4MMAajJk7Tvy-_A";
    public const string BearerTokenInvalido = "eyJleHAiOjE2Nzg0NT";
    public const string BearerTokenValidoActivoAppInvalido = "eyJleHAiOjE3MDA4MDU4OTMsImlhdCI6MTcwMDgwMjI5MywiaXNzIjoiOTAwMzkwMTI2IiwiY2";
    public const string BearerTokenValidoEmpresaInvalidaGestionAccesoApp = "F7AE106AAF6F2F9A61BE3D436157FB03CEF0F2";
    public const string BearerTokenValidoUsuarioInvalidGestionAccesoApp = "F7AE106AAF6F2F9A61BE3D436157FB03CEF0F2";
    public const string BearerTokenValidoUsuarioNoEncontradoGestionAccesoApp = "E5AE39C5B081ACB6A97973DA5E0035E5C295ED947F1";
    public const string BearerTokenValidoActivoAppUsuarioInvalido = "aGVtZWlkIjoiMzEiLCJlbnZpcm9tZW50IjowfX1";

    public const string ValorBearerTokenValido = "{\"user\":{\"enterpriseToken\":\"111111111111111111\",\"entepriseId\":1111111111,\"enterpriseNit\":\"111111111111111111\",\"enterpriseschemeid\":\"31\",\"enviroment\":0}}";
    public const string ValorBearerTokenValidoTimeOutRedisCache = "{\"user\":{\"enterpriseToken\":\"09809879865453\",\"entepriseId\":1111111111,\"enterpriseNit\":\"78769879908\",\"enterpriseschemeid\":\"31\",\"enviroment\":0}}";
    public const string ValorBearerTokenValidoConsultarPorid = "{\"user\":{\"enterpriseToken\":\"56443545243\",\"entepriseId\":465464523,\"enterpriseNit\":\"111111111111111111\",\"enterpriseschemeid\":\"31\",\"enviroment\":0}}";
    public const string ValorBearerTokenValidoActivoAppInvalido = "{\"user\":{\"enterpriseToken\":\"111111111111111111\",\"entepriseId\":3333333333,\"enterpriseNit\":\"111111111111111111\",\"enterpriseschemeid\":\"31\",\"enviroment\":0}}";
    public const string ValorBearerTokenInvalido = "{\"user\":{\"enterpriseToken\":\"2222222\",\"entepriseId\":2222222,\"enterpriseNit\":\"111111111111111111\",\"enterpriseschemeid\":\"31\",\"enviroment\":0}}";
    public const string ValorBearerTokenValidoActivoAppUsuarioInvalido = "{\"user\":{\"enterpriseToken\":\"111111111111111111\",\"entepriseId\":4545454545,\"enterpriseNit\":\"111111111111111111\",\"enterpriseschemeid\":\"31\",\"enviroment\":0}}";
    public const string ValorBearerTokenValidoEmpresaInvalidaGestionAccessoApp = "{\"user\":{\"enterpriseToken\":\"111111111111111111\",\"entepriseId\":986865856,\"enterpriseNit\":\"111111111111111111\",\"enterpriseschemeid\":\"31\",\"enviroment\":0}}";
    public const string ValorBearerTokenValidoUsuarioInvalidoGestionAccessoApp = "{\"user\":{\"enterpriseToken\":\"111111111111111111\",\"entepriseId\":1111111111,\"enterpriseNit\":\"111111111111111111\",\"enterpriseschemeid\":\"31\",\"enviroment\":0}}";

    public const string LlaveRedisCacheValida = "FEL_CO_BackendAppMovil_IniciarSesion_Dev_eyJleHAiOjE2Nzg0NTczOTMsImlhdCI6MTY3ODQ1Mzc5MywiaXNzIjoiOTAwMzkwMTI2IiwiY29udGV4dCI6eyJ1c2VyIjp7I";
    public const string LlaveRedisCacheInvalida = "FEL_CO_BackendAppMovil_IniciarSesion_Dev_eyJleHAiOjE2Nzg0NT";

    public const string PlataformaNoDisponible = "DIAN";
    public const string PlataformaTFHKA = "TFHKA";

    public const string CorreoUsuarioExistente = "usuario@tfhka.com";
    public const string CorreoUsuarioInexistente = "usuario@gmail.com";
    public const string CorreoUsuarioRolEstandar = "usuario_rol_estandar@tfhka.com";

    public const int CodigoDatosValidos = 200;
    public const string ResultadoDatosValidos = "Exitoso";
    public const string MensajeDatosValidosActualizacion = "Actualización Exitosa";
    public const string MensajeDatosValidos = "Consulta Exitosa";

    public const string IdUsuario = "123";
    public const string IdUsuarioAutenticadoConErrores = "155443";
    public const string IdUsuarioRolEstandar = "222";
    public const string IdUsuarioAppInactivo = "853";
    public const string IdUsuarioInactivo = "593";
    public const string IdUsuarioInvalid0GestionAccesoApp = "5435387876";


    public const int CodigoDatosInvalidos = 400;
    public const string ResultadoDatosInvalidos = "Error";
    public const string MensajeDatosInvalidos = "Los datos presentes en la solicitud no han pasado las validaciones";

    public const int CodigoNoEncontrado = 404;
    public const int CodigoNitNoExiste = 404;
    public const string ResultadoNitNoExiste = "Error";
    public const string MensajeNitNoExiste = "No se encontraron datos para el Nit";

    public const int CodigoTokensInvalidos = 401;
    public const string ResultadoTokensInvalidos = "Error";
    public const string MensajeTokensInvalidos = "No autorizado";

    public const int CodigoPlataformaNoDisponible = 403;
    public const string ResultadoPlataformaNoDisponible = "Error";
    public const string MensajePlataformaNoDisponible = "Funcionalidad no disponible";

    public const int CodigoConsultaExitosaClaveSecreta = 200;
    public const string ResultadoClaveSecretaEncontrada = "Exitoso";
    public const string MensajeClaveSecretaEncontrada = "Clave Secreta Correcta";

    public const int CodigoConsultaInvalidaClaveSecreta = 401;
    public const string ResultadoClaveSecretaInvalida = "Error";
    public const string MensajeClaveSecretaInvalida = "Clave secreta Invalida";
    public const string MensajeClaveSecretaInactiva = "Clave Secreta Inactiva";

    public const string ConsecutivoDocumentoValido = "ARF84";
    public const string MensajeDatosValidosEstadoDocumento = "Se retornan datos de la Factura.";
    public const int CodigoNroDocumentoInvalido = 102;
    public const string ResultadoNroDocumentoInvalido = "Error";
    public const string MensajeNroDocumentoInvalido = "Número de documento inválido";

    public const int CodigoSessionCerrada = 401;
    public const string ResultadoError = "Error";
    public const string MensajeSesionCerrada = "Se ha cerrado la sesión del usuario";

    public const string MensajeDatosInvalidosAsociarAlias = "Registro no encontrado para el dispositivo con SerialLogico: 45edafd6eccb4d79b0854180d68f5b103";

    public const string IdInvoiceDocumentoValido = "1234";
    public const string TipoConsulta1 = "1";
    public const string TipoConsulta0 = "0";
    public const string MensajeExitosoConsultarReferencia1 = "Documento Procesado.";
    public const string ResultadoDatosValidosConsultarReferencia = "Procesado";
    public const string MensajeExitosoConsultarReferencia2 = "Documento candidato a corregir.";
    public const int CodigoReferenciaDocumentoInvalido = 101;
    public const string MensajeDocumentoNoExiste = "Documento no disponible, no tiene ningún documento rechazado por corregir.";

    public const string TokensEnterpriseValido = "e30c0e86ef20aqwe929f65dfd83752249fdf266d";
    public const int CodigoClienteNuevo = 201;
    public const string ResultadoClienteNuevo = "Exitoso";
    public const string MensajeClienteNuevo = "Cliente creado con exito";
    public const string IdNuevoCliente = "123456";
    public const string NitNuevoCliente = "948390127";
    public const string ResultadoTokenInvalidos = "Error";
    public const string MensajeTokenInvalidos = "No autorizado";
    public const string TokensPasswordValidos = "e30c0e86ef20a7a1929f65dfd83752249fdf26e6d";
    public const string ClaveSecretaValida = "d4732cc462090c5724169a4aec256ee8fbeecb48";
    public const string TokensEnterpriseInvalidos = "e30c0e86ef20a7a1929f65dfd83752249fdf26erd";
    public const string TokensPasswordInvalidos = "e30c0e86ef20a7a1929f65dfd83752249fdf266d";
    public const string ClaveSecretaInvalida = "d4732cc462090c5724169a4aec256ee8fbeecb44e";
    public const string ClaveSecretaInactiva = "djsakjbdj6y5hjsabd5t7iun,alsn";
    public const string NoTieneClaveSecreta = "Los datos presentes en la solicitud no han pasado las validaciones";
    public const string TokenClaveValido = "e30c0e86ef20a7a1929f65dfd83752249fdf26e61";
    public const string TimeoutRedisCache = "No se pudo actualizar la caché. La operación de Inserción/Actualización/Eliminación del registro falló.Message: The operation timed out.. InnerException: ";



    public const int CodigoProductoNuevo = 201;
    public const string ResultadoProductoNuevo = "Exitoso";
    public const string MensajeProductoNuevo = "Cliente creado con exito";
    public const string IdNuevoProducto = "334422";

    public const int UsuarioActivo = 1;
    public const int UsuarioInactivo = 0;

    public const int UsuarioActivoApp = 1;
    public const int UsuarioInactivoApp = 0;

    public const string TokenUsuarioGestionAccesoAppMovil = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3MDA3NzU2MzYsImlhdCI6MTcwMDc3MjAzNiwiaXNzIjoiOTAwMzkwMTI2IiwiY29udGV4dCI6eyJVc3VhcmlvIjp7IklkVXN1YXJpbyI6NCwiVXN1YXJpbyI6Imp5ZXBlc19mYWN0dXJhY2lvbl9kZXYiLCJDb250cmFzZW5hIjoiMTI0ZDM4YWU5ODkzZTljNzc2NzVmOTc2OTk0NWZkYTc3ODRkMDMxMyJ9fX0.1IchYb6RgLS4RfI28XqQobOwD_OqkSB14Yc2oThmAg0";

    public const int CodigoDispositivoNuevo = 201;
    public const string ResultadoDispositivoNuevo = "Exitoso";
    public const string MensajeDispositivoNuevo = "Creación Exitosa";
    public const string IdDispositivoNuevo = "34234";
    public const string ActivoAppDispositivoNuevo = "1";
    public const string MensajeDatosInvalidosDispositivoExiste = "Dispositivo está asociado a otra empresa";
    public const string SerialLogicoDispositivoValido = "N1-03";
    public const string AliasDispositivoValido = "AliasPruebasUnitarias";
    public const string IdTipoAppExistente = "1";
    public const string IdTipoAppTili = "2";
    public const string NombreTipoAppTili = "HKA-Tili";

    public const string IdEstablecimientoValido = "1";
    public const string IdEstablecimientoInvalido = "2";
    public const string IdEstablecimientoYaSeleccionado = "3";

    public const string IdNumeracionValido = "1254";
    public const string IdNumeracionInvalido = "29907";
    public const string IdIdNumeracionYaSeleccionado = "3939";

    public const int CodigoErrorValidacionSuscripcion = 109;
    public const string MensajeDocumentoNoSuperoLasValidaciones = "El documento no superó las validaciones.";
    public const string SerialLogicoDispositivoSuscripcionExistente = "3SLQE";
    public const string IdDispositivoSuscripcionExistente = "10003";
    public const string IdSuscripcionDispositivoExistente = "20003";
    public const string SerialSuscripcionDispositivoExistente = "0cf4a7536f1e41efa1e26b1338904a23";
    public const string SerialLogicoDispositivoSuscripcionNoActiva = "6SLQE";
    public const string SerialLogicoDispositivoSuscripcionNoVigente = "7SLQE";
    public const string SerialLogicoDispositivoSuscripcionAunNoVigente = "8SLQE";
    public const string SerialLogicoDispositivoSuscripcionInexistente = "9SLQE";
    public const string IdDispositivoSuscripcionInexistente = "10009";
    public const string IdSuscripcionDispositivoInexistente = "20009";
    public const string SerialLogicoDispositivoNoAsociadoAEmpresa = "SerialLogicoNoAsociado";
    public const string MensajeSuscripcionDispositivoExistente = "Validación Exitosa";
    public const string MensajeSuscripcionDispositivoNoActiva = "Dispositivo no posee suscripción activa";
    public const string MensajeSuscripcionDispositivoNoVigente = "Dispositivo no posee suscripción vigente";
    public const string MensajeSuscripcionDispositivoAunNoVigente = "Suscripción aún no está vigente";
    public const string MensajeSuscripcionDispositivoInexistente = "Dispositivo no posee suscripción asociada";
    public const string MensajeSuscripcionDispositivoNoAsociadoAEmpresa = "SerialLogico no se encuentra asociado a la empresa";
    public const string MensajeRegistroNoEncontradoDispositivoSerialLogicoInexistente = "Registro no encontrado para el dispositivo con SerialLogico";
  }
}
