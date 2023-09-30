using UnityEngine;

public class CappedInt : IReadOnlyCappedInt
{
    public CappedInt() : this(0, 0) { }
    public CappedInt(int value, int max) : this(value, 0, max) { }
    public CappedInt(int value, int min, int max)
    {
        Min = min;
        Max = max;
        Value = value;
    }
    public float Normalized { get => (Value - Min) / Mathf.Max(1f, (float)(Max - Min)); }
    private int val;
    public int Value { get => Mathf.Clamp(val, Min, Max); set => val = Mathf.Clamp(value, Min, Max); }
    public int Min { get; set; }
    public int Max { get; set; }

    public void Maximize() => val = Max;
    public void Minimize() => val = Min;
}