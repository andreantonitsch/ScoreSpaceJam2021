public struct Tuple<T1, T2>
{
    public readonly T1 I1;
    public readonly T2 I2;
    public Tuple(T1 i1, T2 i2)
    {
        I1 = i1;
        I2 = i2;
    }
}
public static class Tuple
{
    public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
    {
        return new Tuple<T1, T2>(item1, item2);
    }
}