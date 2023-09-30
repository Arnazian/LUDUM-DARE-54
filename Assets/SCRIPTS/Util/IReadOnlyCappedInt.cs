public interface IReadOnlyCappedInt
{
    public float Normalized { get; }
    public int Value { get; }
    public int Min { get; }
    public int Max { get; }
}