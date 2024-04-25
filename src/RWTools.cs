class RWTools {
    public static bool IsTrue(char letter) {
        if (char.ToLower(letter) == 't') {
            return true;
        }
        return false;
    }
    public static char BoolToChar (bool argument) {
        if (argument) {
            return 'T';
        }
        return 'F';
    }
}