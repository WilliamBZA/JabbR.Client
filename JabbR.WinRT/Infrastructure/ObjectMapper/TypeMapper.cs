using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections;

namespace JabbR.WinRT.Infrastructure.ObjectMapper
{
    public class TypeMapper<TSource, TDestination> where TDestination : new()
    {
        public TDestination Map(TSource source)
        {
            TDestination destination = new TDestination();

            MapOnto(source, destination);

            return destination;
        }

        public void MapOnto(TSource source, TDestination destination)
        {
            var destinationProperties = destination.GetType().GetTypeInfo().DeclaredProperties;

            var properties = source.GetType().GetTypeInfo().DeclaredProperties.Where(p => destinationProperties.Any(dp => dp.Name == p.Name && dp.SetMethod.IsPublic));
            foreach (var prop in properties)
            {
                var propertyValue = prop.GetValue(source);

                SetPropertyValue(destination, destinationProperties, propertyValue, prop.Name);
            }
        }

        private void SetPropertyValue(TDestination destination, IEnumerable<PropertyInfo> destinationProperties, object propertyValue, string propertyName)
        {
            var destinationProperty = destinationProperties.FirstOrDefault(p => p.Name == propertyName);

            if (destinationProperty != null)
            {
                if (propertyValue != null && propertyValue.GetType().IsConstructedGenericType)
                {
                    if (propertyValue is IEnumerable)
                    {
                        var sourceList = propertyValue as IEnumerable;
                        var currentValue = destinationProperty.GetValue(destination);

                        if (currentValue == null)
                        {
                            currentValue = Activator.CreateInstance(destinationProperty.PropertyType);
                            if (currentValue is IList)
                            {
                                var list = currentValue as IList;

                                var sourceGenericType = propertyValue.GetType().GenericTypeArguments.First();
                                var destinationGenericType = currentValue.GetType().GenericTypeArguments.First();

                                if (sourceGenericType != destinationGenericType)
                                {
                                    var mapper = CreateGeneric(typeof(TypeMapper<,>), sourceGenericType, destinationGenericType);

                                    foreach (var item in sourceList)
                                    {
                                        var destinationItem = mapper.GetType().GetTypeInfo().GetDeclaredMethod("Map").Invoke(mapper, new object[] { item });
                                        list.Add(destinationItem);
                                    }
                                }
                                else
                                {
                                    foreach (var item in sourceList)
                                    {
                                        list.Add(item);
                                    }
                                }
                            }

                            destinationProperty.SetValue(destination, currentValue);
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("ahhhhh, generics hell");
                    }
                }
                else
                {
                    if (propertyValue != null)
                    {
                        if (propertyValue.GetType() != destinationProperty.PropertyType)
                        {
                            var sourceGenericType = propertyValue.GetType();
                            var destinationGenericType = destinationProperty.PropertyType;

                            var mapper = CreateGeneric(typeof(TypeMapper<,>), sourceGenericType, destinationGenericType);
                            propertyValue = mapper.GetType().GetTypeInfo().GetDeclaredMethod("Map").Invoke(mapper, new object[] { propertyValue });
                        }
                    }

                    destinationProperty.SetValue(destination, propertyValue);
                }
            }
        }

        public static object CreateGeneric(Type generic, params Type[] innerTypes)
        {
            System.Type specificType = generic.MakeGenericType(innerTypes);
            return Activator.CreateInstance(specificType);
        }
    }
}