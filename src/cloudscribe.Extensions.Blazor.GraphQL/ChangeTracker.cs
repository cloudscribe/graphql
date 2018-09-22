// Author:					Joe Audette
// Created:					2018-09-22
// Last Modified:			2018-09-22
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace cloudscribe.Extensions.Blazor.GraphQL
{
    /// <summary>
    /// a class that can get just the changed properties of a model as a dictionary.
    /// Used for sending a patch of just the changed model props to graphql endpoint
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ChangeTracker<T> where T : class
    {
        public ChangeTracker(T model)
        {
            _model = model;
            
            _originalValues = new Dictionary<string, object>();

            _modelProps = _model.GetType().GetProperties();
            foreach(var prop in _modelProps)
            {
                _originalValues.Add(prop.Name, prop.GetValue(_model));
            }

        }

        private PropertyInfo[] _modelProps;
        private T _model;
        private Dictionary<string, object> _originalValues;

        public Dictionary<string, object> GetChanges()
        {
            // Get all properties
            var tempProperties = _model.GetType().GetProperties().ToArray();

            // Filter properties by only getting what has changed
            var changedProps = tempProperties.Where(p => !Equals(p.GetValue(_model, null), _originalValues[p.Name])).ToArray();

            var comparer = StringComparer.OrdinalIgnoreCase;
            var latestChanges = new Dictionary<string, object>(comparer);

            foreach (var property in changedProps)
            {
                latestChanges.Add(
                    property.Name.ToLowerFirstChar(), //we need the first char lower for graphql
                    property.GetValue(_model)
                    );
            }

            return latestChanges;
        }


    }
}
