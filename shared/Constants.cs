namespace shared; 

public static class  Constants
{
    public static class PayOut
    {
        public static readonly Dictionary<string, string> Move = new Dictionary<string, string>
        {
            { MoveType.Outside.Red,  "1:1"},
            { MoveType.Outside.Black,  "1:1"},
            { MoveType.Outside.Even,  "1:1"},
            { MoveType.Outside.Odd,  "1:1"},
            { MoveType.Outside.OneTo18,  "1:1"},
            { MoveType.Outside.NineteenTo36,  "1:1"},
            { MoveType.Outside.First12, "2:1"},
            { MoveType.Outside.Second12,  "2:1"},
            { MoveType.Outside.Third12, "2:1"},
            { MoveType.Outside.A2To1,  "2:1"},
            { MoveType.Outside.B2To1,  "2:1"},
            { MoveType.Outside.C2To1,  "2:1"},
            { MoveType.Inside.StraightUp, "35:1"},
            { MoveType.Inside.Split, "17:1"},
            { MoveType.Inside.Street,"11:1" },
            { MoveType.Inside.Trio, "11:1" },
            { MoveType.Inside.Corner, "8:1" },
            { MoveType.Inside.DoubleStreet, "5:1" },
            { MoveType.Inside.TopLine, "6:1" }
        };
    }
    
    public static class BoardArea
    {
        public const string Inside = "Inside";
        public const string Outside = "Outside";
    }

    public static class MoveType
    {
        public static class Outside
        {
            public const string Red = "Red";
            public const string Black = "Black";
            public const string Even = "Even";
            public const string Odd = "Odd";
            public const string OneTo18 = "OneTo18";
            public const string NineteenTo36 = "NineteenTo36";
            public const string First12 = "First12";
            public const string Second12 = "Second12";
            public const string Third12 = "Third12";
            public const string A2To1 = "A2To1";
            public const string B2To1 = "B2To1";
            public const string C2To1 = "C2To1";
        }

        public static class Inside
        {
            public const string StraightUp = "StraightUp";
            public const string Split = "Split";
            public const string Corner = "Corner";
            public const string Street = "Street";
            public const string DoubleStreet = "DoubleStreet";
            public const string Trio = "Trio";
            public const string TopLine = "TopLine";
        }
        
        public static readonly Dictionary<string, string> Keys = new Dictionary<string, string>
        {
            { "Red", Outside.Red},
            { "Black", Outside.Black},
            { "Even", Outside.Even},
            { "Odd", Outside.Odd},
            { "OneTo18", Outside.OneTo18},
            { "NineteenTo36", Outside.NineteenTo36 },
            { "First12", Outside.First12 },
            { "Second12", Outside.Second12 },
            { "Third12", Outside.Third12 },
            { "A2To1", Outside.A2To1 },
            { "B2To1", Outside.B2To1 },
            { "C2To1", Outside.C2To1 },
            { "StraightUp", Inside.StraightUp },
            { "Split", Inside.Split },
            { "Street", Inside.Street  },
            { "Trio", Inside.Trio  },
            { "Corner", Inside.Corner  },
            { "DoubleStreet", Inside.DoubleStreet },
            { "TopLine", Inside.TopLine  }
        };
    }
    
    public static class Moves
    {
        public static readonly Dictionary<string, List<string>> Out = new Dictionary<string, List<string>>
        {
            { MoveType.Outside.Red, new List<string> { "1", "3", "5", "7",  "9", "12", "14", "18", "16", "21", "19", "23", "25", "27", "30", "32", "34", "36" } },
            { MoveType.Outside.Black, new List<string> { "2", "4", "6", "8", "10", "11", "13", "15", "17", "20", "22", "24", "26", "28", "29", "31", "33", "35"} },
            { MoveType.Outside.Even, new List<string> { "2", "4", "6", "8", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "30", "32", "34", "36"} },
            { MoveType.Outside.Odd, new List<string> { "1", "3", "5", "7",  "9", "11", "13", "15", "17", "19", "21", "23", "25", "27", "29", "31", "33", "34"} },
            { MoveType.Outside.OneTo18, new List<string> {"1",  "2",  "3",  "4",  "5",  "6",  "7",  "8",  "9", "10", "11", "12", "13", "14", "15", "16", "17", "18"} },
            { MoveType.Outside.NineteenTo36, new List<string> { "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36"} },
            { MoveType.Outside.First12, new List<string> { "1",  "2",  "3",  "4",  "5",  "6",  "7",  "8",  "9", "10", "11", "12"} },
            { MoveType.Outside.Second12, new List<string> { "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24" } },
            { MoveType.Outside.Third12, new List<string> { "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36"} },
            { MoveType.Outside.A2To1, new List<string> { "3", "6", "9", "12", "15", "18", "21", "24", "27", "30", "33", "36"} },
            { MoveType.Outside.B2To1, new List<string> { "2", "5", "8", "11", "14", "17", "20", "23", "26", "29", "32", "35" } },
            { MoveType.Outside.C2To1, new List<string> { "1", "4", "7", "10", "13", "16", "19", "22", "25", "28", "31", "34"} }
        };

        public static readonly Dictionary<int, List<string>> In = new Dictionary<int, List<string>>()
        {
            { 1, new List<string> {MoveType.Inside.StraightUp} },
            { 2, new List<string> {MoveType.Inside.Split} },
            { 3, new List<string> {MoveType.Inside.Street, MoveType.Inside.Trio} },
            { 4, new List<string> {MoveType.Inside.Corner} },
            { 6, new List<string> {MoveType.Inside.DoubleStreet} },
            { 5, new List<string> {MoveType.Inside.TopLine }}
        };
    }
}