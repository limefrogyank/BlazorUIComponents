using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace BlazorUIComponents.Core
{
    public static class RegisterExtensions
    {
        /// <summary>
        /// Version of get service that uses a generic to resolve.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="resolver">The resolver to use to get the service.</param>
        /// <returns>The service.</returns>
        public static T GetServiceExt<T>(this IReadonlyDependencyResolver resolver, string contract = null)
        {
            return (T)resolver.GetService(typeof(T), contract);
        }

        /// <summary>
        /// Helper class for having a object's constructor automatically assigned by a "GetService" request.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <typeparam name="TConcrete">The concrete class type.</typeparam>
        /// <typeparam name="TInterface">The interface type.</typeparam>
        public static void Register<TConcrete, TInterface>(this IMutableDependencyResolver resolver, string contract = null)
        {
            resolver.Register(() =>
            {
                return Activator.CreateInstance<TConcrete>();

            }, typeof(TInterface));
            //Func<IMutableDependencyResolver, object> func = RegisterCache<TConcrete>.GetRegisterFunc();
            //resolver.Register(() => func(resolver), typeof(TInterface), contract);
        }

        ///// <summary>
        ///// Helper class for having a object's constructor automatically assigned by a "GetService" request.
        ///// </summary>
        ///// <param name="resolver">The resolver.</param>
        ///// <typeparam name="TConcrete">The concrete class type.</typeparam>
        public static void Register<TConcrete>(this IMutableDependencyResolver resolver)
        {
            resolver.Register(() =>
            {
                return Activator.CreateInstance<TConcrete>();

            }, typeof(TConcrete));
            //Func<IMutableDependencyResolver, object> func = RegisterCache<TConcrete>.GetRegisterFunc();
            //resolver.Register(() => func(resolver), typeof(TConcrete));
        }

        //public static void Register(this IMutableDependencyResolver resolver, Type implementationType, Type serviceType)
        //{
        //    //Debug.Assert(implementationType.ContainsGenericParameters != true);
        //    Func<IMutableDependencyResolver, object> func = RegisterCache.GetRegisterFunc(implementationType);
        //    //var genType = typeof(RegisterCache<>).MakeGenericType(type);
        //    //var method = genType.GetMethod("GetRegisterFunc", BindingFlags.Static | BindingFlags.NonPublic);
        //    //Func<IDependencyResolver, object> func = (Func<IDependencyResolver,object>)method.Invoke(null,null);
        //    resolver.Register(() => func(resolver), serviceType);
        //}

        //private static class RegisterCache<TConcrete>
        //{
        //    private static Func<IMutableDependencyResolver, object> cachedFunc;

        //    public static Func<IMutableDependencyResolver, object> GetRegisterFunc()
        //    {
        //        return cachedFunc ?? (cachedFunc = GenerateFunc());
        //    }

        //    private static Func<IMutableDependencyResolver, object> GenerateFunc()
        //    {
        //        try
        //        {
        //            ParameterExpression funcParameter = Expression.Parameter(typeof(IMutableDependencyResolver));

        //            Type concreteType = typeof(TConcrete);

        //            // Must be a single constructor
        //            ConstructorInfo constructor =
        //                //concreteType.GetTypeInfo().DeclaredConstructors.Single(x => x.GetParameters().Length > 0);
        //                concreteType.GetTypeInfo().DeclaredConstructors.First();

        //            Debug.Assert(constructor.ContainsGenericParameters != true);

        //            //OK, should really check to see if one of the methods has arguments that can ALL be supported... so just pick first for now.

        //            MethodInfo getServiceMethodInfo =
        //                typeof(IDependencyResolver).GetTypeInfo().GetDeclaredMethod("GetService");

        //            IList<Expression> parameterExpressions =
        //                constructor.GetParameters().Select(
        //                    parameter =>
        //                        Expression.Convert(
        //                            Expression.Call(
        //                                funcParameter,
        //                                getServiceMethodInfo,
        //                                Expression.Constant(parameter.ParameterType),
        //                                Expression.Convert(Expression.Constant(null), typeof(string))),
        //                            parameter.ParameterType)).Cast<Expression>().ToList();

        //            NewExpression newValue = Expression.New(constructor, parameterExpressions);
        //            Expression converted = Expression.Convert(newValue, typeof(object));
        //            return Expression.Lambda<Func<IMutableDependencyResolver, object>>(converted, funcParameter).Compile();
        //        }
        //        catch (Exception ex)
        //        {
        //            var e = ex;
        //            return null;
        //        }
        //    }
        //}

        //private static class RegisterCache
        //{
        //    private static Func<IMutableDependencyResolver, object> cachedFunc;

        //    public static Func<IMutableDependencyResolver, object> GetRegisterFunc(Type type)
        //    {
        //        return cachedFunc ?? (cachedFunc = GenerateFunc(type));
        //    }

        //    private static Func<IMutableDependencyResolver, object> GenerateFunc(Type type)
        //    {
        //        try
        //        {
        //            ParameterExpression funcParameter = Expression.Parameter(typeof(IMutableDependencyResolver));

        //            Type concreteType = type;
        //            //if (type.IsGenericType)
        //            //{
        //            //    var genArgs = type.GetGenericArguments();
        //            //    concreteType.GetTypeInfo().DeclaredConstructors.First()
        //            //}

        //            // Must be a single constructor
        //            ConstructorInfo constructor =
        //                //concreteType.GetTypeInfo().DeclaredConstructors.Single(x => x.GetParameters().Length > 0);
        //                concreteType.GetTypeInfo().DeclaredConstructors.First();

        //            Debug.Assert(constructor.ContainsGenericParameters != true);
        //            //OK, should really check to see if one of the methods has arguments that can ALL be supported... so just pick first for now.

        //            MethodInfo getServiceMethodInfo =
        //                typeof(IMutableDependencyResolver).GetTypeInfo().GetDeclaredMethod("GetService");

        //            IList<Expression> parameterExpressions =
        //                constructor.GetParameters().Select(
        //                    parameter =>
        //                        Expression.Convert(
        //                            Expression.Call(
        //                                funcParameter,
        //                                getServiceMethodInfo,
        //                                Expression.Constant(parameter.ParameterType),
        //                                Expression.Convert(Expression.Constant(null), typeof(string))),
        //                            parameter.ParameterType)).Cast<Expression>().ToList();


        //            NewExpression newValue = Expression.New(constructor, parameterExpressions);
        //            Expression converted = Expression.Convert(newValue, typeof(object));
        //            return Expression.Lambda<Func<IMutableDependencyResolver, object>>(converted, funcParameter).Compile();
        //        }
        //        catch (Exception ex)
        //        {
        //            var e = ex;
        //            return null;
        //        }
        //    }
        //}

    }
}
