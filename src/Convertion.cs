using System.Text;

class Convertion {
    public static string BytesToString(byte[] bytes) {
        string returnable = "";
        foreach (byte letter in bytes) {
            returnable += (char) letter;
        }
        return returnable;
    }
}