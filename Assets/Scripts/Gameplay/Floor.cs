using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour, SpawnFactory
{
    #region EXPOSED_FIELDS

    [SerializeField] private Spawn[] spawns = null;

    #endregion

    #region PROPERTIES

    public List<GameObject> ObstacleList { get; set; } = null;
    public GameObject ScoreGO { get; set; } = null;

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
            index = UnityEngine.Random.Range(0, spawns.Length);
        } while (spawns[index].inUse);

        spawns[index].inUse = true;
        return spawns[index].transform;
    }

    #endregion
}
