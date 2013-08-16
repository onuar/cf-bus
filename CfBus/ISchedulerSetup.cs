using System;
using System.Linq.Expressions;

namespace CfBus
{
    public interface ISchedulerSetup<out TServiceType> : ISchedulerReturns
    {
        ISchedulerSetup<TServiceType> Setup(Action<TServiceType> exp);

        void Callback(Action<object> callbackAction);
    }
}