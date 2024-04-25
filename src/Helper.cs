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

            string? retrunable = Console.ReadLine()?.Trim().Substring(0, maxLength);
            if (retrunable != null) {
                return retrunable;
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