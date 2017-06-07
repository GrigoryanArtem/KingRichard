namespace KingRichard.Bot.Modules.WordFilter
{
    internal class Constants
    {
        public const int Delay = 5000;

        #region parameters

        public const string Regex = "regex";
        public const string Id = "id";

        #endregion

        #region Filter module

        public const string FilterModuleName = "Filters";
        public const string FilterModuleDescription = "";

        #endregion

        #region Bad speech filter module

        public const string BadSpeechFilterModuleName = "Bad speech filter";
        public const string BadSpeechFilterModuleDescription = "";

        #endregion

        #region Regex module

        public const string RegexEventName = "regex filter";
        public const string ObsceneEventName = "obscene filter";

        #endregion

        #region Filter list

        public const string FilterListCommandAlias = "fl";
        public const string FilterListCommandName = "filter list";
        public const string FilterListCommandDescription = "Показывает текущий список фильтров.";

        #endregion

        #region Filter list

        public const string AddFilterCommandAlias = "af";
        public const string AddFilterCommandName = "add filter";
        public const string AddFilterCommandDescription = "Добавляет фильтр.";

        #endregion

        #region Filter list

        public const string RemoveFilterCommandAlias = "rf";
        public const string RemoveFilterCommandName = "remove filter";
        public const string RemoveFilterCommandDescription = "Удаляет фильтр по <id>.";

        #endregion
    }
}
