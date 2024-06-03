class Override<T> : Content where T : Content, new() {
    public override string Name => throw new NotImplementedException();

    protected OverrideDirectory overrideTarget = new();

    public Override() {}
    public Override(string? input) {
        if (input == null) {
            throw new Book.Exceptions.OverrideException();
        }
            
    }

    public override void Display() {
        throw new NotImplementedException();
    }

    public override void Save(StreamWriter writer) {
        throw new NotImplementedException();
    }
}