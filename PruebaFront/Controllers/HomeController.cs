using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Newtonsoft.Json;
using PruebaFront.Models;
using Services.Services;
using ServiceStack;
using System.Diagnostics;

namespace PruebaFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PruebaServices _PruebaServices;

        public HomeController(ILogger<HomeController> logger, PruebaServices pruebaServices)
        {
            _logger = logger;
            _PruebaServices = pruebaServices;
        }

        public IActionResult Index()
        {
            var modelEstudiantes = new List<RegistrarUsuario>();
            var modelModulos = new List<Modulos>();
            var modelClases = new List<Clases>();
            var listaEstudiantes = new List<RegistrarUsuario>();
            var listaModulos = new List<Modulos>();
            var listaClases = new List<Clases>();

            try
            {
                Parallel.Invoke(
                    () => modelEstudiantes = JsonConvert.DeserializeObject<List<RegistrarUsuario>>(_PruebaServices.ConsultaEstudiantes().Result),
                    () => modelModulos = JsonConvert.DeserializeObject<List<Modulos>>(_PruebaServices.ConsultaModulos().Result),
                    () => modelClases = JsonConvert.DeserializeObject<List<Clases>>(_PruebaServices.ConsultaClases().Result)
                    );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Error));

            }
            foreach (var item in modelEstudiantes)
            {
                var datos = new RegistrarUsuario();
                datos.IdEstudiante = item.IdEstudiante;
                datos.nombres = item.nombres;
                datos.apellidos = item.apellidos;
                datos.identificacion = item.identificacion;
                datos.edad = item.edad;
                datos.tipoLicencia = item.tipoLicencia;
                listaEstudiantes.Add(datos);
            }

            var modeloVista = new
            {
                consultaEstudiantes = listaEstudiantes,
                consultaModulos = modelModulos,
                consultaClases = modelClases,

            };
            return View(modeloVista);
        }
        public async Task<IActionResult> ConsultaCursos(int estudianteId)
        {
            var modelCursos = await _PruebaServices.ConsultaModulosEstudiantes(estudianteId);
            var listaEstudiantesCursos = new List<RegistrarUsuario>();
            var modelos = JsonConvert.DeserializeObject<List<ModulosClasesEstudiante>>(modelCursos);
            foreach (var modulosCursos in modelos)
            {
                var estudianteCurso = new RegistrarUsuario();
                estudianteCurso.NombreModulo = modulosCursos.NombreModulo;
                estudianteCurso.NombreClase = modulosCursos.NombreClase;
                listaEstudiantesCursos.Add(estudianteCurso);
            }

            return Json(listaEstudiantesCursos);
        }

        public IActionResult RegistrarEstudiante()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarEstudiante(RegistrarUsuario registrarUsuario)
        {
            var estudiante = string.Empty;
            try
            {
                estudiante = await _PruebaServices.RegistrarUsuario(registrarUsuario);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction(nameof(Error));

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarClaseEstudiante(RegistrarUnion registrarUnion)
        {
            var estudiante = string.Empty;
            if (registrarUnion.idClase != 0 || registrarUnion.idModulo != 0)
            {
               
                try
                {
                    estudiante = JsonConvert.DeserializeObject<string>(_PruebaServices.RegistrarClaseUsuario(registrarUnion).Result);
                    Convert.ToString(estudiante);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return RedirectToAction(nameof(Error));

                }
            }
            else
            {
                estudiante = "0";
            }
            return Json(Convert.ToInt32(estudiante));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}