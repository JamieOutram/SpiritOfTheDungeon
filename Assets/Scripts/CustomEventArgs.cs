using System;

// Define a class to hold custom event info
public class OnDamageArgs : EventArgs
{
    public OnDamageArgs(UnitResource health, int damage)
    {
        Health = health;
        Damage = damage;
    }

    public UnitResource Health { get; set; }
    public int Damage { get; set; }
}

public class OnCastArgs : EventArgs
{
    public OnCastArgs(UnitResource mana, Ability ability)
    {
        Mana = mana;
        Ability = ability;
    }

    public UnitResource Mana { get; set; }
    public Ability Ability { get; set; }
}
