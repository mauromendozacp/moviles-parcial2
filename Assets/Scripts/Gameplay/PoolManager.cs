using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private GameObject prefab = null;
    [SerializeField] private int length = 0;

    #endregion

    #region PRIVATE_FIELDS

    private Queue<GameObject> pool = null;

    #endregion

    #region UNITY_CALLS

    void Awake()
    {
        pool = new Queue<GameObject>();

        for (int i = 0; i < length; i++)
        {
            GameObject objectGO = Instantiate(prefab, transform, true);
            ReturnObjectToPool(objectGO);
        }
    }

    #endregion

    #region PUBLIC_METHODS

    public GameObject GetObjectFromPool()
    {
        GameObject objectGO = pool.Dequeue();
        objectGO.SetActive(true);

        return objectGO;
    }

    public void ReturnObjectToPool(GameObject objectGO)
    {
        objectGO.SetActive(false);
        pool.Enqueue(objectGO);
    }

    #endregion
}
