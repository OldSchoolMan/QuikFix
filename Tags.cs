namespace QuikFix
{
    /// <summary>
    /// Tags
    /// </summary>
    public enum Tags
    {
        /// <summary>
        /// Торговый счет
        /// </summary>
        Account = 1,

        /// <summary>
        /// BeginSeqNo
        /// </summary>
        BeginSeqNo = 7,

        /// <summary>
        /// Значения:
        /// «FIX.4.2» — для протокола версии 4.2;
        /// «FIX.4.4» — для протокола версии 4.4;
        /// «FIXT.1.1» — для протокола версии 5.0
        /// </summary>
        BeginString = 8,

        /// <summary>
        /// BodyLength
        /// </summary>
        BodyLength = 9,

        /// <summary>
        /// Контрольная сумма сообщения
        /// </summary>
        CheckSum = 10,

        /// <summary>
        /// Уникальный идентификатор заявки
        /// </summary>
        ClOrdID = 11,

        /// <summary>
        /// EndSeqNo
        /// </summary>
        EndSeqNo = 16,

        /// <summary>
        /// Уникальный идентификатор сделки.
        /// </summary>
        ExecID = 17,

        /// <summary>
        /// HandlInst «1», «2»
        /// </summary>
        HandlInst = 21,

        /// <summary>
        /// Способ идентификации инструмента. Возможные значения:
        /// «1» — CUSIP;
        /// «2» — SEDOL;
        /// «4» — ISIN;
        /// «5» — RIC-код;
        /// «6» — ISO-код;
        /// «8» — код биржи;
        /// «100» — краткое наименование
        /// </summary>
        IDSource = 22,

        /// <summary>
        /// MsgSeqNum
        /// </summary>
        MsgSeqNum = 34,

        /// <summary>
        /// MsgType
        /// </summary>
        MsgType = 35,

        /// <summary>
        /// Уникальный идентификатор заявки.
        /// </summary>
        OrderID = 37,

        /// <summary>
        /// Количество в заявке. При получении значения проверяется
        /// на кратность размеру лота и конвертируется в лоты
        /// </summary>
        OrderQty = 38,

        /// <summary>
        /// Тип заявки. «1» — Market; «2» — Limit.
        /// </summary>
        OrdType = 40,

        /// <summary>
        /// Цена
        /// </summary>
        Price = 44,

        /// <summary>
        /// Номер отвергнутого сообщения
        /// </summary>
        RefSeqNum = 45,

        /// <summary>
        /// код инструмента, краткое наименование, ISO- или ISIN-код (в зависимости от значения тега [22] IDSource)
        /// </summary>
        SecurityID = 48,

        /// <summary>
        /// SenderCompID
        /// </summary>
        SenderCompID = 49,

        /// <summary>
        /// Пример: «20070830-09:38:26»
        /// </summary>
        SendingTime = 52,

        /// <summary>
        /// Направление котировки
        /// «1» — покупка;
        /// «2» — продажа
        /// </summary>
        Side = 54,

        /// <summary>
        /// Краткое наименование инструмента
        /// </summary>
        Symbol = 55,

        /// <summary>
        /// TargetCompID
        /// </summary>
        TargetCompID = 56,

        /// <summary>
        /// Text
        /// </summary>
        Text = 58,

        /// <summary>
        /// Время транзакции в формате <ГГГГММДД-ЧЧ:ММ:СС.ссс>.
        /// Пример: «20070830-09:38:26.116»
        /// </summary>
        TransactTime = 60,

        /// <summary>
        /// Значение: «0» (не использовать криптование)
        /// </summary>
        EncryptMethod = 98,

        /// <summary>
        /// В секундах
        /// </summary>
        HearBitInt = 108,

        /// <summary>
        /// Код клиента
        /// </summary>
        ClientID = 109,

        /// <summary>
        /// Только по запросу «Test Request»
        /// </summary>
        TestReqID = 112,

        /// <summary>
        /// Сброс счетчика сессии в 1, очистка хранилища отправленных сообщений
        /// </summary>
        ResetSeqNumFlag = 141,

        /// <summary>
        /// SecurityType «FXSPOT»
        /// </summary>
        SecurityType = 167,

        /// <summary>
        /// Тип подписки. Возможные значения
        /// «0» — SNAPSHOT;
        /// </summary>
        SubscriptionRequestType = 263,

        /// <summary>
        /// Клиентский идентификатор запроса
        /// </summary>
        SecurityReqID = 320,

        /// <summary>
        /// «0» — запрос описания одного инструмента;
        /// «3» — Request List Securities
        /// </summary>
        SecurityReqType = 321,

        /// <summary>
        /// Статус торговли инструментом
        /// </summary>
        SecurityTradingStatus = 326,

        /// <summary>
        /// Статус торговой сессии
        /// </summary>
        TradingSessionID = 336,

        /// <summary>
        /// Тип отвергнутого сообщения
        /// </summary>
        RefMsgType = 372,

        /// <summary>
        /// Код причины отклонения сообщения
        /// </summary>
        SessionRejectReason = 373,

        /// <summary>
        /// Возможные значения в зависимости от значения PartyRole:
        /// «3» — Код клиента для денежных и бумажных лимитов;
        /// «38» — Position account
        /// </summary>
        PartyID = 448,

        /// <summary>
        /// «3» — ClientID;
        /// «13» — Order Origination Firm;
        /// «13» — Код фирмы;
        /// </summary>
        PartyRole = 452,

        /// <summary>
        /// Клиентский идентификатор запроса
        /// </summary>
        PosReqID = 710,

        /// <summary>
        /// Значение: «0» — Positions
        /// </summary>
        PosReqType = 724,



    }
}
