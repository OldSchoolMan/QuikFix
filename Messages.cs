namespace QuikFix
{
    /// <summary>
    /// Сообщения Session Level
    /// </summary>
    public enum Messages
    {
        /// <summary>
        /// Heart Beat
        /// </summary>
        HeartBeat = '0',

        /// <summary>
        /// Logon
        /// </summary>
        Logon = 'A',

        /// <summary>
        /// Logout
        /// </summary>
        Logout = '5',

        /// <summary>
        /// Resend Request
        /// </summary>
        ResendRequest = '2',

        /// <summary>
        /// Reject
        /// </summary>
        Reject = '3',

        /// <summary>
        /// Sequence Reset
        /// </summary>
        SequenceReset = '4',

        /// <summary>
        /// Test Request
        /// </summary>
        TestRequest = '1',

        /// <summary>
        /// New Order Single
        /// </summary>
        NewOrderSingle = 'D',

        /// <summary>
        /// Order Cancel Request
        /// </summary>
        OrderCancelRequest = 'F',

        /// <summary>
        /// Execution Report
        /// </summary>
        ExecutionReport = '8',

        /// <summary>
        /// Market Data Request
        /// </summary>
        MarketDataRequest = 'V',

        /// <summary>
        /// Security Definition Request
        /// </summary>
        SecurityDefinitionRequest = 'c',

        /// <summary>
        /// Security Definition
        /// </summary>
        SecurityDefinition = 'd',

    }
}