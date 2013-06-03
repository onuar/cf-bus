namespace CfBus
{
    using System.Reflection;

    public interface IServiceInvoker
    {
        object Invoke(object serviceInstance, string methodName, object[] arguments);

        object Invoke(object serviceInstance, MethodInfo method, object[] arguments);
    }
}