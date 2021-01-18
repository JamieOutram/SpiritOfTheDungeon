public enum UnitType
{
    HeroRanger,
    HeroTank,
    HeroFighter,
    HeroHealer,
    EnemyBasic,
}

public static class UnitTypeMap
{
    private static Map<string, UnitType> map = new Map<string, UnitType>();

    static UnitTypeMap()
    {
        map.Add("Hero_Ranger", UnitType.HeroRanger);
        map.Add("Hero_Tank", UnitType.HeroTank);
        map.Add("Hero_Fighter", UnitType.HeroFighter);
        map.Add("Hero_Healer", UnitType.HeroHealer);
        map.Add("Enemy_Basic", UnitType.EnemyBasic);
    }

    public static UnitType GetUnitType(string s)
    {
        return map.Forward[s];
    }

    public static string GetString(UnitType type)
    {
        return map.Reverse[type];
    }

}