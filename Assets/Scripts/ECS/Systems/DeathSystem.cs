using Leopotam.Ecs;
using UnityEngine;

sealed class DeathSystem : IEcsRunSystem
{
    EcsFilter<UnitStack, DeathRequest> deadUnits;

    public void Run()
    {
        foreach (var unitIndex in deadUnits)
        {
            ref var unit = ref deadUnits.Get1(unitIndex);
            ref var entity = ref deadUnits.GetEntity(unitIndex);

            GameObject.Destroy(unit.transform.gameObject);

            Debug.Log($"{entity} умер");

            entity.Destroy();
        }
    }
}
