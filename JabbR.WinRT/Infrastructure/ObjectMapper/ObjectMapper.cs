using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JabbR.WinRT.Infrastructure.ObjectMapper
{
    public class ObjectMapper
    {
        private static ObjectMapper _instance;
        public static ObjectMapper DefaultInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ObjectMapper();
                }

                return _instance;
            }
        }

        internal TypeMapper<TSource, TDestination> GetMapper<TSource, TDestination>() where TDestination : new()
        {
            return new TypeMapper<TSource, TDestination>();
        }
    }
}