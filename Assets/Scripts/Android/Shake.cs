using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviourSingleton<Shake>
{
    public void MakeShake()
    {
        Handheld.Vibrate();
    }
}
