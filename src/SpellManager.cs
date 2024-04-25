class SpellManager : Manager<Spell> {
    public void ListedDisplay() {
        for (int i = 0; i < Count; i++) {
            Console.WriteLine($"{i + 1}). {this[i].Name}");
        }
    }


}