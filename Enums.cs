namespace QuikFix
{
    /// <summary>
    /// Направление заявки
    /// </summary>
    class Operation
    {
        /// <summary>
        /// «Buy» – купить
        /// </summary>
        public const string Buy = "1";

        /// <summary>
        /// «Sell» – продать
        /// </summary>
        public const string Sell = "2";
    }
    /// <summary>
    /// Тип заявки
    /// </summary>
    public enum OrdType
    {
        /// <summary>
        /// Market - рыночная
        /// </summary>
        Market = 1,
        /// <summary>
        /// Limit - лимитированная
        /// </summary>
        Limit = 2
    }

}
