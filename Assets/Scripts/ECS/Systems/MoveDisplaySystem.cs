using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

sealed class MoveDisplaySystem : IEcsInitSystem, IEcsDestroySystem
{
    EcsFilter<UnitStack, Turn> currentUnitFilter;
    public void Init()
    {
        GlobalEvents.onChangeToMoveState += SetMoveTiles;
    }

    public void Destroy()
    {
        GlobalEvents.onChangeToMoveState -= SetMoveTiles;
    }


    void SetMoveTiles()
    {
        TileManager.singleton.ClearTiles();

        foreach (var unitIndex in currentUnitFilter)
        {
            ref var unit = ref currentUnitFilter.Get1(unitIndex);
            var unitEntity = currentUnitFilter.GetEntity(unitIndex);

            StateManager.singleton.ClearStates();
            unitEntity.Get<MoveState>();

            var moveTiles = unit.tilePos.SelectRange(unit.moveRange);
            TileManager.singleton.RemoveSameTiles(ref moveTiles,TileManager.singleton.AllUnitsPositions());

            foreach (var tilePos in moveTiles)
            {
                if (!SceneData.singleton.bordersMap.HasTile(tilePos))
                {
                    SceneData.singleton.moveMap.SetTile(tilePos, SceneData.singleton.tile);
                }
            }
        }
    }


}
