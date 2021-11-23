using System.Collections.Generic;

public class CareTaker
{
    private List<Memento> mementos = new List<Memento>();

    public void Add(Memento m)
    {
        mementos.Add(m);
    }

    public Memento GetMemento(int index)
    {
        return mementos[index];
    }
}
