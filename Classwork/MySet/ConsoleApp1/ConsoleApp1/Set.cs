using MySet.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MySet
{
    /// <summary>
    /// Set of unique elements implementing IEquatable<T>. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Set<T>
        where T : IEquatable<T>
    {
        private readonly HashSet<T> _set = new HashSet<T>();

        /// <summary>
        /// Standart constructor for the Set.
        /// </summary>
        public Set()
        {
        }

        /// <summary>
        /// Constroctor of the Set 
        /// </summary>
        /// <param name="startEnumeration"></param>
        public Set(IEnumerable<T> startEnumeration)
        {
            foreach (var item in startEnumeration)
            {
                Add(item);
            }
        }

        public int Count { get => _set.Count; }

        public void Add(T item)
        {
            if (_set.FirstOrDefault(t => t.Equals(item)) != null)
            {
                throw new AdditionException("Set already contains this item.");
            }
            _set.Add(item);
        }

        public void Add(T item, SetEventArgs e)
        {
            if (_set.FirstOrDefault(t => t.Equals(item)) != null)
            {
                throw new AdditionException("Set already contains adding item.");
            }
            _set.Add(item);
            ItemAdded(this, e);
        }

        public void Remove(T item)
        {
            if (_set.FirstOrDefault(t => t.Equals(item)) == null)
            {
                throw new RemovalException("Set doesn't contain removing item.");
            }
            _set.Remove(item);
        }

        public void Remove(T item, SetEventArgs e)
        {
            if (_set.FirstOrDefault(t => t.Equals(item)) == null)
            {
                throw new RemovalException("Set doesn't contain removing item.");
            }
            _set.Remove(item);
            ItemRemoved(this, e);
        }

        public T Find(T item)
        {
            return _set.FirstOrDefault(t => t.Equals(item));
        }

        public T Find(Func<T, bool> predicate)
        {
            return _set.FirstOrDefault(predicate);
        }

        public event EventHandler<SetEventArgs> ItemAdded;

        public event EventHandler<SetEventArgs> ItemRemoved;
    }
}
