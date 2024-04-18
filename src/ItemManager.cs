class ItemManager {
    private List<Item> items = new();
    public Item this[int index] => items[index];
}