using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SceneData : MonoBehaviour
{
    public static SceneData singleton { get; private set; }
    public List<GameObject> knights;
    public List<GameObject> archers;
    public List<GameObject> skeletons;
    public List<GameObject> zombies;
    public Tilemap groundMap;
    public Tilemap moveMap;
    public Tilemap meleeAttackMap;
    public Tilemap rangeAttackMap;
    public Tilemap abilityMap;
    public Tilemap bordersMap;
    public TileBase tile;
    public Transform turnIndicator;
    public GameObject abilityBtn;






    void Awake()
    {
        singleton = this;
    }

    

}
