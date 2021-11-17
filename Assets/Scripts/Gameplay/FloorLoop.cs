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
    [SerializeField] private PoolManager scoreManager = null;

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
        for (int i = 0; i < floor.ObstacleList.Count; i++)
        {
            floor.ObstacleList[i].transform.parent = obstacleManager.transform;
            obstacleManager.ReturnObjectToPool(floor.ObstacleList[i]);
        }
        floor.ObstacleList.Clear();

        if (floor.ScoreGO != null)
        {
            ReturnScore(floor);
        }

        floorSpaces++;
        if (floorSpaces >= levels[levelIndex].emptySpaces)
        {
            Transform spawnTransform = null;
            GameObject spawnGO = null;

            for (int i = 0; i < levels[levelIndex].obstacleCount; i++)
            {
                spawnTransform = floor.GetSpawnRandom();
                spawnGO = obstacleManager.GetObjectFromPool();
                spawnGO.transform.position = spawnTransform.position;
                spawnGO.transform.parent = spawnTransform;
                floor.ObstacleList.Add(spawnGO);
            }

            spawnTransform = floor.GetSpawnRandom();
            spawnGO = scoreManager.GetObjectFromPool();
            spawnGO.transform.position = spawnTransform.position;
            spawnGO.transform.parent = spawnTransform;
            floor.ScoreGO = spawnGO;
            
            PropScore propScore = spawnGO.GetComponent<PropScore>();
            propScore.Floor = floor;
            propScore.FloorLoop = this;

            floorSpaces = 0;
        }
    }

    public void ReturnScore(Floor floor)
    {
        floor.ScoreGO.transform.parent = scoreManager.transform;
        scoreManager.ReturnObjectToPool(floor.ScoreGO);
        floor.ScoreGO = null;
    }

    #endregion
}
