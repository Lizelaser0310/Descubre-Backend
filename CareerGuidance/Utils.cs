using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CareerGuidance
{
    public class Utils
    {
        public class NonVirtualContractResolver : DefaultContractResolver
        {
            public NonVirtualContractResolver()
            {
                NamingStrategy = new CamelCaseNamingStrategy();
            }

            protected override JsonProperty CreateProperty(MemberInfo member,
                MemberSerialization memberSerialization)
            {
                var prop = base.CreateProperty(member, memberSerialization);
                var propInfo = (PropertyInfo) member;

                if (propInfo.GetMethod != null)
                {
                    // !propInfo.GetMethod.IsFinal can be added.
                    prop.ShouldSerialize = _ => !propInfo.GetMethod.IsVirtual;
                }

                return prop;
            }
        }
    
        public class KebabCaseParameterTransformer : IOutboundParameterTransformer
        {
            public string TransformOutbound(object? value)
            {
                var input = value?.ToString() ?? string.Empty;
                var replace = Regex.Replace(input, "([a-z])([A-Z])", "$1-$2",
                    RegexOptions.CultureInvariant);
                return replace.ToLowerInvariant();
            }
        }
    }
}