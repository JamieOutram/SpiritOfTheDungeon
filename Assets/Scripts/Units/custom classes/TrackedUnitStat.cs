public class TrackedUnitStat
{
    public int value;
    
    public readonly UnitStatType type;

    public TrackedUnitStat(int Value, UnitStatType type)
    {
        this.value = Value;
        this.type = type;
    }
}