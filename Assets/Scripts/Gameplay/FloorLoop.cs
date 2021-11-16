using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIFFICULTY
{
    EASY,
    MEDIUM,
    HARD
}

[System.Serializable]
public struct Level
{
    public DIFFICULTY difficulty;
    public int emptySpaces;
    public int obstacleCount;
}

public class FloorLoop : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private int levelIndex = 0;
    [SerializeField] private Level[] levels = null;

    [SerializeField] private Floor[] floors = null;
    [SerializeField] private float speed = 0f;

    [SerializeField] private PoolManager obstacleManager = null;

    [SerializeField] private Transform startPos = null;
    [SerializeField] private Transform endPos = null;

    #endregion

    #region PRIVATE_FIELDS

    private int floorSpaces = 0;

    #endregion

    #region PROPERTIES

    public int LevelIndex
    {
        get => levelIndex;
        set => levelIndex = value;
    }

    #endregion

    #region UNITY_CALLS

    void Start()
    {
        
    }

    void Update()
    {
        MoveFloors();
    }

    #endregion

    #region PRIVATE_METHODS

    private void MoveFloors()
    {
        foreach (Floor floor in floors)
        {
            Vector3 newPos = floor.transform.position;
            newPos += (endPos.position - startPos.position).normalized * (speed * Time.deltaTime);

            if (newPos.z < endPos.position.z)
                ResetFloor(floor);
            else
                floor.transform.position = newPos;
        }
    }

    private void ResetFloor(Floor floor)
    {
        Vector3 auxPos = startPos.position;
        auxPos.y -= endPos.position.y - floor.transform.position.y;
        auxPos.z += endPos.position.z - floor.transform.position.z;
        floor.transform.position = auxPos;

        floor.SpawnRestart();
        for (int i = 0; i < floor.obstacleList.Count; i++)
        {
            floor.obstacleList[i].transform.parent = obstacleManager.transform;
            obstacleManager.ReturnObjectToPool(floor.obstacleList[i]);
        }

        floorSpaces++;
        if (floorSpaces >= levels[levelIndex].emptySpaces)
        {
            for (int i = 0; i < levels[levelIndex].obstacleCount; i++)
            {
                Transform spawnGO = floor.GetSpawnRandom();
                GameObject obstacleGO = obstacleManager.GetObjectFromPool();
                obstacleGO.transform.position = spawnGO.position;
                obstacleGO.transform.parent = spawnGO;
                floor.obstacleList.Add(obstacleGO);
            }

            floorSpaces = 0;
        }
    }

    #endregion
}
