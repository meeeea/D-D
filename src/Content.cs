abstract class Content {
    public Content() {}
    public Content(bool Manual) {}
    public abstract string Name {get;}
    public abstract void Display();
    public abstract void Save(StreamWriter writer);
}