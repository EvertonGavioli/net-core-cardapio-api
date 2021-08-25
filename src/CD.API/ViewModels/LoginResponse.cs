using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CD.API.ViewModels
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public double ExpiracaoEm { get; set; }
        public UsuarioToken Usuario { get; set; }
    }

    public class UsuarioToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }
    }

    public class UsuarioClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
