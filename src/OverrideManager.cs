class OverrideManager {
    private Manager<SpellOverride> spell = new();
    public Manager<SpellOverride> Spell => spell;

    public OverrideManager() { }
}