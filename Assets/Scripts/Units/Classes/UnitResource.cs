using UnityEditorInternal;

public class UnitResource
{
    private int _value;
    private int _maxValue;
    private int _minValue;

    public int Value
    {
        get { return _value; } 
        set { SetStat(value); }
    }
    

    public readonly UnitStatType type;

    public UnitResource(int Value, UnitStatType type)
    {
        this._value = Value;
        this.type = type;
        this._maxValue = 999;
        this._minValue = 0;
    }

    public void UpdateLimits(int max, int min = 0)
    {
        _maxValue = max;
        _minValue = min;
    }

    private void SetStat(int value)
    {
        if (value > _maxValue)
        {
            _value = _maxValue;
        }
        else if (value < _minValue)
        {
            _value = _minValue;
        }
        else
        {
            _value = value;
        }
    }
}