using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Point,
    Directional,
    Area,
    Pure,
}

[RequireComponent(typeof(Unit_Statistics))]
[RequireComponent(typeof(Animator))]
public class Unit_Actions : MonoBehaviour
{
    private Unit_Statistics unit_Stats;
    private Unit_Abilities unit_Abilities;
    private Unit_Items unit_Items;
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        unit_Stats = GetComponent<Unit_Statistics>();
        unit_Abilities = GetComponent<Unit_Abilities>();
        unit_Items = GetComponent<Unit_Items>();
        anim = GetComponent<Animator>();
    }

    public void Attack(Ability ability)
    {
        if (ability == null)
            return;

        if (unit_Abilities.GetCooldownLeftSeconds(ability.aName) == 0f)
        {
            anim.SetBool("Attack", true);
            if (ability.GetType() == typeof(UnitTargetAbility))
            {
                //might be causing memory leak, see OverlapCircleNonAlloc
                List<Collider2D> colliders = Physics2D.OverlapCircleAll((Vector2)this.transform.position, ability.aRange).ToList();

                //filter valid colliders only, reverse itteration to avoid indexing errors
                for (int i = colliders.Count - 1; i > -1; i--)
                {
                    if (!InteractionManager.IsHealed(this.gameObject, colliders[i].gameObject))
                    {
                        colliders.RemoveAt(i);
                    }
                }

                if (colliders.Count > 0)
                {

                    Collider2D closestCollider = colliders[0];

                    float magnitude = (gameObject.transform.position - closestCollider.transform.position).magnitude;
                    float lowestMagnitude = magnitude;
                    if (ReferenceEquals(this.gameObject, closestCollider.gameObject))
                    {
                        lowestMagnitude = 999;
                    }

                    foreach (var collider in colliders)
                    {
                        magnitude = (gameObject.transform.position - collider.transform.position).magnitude;
                        if (!ReferenceEquals(this.gameObject, collider.gameObject))
                        {
                            if ((magnitude < lowestMagnitude))
                            {
                                closestCollider = collider;
                                lowestMagnitude = magnitude;
                            }
                        }
                    }
                    //Debug.Log(string.Format("Using {0}", ability.aName));
                    unit_Abilities.TryUseAbility(ability.aName, closestCollider.gameObject);
                }
                else
                {
                    Debug.Log("No target in range");
                }
            }
            else
            {
                unit_Abilities.TryUseAbility(ability.aName);
            }
        }
    }

    public void Damage(int damage, Transform source = null, bool isBlockable = true)
    {
        UnitResource currentHealth = unit_Stats.GetResource(UnitStatType.Health);
        //Check or apply target effects here. Basic modifiers from source object should be applied through damage.
        //Directional Damage
        if (!ReferenceEquals(source, null))
        {
            if (isBlockable)
            {
                EquipableItem item = unit_Items.GetItemOfType(EquipableItemType.Shield);
                if (!ReferenceEquals(item, null))
                {
                    damage = item.DamageModTrigger(damage, source);
                }
            }
        }



        currentHealth.Value -= damage;
        anim.SetBool("Hit", true);
        Debug.Log(string.Format("{0} damaged for {1}, current health {2}", gameObject.name, damage, currentHealth.Value));

        if (currentHealth.Value <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
