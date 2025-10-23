using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutorModels
{
    public class Usuario
    {
        public Usuario()

        {

        }

        public Guid Id { get; set; } //Global Unit Identifier

        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")] //validacion

        public string Email { get; set; }

        public string Password { get; set; }

    }

    public class UsuarioDTOC //Data Transform Object

    {

        public UsuarioDTOC()

        {

        }

        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address.")] //validacion email

        public string Email { get; set; }

        public string Password { get; set; }


    }

}
