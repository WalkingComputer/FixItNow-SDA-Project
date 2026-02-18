using System;

namespace FixItNow.Infrastructure.Data
{
    /// <summary>
    /// SINGLETON PATTERN (Creational)
    /// Ensures only one instance of database connection manager exists
    /// Thread-safe implementation using double-check locking
    /// </summary>
    public sealed class DatabaseConnectionManager
    {
        private static DatabaseConnectionManager _instance;
        private static readonly object _lock = new object();
        private readonly string _connectionString;
        private DateTime _lastConnectionTime;

        // Private constructor prevents external instantiation
        private DatabaseConnectionManager()
        {
            _connectionString = "mongodb+srv://..."; // Will be set from config
            _lastConnectionTime = DateTime.Now;
            Console.WriteLine("✅ DatabaseConnectionManager instance created (Singleton Pattern)");
        }

        /// <summary>
        /// Thread-safe singleton instance accessor
        /// Uses double-check locking for performance
        /// </summary>
        public static DatabaseConnectionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseConnectionManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public string ConnectionString => _connectionString;
        public DateTime LastConnectionTime => _lastConnectionTime;

        public void UpdateConnectionTime()
        {
            _lastConnectionTime = DateTime.Now;
        }

        public string GetConnectionInfo()
        {
            return $"Connection established at: {_lastConnectionTime:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
