using System;
using UnityEngine;

public class PActions
{
    public Action<int> OnScore = null;
}

public class Player : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private float horSpeed = 0f;

    [SerializeField] private LayerMask obstacleMask = default;
    [SerializeField] private LayerMask scoreMask = default;
    [SerializeField] private LayerMask floorMask = default;

    [SerializeField] private Transform start = null;
    [SerializeField] private Transform end = null;

    #endregion

    #region PRIVATE_METHODS

    private int score = 0;
    private bool fall = false;
    private bool dead = false;
    private float checkFloorDistance = 8f;
    private float maxFloorDistance = 1.18f;
    private float screenHalf = 0f;
    private float minLimitX = -4.8f;
    private float maxLimitX = 4.8f;

    private Rigidbody rigid = null;

    private GMActions gmActions = null;
    private PActions pActions = null;

    #endregion

    #region PROPERTIES

    public bool Started { get; set; } = false;

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
        screenHalf = Screen.width / 2;
    }

    void Update()
    {
        if (Started)
        {
            if (dead || fall)
                return;

            Move();
            Jump();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckLayerInMask(obstacleMask, other.gameObject.layer))
        {
            Death();
        }
        else if (CheckLayerInMask(scoreMask, other.gameObject.layer))
        {
            PropScore propScore = other.gameObject.GetComponent<PropScore>();
            Score += propScore.Points;
            propScore.ReturnScore();
        }
    }

    #endregion

    #region PUBLIC_METHODS

    public void Init(GMActions gmActions, Skin skin)
    {
        pActions = new PActions();
        this.gmActions = gmActions;

        Instantiate(skin.BallPrefab, transform);
    }

    #endregion

    #region PRIVATE_METHODS

    private void Move()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.GetTouch(0);
            Vector3 dir;

            if (myTouch.position.x > screenHalf)
            {
                dir = Vector3.right;
            }
            else
            {
                dir = Vector3.left;
            }

            transform.Translate(dir * (horSpeed * Time.deltaTime));
        }
#endif

#if UNITY_EDITOR
        float hor = Input.GetAxis("Horizontal");

        if (Mathf.Abs(hor) > Mathf.Epsilon)
        {
            transform.Translate(Vector3.right * (hor * horSpeed * Time.deltaTime));
        }
#endif
    }


    private void Jump()
    {
        if (!fall)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, checkFloorDistance, floorMask))
            {
                Transform floorTransform = hit.transform;

                float distanceZ =  Mathf.Abs(transform.position.z - floorTransform.position.z);
                Vector3 newPos = transform.position;
                newPos.y = Mathf.Lerp(end.position.y, start.position.y, distanceZ / maxFloorDistance);
                transform.position = newPos;
            }
            else
            {
                if (transform.position.x < minLimitX || transform.position.x > maxLimitX)
                {
                    fall = true;
                    rigid.useGravity = true;
                    rigid.isKinematic = false;
                    rigid.AddForce(Vector3.down * 5f, ForceMode.Impulse);
                    Shake.Get().MakeShake();
                    Invoke(nameof(Death), 2f);
                }
            }
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