using Microsoft.EntityFrameworkCore;
using Proyecto_POO.Data;
using Proyecto_POO.Models;
using System.Data.SqlTypes;

namespace Proyecto_POO.Services
{
    public class PersonService : IPersonService
    {
        private readonly ProjectDbContext _context;

        public PersonService(ProjectDbContext context)
        {
            _context = context;
        }

        public void CrearPersona(Person person)
        {
            person.CalcularEdades();
            _context.Persons.Add(person);
            _context.SaveChanges();

            person.User = GenerarUsuario(person);

            _context.SaveChanges();
        }

        public IEnumerable<Person> ObtenerTodasLasPersonas()
        {
            return _context.Persons.Include(p => p.User).ToList();
        }

        public Person? PersonaPorID(int id)
        {
            return _context.Persons.Include(_p => _p.User).FirstOrDefault(p => p.Id == id);
        }

        public Person? PersonaPorIdentificacion(string identificacion)
        {
            return _context.Persons.Include(p => p.User)
                .FirstOrDefault(p => p.Identificacion == identificacion);
        }

        public IEnumerable<Person> PersonaPorEdad(int edad)
        {
            return _context.Persons.Include (_p => _p.User).Where(p => p.Fechanacimiento.HasValue && (DateTime.Now.Year - p.Fechanacimiento.Value.Year) == edad)
                .ToList();
        }

        public IEnumerable<Person> PersonaPorPNombre(string Pnombre)
        {
            return _context.Persons.Include(p => p.User).Where(p => p.Pnombre.Contains(Pnombre))
                .ToList();
        }

        public IEnumerable<Person> PersonaPorApellido(string PApellido)
        {
            return _context.Persons.Include(p => p.User).Where(p => p.Papellido.Contains(PApellido))
                .ToList();
        }

        public bool ActualizarPersona(Person person)
        {
            _context.Persons.Update(person);
            return _context.SaveChanges() > 0;
        }

        public bool EliminarPersona(int id)
        {
            var person = _context.Persons.Find(id);
            if (person == null) return false;
            _context.Persons.Remove(person);
            return _context.SaveChanges() > 0;
        }

        public bool CambiarPassword(int personaid, string newPasswrod)
        {
            var user = _context.Users.Find(personaid);
            if (user == null) return false;
            user.Password = newPasswrod;
            return _context.SaveChanges() > 0;
        }

        public User GetUserDetails(int personid)
        {
            return _context.Users.Where(u => u.Idpersona == personid).FirstOrDefault();

        }








        public string GenerarLogin(Person person)
        {
            return $"{person.Pnombre}{person.Papellido[0]}{person.Id}";
        }

        public string GenerarPassword()
        {
            return Guid.NewGuid().ToString().Substring(0,8);
        }

        public User GenerarUsuario(Person person)
        {
            return new User()
            {
                Idpersona = person.Id,
                Login = GenerarLogin(person),
                Password = GenerarPassword()
            };
        }
          
    }
}
