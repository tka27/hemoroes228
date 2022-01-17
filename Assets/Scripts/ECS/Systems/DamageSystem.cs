using Leopotam.Ecs;
using UnityEngine;

sealed class DamageSystem : IEcsRunSystem
{
    EcsFilter<UnitStack, DamageRequest> damagedUnits;

    public void Run()
    {
        TakeDamage();
    }


    void TakeDamage()
    {
        foreach (var unitIndex in damagedUnits)
        {
            ref var unit = ref damagedUnits.Get1(unitIndex);
            ref var request = ref damagedUnits.Get2(unitIndex);
            ref var entity = ref damagedUnits.GetEntity(unitIndex);

            if (entity.Has<Knight>())
            {
                ref var knight = ref entity.Get<Knight>();
                request.value = knight.GetReducedDamage(request.value);
            }


            unit.stackHealth -= request.value;

            unit.unitsCount -= request.value / unit.unitHealth;


            Debug.Log($"{request.dealer} нанес {request.value} урона {entity}. Осталось {unit.stackHealth} здоровья");

            if (unit.stackHealth <= 0)
            {
                entity.Get<DeathRequest>();
            }

            entity.Del<DamageRequest>();


            GlobalEvents.updateUI?.Invoke();
        }
    }


}
