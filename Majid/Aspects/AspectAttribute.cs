using System;
using System.Reflection;

namespace Majid.Aspects
{
    //THIS NAMESPACE IS WORK-IN-PROGRESS

    internal abstract class AspectAttribute : Attribute
    {
        public Type InterceptorType { get; set; }

        protected AspectAttribute(Type interceptorType)
        {
            InterceptorType = interceptorType;
        }
    }

    internal interface IMajidInterceptionContext
    {
        object Target { get; }

        MethodInfo Method { get; }

        object[] Arguments { get; }

        object ReturnValue { get; }

        bool Handled { get; set; }
    }

    internal interface IMajidBeforeExecutionInterceptionContext : IMajidInterceptionContext
    {

    }


    internal interface IMajidAfterExecutionInterceptionContext : IMajidInterceptionContext
    {
        Exception Exception { get; }
    }

    internal interface IMajidInterceptor<TAspect>
    {
        TAspect Aspect { get; set; }

        void BeforeExecution(IMajidBeforeExecutionInterceptionContext context);

        void AfterExecution(IMajidAfterExecutionInterceptionContext context);
    }

    internal abstract class MajidInterceptorBase<TAspect> : IMajidInterceptor<TAspect>
    {
        public TAspect Aspect { get; set; }

        public virtual void BeforeExecution(IMajidBeforeExecutionInterceptionContext context)
        {
        }

        public virtual void AfterExecution(IMajidAfterExecutionInterceptionContext context)
        {
        }
    }

    internal class Test_Aspects
    {
        internal class MyAspectAttribute : AspectAttribute
        {
            public int TestValue { get; set; }

            public MyAspectAttribute()
                : base(typeof(MyInterceptor))
            {
            }
        }

        internal class MyInterceptor : MajidInterceptorBase<MyAspectAttribute>
        {
            public override void BeforeExecution(IMajidBeforeExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }

            public override void AfterExecution(IMajidAfterExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }
        }

        public class MyService
        {
            [MyAspect(TestValue = 41)] //Usage!
            public void DoIt()
            {

            }
        }

        public class MyClient
        {
            private readonly MyService _service;

            public MyClient(MyService service)
            {
                _service = service;
            }

            public void Test()
            {
                _service.DoIt();
            }
        }
    }
}
