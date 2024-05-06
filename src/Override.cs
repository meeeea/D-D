class Override<T> : Content where T : Content, new() {
    public override string Name => throw new NotImplementedException();

    private int _overridenBookID;
    private int? _overridenManagerID => ChildrenInt(new T());
    private int _overridenItemID;

    public T? GetOverridedItem {
        get {
            Book? book = Program.bookSet.SelectBookByID((ushort)_overridenBookID);
            if (book != null) {
                Manager<T>? contents = (Manager<T>?) book[_overridenBookID];
                if (contents != null) {
                    return (T?) contents[_overridenItemID];
                }
            }
            return null;
        }
    }
    public override void Display() {
        throw new NotImplementedException();
    }

    public override void Save(StreamWriter writer) {
        throw new NotImplementedException();
    }
}