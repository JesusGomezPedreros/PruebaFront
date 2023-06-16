using Microsoft.Extensions.Configuration;
using Models.Models;
using Newtonsoft.Json;
using Servicios;

namespace Services.Services
{
    public class PruebaServices
    {
        private readonly DBHelper _dbHelper;
        private readonly IConfiguration _configuration;
        public PruebaServices(DBHelper dbHelper, IConfiguration configuration)
        {
            _dbHelper = dbHelper;
            _configuration = configuration;
        }

        public async Task<string> ConsultaEstudiantes()
        {
            var urlApi = _configuration["PricingHost"];
            var urlRequest = "api/controller/ConsultaEstudiantes";
            return _dbHelper.EjecutarGet(urlApi, urlRequest); ;
        }
        public async Task<string> ConsultaModulos()
        {
            var urlApi = _configuration["PricingHost"];
            var urlRequest = "api/controller/ConsultaModulos";
            return _dbHelper.EjecutarGet(urlApi, urlRequest); ;
        }
        public async Task<string> ConsultaClases()
        {
            var urlApi = _configuration["PricingHost"];
            var urlRequest = "api/controller/ConsultaClases";
            return _dbHelper.EjecutarGet(urlApi, urlRequest); ;
        }
        public async Task<string> ConsultaModulosEstudiantes(int idEstudiante)
        {
            var urlApi = _configuration["PricingHost"];
            var urlRequest = "api/controller/ConsultaModuloClaseEstudiante?idEstudiante="+ idEstudiante;
            return _dbHelper.EjecutarGet(urlApi, urlRequest); ;
        }

        public async Task<string> RegistrarUsuario(RegistrarUsuario registrarUsuario)
        {
            var url = _configuration["PricingHost"];
            var request = "api/controller/RegistrarEstudiantes";
            var datos = JsonConvert.SerializeObject(registrarUsuario);
            return _dbHelper.EjecutarPostJson(datos, url + request);
        }
        public async Task<string> RegistrarClaseUsuario(RegistrarUnion registrarUnion)
        {
            var url = _configuration["PricingHost"];
            var request = "api/controller/InscribirModuloClase";
            var datos = JsonConvert.SerializeObject(registrarUnion);
            return _dbHelper.EjecutarPostJson(datos, url + request);
        }

    }
}
