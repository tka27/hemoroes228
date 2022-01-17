using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

sealed class AttackDisplaySystem : IEcsInitSystem, IEcsDestroySystem
{
    EcsFilter<UnitStack, Turn> currentUnitFilter;
    EcsFilter<UnitStack, RangeAttacker, Turn> rangeUnitFilter;
    public void Init()
    {
        GlobalEvents.onChangeToAttackState += Prepare;
    }

    public void Destroy()
    {
        GlobalEvents.onChangeToAttackState -= Prepare;
    }


    void Prepare()
    {
        TileManager.singleton.ClearTiles();
        foreach (var unitIndex in currentUnitFilter)
        {
            ref var unit = ref currentUnitFilter.Get1(unitIndex);
            var unitEntity = currentUnitFilter.GetEntity(unitIndex);


            StateManager.singleton.ClearStates();
            unitEntity.Get<AttackState>();



            SetRangeTiles();


            SetMeleeTiles();

        }

    }

    void SetMeleeTiles()
    {

        foreach (var unitIndex in currentUnitFilter)
        {
            ref var unit = ref currentUnitFilter.Get1(unitIndex);

            var meleeTiles = unit.tilePos.SelectRange(1);

            foreach (var tilePos in meleeTiles)
            {
                if (!SceneData.singleton.bordersMap.HasTile(tilePos))
                {
                    SceneData.singleton.meleeAttackMap.SetTile(tilePos, SceneData.singleton.tile);
                }
            }
        }
    }

    void SetRangeTiles()
    {
        foreach (var unitIndex in rangeUnitFilter)
        {
            ref var unit = ref rangeUnitFilter.Get1(unitIndex);
            ref var range = ref rangeUnitFilter.Get2(unitIndex);

            var rangeTiles = unit.tilePos.SelectRange(range.maxRange);
            var tilesToRemove = unit.tilePos.SelectRange(range.minRange);
            TileManager.singleton.RemoveSameTiles(ref rangeTiles, tilesToRemove);


            foreach (var tilePos in rangeTiles)
            {
                if (!SceneData.singleton.bordersMap.HasTile(tilePos))
                {
                    SceneData.singleton.rangeAttackMap.SetTile(tilePos, SceneData.singleton.tile);
                }
            }
        }
    }

}
