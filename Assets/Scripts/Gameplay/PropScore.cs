using UnityEngine;

public class PropScore : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [SerializeField] private int points = 0;

    #endregion

    #region PROPERTIES

    public int Points => points;
    public Floor Floor { get; set; } = null;
    public FloorLoop FloorLoop { get; set; } = null;

    #endregion

    #region PUBLIC_METHODS

    public void ReturnScore()
    {
        FloorLoop.ReturnScore(Floor);
    }

    #endregion
}
