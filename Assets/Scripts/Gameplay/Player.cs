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
    [SerializeField] private float jumpTransition = 0f;

    [SerializeField] private LayerMask obstacleMask = default;
    [SerializeField] private LayerMask scoreMask = default;
    [SerializeField] private LayerMask floorMask = default;

    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;

    #endregion

    #region PRIVATE_METHODS

    private int points = 50;
    private float timer = 0f;
    private int score = 0;
    private bool fall = false;
    private bool dead = false;
    private bool moveUp = false;

    private Rigidbody rigid = null;

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
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (dead || fall)
            return;

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
            Score += points;
            PropScore propScore = other.gameObject.GetComponent<PropScore>();
            propScore.ReturnScore();
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
        timer += Time.deltaTime;
        float interpole = timer / jumpTransition;

        Vector3 newPos = transform.position;
        newPos.y = moveUp 
            ? Mathf.Lerp(start.position.y, end.position.y, interpole) 
            : Mathf.Lerp(end.position.y, start.position.y, interpole);
        transform.position = newPos;

        if (interpole >= 1f)
        {
            if (moveUp)
            {
                if (!Physics.Raycast(transform.position, Vector3.down, 2f, floorMask))
                {
                    fall = true;
                    rigid.useGravity = true;
                    rigid.isKinematic = false;
                    Invoke(nameof(Death), 2f);
                }
            }

            moveUp = !moveUp;
            timer = 0f;
        }
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