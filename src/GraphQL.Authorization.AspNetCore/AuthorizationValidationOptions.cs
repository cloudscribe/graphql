namespace GraphQL.Authorization.AspNetCore
{
    public class AuthorizationValidationOptions
    {
        /// <summary>
        /// generally best to make this false in production. when true the error details will include specific informatiion about why it failed including role names.
        /// </summary>
        public bool IncludeErrorDetails { get; set; }
    }
}
