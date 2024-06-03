class TestMaster {
    public static void TestAll() {
        Console.Clear();
        BookTest();


        Environment.Exit(1);
    }

    private static void BookTest() {
        try {
            Book book = new("TB1.book", ".\\src\\Tests\\TestBooks\\");
            comp(book.ID, 64000, "book-id test");
            comp(book.IDSave, "64000", "book-id Save Test");
            comp(book.Name, "TB1", "book-name test");
        }
        catch (Exception _) {
            Console.WriteLine($"BookTest failed due to {_}.");
        }
    }

    private static void Test<T>(T input, Func<T, object> func, string testName) {
        try {
            func(input);
            Console.WriteLine($"{func} test {testName} succeded");
        }
        catch (Exception _) {
            Console.WriteLine($"Exceotion {_} found in {func}.");
        }
    }

    private static bool compTest<T1, T2>(T1 input, T2 expectedOutput, Func<T1, T2> func
    , out T2? result, string testName) {
        result = default;
        try {
            result = func(input);
            bool same = EqualityComparer<T2>.Default.Equals(result, expectedOutput);
            if (same) {
                Console.WriteLine($"{func} test {testName} works as intended");
            }
            return true;
        }
        catch (Exception _) {
            Console.WriteLine($"Exception {_} found in {func}");
            return false;
        }
    }
    
    private static void comp<T>(T value, T comp, string testName) {
        if (EqualityComparer<T>.Default.Equals(value, comp)) {
            Console.WriteLine($"{testName} succeded");
        }
        else {
            Console.WriteLine($"{testName} Failed with a value of {value}");
        }
    }

    private static void TypeComp<T1, T2>(string testName) {
        Console.WriteLine($"Test {testName} result: {default(T1)}, {default(T2)}");
    }
}