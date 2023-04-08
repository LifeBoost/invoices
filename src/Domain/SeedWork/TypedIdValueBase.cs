namespace Domain.SeedWork;

public class TypedIdValueBase : IEquatable<TypedIdValueBase>
{
    public Guid Value { get; }

    protected TypedIdValueBase(Guid value)
    {
        Value = value;
    }

    public bool Equals(TypedIdValueBase? other)
    {
        if (ReferenceEquals(null, other)) return false;
        return this.Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is TypedIdValueBase other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(TypedIdValueBase obj1, TypedIdValueBase obj2)
    {
        return obj1?.Equals(obj2) ?? object.Equals(obj2, null);
    }

    public static bool operator !=(TypedIdValueBase obj1, TypedIdValueBase obj2)
    {
        return !(obj1 == obj2);
    }
}