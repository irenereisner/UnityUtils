using System;
using System.Collections.Generic;


namespace Reko3d
{
    internal class ServiceLocator
    {
        private static ServiceLocator m_instance;

        public static ServiceLocator Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new ServiceLocator();
                }

                return m_instance;
            }
        }
        
        
        private Dictionary<Type, object> dependencies = new Dictionary<Type, object>();


        public void Register(Type type, object instance)
        {
            dependencies[type] = instance;
        }

        public void Unregister(Type type, object instance)
        {
            if (dependencies.TryGetValue(type, out object boundInstance))
            {
                if(boundInstance == instance)
                {
                    dependencies.Remove(type);
                }
            }
        }

        public object Get(Type type)
        {
            if (!dependencies.ContainsKey(type))
            {
                return null;
            }

            var dependency = dependencies[type];
            return dependency;
        }
        
        public bool TryGet(Type type, out Object instance)
        {
            return dependencies.TryGetValue(type, out instance);
        }

        public void Register<T>(T instance)
        {
            Register(typeof(T), instance);
        }
        public void Unregister<T>(T instance)
        {
            Unregister(typeof(T), instance);
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public bool TryGet<T>(out T instance)
        {
            if(dependencies.TryGetValue(typeof(T), out object objInstance))
            {
                instance = (T)objInstance;
                return instance != null;
            }

            instance = default;
            return false;
        }
        
        public void Clear()
        {
            dependencies.Clear();
        }
    }
}