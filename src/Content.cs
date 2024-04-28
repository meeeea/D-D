abstract class Content {
    protected ushort _id;
    public ushort ID => _id;
    public Content() {}
    public Content(bool Manual) {}
    public abstract string Name {get;}
    public abstract void Display();
    public abstract void Save(StreamWriter writer);

    public static bool operator >= (Content l, Content f) {
        return l.ID >= f.ID;
    }

    public static bool operator <= (Content l, Content f) {
        return !(f >= l);
    }
}