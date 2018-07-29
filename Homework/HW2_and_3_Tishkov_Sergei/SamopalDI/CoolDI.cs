using SamopalIndustries.Entities;
using SamopalIndustries.Entities.Enums;
using SamopalIndustries.Entities.Exceptions;
using SamopalIndustries.Entities.KeysAndValues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SamopalIndustries
{

    // Не до конца доделанный, но крутой (вроде) контейнер
    // ДЗ сделал со старым, SamopalDI контейнером
    // этот не успел  до конца дотестить, дописываю эти строчки
    // в 4 утра пятницы 27.07.2018
    // SamopalDI протестирован лучше
    // XML документация на конструкторах висит из SamopalDI
    // ещё в планах сделать возможным late binding из аргументов незабиндженых конструкторов, вызываемых через рефлексию
    // (если будет поставлена соответствующее булевое свойство)
    // и ещё одно свойство LateBindingOptions для рефлексивного создания незабиндженых классов
    // сделать бы ещё попытку создать незабиндженый класс через все конструкторы, начная с условленного в LateBindingOptions,
    // если другие конструкторы не преуспели, надо думать (рекурсию в catch блоке скорее всего попробую сделать)
    public class CoolDI
    {
        private Dictionary<Key, Value_CoolDI> _dict;

        /// <summary>
        /// Initializes a new instance of the CoolDI class.
        /// </summary>
        public CoolDI() : this(LateBindingOptions.MaxCtor)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CoolDI class by using the specified LateBindingOptions object.
        /// </summary>
        /// <param name="options">The kind of constructor that will be used in late binding process.</param>
        public CoolDI(LateBindingOptions options) : this(options, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the CoolDI class by using the specified LateBindingOptions object.
        /// </summary>
        /// <param name="bindedLateBindingOptions">The kind of constructor that will be using in creating binded types by reflection.</param>
        /// <param name="invokeUnbindedTypes">Set "true" to get unbinded types by reflection.</param>
        public CoolDI(LateBindingOptions bindedLateBindingOptions, bool invokeUnbindedTypes)
        {
            BindedLateBindingOption = bindedLateBindingOptions;
            InvokeUnbindedTypes = invokeUnbindedTypes;
            _dict = new Dictionary<Key, Value_CoolDI>(new KeyComparer());
        }

        /// <summary>
        /// Gets or sets options of late binding for this CoolDI instance.
        /// </summary>
        public LateBindingOptions BindedLateBindingOption { get; set; }

        /// <summary>
        /// Gets or sets whether you'd like to invoke unbinded types by LateBindingOption rule (set "true") or get UnbindedTypeException (set "false").
        /// </summary>
        public bool InvokeUnbindedTypes { get; set; }

        /// <summary>
        /// Returns special Binder object designed to bind TKey default example as singleton to some of optional variants of binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <returns>Special Binder type designed to bind TKey default example as singleton to some of optional variants of binding.</returns>
        public Binder<TKey> BindDefaultAsSingleton<TKey>()
        {
            return GetBinder<TKey>(0, false, true);
        }

        /// <summary>
        /// Returns special Binder object designed to bind TKey default example to some of optional variants of binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <returns>Special Binder object designed to bind TKey default example to some of optional variants of binding.</returns>
        public Binder<TKey> BindDefault<TKey>()
        {
            return GetBinder<TKey>(0, false, false);
        }

        /// <summary>
        /// Returns special Binder object designed to bind TKey specific example to some of optional variants of binding.
        /// </summary>
        /// <typeparam name="TKey">Binding type.</typeparam>
        /// <param name="example">Specific example of getting the TKey type instance.</param>
        /// <returns>Special Binder object designed to bind TKey specific example to some of optional variants of binding.</returns>
        public Binder<TKey> BindExample<TKey>(int example)
        {
            return GetBinder<TKey>(example, true, false);
        }

        /// <summary>
        /// Returns TKey instance getting by default example getting variant.
        /// Make sure you didn't bind TKey by DelegateWithArgs method before using this method.
        /// </summary>
        /// <typeparam name="TKey">Getting type.</typeparam>
        /// <returns>TKey instance getting by default example getting variant.</returns>
        public TKey GetDefault<TKey>()
        {
            return (TKey)GetObject(typeof(TKey), 0, null);
        }

        /// <summary>
        /// Returns TKey instance getting by default example getting variant, binded by DelegateWithArgs method.
        /// Make sure you binded TKey by DelegateWithArgs method before using this method.
        /// </summary>
        /// <typeparam name="TKey">Getting type.</typeparam>
        /// <param name="args">Arguments needed for delegate creating TKey instance.</param>
        /// <returns>TKey instance getting by default example getting variant.</returns>
        public TKey GetDefault<TKey>(params object[] args)
        {
            return (TKey)GetObject(typeof(TKey), 0, args);
        }

        /// <summary>
        /// Returns TKey instance getting by specific example getting variant.
        /// Make sure you didn't bind TKey by DelegateWithArgs method before using this method.
        /// </summary>
        /// <typeparam name="TKey">Getting type.</typeparam>
        /// <param name="example">Specific example of getting the instance of TKey.</param>
        /// <returns>TKey instance getting by specific example getting variant.</returns>
        public TKey GetExample<TKey>(int example)
        {
            return (TKey)GetObject(typeof(TKey), example, null);
        }

        /// <summary>
        /// Returns TKey instance getting by specific example getting variant.
        /// Make sure you binded TKey by DelegateWithArgs method before using this method.
        /// </summary>
        /// <typeparam name="TKey">Getting type.</typeparam>
        /// <param name="example">Specific example of getting the instance of TKey.</param>
        /// <param name="args">Arguments needed for delegate creating TKey instance.</param>
        /// <returns>TKey instance getting by specific example getting variant.</returns>
        public TKey GetExample<TKey>(int example, params object[] args)
        {
            return (TKey)GetObject(typeof(TKey), example, args);
        }

        internal void BindByBinder(Key key, Type typeOfValue, Delegate creator, bool isExampleBind, bool isSingleton)
        {
            if (isExampleBind && key.Example == 0)
            {
                throw new ArgumentException("Zero example is reserved for default bind and not available for binding new examples manually.");
            }

            _dict[key] = new Value_CoolDI(typeOfValue, creator, isSingleton);
        }

        private Binder<TKey> GetBinder<TKey>(int example, bool isExampleBind, bool isSingleton)
        {
            Key key = new Key(typeof(TKey), example);

            return new Binder<TKey>(key, this, isExampleBind, isSingleton);
        }

        private object GetObject(Type keyType, int example, object[] args)
        {
            Key key = new Key(keyType, example);

            if (!_dict.TryGetValue(key, out Value_CoolDI value))
            {
                if (InvokeUnbindedTypes)
                {
                    return GetObjectByReflection(keyType);
                }
                else
                {
                    throw new UnbindedTypeException
                        ($"You didn't do the {(example == 0 ? "default" : $"{example} specific example")} bind of {keyType.FullName} and InvokeUnbindedTypes property is set to \"false\".");
                }
            }

            if (value.IsSingleton)
            {
                if (value.Singleton == null)
                {
                    value.Singleton = GetObject(key, value, args);
                }
                return value.Singleton;
            }

            return GetObject(key, value, args);
        }

        private object GetObject(Key key, Value_CoolDI value, object[] args)
        {
            if(value.Creator != null)
            {
                return GetObjectByDelegate(key, value, args);
            }
            else
            {
                return GetObjectByReflection(value.Type);
            }
        }

        private object GetObjectByDelegate(Key key, Value_CoolDI value, object[] args)
        {
            try
            {
                if (args == null)
                {
                    return value.Creator.DynamicInvoke();
                }
                else
                {
                    object[] wrapper = new object[1] { args };
                    return value.Creator.DynamicInvoke(wrapper);
                }
            }
            catch (TargetParameterCountException innerExc)
            {
                throw CreateWrongParametersException(key, value, args, innerExc);
            }
        }

        private object GetObjectByReflection(Type type)
        {
            if (type.IsAbstract)
            {
                throw new LateBindingException($"{type.FullName} is abstract and can't be late binded.");
            }
            if (type.IsInterface)
            {
                throw new LateBindingException($"{type.FullName} is abstract and can't be late binded.");
            }
            if (type.IsEnum)
            {
                return type.GetEnumValues().GetValue(0);
            }
            
            ConstructorInfo ctor = GetConstructor(type);
            ParameterInfo[] parameterInfos = ctor.GetParameters();

            object[] parameters = new object[parameterInfos.Length];

            for (int i = 0; i < parameterInfos.Length; i++)
            {
                Type typeOfParameter = parameterInfos[i].ParameterType;
                parameters[i] = GetObject(typeOfParameter, 0, null);
            }

            return ctor.Invoke(parameters);
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            List<ConstructorInfo> ctors = type.GetConstructors().Where(info => info.IsPublic).ToList();
            ctors.Sort(CompareCtorInfo);
            ConstructorInfo result = null;

            switch (BindedLateBindingOption)
            {
                case LateBindingOptions.DefaultCtor:
                    result = ctors[0];
                    if (result.GetParameters().Length > 0)
                        throw new LateBindingException
                            ($"The LateBindingOption property is set to DefaultCtor, but there isn't default constructor of {type.FullName}.");
                    break;
                case LateBindingOptions.DefaultOrMinCtor:
                    result = ctors.First();
                    break;
                case LateBindingOptions.DefaultOrMaxCtor:
                    if (ctors[0].GetParameters().Length == 0)
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

        private WrongParametersException CreateWrongParametersException(Key key, Value_CoolDI value, object[] args, Exception innerExc)
        {
            StringBuilder message = new StringBuilder("Fault to invoke ");
            message.Append(key.Example == 0 ? "default " : $"{key.Example} specific example ");
            message.Append($"delegate binded to {key.KeyType.FullName} due to wrong arguments: there was {(args == null ? 0 : args.Length)} arguments:\n");
            if (args != null && args.Length > 0)
            {
                foreach (var arg in args ?? new object[0])
                {
                    message.Append(arg.GetType().FullName);
                    message.Append(Environment.NewLine);
                }
            }
            message.Append("but binded delegate has next arguments:\n");
            foreach (var arg in value.Creator.GetMethodInfo().GetParameters())
            {
                message.Append(arg.ParameterType.FullName);
                message.Append(Environment.NewLine);
            }

            throw new WrongParametersException(message.ToString(), innerExc);
        }
    }
}
