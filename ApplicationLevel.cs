namespace QuikFix
{
    //  string enum
    //  https://www.codeproject.com/Articles/11130/String-Enumerations-in-C
    //  https://stackoverflow.com/questions/424366/string-representation-of-an-enum

    /// <summary>
    /// Application Level MsgTypes Field 35
    /// </summary>
    class ApplicationLevel
    {
        private ApplicationLevel()
        { }

        /// <summary>
        /// New Order Single
        /// </summary>
        public const string NewOrderSingle = "D";

        /// <summary>
        /// Order Cancel Request
        /// </summary>
        public const string OrderCancelRequest = "F";

        /// <summary>
        /// Market Data Request
        /// </summary>
        public const string MarketDataRequest = "V";

        /// <summary>
        /// Market Data Request
        /// </summary>
        public const string MarketDataRequestReject = "Y";

        /// <summary>
        /// Execution Report
        /// </summary>
        public const string ExecutionReport = "8";
        
        /// <summary>
        /// Security Definition Request
        /// </summary>
        public const string SecurityDefinitionRequest = "c";

        /// <summary>
        /// Security Definition
        /// </summary>
        public const string SecurityDefinition = "d";

        /// <summary>
        /// Request For Positions
        /// </summary>
        public const string RequestForPositions = "AN";

        /// <summary>
        /// Position Report
        /// </summary>
        public const string PositionReport = "AP";

        /// <summary>
        /// Trade Capture Report
        /// </summary>
        public const string TradeCaptureReport = "AE";
    }
}
