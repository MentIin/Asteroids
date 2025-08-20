using UnityEngine;

namespace CodeBase.Data
{
    /// <summary>
    /// Class representing a gauge that can be filled or emptied, like health, energy, etc.
    /// </summary>
    public class Gauge
    {
        public bool IsFull => Value >= MaxValue;
        public bool IsEmpty => Value <= 0;
        public float FillPercentage => (float)Value / MaxValue;
        public bool IsZero => Value == 0;
        public bool IsMax => Value == MaxValue;
        public bool IsNotFull => !IsFull;
        public bool IsNotEmpty => !IsEmpty;
        
        private int _value;
        private int _maxValue;
        public int Value
        {
            get => _value;
            private set
            {
                _value = Mathf.Clamp(value, 0, MaxValue);
            }
        }
        public int MaxValue
        {
            get => _maxValue;
            private set
            {
                _maxValue = Mathf.Max(value, 1); // Ensure MaxValue is at least 1 to avoid division by zero
                Value = Mathf.Clamp(Value, 0, _maxValue);
            }
        }

        public Gauge(int initialValue)
        {
            Value = initialValue;
            MaxValue = initialValue;
        }
        
        public static Gauge operator --(Gauge gauge)
        {
            gauge.Decrease(1);
            return gauge;
        }
        
        public static Gauge operator ++(Gauge gauge)
        {
            gauge.Increase(1);
            return gauge;
        }

        public void Increase(int amount)
        {
            Value = Mathf.Clamp(Value + amount, 0, MaxValue);
        }

        public void Decrease(int amount)
        {
            Value = Mathf.Clamp(Value - amount, 0, MaxValue);
        }

        public void Reset()
        {
            Value = 0;
        }
    }
}