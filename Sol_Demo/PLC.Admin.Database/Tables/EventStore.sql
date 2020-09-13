CREATE TABLE [dbo].[EventStore]
(
	[EventId] NUMERIC(18,0) IDENTITY(1,1) NOT NULL PRIMARY KEY,
	TransactionId Varchar(250),
	EventName Varchar(100),
	OldData Varchar(MAX),
	NewData Varchar(MAX),
	CreatedDate Datetime
)
