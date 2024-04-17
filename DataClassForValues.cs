namespace Tsyrkov.Tgen.Data;

public class DataClassForValues
{
    public List<ValueRecord> Values { get; set;} = [];
}

public class ValueRecord
{
    public string Field { get; set;} = string.Empty;
    public string Value { get; set;} = string.Empty;
}