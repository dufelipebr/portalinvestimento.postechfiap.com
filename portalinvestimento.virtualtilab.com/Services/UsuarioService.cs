using portalinvestimento.virtualtilab.com.Interfaces.Service;

namespace portalinvestimento.virtualtilab.com.Services
{
    public class UsuarioService : IUsuarioService
    {
        //private readonly IConfiguration _configuration;

        //public UsuarioService(IConfiguration cfg)
        //{
        //    _configuration = cfg;

        //}

        public bool CheckPassword(string password)
        {
            var passwd = password;
            
            //Min 8 char and max 30 char
            if (passwd.Length < 8 || passwd.Length > 30)
                return false;

            //One upper case
            if (!passwd.Any(char.IsUpper))
                return false;
            
            //Atleast one lower case
            if (!passwd.Any(char.IsLower))
                return false;
            
            //No white space
            if (passwd.Contains(" "))
                return false;

            //Check for one special character
            string specialCh = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialChArray = specialCh.ToCharArray();
            foreach (char ch in specialChArray)
            {
                if (passwd.Contains(ch))
                    return true;
            }

            return false;
        }
    }
}
