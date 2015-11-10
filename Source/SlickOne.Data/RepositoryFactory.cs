using System;
using System.Collections.Generic;
using System.Linq;

namespace SlickOne.Data
{
    public class RepositoryFactory
    {
        private readonly static object _lock = new object();
        private static IRepository _instance;
        public static IRepository CreateRepository()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Repository();
                    }
                }
            }
            return _instance;
        }
    }
}
