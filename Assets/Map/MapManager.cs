using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct All_Map
{
    public List<GameObject> Save_Map;
}
public class MapManager : Singleton<MapManager>
{

    public All_Map all = new All_Map();
    private void Start()
    {
        DisableAllMap();
    }

    public void EnableMap( )
    {

    }

    private void DisableAllMap()
    {
        foreach (var a in all.Save_Map)
        {
            if (a.layer == 9)
                a.gameObject.SetActive(false);
        }
    }
}
