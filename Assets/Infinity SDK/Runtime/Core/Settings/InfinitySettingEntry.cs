using System;
using UnityEngine;

namespace Infinity.Runtime.Core.Settings
{
    public class InfinitySettingEntry<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                OnValueChanged?.Invoke(_value, value);
                _value = value;
            }
        }

        public string SettingKey { get; private set; }

        public event Action<T, T> OnValueChanged;

        public InfinitySettingEntry()
        {
        }

        public InfinitySettingEntry(string key, T value)
        {
            SettingKey = $"infinity.{Application.productName.Replace(" ", "_").ToLower()}.{key}";
            _value = value;
        }

        public override string ToString()
        {
            return $"[{SettingKey}: {Value.ToString()}]";
        }
    }
}