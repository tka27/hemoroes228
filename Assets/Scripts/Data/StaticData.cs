using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    public static StaticData singleton { get; private set; }











    private void OnEnable()
    {
        singleton = this;
    }
}
