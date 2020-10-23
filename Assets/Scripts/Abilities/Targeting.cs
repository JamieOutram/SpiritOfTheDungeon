using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Targeting
{
    public static GameObject GetClosestUnitTarget(Transform caster, Ability ability)
    {
        
        if (ability.GetType() == typeof(UnitTargetAbility))
        {
            //might be causing memory leak, see OverlapCircleNonAlloc
            List<Collider2D> colliders = Physics2D.OverlapCircleAll((Vector2)caster.position, ((UnitTargetAbility)ability).baseRange).ToList();

            //filter valid colliders only, reverse itteration to avoid indexing errors
            for (int i = colliders.Count - 1; i > -1; i--)
            {
                if (!InteractionManager.IsHealed(caster.gameObject, colliders[i].gameObject))
                {
                    colliders.RemoveAt(i);
                }
            }

            if (colliders.Count > 0)
            {

                Collider2D closestCollider = colliders[0];

                float magnitude = (caster.position - closestCollider.transform.position).magnitude;
                float lowestMagnitude = magnitude;
                if (ReferenceEquals(caster.gameObject, closestCollider.gameObject))
                {
                    lowestMagnitude = 999;
                }

                foreach (var collider in colliders)
                {
                    magnitude = (caster.position - collider.transform.position).magnitude;
                    if (!ReferenceEquals(caster.gameObject, collider.gameObject))
                    {
                        if ((magnitude < lowestMagnitude))
                        {
                            closestCollider = collider;
                            lowestMagnitude = magnitude;
                        }
                    }
                }
                //Debug.Log(string.Format("Using {0}", ability.aName));
                return closestCollider.gameObject;
            }
            else
            {
                Debug.Log("No target in range");
                return null;
            }
        }
        else
        {
            Debug.Log("GetClosestUnitTarget called by Ability which is not unit target");
            return null;
        }
    }
}
