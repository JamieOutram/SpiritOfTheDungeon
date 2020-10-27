using UnityEditorInternal;

public class UnitResource
{
    private int _value;
    public int minValue { get; private set; }
    public int maxValue { get; private set; }

    public int Value
    {
        get { return _value; } 
        set { SetStat(value); }
    }
    

    public readonly UnitStatType type;

    public UnitResource(UnitStatType type)
    {
        this._value = 999;
        this.type = type;
        this.maxValue = 999;
        this.minValue = 0;
    }

    public UnitResource(int Value, UnitStatType type)
    {
        this._value = Value;
        this.type = type;
        this.maxValue = 999;
        this.minValue = 0;
    }

    public void UpdateLimits(int max, int min = 0)
    {
        maxValue = max;
        if (Value > max) Value = max;
        minValue = min;
        if (Value < min) Value = min;
    }

    private void SetStat(int value)
    {
        if (value > maxValue)
        {
            _value = maxValue;
        }
        else if (value < minValue)
        {
            _value = minValue;
        }
        else
        {
            _value = value;
        }
    }
}