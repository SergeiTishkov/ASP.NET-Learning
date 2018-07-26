using SamopalIndustries.Entities;
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
    class SamopalDI_Dev
    {
        private Dictionary<Key, Value> _dict;

        private Key _lastBind;

        /// <summary>
        /// Initializes a new instance of the SamopalDI class.
        /// </summary>
        public SamopalDI_Dev() : this(LateBindingOptions.MaxCtor)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SamopalDI class by using the specified LateBindingOptions object.
        /// </summary>
        /// <param name="options">Choose the kind of constructor that will be used in late binding process.</param>
        public SamopalDI_Dev(LateBindingOptions options)
        {
            LateBindingOption = options;
            _dict = new Dictionary<Key, Value>(new KeyComparer());
        }

        public LateBindingOptions LateBindingOption { get; set; }

        public SamopalDI_Dev BindDefault<TKey>()
        {
            BindDef<TKey>();
            return this;
        }

        public void ToSelf()
        {
            _dict[_lastBind] = new Value(_lastBind.KeyType, null, null);
        }

        public void To<TValue>()
        {
            _dict[_lastBind] = new Value(typeof(TValue), null, null);
        }

        public void ToDelegateWOArgs<TValue>(Func<object> creatorWOArgs)
        {
            _dict[_lastBind] = new Value(typeof(TValue), creatorWOArgs, null);
        }

        public void ToDelegateWithArgs<TValue>(Func<object> creatorWOArgs)
        {
            _dict[_lastBind] = new Value(typeof(TValue), creatorWOArgs, null);
        }

        private void BindDef<TKey>()
        {
            Key key = new Key(typeof(TKey), 0);

            if (_dict.ContainsKey(key))
            {
                _dict[key] = null;
            }
            else
            {
                _dict.Add(key, null);
            }

            _lastBind = key;
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
            if (!(result is TKey))
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
            catch (KeyNotFoundException e)
            {
                if (example == 0)
                    throw new ArgumentException($"You didn't do the default bind of {keyType.FullName}", e);
                else
                    throw new ArgumentException($"You didn't do the {example} specific example bind of {keyType.FullName}", e);
            }

            if (value.CreatorWOArgs != null)
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
    }
}
