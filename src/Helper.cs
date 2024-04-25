class Helper {
    public static bool GetBool(string message) {
        while (true) {
            Console.WriteLine(message + " (y/n)");
            string? response = Console.ReadLine()?.ToLower();
            if (response == "y") {
                return true;
            }
            else if (response == "n") {
                return false;
            }
        }
    }

    public static string GetString(string message, int maxLength = 999) {
        while (true) {
            Console.WriteLine(message);

            string? returnable = Console.ReadLine();
            if (returnable != null) {
                return returnable.Trim().Substring(0, Math.Min(maxLength, returnable.Length));
            }
        }
    }

    public static int GetInt(string message) {
        while (true) {
            Console.WriteLine(message);

            try {
                    #pragma warning disable CS8604
                return int.Parse(Console.ReadLine());
                    #pragma warning restore CS8604
            }
            catch {
                continue;
            }
        }
    }
}