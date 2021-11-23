public class Memento
{
    private Skin skin;

    public Memento() { }

    public Memento(Skin skin)
    {
        this.skin = skin;
    }

    public Skin Skin => skin;
}
