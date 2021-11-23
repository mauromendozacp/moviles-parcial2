using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinStore
{
    public Skin Skin { get; set; } = null;

    public Memento SaveToMemento()
    {
        return new Memento(Skin);
    }

    public void RestoreToMemento(Memento m)
    {
        Skin = m.Skin;
    }
}
