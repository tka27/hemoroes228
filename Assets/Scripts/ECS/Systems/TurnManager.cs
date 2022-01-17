using Leopotam.Ecs;
using UnityEngine;

sealed class TurnManager : IEcsInitSystem, IEcsDestroySystem
{
    EcsFilter<UnitStack> allUnits;
    EcsFilter<UnitStack, Turn> currentUnit;
    public void Init()
    {
        GlobalEvents.onTurnEnd += NextTurn;
        NextTurn();
    }
    public void Destroy()
    {
        GlobalEvents.onTurnEnd -= NextTurn;
    }



    void NextTurn()
    {

        if (IsKnightHasAdditionalTurn()) return;

        ResetInitiative();

        var higherInitiative = 0;
        var unitWithHigherInitiativeIndex = 0;

        foreach (var unitIndex in allUnits)
        {
            ref var unit = ref allUnits.Get1(unitIndex);

            unit.currentInitiative += unit.defaultInitiative;

            if (higherInitiative < unit.currentInitiative)
            {
                higherInitiative = unit.currentInitiative;
                unitWithHigherInitiativeIndex = unitIndex;
            }
        }
        allUnits.GetEntity(unitWithHigherInitiativeIndex).Get<Turn>();
        GlobalEvents.updateUI?.Invoke();
    }

    void ResetInitiative()
    {
        foreach (var unitIndex in currentUnit)
        {
            currentUnit.Get1(unitIndex).currentInitiative = 0;
            currentUnit.GetEntity(unitIndex).Del<Turn>();
        }
    }

    bool IsKnightHasAdditionalTurn()
    {
        foreach (var unitIndex in currentUnit)
        {
            ref var entity = ref currentUnit.GetEntity(unitIndex);
            if (entity.Has<Knight>())
            {
                int roll = Random.Range(0, 101);
                Debug.Log(roll);
                if (roll < 46)
                {
                    GlobalEvents.updateUI?.Invoke();
                    return true;
                }
            }
        }
        return false;
    }
}
