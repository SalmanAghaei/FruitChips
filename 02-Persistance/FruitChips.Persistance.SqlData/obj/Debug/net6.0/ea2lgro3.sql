IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AuditLogs] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NULL,
    [Type] nvarchar(max) NOT NULL,
    [TableName] nvarchar(max) NOT NULL,
    [DateTime] datetime2 NOT NULL,
    [OldValues] nvarchar(max) NOT NULL,
    [NewValues] nvarchar(max) NOT NULL,
    [AffectedColumns] nvarchar(max) NOT NULL,
    [PrimaryKey] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Category] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Feature] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Feature] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [SKU] varchar(10) NOT NULL,
    [Name] nvarchar(100) NOT NULL,
    [ShortDescription] nvarchar(500) NOT NULL,
    [Description] nvarchar(1000) NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CategoryProduct] (
    [CategoriesId] int NOT NULL,
    [ProductsId] int NOT NULL,
    CONSTRAINT [PK_CategoryProduct] PRIMARY KEY ([CategoriesId], [ProductsId]),
    CONSTRAINT [FK_CategoryProduct_Category_CategoriesId] FOREIGN KEY ([CategoriesId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CategoryProduct_Products_ProductsId] FOREIGN KEY ([ProductsId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FeatureProduct] (
    [FeaturesId] int NOT NULL,
    [ProductsId] int NOT NULL,
    CONSTRAINT [PK_FeatureProduct] PRIMARY KEY ([FeaturesId], [ProductsId]),
    CONSTRAINT [FK_FeatureProduct_Feature_FeaturesId] FOREIGN KEY ([FeaturesId]) REFERENCES [Feature] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FeatureProduct_Products_ProductsId] FOREIGN KEY ([ProductsId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_CategoryProduct_ProductsId] ON [CategoryProduct] ([ProductsId]);
GO

CREATE INDEX [IX_FeatureProduct_ProductsId] ON [FeatureProduct] ([ProductsId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220301175140_init', N'6.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'Type');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [AuditLogs] ALTER COLUMN [Type] nvarchar(max) NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'TableName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AuditLogs] ALTER COLUMN [TableName] nvarchar(max) NULL;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'PrimaryKey');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [AuditLogs] ALTER COLUMN [PrimaryKey] nvarchar(max) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'OldValues');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [AuditLogs] ALTER COLUMN [OldValues] nvarchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'NewValues');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [AuditLogs] ALTER COLUMN [NewValues] nvarchar(max) NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'AffectedColumns');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [AuditLogs] ALTER COLUMN [AffectedColumns] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220301184551_nuallableeffectedcolumnsAuditTable', N'6.0.2');
GO

COMMIT;
GO

