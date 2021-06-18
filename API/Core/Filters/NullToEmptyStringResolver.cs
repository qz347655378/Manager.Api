using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Core.Filters
{
    public class NullToEmptyStringResolver : CamelCasePropertyNamesContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return type.GetProperties().Select(c =>
            {
                var jsonProperty = base.CreateProperty(c, memberSerialization);
                jsonProperty.ValueProvider = new NullToEmptyStringValueProvider(c);
                return jsonProperty;
            }).ToList();
        }
    }

    public class NullToEmptyStringValueProvider : IValueProvider
    {
        private readonly PropertyInfo _propertyInfo;

        public NullToEmptyStringValueProvider(PropertyInfo propertyInfo)
        {
            this._propertyInfo = propertyInfo;
        }

        public void SetValue(object target, object value)
        {
            _propertyInfo.SetValue(target, value);
        }

        public object GetValue(object target)
        {
            var result = _propertyInfo.GetValue(target);
            if (_propertyInfo.PropertyType == typeof(string) && result == null)
            {
                result = string.Empty;
            }

            return result;
        }
    }
}
