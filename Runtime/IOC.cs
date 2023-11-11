namespace Reko3d
{
    public static class IOC
    {
        public static void Register<T>(T instance)
        {
            ServiceLocator.Instance.Register<T>(instance);
        }
        public static void Unregister<T>(T instance)
        {
            ServiceLocator.Instance.Unregister(instance);
        }
        public static T Get<T>()
        {
            return ServiceLocator.Instance.Get<T>();
        }

        public static bool TryGet<T>(out T instance)
        {
            return ServiceLocator.Instance.TryGet<T>(out instance);
        }
        public static void Clear()
        {
            ServiceLocator.Instance.Clear();
        }
    }
}

