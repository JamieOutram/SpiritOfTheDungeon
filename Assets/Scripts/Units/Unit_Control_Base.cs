﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileTriggerable))]
[RequireComponent(typeof(UnitTargetTriggerable))]
[RequireComponent(typeof(AreaOfEffectTriggerable))]
public class Unit_Control_Base : MonoBehaviour
{
    public int verticalMove = 0;
    public int horizontalMove = 0;
    public int turnMove = 0;

    //TODO: add tick system for AI inputs rather than update inputs every frame
}
