using Leopotam.Ecs;
using UnityEngine;

sealed class MoveUnitsSystem : IEcsRunSystem
{
    EcsFilter<UnitStack, Turn, MoveState> currentUnit;

    public void Run()
    {
        foreach (var unitIndex in currentUnit)
        {
            ref var unit = ref currentUnit.Get1(unitIndex);

            if (Input.GetMouseButtonDown(0))
            {
                var tilePos = TileManager.singleton.GetMouseTile();

                bool ableToMove = SceneData.singleton.moveMap.HasTile(tilePos);
                if (ableToMove)
                {
                    TileManager.singleton.MoveAtTileCenter(ref unit, tilePos);
                    GlobalEvents.onTurnEnd?.Invoke();
                }
            }
        }
    }
}
