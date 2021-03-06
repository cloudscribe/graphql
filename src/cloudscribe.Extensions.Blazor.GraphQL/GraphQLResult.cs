﻿namespace cloudscribe.Extensions.Blazor.GraphQL
{
    public class GraphQLResult<T> where T : class
    {
        public GraphQLResult(T model, ErrorModel errors)
        {
            SuccessResult = model;
            Errors = errors;
        }

        public T SuccessResult { get; private set; } 

        public ErrorModel Errors { get; private set; } 

    }
}
