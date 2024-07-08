using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using OneOf;
using OneOf.Types;

abstract class Content {
    public class OneOfManager : OneOfBase<Manager<Ability>, Manager<Background>, Manager<Feat>, 
        Manager<Item>, Manager<Monster>, Manager<PCClass>, 
        Manager<Race>, Manager<Spell>, Manager<SubClass>> {
        protected OneOfManager(OneOf<Manager<Ability>, Manager<Background>, Manager<Feat>,
        Manager<Item>, Manager<Monster>, Manager<PCClass>, Manager<Race>, Manager<Spell>,
        Manager<SubClass>> input) : base(input) {}

        public static implicit operator OneOfManager(Manager<Ability> _) => new(_);
        public static implicit operator OneOfManager(Manager<Background> _) => new(_);
        public static implicit operator OneOfManager(Manager<Feat> _) => new(_);
        public static implicit operator OneOfManager(Manager<Item> _) => new(_);
        public static implicit operator OneOfManager(Manager<Monster> _) => new(_);
        public static implicit operator OneOfManager(Manager<PCClass> _) => new(_);
        public static implicit operator OneOfManager(Manager<Race> _) => new(_);
        public static implicit operator OneOfManager(Manager<Spell> _) => new(_);
        public static implicit operator OneOfManager(Manager<SubClass> _) => new(_);

        [Serializable]
        public class ManagerTypeException : Exception {
            public ManagerTypeException() : base() {}
        }
    }

    public class OverrideDirectory<T> where T: Content {
        private ushort _overridenBookID;
        private ushort _overridenManagerID;
        private ushort _overridenItemID;

        public OverrideDirectory(ushort bookID = 0, ushort itemID = 1) {
            _overridenBookID = bookID;
            _overridenManagerID = (ushort) Book.ContentTypeToManagerID(GetType());
            _overridenItemID = itemID;
        }
        public Content? GetContent() {
            return Program.bookSet.SelectBookByID(_overridenBookID)?.SelectContentByID(_overridenManagerID,
                                                                         _overridenItemID);
        }
    }
    public static int? ChildrenInt(Content type) {
        switch (type) {
            case Ability : return 1;
            case Background : return 2;
            case PCClass : return 3;
            case Feat : return 4;
            case Item : return 5;
            case Monster : return 6;
            case Race : return 7;
            case Spell : return 8;
            case SubClass : return 9;
        }
        return null;
    }

    protected static int IDLength = 3;
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