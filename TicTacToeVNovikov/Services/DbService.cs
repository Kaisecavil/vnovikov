using TicTacToeVNovikov.Repository;

namespace TicTacToeVNovikov.Services
{
    internal static class DbService
    {
        private static UnitOfWork _unit;

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
