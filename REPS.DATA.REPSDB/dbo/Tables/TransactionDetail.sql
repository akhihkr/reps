CREATE TABLE [dbo].[TransactionDetail] (
    [TransactionDetailGUID] UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [TransactionDetailID]   BIGINT           IDENTITY (1, 1) NOT NULL,
    [TransactionID]         BIGINT           NOT NULL,
    [WorkflowActionVarID]   INT              NULL,
    [VariableTypeID]        INT              NOT NULL,
    [Value]                 VARCHAR (MAX)    NOT NULL,
    [DateCreated]           DATETIME         CONSTRAINT [DF_TransactionDetail_DateCreated] DEFAULT (getdate()) NOT NULL,
    [DateModified]          DATETIME         NULL,
    [Deleted]               BIT              CONSTRAINT [DF_TransactionDetail_Deleted] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TransactionDetail] PRIMARY KEY CLUSTERED ([TransactionDetailID] ASC),
    CONSTRAINT [FK_TransactionDetail_TransactionDetail] FOREIGN KEY ([TransactionID]) REFERENCES [dbo].[Transaction] ([TransactionID]),
    CONSTRAINT [FK_TransactionDetail_VariableType] FOREIGN KEY ([VariableTypeID]) REFERENCES [dbo].[VariableType] ([ID]),
    CONSTRAINT [FK_TransactionDetail_WorkflowActionVariable] FOREIGN KEY ([WorkflowActionVarID]) REFERENCES [dbo].[WorkflowActionVariable] ([ID])
);



