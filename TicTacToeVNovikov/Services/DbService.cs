using TicTacToeVNovikov.Repository;

namespace TicTacToeVNovikov.Services
{
    /// <summary>
    /// This class allows us to interact with Db anywhere in project
    /// </summary>
    internal static class DbService
    {
        private static UnitOfWork _unit;
        
        /// <summary>
        /// Property that gives access to UnitOfWork
        /// </summary>
        public static UnitOfWork Unit
        {
            get { return _unit; }
            private set
            {
                _unit = value;
            }
        }

        static DbService()
        {
            try
            {
                Unit = new(new ApplicationContext());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
