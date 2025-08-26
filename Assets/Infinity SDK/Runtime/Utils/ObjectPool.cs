using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infinity.Runtime.Utils
{
    public class ObjectPool<T>  where T : class
    {
        private readonly Queue<T> _pool = new Queue<T>();
        private readonly Func<T> _factory;
        private readonly bool _expandable = false;
        
        public ObjectPool(Func<T> factory, int initialSize = 20, bool expandable = false)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _expandable = expandable;
            
            for (int i = 0; i < initialSize; ++i)
                _pool.Enqueue(_factory());
        }
        
        public T Get()
        {
            return _pool.Count > 0 ? 
                _pool.Dequeue() : 
                _expandable ? _factory() : null;
        }

        public void Return(T obj)
        {
            _pool.Enqueue(obj);
        }
    }
}