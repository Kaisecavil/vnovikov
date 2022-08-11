
namespace TicTacToeVNovikov.Constants
{
    internal class GameConstants
    {
        public const int FieldSize = 3;
        public const int MaxNameLeangth = 25;
        public const int MinNameLeangth = 3;
        public const int PlayersCount = 2;
        public const int MaxMistakesCount = 3;

        public static readonly Dictionary<int, char> GameMarks = new Dictionary<int, char>()
        {
            {0,'.'},
            {1,'X'},
            {2,'O'}
        };
        public enum Marks
        {
            FirstPlayerMark = 'X',
            SecondPlayerMark = 'O',
            EmptyFieldMark ='.'
        }

    }
}
