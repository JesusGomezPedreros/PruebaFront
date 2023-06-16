
namespace Models.Models
{
    public class RegistrarUsuario: ModulosClasesEstudiante
    {
        public int IdEstudiante { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int identificacion { get; set; }
        public int edad { get; set; }
        public string tipoLicencia { get; set; }
    }


    public class ModulosClasesEstudiante
    {
        public string NombreModulo { get; set; }
        public string NombreClase { get; set; }
    } 
    
    public class Modulos
    {
        public int idModulo { get; set; }
        public string nombreModulo { get; set; }
    }
    public class Clases
    {
        public int idClase { get; set; }
        public string nombreClase { get; set; }
    }
    public class RegistrarUnion
    {
        public int idClase { get; set; }
        public int idModulo { get; set; }
        public int IdEstudiante { get; set; }
      
    }

    
}
