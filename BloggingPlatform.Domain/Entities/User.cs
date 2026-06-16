using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        public ICollection<Article> Articles { get; private set; } = new List<Article>();

        // Requerido por EF Core
        private User() { }

        public User(string? username, string? email)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario no puede estar vacío.", nameof(username));

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("El formato del correo electrónico no es válido.", nameof(email));

            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Los datos de actualización no pueden ser vacíos.");

            Username = username;
            Email = email;
        }
    }
}
