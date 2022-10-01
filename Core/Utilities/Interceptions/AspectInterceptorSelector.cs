using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptions
{
    public class AspectInterceptorSelector :IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type , MethodInfo method ,  IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttirbute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttirbute>(true);
            classAttributes.AddRange(methodAttributes);

            return classAttributes.OrderBy ( x => x.Priority).ToArray();




        } 
    }
}
