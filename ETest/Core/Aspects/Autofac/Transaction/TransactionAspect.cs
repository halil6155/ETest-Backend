using System.Transactions;
using Castle.DynamicProxy;
using Core.Exceptions;
using Core.Utilities.Interceptors;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using var transactionScope = new TransactionScope();
            try
            {
                invocation.Proceed();
                transactionScope.Complete();
            }
            catch (System.Exception ex)
            {
                throw new TransactionScopeException(ex.Message);
            }
        }
    }
}