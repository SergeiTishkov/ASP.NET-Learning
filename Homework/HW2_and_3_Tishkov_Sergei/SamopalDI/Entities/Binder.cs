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

        public void ToSelf()
        {
            DI.BindByBinder(BindedKey, BindedKey.KeyType, null, IsExampleBind, IsSingleton);
        }

        public void To<TValue>()
            where TValue : TKey
        {
            DI.BindByBinder(BindedKey, typeof(TValue), null, IsExampleBind, IsSingleton);
        }

        public void ToDelegateWOArgs<TValue>(Func<TValue> creatorWOArgs)
            where TValue : TKey
        {
            DI.BindByBinder(BindedKey, typeof(TValue), creatorWOArgs, IsExampleBind, IsSingleton);
        }

        public void ToDelegateWithArgs<TValue>(Func<object[], TValue> creatorWithArgs)
            where TValue : TKey
        {
            DI.BindByBinder(BindedKey, typeof(TValue), creatorWithArgs, IsExampleBind, IsSingleton);
        }
    }
}
