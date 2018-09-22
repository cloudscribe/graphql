using System;
using System.Collections.Generic;

namespace cloudscribe.Extensions.GraphQL
{
    public static class PatchExtensions
    {
        public static void ApplyPatch<TPatchModel, TDestinationModel>(this Dictionary<string, object> patch, TDestinationModel model) 
            where TPatchModel : class 
            where TDestinationModel : class
        {
            var allowedProps = typeof(TPatchModel).GetProperties();
            var modelProps = model.GetType().GetProperties();

            var comparer = StringComparer.OrdinalIgnoreCase;
            var caseInsensitivePatch = new Dictionary<string, object>(patch, comparer);

            foreach (var allowedProp in allowedProps)
            {
                foreach (var modeProp in modelProps)
                {
                    if (modeProp.Name == allowedProp.Name)
                    {
                        if (caseInsensitivePatch.ContainsKey(modeProp.Name))
                        {
                            var newValue = caseInsensitivePatch[modeProp.Name];
                            modeProp.SetValue(model, Convert.ChangeType(newValue, modeProp.PropertyType), null);
                        }
                    }
                }
            }

        }
    }
}
