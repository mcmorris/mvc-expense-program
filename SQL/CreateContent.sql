
USE [ExpensesDB];

SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 
 BEGIN TRANSACTION;

/****** Object:  Primary Database Creation ******/
CREATE TABLE AuditAll (
    Id					int				NOT NULL	IDENTITY(100,1),
    [OccurredAt]		datetime		NOT NULL,
	UserName			nvarchar(255)	NOT NULL,
    TableName			nvarchar(255)	NOT NULL,
	AuditEntry			xml				NULL,

    CONSTRAINT PK_AuditAll					PRIMARY KEY CLUSTERED(Id ASC)
)

CREATE TABLE ImportStatus (
	Id					int				NOT NULL	IDENTITY(100, 1),
	Name				nvarchar(255)	NOT NULL,
	Active				bit				NOT NULL	DEFAULT 1,

	CONSTRAINT PK_Statuses PRIMARY KEY CLUSTERED(Id ASC)
)

CREATE TABLE ISO4217Currency (
	Code				nvarchar(3)			PRIMARY KEY CLUSTERED(Code ASC),
	Exponent			int					NOT NULL	DEFAULT 0,
	[Name]				nvarchar(255)		NOT NULL,
	WithdrawalDate		datetime			NULL,
	Active				bit					NOT NULL	DEFAULT 1,

	CONSTRAINT CN_WithdrawalDateTooEarlyOrLate	CHECK(WithdrawalDate = NULL OR (WithdrawalDate > '1.1.1980 00:00:00' AND WithdrawalDate < GetDate()))
)

CREATE TABLE EntityHistory (
    Id					int					NOT NULL	IDENTITY(100,1),
	Created				datetime			NOT NULL	DEFAULT GetDate(),
	CreatedBy			nvarchar(255)		NOT NULL,
	Modified			datetime			NOT NULL	DEFAULT GetDate(),
	ModifiedBy			nvarchar(255)		NOT NULL,
	InactiveSince		datetime			NULL,

	CONSTRAINT PK_EntityHistory				PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT CN_CreatedByTooEarlyOrLate	CHECK(CreatedBy > '1.1.1980 00:00:00' AND CreatedBy < GetDate()),
	CONSTRAINT CN_ModifiedByTooEarlyOrLate	CHECK(ModifiedBy > '1.1.1980 00:00:00' AND ModifiedBy < GetDate())
)

CREATE TABLE ExchangeRate (
	Id					int					IDENTITY(100, 1)			NOT NULL,
	Effective			datetime			NOT NULL					DEFAULT GetDate(),
	ConversionRate		decimal(18, 4)		NOT NULL,
	CurrencyFrom		nvarchar(3)			NOT NULL,
	CurrencyTo			nvarchar(3)			NOT NULL,

	CONSTRAINT PK_ExchangeRate				PRIMARY KEY					CLUSTERED(Id ASC),
	CONSTRAINT FK_ExchangeRateCurrencyFrom	FOREIGN KEY(CurrencyFrom)	REFERENCES ISO4217Currency(Code),
	CONSTRAINT FK_ExchangeRateCurrencyTo	FOREIGN KEY(CurrencyTo)		REFERENCES ISO4217Currency(Code),
	CONSTRAINT CN_EffectiveTooEarlyOrLate	CHECK(Effective > '1.1.1980 00:00:00' AND Effective < GetDate())
)

CREATE TABLE [Money] (
	Id					int					IDENTITY(100, 1) NOT NULL,
	ExchangeRate		int					NOT NULL,
	IncurredOn			datetime			NOT NULL,
	ExternalAmount		decimal(18, 4)		NOT NULL					DEFAULT 0.000,
	InternalAmount		decimal(18, 4)		NOT NULL					DEFAULT 0.000,

	CONSTRAINT PK_Money						PRIMARY KEY					CLUSTERED(Id ASC),
	CONSTRAINT FK_MoneyExchangeRate			FOREIGN KEY(ExchangeRate)	REFERENCES ExchangeRate(Id),
	CONSTRAINT CN_IncurredOnTooEarlyOrLate	CHECK(IncurredOn > '1.1.1980 00:00:00' AND IncurredOn < GetDate()),
	CONSTRAINT CN_PositiveAmount			CHECK(ExternalAmount > 0.00),
)

CREATE TABLE MoneyRunningTotal (
	Id					int						IDENTITY(100, 1)		NOT NULL,		
	DebitSum			int						NOT NULL,
	CreditSum			int						NOT NULL,

	CONSTRAINT PK_MoneyRunningTotal				PRIMARY KEY				CLUSTERED(Id ASC),
	CONSTRAINT FK_MoneyRunningTotalDebitMoney	FOREIGN KEY(DebitSum)	REFERENCES [Money](Id),
	CONSTRAINT FK_MoneyRunningTotalCreditMoney	FOREIGN KEY(CreditSum)	REFERENCES [Money](Id)
)

CREATE TABLE [Users] (
	Id					nvarchar(255)	NOT NULL	PRIMARY KEY,
	ManagerId			nvarchar(255)	NULL,
	FullName			nvarchar(255)	NOT NULL,
	DepartmentName		nvarchar(255)	NULL,
	JobTitle			nvarchar(255)	NULL,

	RunningSumId		int				NOT NULL,
	HistoryId			int				NOT NULL,
	Active				bit				NOT NULL	DEFAULT 1,

	CONSTRAINT FK_UserEntryStatus		FOREIGN KEY(HistoryId)		REFERENCES EntityHistory(Id),
	CONSTRAINT FK_UserRunningTotal		FOREIGN KEY(RunningSumId)	REFERENCES MoneyRunningTotal(Id)
)

CREATE TABLE Files (
	Id					int				IDENTITY(1000, 1)			NOT NULL,
	Uploader			nvarchar(255)	NOT NULL,
	[FileName]			nvarchar(255)	NOT NULL,
	[FilePath]			nvarchar(max)	NOT NULL,
	ContentType			nvarchar(255)	NOT NULL,
	[Description]		nvarchar(max)	NULL,

	FileSize			int				NOT NULL,
	RunningSumId		int				NOT NULL,
	HistoryId			int				NOT NULL,
	Active				bit				NOT NULL					DEFAULT 1,

	CONSTRAINT PK_Files					PRIMARY KEY					CLUSTERED(Id ASC),
	CONSTRAINT FK_FileUploader			FOREIGN KEY (Uploader)		REFERENCES Users(Id),
	CONSTRAINT FK_FileEntryStatus		FOREIGN KEY(HistoryId)		REFERENCES EntityHistory(Id),
	CONSTRAINT FK_FileRunningTotal		FOREIGN KEY(RunningSumId)	REFERENCES MoneyRunningTotal(Id)
)

CREATE TABLE Accounts (
	UserName			nvarchar(255)	NOT NULL,
	MaskedCardNumber	nvarchar(16)	NOT NULL,
	Expiry				date			NOT NULL,

	RunningSumId		int				NOT NULL,
	HistoryId			int				NOT NULL,
	Active				bit				NOT NULL					DEFAULT 1,

	CONSTRAINT PK_Accounts				PRIMARY KEY(UserName, MaskedCardNumber),
	CONSTRAINT FK_AccountTotals			FOREIGN KEY(RunningSumId)	REFERENCES MoneyRunningTotal(Id),
	CONSTRAINT FK_AccountUser			FOREIGN KEY(UserName)		REFERENCES Users(Id),
	CONSTRAINT FK_AccountStatus			FOREIGN KEY(HistoryId)		REFERENCES EntityHistory(Id),
	CONSTRAINT CN_EnforceMasking		CHECK(CHARINDEX('********', MaskedCardNumber) = 4),
	CONSTRAINT CN_ExpiryTooEarly		CHECK(Expiry > '01.01.1980 00:00:00')
)

CREATE TABLE Statements (
	Id							int				NOT NULL	IDENTITY(1000, 1),
	[Month]						date			NOT NULL,
	StatusId					int				NOT NULL	DEFAULT 100,

	-- Calculated columns (EF)
	CoversFrom					datetime		NOT NULL,
	CoversUntil					datetime		NOT NULL,

	RunningSumId				int				NOT NULL,
	ImportCount					int				NOT NULL	DEFAULT 1,
	AttachmentCount				int				NOT NULL	DEFAULT 0,
	SuccessfulTransactionsCount	int				NOT NULL	DEFAULT 0,
	TransactionErrorsCount		int				NOT NULL	DEFAULT 0,
	HistoryId					int				NOT NULL, 

	Active						bit				NOT NULL	DEFAULT 1,

	CONSTRAINT PK_Statements				PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_StatementStatus			FOREIGN KEY(StatusId)			REFERENCES ImportStatus(Id),
	CONSTRAINT FK_StatementHistory			FOREIGN KEY(HistoryId)			REFERENCES EntityHistory(Id),
	CONSTRAINT FK_StatementRunningTotal		FOREIGN KEY(RunningSumId)		REFERENCES MoneyRunningTotal(Id)
)

CREATE TABLE BankImports (
	Id					int				NOT NULL	IDENTITY(1000, 1),
	StatementId			int				NOT NULL,
	ImportFileId		int				NOT NULL,
	ImportedBy			nvarchar(255)	NOT NULL,
	[Month]				date			NOT NULL,
	Bank				nvarchar(255)	NOT NULL	DEFAULT 'TD',
	StatusId			int				NOT NULL	DEFAULT 100,
	TotalTransactions	int				NOT NULL,

	RunningSumId		int				NOT NULL,
	CoversFrom			datetime		NOT NULL,
	CoversUntil			datetime		NOT NULL,

 	HistoryId			int				NOT NULL, 
	Active				bit				NOT NULL	DEFAULT 1,

	-- Calculated columns (EF)
	AttachmentCount					int				NOT NULL	DEFAULT 0,
	SuccessfulTransactionsCount		int				NOT NULL	DEFAULT 0,
	TransactionErrorsCount			int				NOT NULL	DEFAULT 0,

	CONSTRAINT PK_BankImports				PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_BankImportStatement		FOREIGN KEY(StatementId)		REFERENCES Statements(Id),
	CONSTRAINT FK_BankImportStatus			FOREIGN KEY(StatusId)			REFERENCES ImportStatus(Id),
	CONSTRAINT FK_BankImportImporter		FOREIGN KEY(ImportedBy)			REFERENCES Users(Id),
	CONSTRAINT FK_BankImportFile			FOREIGN KEY(ImportFileId)		REFERENCES Files(Id),
	CONSTRAINT FK_BankImportHistory			FOREIGN KEY(HistoryId)			REFERENCES EntityHistory(Id),
	CONSTRAINT FK_BankImportRunningTotal	FOREIGN KEY(RunningSumId)		REFERENCES MoneyRunningTotal(Id)
)

CREATE TABLE Attachments (
	Id					int				NOT NULL						IDENTITY(100, 1),
	AttachmentFileId	int				NOT NULL,
	Issue				nvarchar(255),

 	StatusId			int				NOT NULL,
	HistoryId			int				NOT NULL,
	Active				bit				NOT NULL						DEFAULT 1,

	CONSTRAINT PK_Attachments			PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_AttachmentFileId		FOREIGN KEY (AttachmentFileId)	REFERENCES	Files(Id),
	CONSTRAINT FK_AttachmentHistory		FOREIGN KEY (HistoryId)			REFERENCES	EntityHistory(Id),
)


CREATE TABLE Transactions (
	Id					int				NOT NULL	IDENTITY(100, 1),
	StatementId			int				NOT NULL,
	BankImportId		int				NOT NULL,
	UserName			nvarchar(255)	NOT NULL,
	DateIncurred		datetime		NOT NULL,
	[Description]		nvarchar(max),
	DebitMoneyId		int,
	CreditMoneyId		int,
	MaskedCardNumber	nvarchar(16)	NOT NULL,
		
	CONSTRAINT PK_Transactions								PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_TransactionStatement						FOREIGN KEY(StatementId)				REFERENCES Statements(Id),
	CONSTRAINT FK_TransactionBankImport						FOREIGN KEY(BankImportId)				REFERENCES BankImports(Id),
	CONSTRAINT FK_TransactionDebitMoney						FOREIGN KEY(DebitMoneyId)				REFERENCES [Money](Id),
	CONSTRAINT FK_TransactionCreditMoney					FOREIGN KEY(CreditMoneyId)				REFERENCES [Money](Id),
	CONSTRAINT FK_TransactionUserName						FOREIGN KEY(UserName, MaskedCardNumber)	REFERENCES Accounts(UserName, MaskedCardNumber),
	CONSTRAINT CN_TransactionDebitOrCreditNonZeroDecimal	CHECK ((DebitMoneyId IS NOT NULL AND CreditMoneyId IS NULL) OR (DebitMoneyId IS NULL AND CreditMoneyId IS NOT NULL)),
	CONSTRAINT CN_TransactionCreatedByTooEarlyOrLate		CHECK(DateIncurred	> '1.1.1980 00:00:00'	AND DateIncurred < GetDate())
)

CREATE TABLE AttachmentCoverage (
	Id					int				NOT NULL	IDENTITY(1000, 1),
	AttachmentId		int				NOT NULL,
	StatementCovered	int,
	TransactionCovered	int,

	CONSTRAINT PK_AttachmentCoverage	PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_Attachment			FOREIGN KEY (AttachmentId)			REFERENCES Attachments(Id),
	CONSTRAINT FK_AttachmentStatement	FOREIGN KEY (StatementCovered)		REFERENCES Statements(Id),
	CONSTRAINT FK_AttachmentTransaction	FOREIGN KEY (TransactionCovered)	REFERENCES Transactions(Id),
)

CREATE TABLE InvalidTransactions (
	Id					int					NOT NULL	IDENTITY(100, 1),
	ImportId			int					NOT NULL,
	UserName			nvarchar(255),
	DateIncurred		nvarchar(255),
	[Description]		nvarchar(max),
	DebitCurrencyCode	nvarchar(3),
	DebitValue			nvarchar(255),
	CreditCurrencyCode	nvarchar(3),
	CreditValue			nvarchar(255),
	Issue				nvarchar(255),
	MaskedCardNumber	nvarchar(16),
	CardExpiry			nvarchar(255),

	CONSTRAINT PK_InvalidTransactions		PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_InvalidTransactionImport	FOREIGN KEY(ImportId) REFERENCES BankImports(Id)
)

GO

COMMIT TRANSACTION;