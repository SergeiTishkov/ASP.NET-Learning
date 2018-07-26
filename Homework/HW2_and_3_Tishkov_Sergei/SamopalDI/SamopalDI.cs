using SamopalIndustries.Entities;
using SamopalIndustries.Entities.Exceptions;
using SamopalIndustries.Entities.KeysAndValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SamopalIndustries
{
    /// <summary>
    /// Simple Dependency Injector.
    /// </summary>
    public class SamopalDI
    {
        private Dictionary<Key, Value> _dict;

        /// <summary>
        /// Initializes a new instance of the SamopalDI class.
        /// </summary>
        public SamopalDI() : this(LateBindingOptions.MaxCtor)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SamopalDI class by using the specified LateBindingOptions object.
        /// </summary>
        /// <param name="options">Choose the kind of constructor that will be used in late binding process.</param>
        public SamopalDI(LateBindingOptions options)
        {
            LateBindingOption = options;
            _dict = new Dictionary<Key, Value>(new KeyComparer());
        }

        public LateBindingOptions LateBindingOption { get; set; }

        /// <summary>
        /// Binds class TKey as a default value to itself.
        /// Instance will be created by reflection.
        /// Make sure TKey isn't abstract before binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        public void BindDefault<TKey>()
        {
            Bind<TKey, TKey>(0, null, null, false);
        }

        /// <summary>
        /// Binds returned by Func delegate object as a default value to class TKey.
        /// Make sure returning object is convertable to TKey before binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <param name="creatorWOArgs">Delegate responsible for returning the instance of TKey.</param>
        public void BindDefault<TKey>(Func<object> creatorWOArgs)
        {
            Bind<TKey, TKey>(0, creatorWOArgs, null, false);
        }

        /// <summary>
        /// Binds returned by Func delegate object as a default value to class TKey.
        /// Make sure returning object is convertable to TKey before binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <param name="creatorWithArgs">Delegate responsible for returning the instance of TKey.</param>
        public void BindDefault<TKey>(Func<object[], object> creatorWithArgs)
        {
            Bind<TKey, TKey>(0, null, creatorWithArgs, false);
        }

        /// <summary>
        /// Binds class TValue as a default value to class TKey.
        /// Instance will be created by invoking construtor with the largest number of parameters.
        /// Make sure TValue isn't abstract before binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <typeparam name="TValue">Type to which binding type is binding.</typeparam>
        public void BindDefault<TKey, TValue>()
            where TValue : TKey
        {
            Bind<TKey, TValue>(0, null, null, false);
        }

        /// <summary>
        /// Binds class TKey as a specific example value to itself.
        /// Instance will be created by reflection.
        /// If specific example is zero, ArgumentException will be thrown.
        /// Make sure TKey isn't abstract before binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <param name="example">Specific example of this binding.</param>
        /// <exception cref="ArgumentException"></exception>
        public void BindExample<TKey>(int example)
        {
            Bind<TKey, TKey>(example, null, null, true);
        }

        /// <summary>
        /// Binds returned by Func delegate object as a specific example value to class TKey.
        /// If specific example is zero, ArgumentException will be thrown.
        /// Make sure returning object is convertable to TKey before binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <param name="example">Specific example of this binding.</param>
        /// <param name="creatorWOArgs">Delegate responsible for returning the instance of TKey.</param>
        /// <exception cref="ArgumentException"></exception>
        public void BindExample<TKey>(int example, Func<object> creatorWOArgs)
        {
            Bind<TKey, TKey>(example, creatorWOArgs, null, true);
        }

        /// <summary>
        /// Binds returned by Func delegate object as a specific example value to class TKey.
        /// If specific example is zero, ArgumentException will be thrown.
        /// Make sure returning object is convertable to TKey before binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <param name="example">Specific example of this binding.</param>
        /// <param name="creatorWithArgs">Delegate responsible for returning the instance of TKey.</param>
        /// <exception cref="ArgumentException"></exception>
        public void BindExample<TKey>(int example, Func<object[], object> creatorWithArgs)
        {
            Bind<TKey, TKey>(example, null, creatorWithArgs, true);
        }

        /// <summary>
        /// Binds class TValue as a specific example value to class TKey.
        /// Instance will be created by invoking construtor with the largest number of parameters.
        /// If specific example is zero, ArgumentException will be thrown.
        /// Make sure TValue isn't abstract before binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <typeparam name="TValue">Type to which binding type is binding.</typeparam>
        /// <param name="example">Specific example of this binding.</param>
        /// <exception cref="ArgumentException"></exception>
        public void BindExample<TKey, TValue>(int example)
            where TValue : TKey
        {
            Bind<TKey, TValue>(example, null, null, true);
        }

        /// <summary>
        /// Returns the default value of the TKey, returning by reflection.
        /// Instance will be created by invoking construtor with the largest number of parameters.
        /// Make sure that the TKey and all the parameters of the largest constructor were binded before invocation of this method.
        /// If you didn't do default bind, ArgumentException will be thrown.
        /// </summary>
        /// <typeparam name="TKey">Returning type.</typeparam>
        /// <returns>Instance of TKey.</returns>
        /// <exception cref="ArgumentException"></exception>
        public TKey GetDefault<TKey>()
        {
            return GetAndConvert<TKey>(0, null);
        }

        /// <summary>
        /// Returns the default value of the TKey, returning by delegate.
        /// If you didn't do default bind, ArgumentException will be thrown.
        /// </summary>
        /// <typeparam name="TKey">Returning type.</typeparam>
        /// <param name="args">Arguments to invoke the default delegate.</param>
        /// <returns>Instance of TKey.</returns>
        /// <exception cref="ArgumentException"></exception>
        public TKey GetDefault<TKey>(object[] args)
        {
            return GetAndConvert<TKey>(0, args);
        }

        /// <summary>
        /// Returns the specific example value of the TKey, returning by reflection.
        /// Instance will be created by invoking construtor with the largest number of parameters.
        /// Make sure that the TKey and all the parameters of the largest constructor were binded before invocation of this method.
        /// If you didn't do specific example bind, ArgumentException will be thrown.
        /// </summary>
        /// <typeparam name="TKey">Returning type.</typeparam>
        /// <param name="example">Specific example of creation of the returning type.</param>
        /// <returns>Instance of TKey.</returns>
        /// <exception cref="ArgumentException"></exception>
        public TKey GetExample<TKey>(int example)
        {
            return GetAndConvert<TKey>(example, null);
        }

        /// <summary>
        /// Returns the specific example value of the TKey, returning by delegate.
        /// If you didn't do specific example bind, ArgumentException will be thrown.
        /// </summary>
        /// <typeparam name="TKey">Returning type.</typeparam>
        /// <param name="example">Specific example of creation of the returning type.</param>
        /// <param name="args">Arguments to invoke the default delegate.</param>
        /// <returns>Instance of TKey.</returns>
        /// <exception cref="ArgumentException"></exception>
        public TKey GetExample<TKey>(int example, object[] args)
        {
            return GetAndConvert<TKey>(example, args);
        }

        private void Bind<TKey, TValue>(int example, Func<object> creatorWOArgs, Func<object[], object> creatorWithArgs, bool isExampleBind)
            where TValue : TKey
        {
            if (isExampleBind && example == 0)
            {
                throw new ArgumentException("Zero example is reserved for default bind and not available for binding new examples manually.");
            }

            Key key = new Key(typeof(TKey), example);
            Value value = new Value(typeof(TValue), creatorWOArgs, creatorWithArgs);

            if (_dict.ContainsKey(key))
            {
                _dict[key] = value;
            }
            else
            {
                _dict.Add(key, value);
            }
        }

        private TKey GetAndConvert<TKey>(int example, object[] args)
        {
            object result = Get(typeof(TKey), example, args);
            if(!(result is TKey))
            {
                throw new InvalidDelegateReturnTypeException($"Your delegate return type isn't convertible to {typeof(TKey).FullName}.");
            }
            return (TKey)result;
        }

        private object Get(Type keyType, int example, object[] args)
        {
            Key key = new Key(keyType, example);
            Value value;
            try
            {
                value = _dict[key];
            }
            catch(KeyNotFoundException e)
            {
                if (example == 0)
                    throw new ArgumentException($"You didn't do the default bind of {keyType.FullName}", e);
                else
                    throw new ArgumentException($"You didn't do the {example} specific example bind of {keyType.FullName}", e);
            }

            if(value.CreatorWOArgs != null)
            {
                return value.CreatorWOArgs();
            }
            if (value.CreatorWithArgs != null)
            {
                return value.CreatorWithArgs(args);
            }
            else
            {
                return GetByReflection(value.Type);
            }
        }

        private object GetByReflection(Type type)
        {
            ConstructorInfo ctor = GetConstructor(type);
            ParameterInfo[] parameterInfos = ctor.GetParameters();

            object[] parameters = new object[parameterInfos.Length];

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                Type typeOfParameter = parameterInfos[i].ParameterType;
                parameters[i] = Get(typeOfParameter, 0, null);
            }

            return ctor.Invoke(parameters);
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            List<ConstructorInfo> ctors = type.GetConstructors().Where(info => info.IsPublic).ToList();
            ctors.Sort(CompareCtorInfo);
            ConstructorInfo result = null;

            switch (LateBindingOption)
            {
                case LateBindingOptions.DefaultCtor:
                    result = ctors[0];
                    if (result.GetParameters().Length > 0)
                        throw new LateBindingException
                            ($"There isn't default constructor of {type.FullName}.\nIf you don't want to get this type of exception again, just change the value of the LateBindingOption property.");
                    break;
                case LateBindingOptions.DefaultOrMinCtor:
                    result = ctors.First();
                    break;
                case LateBindingOptions.DefaultOrMaxCtor:
                    if(ctors[0].GetParameters().Length == 0)
                    {
                        result = ctors.First();
                    }
                    else
                    {
                        result = ctors.Last();
                    }
                    break;
                case LateBindingOptions.MaxCtor:
                    result = ctors.Last();
                    break;
            }
            return result;
        }

        private int CompareCtorInfo(ConstructorInfo c1, ConstructorInfo c2)
        {
            var args1 = c1.GetParameters();
            var args2 = c2.GetParameters();

            if (args1.Length > args2.Count())
                return 1;
            else if (args1.Length < args2.Count())
                return -1;
            else return 0;
        }
    }
}