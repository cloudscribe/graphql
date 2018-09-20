using System;

namespace cloudscribe.Extensions.Blazor.Oidc
{
    public class User
    {
        /// <summary>
        /// Gets or sets the user scope
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets the user access token
        /// </summary>
        public string Access_Token { get; set; }

        /// <summary>
        /// Gets or sets the user identifier token
        /// </summary>
        public string Id_Token { get; set; }

        /// <summary>
        /// Gets or sets the user token type
        /// </summary>
        public string Token_Type { get; set; }

        /// <summary>
        /// The expires at returned from the OIDC provider.
        /// </summary>
        public int Expires_At { get; set; }

        public DateTime GetExpirationTime()
        {
            var jan1970 = Convert.ToDateTime("1970-01-01T00:00:00Z");
            return jan1970.AddSeconds(Expires_At);

        }
           
        

        /// <summary>
        /// Calculated number of seconds the access token has remaining.
        /// </summary>
       // public string Expires_In { get; set; }

        public ProfileProps Profile { get; set; }

        public class ProfileProps
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}
