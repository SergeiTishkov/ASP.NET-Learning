using SamopalIndustries.Entities.KeysAndValues;
using System;

namespace SamopalIndustries.Entities
{
    public class Binder<TKey>
    {
        internal Key BindedKey { get; }

        internal CoolDI DI { get; }

        internal bool IsSingleton { get; }

        internal Binder(Key bindedKey, CoolDI di, bool isSingleton)
        {
            BindedKey = bindedKey;
            DI = di;
            IsSingleton = isSingleton;
        }

        public void ToSelf()
        {
            DI.BindByBinder(BindedKey, BindedKey.KeyType, null, IsSingleton);
        }

        public void To<TValue>()
            where TValue : TKey
        {
            DI.BindByBinder(BindedKey, typeof(TValue), null, IsSingleton);
        }

        public void ToDelegateWOArgs<TValue>(Func<TValue> creatorWOArgs)
            where TValue : TKey
        {
            DI.BindByBinder(BindedKey, typeof(TValue), creatorWOArgs, IsSingleton);
        }

        public void ToDelegateWithArgs<TValue>(Func<object[], TValue> creatorWithArgs)
            where TValue : TKey
        {
            DI.BindByBinder(BindedKey, typeof(TValue), creatorWithArgs, IsSingleton);
        }
    }
}
