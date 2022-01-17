using Leopotam.Ecs;


sealed class StateManager : IEcsInitSystem, IEcsDestroySystem
{
    EcsFilter<Turn> currentUnit;
    public static StateManager singleton;
    public void Init()
    {
        singleton = this;
        GlobalEvents.onChangeToAbilityState += AddAbilityState;
    }
    public void Destroy()
    {
        GlobalEvents.onChangeToAbilityState -= AddAbilityState;
    }

    public void ClearStates()
    {
        var unitEntity = currentUnit.GetEntity(0);

        if (unitEntity.Has<AttackState>())
        {
            unitEntity.Del<AttackState>();
        }

        if (unitEntity.Has<MoveState>())
        {
            unitEntity.Del<MoveState>();
        }

        if (unitEntity.Has<AbilityState>())
        {
            unitEntity.Del<AbilityState>();
        }
    }

    void AddAbilityState()
    {
        ClearStates();
        var unitEntity = currentUnit.GetEntity(0);
        unitEntity.Get<AbilityState>();
    }
}
