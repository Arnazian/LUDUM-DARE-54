public class CappedInt
{
    public CappedInt() : this(0, 0) { }
    public CappedInt(int value, int max) : this(value, 0, max) { }
    public CappedInt(int value, int min, int max)
    {
        Value = value;
        Min = min;
        Max = max;
    }
    public int Value { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }

}