using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoundSpawner : MonoBehaviour
{
    public static Transform GROUND_ANCHOR;

    [Header("Set in Inspector")] 
    public int groundLenghtX = 20;
    public int groundLenghtZ = 20;

    public GameObject cubePrefab;
    public GameObject playerPrefab;

    void Awake()
    {
        if (GROUND_ANCHOR == null) {
            GameObject go = new GameObject("_GroundAnchor");
            GROUND_ANCHOR = go.transform;
        }

        for (int z = 1 - groundLenghtZ / 2; z < groundLenghtZ / 2; z++) {
            for (int x = 1 - groundLenghtX / 2; x < groundLenghtX / 2; x++)
            {
                GameObject go = Instantiate<GameObject>(cubePrefab);
                go.transform.SetParent(GROUND_ANCHOR);
                go.transform.position = new Vector3(x, -1, z);
            }
        }
        
        Invoke("SpawnHero", 0.5f);
    }

    void SpawnHero()
    {
        GameObject goPlayer = Instantiate(playerPrefab);
        goPlayer.transform.position = new Vector3(0, 0, 0);
    }
}
