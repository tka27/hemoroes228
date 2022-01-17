using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{

    public static List<Vector3Int> SelectRange(this Vector3Int center, int range)
    {
        int x = center.x;
        int y = center.y;
        int z = 0;

        List<Vector3Int> selectedTiles = new List<Vector3Int>();

        int minX = x - range, maxX = x + range;

        for (int i = minX; i <= maxX; ++i)
        {
            selectedTiles.Add(new Vector3Int(i, y, z));
            // if (i != x)
            // {

            // }
        }

        for (int yOff = 1; yOff <= range; ++yOff)
        {
            if ((y + yOff) % 2 == 1)
            {
                --maxX;
            }
            else ++minX;

            for (int i = minX; i <= maxX; ++i)
            {
                selectedTiles.Add(new Vector3Int(i, y + yOff, z));
                selectedTiles.Add(new Vector3Int(i, y - yOff, z));
            }
        }
        return selectedTiles;
    }


}
