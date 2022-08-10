using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Constants
{
    class GameConstants
    {
        public const int FIELD_SIZE = 3;
        public const int MAX_NAME_LENGTH = 25;
        public static readonly char[] GAME_MARKS = { 'X', 'O','.' };
        public const int MISTAKES_MAX_COUNT = 3; //максимальное кол-во ошибок которое мользователь может совершить подряд
        
    }
}
