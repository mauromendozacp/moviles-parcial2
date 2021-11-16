using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PActions
{
    public Action<int> OnScore = null;
}

public class Player : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private float horSpeed = 0f;
    [SerializeField] private float verSpeed = 0f;

    [SerializeField] private LayerMask obstacleMask = default;
    [SerializeField] private LayerMask scoreMask = default;

    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;

    #endregion

    #region PRIVATE_METHODS

    private int score = 0;
    private bool dead = false;
    private bool moveUp = false;

    private GMActions gmActions = null;
    private PActions pActions = null;

    #endregion

    #region PROPERTIES

    public int Score
    {
        get => score;
        set
        {
            score = value;
            pActions.OnScore?.Invoke(score);
        }
    }

    public PActions PActions => pActions;

    #endregion

    #region UNITY_CALLS

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckLayerInMask(obstacleMask, other.gameObject.layer))
        {
            Death();
        }
        else if (CheckLayerInMask(scoreMask, other.gameObject.layer))
        {
            SetScore(50);
        }
    }

    #endregion

    #region PUBLIC_METHODS

    public void Init(GMActions gmActions)
    {
        pActions = new PActions();
        this.gmActions = gmActions;
    }

    #endregion

    #region PRIVATE_METHODS

    private void Move()
    {
        float hor = Input.GetAxis("Horizontal");

        if (Mathf.Abs(hor) > Mathf.Epsilon)
        {
            transform.Translate(Vector3.right * (hor * horSpeed * Time.deltaTime));
        }
    }

    private void Jump()
    {
        if (moveUp)
        {
            transform.Translate(Vector3.up * (verSpeed * Time.deltaTime));
            if (transform.position.y > start.position.y)
            {
                moveUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * (verSpeed * Time.deltaTime));
            if (transform.position.y < end.position.y)
            {
                moveUp = true;
            }
        }
    }

    private void SetScore(int points)
    {
        Score += points;
    }

    private void Death()
    {
        if (dead)
            return;

        if (gmActions != null)
        {
            gmActions.OnPlayerDeath?.Invoke();
            dead = true;
        }
    }

    private bool CheckLayerInMask(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    #endregion
}