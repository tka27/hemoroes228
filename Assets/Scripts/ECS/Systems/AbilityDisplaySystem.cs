using Leopotam.Ecs;
using UnityEngine;

sealed class AbilityDisplaySystem : IEcsRunSystem
{
    EcsFilter<UnitStack, Turn, AbilityState, Archer> currentUnitFilter;
    Vector3Int lastSelectedTile;

    public void Run()
    {
        foreach (var unit in currentUnitFilter)
        {
            if (lastSelectedTile != TileManager.singleton.GetMouseTile() || lastSelectedTile == null) RebuildMap();
        }
    }

    void RebuildMap()
    {
        TileManager.singleton.ClearTiles();
        lastSelectedTile = TileManager.singleton.GetMouseTile();
        var aoe = lastSelectedTile.SelectRange(1);

        Vector3Int upFlowerCenter = lastSelectedTile;
        upFlowerCenter.y += 2;

        var upFlower = upFlowerCenter.SelectRange(1);

        aoe.AddRange(upFlower);

        foreach (var tile in aoe)
        {
            SceneData.singleton.abilityMap.SetTile(tile, SceneData.singleton.tile);
        }
    }


}
