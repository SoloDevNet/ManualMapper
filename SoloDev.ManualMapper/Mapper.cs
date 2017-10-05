using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace SoloDev.ManualMapper
{
    public static class Mapper
    {
        internal static Dictionary<Type, Dictionary<Type, Type>> Mappings { get; } = new Dictionary<Type, Dictionary<Type, Type>>();

        private static Configuration config;

        public static void Configure(Action<Configuration> configurationAction)
        {
            config = config ?? new Configuration();
            configurationAction(config);
        }

        public static object Map(object instance, object destination, Type sourceType, Type destinationType)
        {
            var mapperType = Mappings?[sourceType]?[destinationType];

            if (mapperType == null)
            {
                throw new Exception("No such mapping");
            }

            var mapper = Activator.CreateInstance(mapperType);
            mapperType
                .GetRuntimeMethods()
                .SingleOrDefault(x => x.Name == "InvokeMap")
                .Invoke(mapper, new object[] { instance, destination });

            return destination;
        }

        public static TDestination Map<TDestination>(object instance)
            where TDestination : class, new()
        {
            return Map(instance, new TDestination(), instance.GetType(), typeof(TDestination)) as TDestination;
        }

        public static void Map<TSource, TDestination>(TSource instance, TDestination destination)
            where TSource : class, new()
            where TDestination : class, new()
        {
            Map(instance, destination, typeof(TSource), typeof(TDestination));
        }

        public static void RegisterMap(Type map, Type sourceType, Type destinationType)
        {
            if (!Mappings.ContainsKey(sourceType))
            {
                Mappings.Add(sourceType, new Dictionary<Type, Type>());
            }

            var sourceDic = Mappings[sourceType];

            sourceDic.Add(destinationType, map);
        }

        public static void RegisterMap<TSource, TDestination>(ManualMap<TSource, TDestination> map)
            where TSource : class, new()
            where TDestination : class, new()
        {
            RegisterMap(map.GetType(), typeof(TSource), typeof(TDestination));
        }

        public static void RegisterMapsAssembly(Assembly assembly)
        {
            foreach (var type in assembly.ExportedTypes.Where(x => IsSubclassOfRawGeneric(typeof(ManualMap<,>), x)))
            {
                var mapType = GetSubclassOfRawGeneric(typeof(ManualMap<,>), type);
                var source = mapType.GenericTypeArguments.ElementAt(0);
                var destination = mapType.GenericTypeArguments.ElementAt(1);

                RegisterMap(type, source, destination);
            }
        }

        private static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            return GetSubclassOfRawGeneric(generic, toCheck) != null;
        }
        
        private static Type GetSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.GetTypeInfo().IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return toCheck;
                }
                toCheck = toCheck.GetTypeInfo().BaseType;
            }
            return null;
        }
    }
}
