using SamopalIndustries.Entities.KeysAndValues;
using System;

namespace SamopalIndustries.Entities
{
    public struct Binder<TKey>
    {
        internal Key BindedKey { get; }

        internal CoolDI DI { get; }

        internal bool IsExampleBind { get; }

        internal bool IsSingleton { get; }

        internal Binder(Key bindedKey, CoolDI di, bool isExampleBind, bool isSingleton)
        {
            BindedKey = bindedKey;
            DI = di;
            IsExampleBind = isExampleBind;
            IsSingleton = isSingleton;
        }

        /// <summary>
        /// Bind binding type to self, instances will be made by reflection.
        /// </summary>
        public void ToSelf()
        {
            DI.BindByBinder(BindedKey, BindedKey.KeyType, null, IsExampleBind, IsSingleton);
        }

        /// <summary>
        /// Bind binding type to TValue, instances will be made by reflection.
        /// </summary>
        /// <typeparam name="TValue">Type to be binded with TKey.</typeparam>
        public void To<TValue>()
            where TValue : TKey
        {
            DI.BindByBinder(BindedKey, typeof(TValue), null, IsExampleBind, IsSingleton);
        }

        /// <summary>
        /// Bind binding type to Func delegate without arguments, returning instances if TValue type.
        /// </summary>
        /// <typeparam name="TValue">Type to be binded with TKey.</typeparam>
        /// <param name="creatorWOArgs">Func delegate for creating the TValue instances without arguments.</param>
        public void ToDelegateWOArgs<TValue>(Func<TValue> creatorWOArgs)
            where TValue : TKey
        {
            DI.BindByBinder(BindedKey, typeof(TValue), creatorWOArgs, IsExampleBind, IsSingleton);
        }

        /// <summary>
        /// Bind binding type to Func delegate with arguments, returning instances if TValue type.
        /// </summary>
        /// <typeparam name="TValue">Type to be binded with TKey.</typeparam>
        /// <param name="creatorWithArgs">Func delegate for creating the TValue instances with arguments.</param>
        public void ToDelegateWithArgs<TValue>(Func<object[], TValue> creatorWithArgs)
            where TValue : TKey
        {
            DI.BindByBinder(BindedKey, typeof(TValue), creatorWithArgs, IsExampleBind, IsSingleton);
        }
    }
}
