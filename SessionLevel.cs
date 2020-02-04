namespace QuikFix
{
    /// <summary>
    /// Session Level MsgTypes Field 35
    /// </summary>
    class SessionLevel
    {
        private SessionLevel()
        { }
        /// <summary>
        /// Logon
        /// </summary>
        public const string Logon = "A";

        /// <summary>
        /// Heart Beat
        /// </summary>
        public const string HeartBeat = "0";

        /// <summary>
        /// Logout
        /// </summary>
        public const string Logout = "5";

        /// <summary>
        /// Test Request
        /// </summary>
        public const string TestRequest = "1";

        /// <summary>
        /// Resend Request
        /// </summary>
        public const string ResendRequest = "2";

        /// <summary>
        /// Reject
        /// </summary>
        public const string Reject = "3";

        /// <summary>
        /// Sequence Reset
        /// </summary>
        public const string SequenceReset = "4";

    }
}
