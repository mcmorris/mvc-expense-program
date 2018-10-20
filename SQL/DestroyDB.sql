ALTER TABLE [dbo].[Accounts]			DROP CONSTRAINT FK_AccountTotals
ALTER TABLE [dbo].[Accounts]			DROP CONSTRAINT FK_AccountUser
GO

ALTER TABLE [dbo].[AttachmentCoverage]	DROP CONSTRAINT FK_Attachment
ALTER TABLE [dbo].[AttachmentCoverage]	DROP CONSTRAINT FK_AttachmentStatement
ALTER TABLE [dbo].[AttachmentCoverage]	DROP CONSTRAINT FK_AttachmentTransaction
GO

ALTER TABLE [dbo].[Attachments]			DROP CONSTRAINT FK_AttachmentFileId
ALTER TABLE [dbo].[Attachments]			DROP CONSTRAINT FK_AttachmentTracking
GO

ALTER TABLE [dbo].[BankImports]			DROP CONSTRAINT FK_BankImportAccountTotals
ALTER TABLE [dbo].[BankImports]			DROP CONSTRAINT FK_BankImportImportFile
ALTER TABLE [dbo].[BankImports]			DROP CONSTRAINT FK_BankImportImporter
ALTER TABLE [dbo].[BankImports]			DROP CONSTRAINT FK_BankImportStatement
ALTER TABLE [dbo].[BankImports]			DROP CONSTRAINT FK_BankImportStatus
GO

ALTER TABLE [dbo].[InvalidTransactions] DROP CONSTRAINT FK_InvalidTransactionImport
GO

ALTER TABLE [dbo].[Statements]			DROP CONSTRAINT FK_StatementStatus
ALTER TABLE [dbo].[Statements]			DROP CONSTRAINT FK_StatementTotals
GO

ALTER TABLE [dbo].[Transactions]		DROP CONSTRAINT FK_TransactionBankImport
ALTER TABLE [dbo].[Transactions]		DROP CONSTRAINT FK_TransactionCreditCurrency
ALTER TABLE [dbo].[Transactions]		DROP CONSTRAINT FK_TransactionDebitCurrency
ALTER TABLE [dbo].[Transactions]		DROP CONSTRAINT FK_TransactionStatement
ALTER TABLE [dbo].[Transactions]		DROP CONSTRAINT FK_TransactionUserName
GO

DROP TABLE [dbo].[Accounts]
GO

DROP TABLE [dbo].[AttachmentCoverage]
GO

DROP TABLE [dbo].[Attachments]
GO

DROP TABLE [dbo].[BankImports]
GO

DROP TABLE [dbo].[ConvertableCurrency]
GO

DROP TABLE [dbo].[CurrencySums]
GO

DROP TABLE [dbo].[ImportStatus]
GO

DROP TABLE [dbo].[InvalidTransactions]
GO

DROP TABLE [dbo].[Statements]
GO

DROP TABLE [dbo].[Transactions]
GO

DROP TABLE [dbo].[Files]
GO

DROP TABLE [dbo].[Users]
GO

DROP TABLE [dbo].[AuditAll]
GO