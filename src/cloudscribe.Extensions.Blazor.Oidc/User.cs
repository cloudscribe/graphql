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
        //public string Expires_At { get; set; }

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
