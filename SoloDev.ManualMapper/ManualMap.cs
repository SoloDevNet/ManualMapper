using System;
using System.Collections.Generic;
using System.Text;

namespace SoloDev.ManualMapper
{
    public abstract class ManualMap<TSource, TDestination>
            where TSource : class, new()
            where TDestination : class, new()
    {
        public abstract void Map(TSource source, TDestination destination);

        internal void InvokeMap(TSource source, TDestination destination = null)
        {
            destination = destination ?? new TDestination();

            Map(source, destination);
        }
    }
}
