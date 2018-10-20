
USE [ExpensesDB];

SET ANSI_NULLS ON
GO
 
SET QUOTED_IDENTIFIER ON
GO
 

/****** Object:  Primary Database Creation ******/
CREATE TABLE AuditAll (
    Id					int				NOT NULL	IDENTITY(100,1),
    [OccurredAt]		datetime		NOT NULL,
	UserName			nvarchar(255)	NOT NULL,
    TableName			nvarchar(255)	NOT NULL,
	AuditEntry			xml				NULL,

    CONSTRAINT PK_AuditAll PRIMARY KEY CLUSTERED(Id ASC)
)

CREATE TABLE CurrencySums (
	Id								int					NOT NULL		IDENTITY(1000, 1),
	InternalCurrencyCode			nvarchar(3),
	InternalDebitTotal				decimal(18, 4),
	InternalCreditTotal				decimal(18, 4),
	InternalBalanceTotal			AS (InternalDebitTotal - InternalCreditTotal) PERSISTED,
	CONSTRAINT PK_CurrencySums		PRIMARY KEY CLUSTERED(Id ASC)
)

CREATE TABLE [Users] (
	Id					nvarchar(255)	NOT NULL	PRIMARY KEY,
	ManagerId			nvarchar(255),
	FullName			nvarchar(255)	NOT NULL,
	DepartmentName		nvarchar(255),
	JobTitle			nvarchar(255),
	Active				bit				NOT NULL	DEFAULT 1,
    FirstCreated		datetime		NOT NULL	DEFAULT GetDate(),
    LastModified		datetime		NOT NULL	DEFAULT GetDate(),
	CreatedBy			nvarchar(255)	NOT NULL,
	LastModifiedBy		nvarchar(255)	NOT NULL,

	CONSTRAINT CN_CreatedByTooEarlyOrLate			CHECK(FirstCreated	> '1.1.1980 00:00:00'	AND FirstCreated < GetDate()),
	CONSTRAINT CN_LastModifiedTooEarlyOrLate		CHECK(LastModified	> FirstCreated			AND LastModified < GetDate()),
)

CREATE TABLE Files (
	Id					int					IDENTITY(1000, 1) NOT NULL,
	Uploader			nvarchar(255)		NOT NULL,
	[FileName]			nvarchar(255)		NOT NULL,
	[FilePath]			nvarchar(max)		NOT NULL,
	ContentType			nvarchar(255)		NOT NULL,
	[Description]		nvarchar(max)		NULL,
	FirstCreated		datetime			NOT NULL	DEFAULT GetDate(),
    LastModified		datetime			NOT NULL	DEFAULT GetDate(),
	CreatedBy			nvarchar(255)		NOT NULL,
	LastModifiedBy		nvarchar(255)		NOT NULL,
	FileSize			int					NOT NULL,

	CONSTRAINT PK_Files			PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_FileUploader	FOREIGN KEY (Uploader)	REFERENCES Users(Id) 
)

CREATE TABLE Accounts (
	UserName			nvarchar(255)	NOT NULL,
	MaskedCardNumber	nvarchar(16)	NOT NULL,
	Expiry				date			NOT NULL,
	CurrencySumId		int				NOT NULL,
    FirstCreated		datetime		NOT NULL	DEFAULT GetDate(),
    LastModified		datetime		NOT NULL	DEFAULT GetDate(),
	CreatedBy			nvarchar(255)	NOT NULL,
	LastModifiedBy		nvarchar(255)	NOT NULL,
	Active				bit				NOT NULL	DEFAULT 1,

	CONSTRAINT PK_Accounts							PRIMARY KEY(UserName, MaskedCardNumber),
	CONSTRAINT FK_AccountTotals						FOREIGN KEY(CurrencySumId)		REFERENCES CurrencySums(Id),
	CONSTRAINT FK_AccountUser						FOREIGN KEY(UserName)			REFERENCES Users(Id),
	CONSTRAINT CN_AccountCreatedByTooEarlyOrLate	CHECK(FirstCreated	> '1.1.1980 00:00:00'	AND FirstCreated < GetDate()),
	CONSTRAINT CN_AccountLastModifiedTooEarlyOrLate	CHECK(LastModified	> FirstCreated			AND LastModified < GetDate()),
	CONSTRAINT CN_EnforceMasking					CHECK(CHARINDEX('********', MaskedCardNumber) = 4),
	CONSTRAINT CN_ExpiryTooEarly					CHECK(Expiry > '01.01.1980 00:00:00')
)

-- Some currencies have three decimal places rather than our standard two.
CREATE TABLE ConvertableCurrency (
	Id					int				NOT NULL	IDENTITY(100, 1),
	Code				nvarchar(3)		NOT NULL,
	Amount				decimal(18, 4)	NOT NULL,
	Effective			datetime		NOT NULL	DEFAULT GETDATE(),
	ConversionRate		decimal(18, 4)	NOT NULL,
	InternalCurrency	nvarchar(3)		NOT NULL,
	InternalAmount		decimal(18, 4)	NOT NULL,
	
	CONSTRAINT PK_ConvertableCurrency	PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT CN_ValidConversion		CHECK (Amount * ConversionRate = InternalAmount),
	CONSTRAINT CN_PositiveAmounts		CHECK (Amount > 0.00 AND InternalAmount > 0.00),
	CONSTRAINT CN_PositiveConversion	CHECK (ConversionRate > 0.00)
)

CREATE TABLE Statements (
	Id					int				NOT NULL	IDENTITY(1000, 1),
	[Month]				date			NOT NULL,
	StatusId			int				NOT NULL	DEFAULT 100,

	CurrencySumId		int				NOT NULL,
	CoversFrom			datetime		NOT NULL,
	CoversUntil			datetime		NOT NULL,

    FirstCreated		datetime		NOT NULL	DEFAULT GetDate(),
    LastModified		datetime		NOT NULL	DEFAULT GetDate(),
	CreatedBy			nvarchar(255)	NOT NULL,
	LastModifiedBy		nvarchar(255)	NOT NULL,

	-- Calculated column: NumberOfImports			int,
	-- Calculated column: NumberOfAttachments		int,
	-- Calculated column: SuccessfulTransactions	int,
	-- Calculated column: TransactionErrors			int,

	CONSTRAINT PK_Statements							PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_StatementStatus						FOREIGN KEY (StatusId)						REFERENCES ImportStatus(Id),
	CONSTRAINT FK_StatementTotals						FOREIGN KEY	(CurrencySumId)					REFERENCES CurrencySums(Id),
	CONSTRAINT CN_StatementCoversFromTooEarlyOrLate		CHECK(CoversFrom	> '1.1.1980 00:00:00'	AND CoversFrom	< GetDate()),
	CONSTRAINT CN_StatementCoversUntilTooEarlyOrLate	CHECK(CoversUntil	> CoversFrom			AND CoversUntil < GetDate()),
	CONSTRAINT CN_StatementCreatedByTooEarlyOrLate		CHECK(FirstCreated	> '1.1.1980 00:00:00'	AND FirstCreated < GetDate()),
	CONSTRAINT CN_StatementLastModifiedTooEarlyOrLate	CHECK(LastModified	> FirstCreated			AND LastModified < GetDate()),
)

CREATE TABLE ImportStatus (
	Id					int				NOT NULL	IDENTITY(100, 1),
	Name				nvarchar(255)	NOT NULL,

	CONSTRAINT PK_Statuses PRIMARY KEY CLUSTERED(Id ASC)
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

	CurrencySumId		int				NOT NULL,
	CoversFrom			datetime		NOT NULL,
	CoversUntil			datetime		NOT NULL,

    FirstCreated		datetime		NOT NULL	DEFAULT GetDate(),
    LastModified		datetime		NOT NULL	DEFAULT GetDate(),
	CreatedBy			nvarchar(255)	NOT NULL,
	LastModifiedBy		nvarchar(255)	NOT NULL,

	-- Calculated column: NumberOfAttachments		int,
	-- Calculated column: SuccessfulTransactions	int,
	-- Calculated column: TransactionErrors			int,

	CONSTRAINT PK_BankImports						PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_BankImportStatement				FOREIGN KEY (StatementId)		REFERENCES Statements(Id),
	CONSTRAINT FK_BankImportStatus					FOREIGN KEY (StatusId)			REFERENCES ImportStatus(Id),
	CONSTRAINT FK_BankImportImporter				FOREIGN KEY (ImportedBy)		REFERENCES Users(Id),
	CONSTRAINT FK_BankImportAccountTotals			FOREIGN KEY (CurrencySumId)		REFERENCES CurrencySums(Id),
	CONSTRAINT FK_BankImportFile					FOREIGN KEY (ImportFileId)		REFERENCES Files(Id),
	CONSTRAINT CN_ImportCoversFromTooEarlyOrLate	CHECK(CoversFrom	> '1.1.1980 00:00:00'	AND CoversFrom	< GetDate()),
	CONSTRAINT CN_ImportCoversUntilTooEarlyOrLate	CHECK(CoversUntil	> CoversFrom			AND CoversUntil < GetDate()),
	CONSTRAINT CN_ImportCreatedByTooEarlyOrLate		CHECK(FirstCreated	> '1.1.1980 00:00:00'	AND FirstCreated < GetDate()),
	CONSTRAINT CN_ImportLastModifiedTooEarlyOrLate	CHECK(LastModified	> FirstCreated			AND LastModified < GetDate()),
)

CREATE TABLE Attachments (
	Id					int				NOT NULL	IDENTITY(100, 1),
	AttachmentFileId	int				NOT NULL,
	Issue				nvarchar(255),
	FirstCreated		datetime		NOT NULL	DEFAULT GetDate(),
    LastModified		datetime		NOT NULL	DEFAULT GetDate(),
	CreatedBy			nvarchar(255)	NOT NULL,
	LastModifiedBy		nvarchar(255)	NOT NULL,

	CONSTRAINT PK_Attachments							PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_AttachmentFileId						FOREIGN KEY (AttachmentFileId)	REFERENCES	Files(Id),
	CONSTRAINT CN_AttachmentCreatedByTooEarlyOrLate		CHECK(FirstCreated	> '1.1.1980 00:00:00'	AND FirstCreated < GetDate()),
	CONSTRAINT CN_AttachmentLastModifiedTooEarlyOrLate	CHECK(LastModified	> FirstCreated			AND LastModified < GetDate()),
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

CREATE TABLE Transactions (
	Id					int				NOT NULL	IDENTITY(100, 1),
	StatementId			int				NOT NULL,
	BankImportId		int				NOT NULL,
	UserName			nvarchar(255)	NOT NULL,
	DateIncurred		datetime		NOT NULL,
	[Description]		nvarchar(max),
	DebitCurrency		int,
	CreditCurrency		int,
	MaskedCardNumber	nvarchar(16)	NOT NULL,
		
	CONSTRAINT PK_Transactions						PRIMARY KEY CLUSTERED(Id ASC),
	CONSTRAINT FK_TransactionStatement				FOREIGN KEY(StatementId)				REFERENCES Statements(Id),
	CONSTRAINT FK_TransactionBankImport				FOREIGN KEY(BankImportId)				REFERENCES BankImports(Id),
	CONSTRAINT FK_TransactionDebitCurrency			FOREIGN KEY(DebitCurrency)				REFERENCES ConvertableCurrency(Id),
	CONSTRAINT FK_TransactionCreditCurrency			FOREIGN KEY(CreditCurrency)				REFERENCES ConvertableCurrency(Id),
	CONSTRAINT FK_TransactionUserName				FOREIGN KEY(UserName, MaskedCardNumber)	REFERENCES Accounts(UserName, MaskedCardNumber),
	CONSTRAINT CN_TransactionDebitOrCreditNonZeroDecimal		CHECK ((DebitCurrency IS NOT NULL AND CreditCurrency IS NULL) OR (DebitCurrency IS NULL AND CreditCurrency IS NOT NULL)),
	CONSTRAINT CN_TransactionCreatedByTooEarlyOrLate			CHECK(DateIncurred	> '1.1.1980 00:00:00'	AND DateIncurred < GetDate()),
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