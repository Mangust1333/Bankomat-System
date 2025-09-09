namespace Model.ResultTypes;

public abstract record DeleteTupleFromDataBaseResultType()
{
    public sealed record Success() : DeleteTupleFromDataBaseResultType();

    public sealed record NotFountTuple() : DeleteTupleFromDataBaseResultType();
}