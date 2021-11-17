using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Spawn
{
    public Transform transform;
    public bool inUse;
}

public class Floor : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private Spawn[] spawns = null;

    #endregion

    #region PROPERTIES

    public List<GameObject> ObstacleList = null;
    public GameObject ScoreGO = null;

    #endregion

    #region UNITY_CALLS

    void Start()
    {
        ObstacleList = new List<GameObject>();
    }

    #endregion

    #region PUBLIC_METHODS

    public void SpawnRestart()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            spawns[i].inUse = false;
        }
    }

    public Transform GetSpawnRandom()
    {
        int index = 0;
        do
        {
            index = Random.Range(0, spawns.Length);
        } while (spawns[index].inUse);

        spawns[index].inUse = true;
        return spawns[index].transform;
    }

    #endregion
}
