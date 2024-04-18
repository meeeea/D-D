class Book {
    private int id;
    public int ID => id;
    private MonsterManager mManager = new();
    public MonsterManager MManager => mManager;
    private SpellManager sManager = new();
    public SpellManager SManager => sManager;
    private ClassManager cManager = new();
    public ClassManager CManager => cManager;
    private FeatManager fManager = new();
    public FeatManager FManager => fManager;
    private ItemManager iManager = new();
    public ItemManager IManager => iManager;
}