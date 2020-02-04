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
        /// Средняя цена в исполненных сделках.
        /// Для сделок РЕПО с ЦК и сделок по календарным спредам: цена первой части сделки
        /// </summary>
        AvgPx = 6,

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
        /// Исполненное количество в штуках
        /// </summary>
        CumQty = 14,

        /// <summary>
        /// Валюта номинала или валюта шага цены
        /// </summary>
        Currency = 15,
        
        /// <summary>
        /// EndSeqNo
        /// </summary>
        EndSeqNo = 16,

        /// <summary>
        /// Уникальный идентификатор сделки.
        /// </summary>
        ExecID = 17,

        /// <summary>
        /// Значение: «0» — New.
        /// «3» для отчетов, отправленных в ответ на сообщение OrderMassStatusRequest
        /// </summary>
        ExecTransType = 20,

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
        /// Цена сделки.
        /// Для сделок РЕПО с ЦК и сделок по календарным спредам: цена первой части сделки
        /// </summary>
        LastPx = 31,

        /// <summary>
        /// Количество в текущей сделке в штуках
        /// </summary>
        LastQty = 32,

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
        /// Текущее состояние заявки.
        /// </summary>
        OrdStatus = 39,

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
        /// TimeInForce
        /// «0» — Day;
        /// «1» — Good Till Cancel;
        /// </summary>
        TimeInForce = 59,

        /// <summary>
        /// Время транзакции в формате «ГГГГММДД-ЧЧ:ММ:СС.ссс».
        /// Пример: «20070830-09:38:26.116»
        /// </summary>
        TransactTime = 60,

        /// <summary>
        /// RawData
        /// </summary>
        RawData = 96,

        /// <summary>
        /// Значение: «0» (не использовать криптование)
        /// </summary>
        EncryptMethod = 98,

        /// <summary>
        /// ExDestination
        /// </summary>
        ExDestination = 100,

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
        /// Тип отчета
        /// </summary>
        ExecType = 150,

        /// <summary>
        /// Оставшееся количество в штуках
        /// </summary>
        LeavesQty = 151,

        /// <summary>
        /// Объем заявки в денежных единицах
        /// </summary>
        CashOrderQty = 152,

        /// <summary>
        /// Информационное поле, возвращается клиенту в ответных сообщениях ExecutionReport.
        /// </summary>
        SecurityExchange = 207,

        /// <summary>
        /// Тип подписки. Возможные значения
        /// «0» — SNAPSHOT;
        /// «1» — SNAPSHOT_PLUS_UPDATES;
        /// «2» — DISABLE_PREVIOS_SNAPSHOT_PLUS_UPDATES
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
        /// Возможные значения:
        /// «D» — Proprietary / Custom code;
        /// «P» — Short code identifier
        /// </summary>
        PartyIDSource = 447,

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
        /// Количество PartyID-групп. Если присутствует, то значение: «2»
        /// </summary>
        NoPartyIDs = 453,

        /// <summary>
        /// Пароль пользователя. (Максимальная длина – 8 символов)
        /// </summary>
        Password = 554,

        /// <summary>
        /// Клиентский идентификатор запроса
        /// </summary>
        PosReqID = 710,

        /// <summary>
        /// Значение: «0» — Positions
        /// </summary>
        PosReqType = 724,

        /// <summary>
        /// Номер сделки;
        /// Номер заявки, выставленной на основании стоп-заявки;
        /// Номер новой выставленной заявки при OrdStatus=«Replaced»
        /// </summary>
        TradeNum = 5001,

        /// <summary>
        /// Номер заявки / неторгового поручения;
        /// Номер замененной (старой) заявки (при OrdStatus=«Replaced»)
        /// </summary>
        OrderNum = 5002,

        /// <summary>
        /// Идентификатор пользователя QUIK, выставившего заявку
        /// </summary>
        UserID = 5015,

        /// <summary>
        /// Идентификатор рабочей станции трейдера
        /// </summary>
        StationID = 5016,

        /// <summary>
        /// Смещение локального времени Интерфейса относительно GMT
        /// </summary>
        TradeTimeGMT = 5017,

        /// <summary>
        /// Идентификатор дилера в сделке
        /// </summary>
        FirmId = 5018,

        /// <summary>
        /// Комиссия за выдачу наличных, для неторговых поручений;
        /// Комиссия техцентра
        /// </summary>
        ServiceCommission = 5010,

        /// <summary>
        /// Клиентский идентификатор транзакции на сервере QUIK
        /// </summary>
        RequestID = 5060,


    }
}
