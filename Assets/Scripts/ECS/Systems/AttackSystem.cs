using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.EventSystems;

sealed class AttackSystem : IEcsRunSystem
{
    EcsFilter<UnitStack, MeleeAttacker, Turn, AttackState> meleeUnit;
    EcsFilter<UnitStack, RangeAttacker, Turn, AttackState> rangeUnit;
    EcsFilter<UnitStack, RangeAttacker, Archer, Turn, AbilityState> archerAbilityFilter;
    EcsFilter<UnitStack> allUnits;

    public void Run()
    {
        TryMeleeAttack();
        TryRangeAttack();
        TryArcherAbility();
    }


    void TryMeleeAttack()
    {
        foreach (var unitIndex in meleeUnit)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            ref var unit = ref meleeUnit.Get1(unitIndex);

            var tilePos = TileManager.singleton.GetMouseTile();

            bool ableToAttack = SceneData.singleton.meleeAttackMap.HasTile(tilePos);
            if (!ableToAttack) return;

            int unitOnTileIndex = -1;
            if (TileManager.singleton.HasUnit(tilePos, out unitOnTileIndex))
            {
                ref var target = ref allUnits.Get1(unitOnTileIndex);
                bool targetIsEnemy = target.leftTeam != unit.leftTeam;
                if (targetIsEnemy)
                {
                    ref var dealer = ref meleeUnit.Get2(unitIndex);
                    int damage = 0;
                    for (int i = 0; i < unit.unitsCount; i++)
                    {
                        damage += Random.Range(dealer.minDamage, dealer.maxDamage);
                    }
                    ref var request = ref allUnits.GetEntity(unitOnTileIndex).Get<DamageRequest>();
                    request.value = damage;
                    request.dealer = meleeUnit.GetEntity(unitIndex);

                    GlobalEvents.onTurnEnd?.Invoke();
                }
            }
        }
    }

    void TryRangeAttack()
    {
        foreach (var unitIndex in rangeUnit)
        {
            if (!Input.GetMouseButtonDown(0)) return;
            ref var unit = ref rangeUnit.Get1(unitIndex);

            var tilePos = TileManager.singleton.GetMouseTile();

            bool ableToAttack = SceneData.singleton.rangeAttackMap.HasTile(tilePos);
            if (!ableToAttack) return;

            int unitOnTileIndex = -1;
            if (TileManager.singleton.HasUnit(tilePos, out unitOnTileIndex))
            {
                ref var target = ref allUnits.Get1(unitOnTileIndex);
                bool targetIsEnemy = target.leftTeam != unit.leftTeam;
                if (targetIsEnemy)
                {
                    ref var dealer = ref rangeUnit.Get2(unitIndex);
                    int damage = 0;
                    for (int i = 0; i < unit.unitsCount; i++)
                    {
                        damage += Random.Range(dealer.minDamage, dealer.maxDamage);
                    }

                    ref var request = ref allUnits.GetEntity(unitOnTileIndex).Get<DamageRequest>();
                    request.value = damage;
                    request.dealer = rangeUnit.GetEntity(unitIndex);

                    GlobalEvents.onTurnEnd?.Invoke();
                }
            }
        }
    }

    void TryArcherAbility()
    {
        foreach (var archerIndex in archerAbilityFilter)
        {
            ref var archer = ref archerAbilityFilter.Get1(archerIndex);
            if (!Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;

            foreach (var targetIndex in allUnits)
            {
                ref var target = ref allUnits.Get1(targetIndex);

                bool targetIsEnemy = target.leftTeam != archer.leftTeam;
                bool targetInAoe = SceneData.singleton.abilityMap.HasTile(target.tilePos);

                if (targetInAoe && targetIsEnemy)
                {
                    ref var dealer = ref archerAbilityFilter.Get2(targetIndex);
                    int damage = 0;
                    for (int i = 0; i < archer.unitsCount; i++)
                    {
                        damage += Random.Range(dealer.minDamage, dealer.maxDamage);
                    }
                    
                    ref var request = ref allUnits.GetEntity(targetIndex).Get<DamageRequest>();
                    request.value = damage / 2;
                    request.dealer = archerAbilityFilter.GetEntity(archerIndex);
                }
            }
            GlobalEvents.onTurnEnd?.Invoke();
        }
    }





}
