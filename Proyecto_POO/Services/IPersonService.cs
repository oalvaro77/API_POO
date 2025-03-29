using Proyecto_POO.Models;

namespace Proyecto_POO.Services
{
    public interface IPersonService
    {
        void CrearPersona(Person person);
        IEnumerable<Person> ObtenerTodasLasPersonas();

        Person PersonaPorID(int id);
        Person? PersonaPorIdentificacion(string identificacion);
        IEnumerable<Person> PersonaPorEdad(int edad);
        IEnumerable<Person> PersonaPorPNombre(string Pnombre);
        IEnumerable<Person> PersonaPorApellido(string PApellido);


        bool ActualizarPersona(Person person);
        bool EliminarPersona(int id);
        bool CambiarPassword(int personaid, string newPasswrod);
        User GetUserDetails(int personid);


        string GenerarLogin(Person person);
        string GenerarPassword();
        User GenerarUsuario(Person person);
    }
}
