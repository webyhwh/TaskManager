namespace TaskManager.Application.Common
{
    /// <summary>
    /// Константы проекта
    /// </summary>
    public static class Consts
    {
        /// <summary>
        /// Константы возвращаемых ошибок
        /// </summary>
        public static class Errors
        {
            public const string InternalError = "Неопределенная ошибка";
            public const string InvalidRequest = "Ошибка валидации входных параметров";
        }

        /// <summary>
        /// Названия ключей конфигурации
        /// </summary>
        public static class ConfigurationNames
        {
            public const string LoggerConfigFilePath = "LoggerConfigFilePath";
            public const string LocalStorageDirectory = "LocalStorageDirectory";
        }

        /// <summary>
        /// Названия подключений к БД
        /// </summary>
        public static class ConnectionStringNames
        {
            public const string TaskManagerDatabase = "TaskManagerDatabase";
        }
    }
}
