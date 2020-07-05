public class UnitTrackedStat
{
    public int value;
    
    public readonly UnitStatType type;

    public UnitTrackedStat(int Value, UnitStatType type)
    {
        this.value = Value;
        this.type = type;
    }
}