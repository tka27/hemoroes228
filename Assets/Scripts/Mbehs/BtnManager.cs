using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    public void ToMoveState()
    {
        GlobalEvents.onChangeToMoveState?.Invoke();
    }

    public void ToAttackState()
    {
        GlobalEvents.onChangeToAttackState?.Invoke();
    }

    public void ToAbilityState()
    {
        GlobalEvents.onChangeToAbilityState?.Invoke();
    }

    public void SkipTurn()
    {
        GlobalEvents.onTurnEnd?.Invoke();
    }
}
