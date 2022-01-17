using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

sealed class TileManager : IEcsInitSystem, IEcsDestroySystem
{
    Camera camera;
    EcsFilter<UnitStack> allUnits;
    public static TileManager singleton;
    public void Init()
    {
        camera = Camera.main;
        singleton = this;
        GlobalEvents.onTurnEnd += ClearTiles;
    }
    public void Destroy()
    {
        GlobalEvents.onTurnEnd -= ClearTiles;
    }

    public Vector3Int GetMouseTile()
    {
        var worldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;

        var tilePos = SceneData.singleton.groundMap.WorldToCell(worldPos);
        return tilePos;
    }


    public List<Vector3Int> AllUnitsPositions()
    {
        var result = new List<Vector3Int>();
        foreach (var unitIndex in allUnits)
        {
            ref var unit = ref allUnits.Get1(unitIndex);
            result.Add(unit.tilePos);
        }
        return result;
    }

    public void MoveAtTileCenter(ref UnitStack unit, Vector3Int tilePos)
    {
        unit.transform.position = SceneData.singleton.groundMap.CellToWorld(tilePos);
        unit.tilePos = tilePos;
        unit.currentInitiative = 0;
    }

    public bool HasUnit(Vector3Int tilePos, out int unitIndexOnTile)
    {
        foreach (var unitIndex in allUnits)
        {
            ref var unit = ref allUnits.Get1(unitIndex);

            if (tilePos == unit.tilePos)
            {
                unitIndexOnTile = unitIndex;
                return true;
            }
        }

        unitIndexOnTile = -1;
        return false;
    }

    public void ClearTiles()
    {
        if (SceneData.singleton.abilityMap.GetUsedTilesCount() > 0)
        {
            SceneData.singleton.abilityMap.ClearAllTiles();
        }

        if (SceneData.singleton.meleeAttackMap.GetUsedTilesCount() > 0)
        {
            SceneData.singleton.meleeAttackMap.ClearAllTiles();
        }

        if (SceneData.singleton.rangeAttackMap.GetUsedTilesCount() > 0)
        {
            SceneData.singleton.rangeAttackMap.ClearAllTiles();
        }

        if (SceneData.singleton.moveMap.GetUsedTilesCount() > 0)
        {
            SceneData.singleton.moveMap.ClearAllTiles();
        }
    }

    public void RemoveSameTiles(ref List<Vector3Int> main, in List<Vector3Int> other)
    {
        for (int i = main.Count - 1; i >= 0; i--)
        {
            Vector3Int mainTile = main[i];
            for (int j = 0; j < other.Count; j++)
            {
                Vector3Int otherTile = other[j];
                if (mainTile == otherTile)
                {
                    main.Remove(otherTile);
                }
            }
        }
    }
}
