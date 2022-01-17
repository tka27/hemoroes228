using Leopotam.Ecs;
using UnityEngine;


sealed class UnitInitSystem : IEcsInitSystem
{
    readonly EcsWorld world;
    EcsFilter<UnitStack> allUnits;
    EcsFilter<MeleeAttacker> meleeUnits;
    EcsFilter<RangeAttacker> rangeUnits;

    public void Init()
    {
        KnightsInit();
        ArchersInit();
        ZombiesInit();
        SkeletonsInit();


        SetDefaultStats();
    }

    void SetDefaultStats()
    {
        foreach (var entityIndex in allUnits)
        {
            ref var stack = ref allUnits.Get1(entityIndex);

            var tilePos = SceneData.singleton.groundMap.WorldToCell(stack.transform.position);
            tilePos.z = 0;
            stack.unitData = stack.transform.gameObject.GetComponent<UnitData>();
            TileManager.singleton.MoveAtTileCenter(ref stack, tilePos);

            stack.stackHealth = stack.unitHealth * stack.unitsCount;
        }

        foreach (var entityIndex in meleeUnits)
        {
            ref var melee = ref meleeUnits.Get1(entityIndex);
            melee.minDamage = melee.minDamageDefault;
            melee.maxDamage = melee.maxDamageDefault;
        }

        foreach (var entityIndex in rangeUnits)
        {
            ref var range = ref rangeUnits.Get1(entityIndex);
            range.minDamage = range.minDamageDefault;
            range.maxDamage = range.maxDamageDefault;
        }
    }

    void KnightsInit()
    {
        foreach (var unitGO in SceneData.singleton.knights)
        {
            var entity = world.NewEntity();

            ref var stack = ref entity.Get<UnitStack>();
            stack.transform = unitGO.transform;
            stack.defaultInitiative = 4;
            stack.moveRange = 5;
            stack.unitsCount = 20;
            stack.unitHealth = 20;
            stack.leftTeam = true;

            ref var melee = ref entity.Get<MeleeAttacker>();
            melee.minDamageDefault = 6;
            melee.maxDamageDefault = 10;

            entity.Get<Knight>().abilityStacks = 3;
        }
    }

    void ArchersInit()
    {
        foreach (var unitGO in SceneData.singleton.archers)
        {
            var entity = world.NewEntity();

            ref var stack = ref entity.Get<UnitStack>();
            stack.transform = unitGO.transform;
            stack.defaultInitiative = 6;
            stack.moveRange = 6;
            stack.unitsCount = 40;
            stack.unitHealth = 10;
            stack.leftTeam = true;

            ref var melee = ref entity.Get<MeleeAttacker>();
            melee.minDamageDefault = 1;
            melee.maxDamageDefault = 6;

            ref var range = ref entity.Get<RangeAttacker>();
            range.minDamageDefault = 6;
            range.maxDamageDefault = 15;
            range.minRange = 1;
            range.maxRange = 22;

            entity.Get<Archer>();
        }
    }


    void SkeletonsInit()
    {
        foreach (var unitGO in SceneData.singleton.skeletons)
        {
            var entity = world.NewEntity();

            ref var stack = ref entity.Get<UnitStack>();
            stack.transform = unitGO.transform;
            stack.defaultInitiative = 6;
            stack.moveRange = 3;
            stack.unitsCount = 50;
            stack.unitHealth = 6;
            stack.leftTeam = false;

            ref var melee = ref entity.Get<MeleeAttacker>();
            melee.minDamageDefault = 6;
            melee.maxDamageDefault = 6;

            entity.Get<Skeleton>();
        }
    }

    void ZombiesInit()
    {
        foreach (var unitGO in SceneData.singleton.zombies)
        {
            var entity = world.NewEntity();

            ref var stack = ref entity.Get<UnitStack>();
            stack.transform = unitGO.transform;
            stack.defaultInitiative = 4;
            stack.moveRange = 2;
            stack.unitsCount = 1;
            stack.unitHealth = 3000;
            stack.leftTeam = false;

            ref var melee = ref entity.Get<MeleeAttacker>();
            melee.minDamageDefault = 0;
            melee.maxDamageDefault = 0;

            entity.Get<Zombie>();
        }
    }

}
