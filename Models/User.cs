namespace HotelUColombia.Models
{
    public class User : BaseClass
    {
        /// <summary>
        /// Get or Set Nombre
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Get or Set Apellido
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Get or Set Rol
        /// </summary>
        public string? Rol { get; set; }

        /// <summary>
        /// Get or Set Correo
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Get or Set Comentario
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Get or Set Nombre_Usuario
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// Get or Set Contraseña
        /// </summary>
        public string? Password { get; set; }
    }
}
