using System;
using System.Collections.Generic;
using UnityEngine;

sealed class GlobalEvents
{
    public static Action onChangeToAttackState;
    public static Action onChangeToMoveState;
    public static Action onChangeToAbilityState;
    public static Action onTurnEnd;
    public static Action updateUI;
}
