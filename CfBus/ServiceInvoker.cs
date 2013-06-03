namespace CfBus
{
    using System.Reflection;

    public class ServiceInvoker : IServiceInvoker
    {
        public object Invoke(object serviceInstance, string methodName, object[] arguments)
        {
            var serviceType = serviceInstance.GetType();
            var method = serviceType.GetMethod(methodName);
            if (method == null)
            {
                throw new MethodNotFoundException(methodName);
            }

            var result = Invoke(serviceInstance, method, arguments);
            return result;
        }

        public object Invoke(object serviceInstance, MethodInfo method, object[] arguments)
        {
            return method.Invoke(serviceInstance, arguments);
        }
    }
}