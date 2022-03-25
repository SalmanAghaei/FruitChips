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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175140_init')
BEGIN
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
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175140_init')
BEGIN
    CREATE TABLE [Category] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175140_init')
BEGIN
    CREATE TABLE [Feature] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Feature] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175140_init')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [SKU] varchar(10) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [ShortDescription] nvarchar(500) NOT NULL,
        [Description] nvarchar(1000) NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175140_init')
BEGIN
    CREATE TABLE [CategoryProduct] (
        [CategoriesId] int NOT NULL,
        [ProductsId] int NOT NULL,
        CONSTRAINT [PK_CategoryProduct] PRIMARY KEY ([CategoriesId], [ProductsId]),
        CONSTRAINT [FK_CategoryProduct_Category_CategoriesId] FOREIGN KEY ([CategoriesId]) REFERENCES [Category] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_CategoryProduct_Products_ProductsId] FOREIGN KEY ([ProductsId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175140_init')
BEGIN
    CREATE TABLE [FeatureProduct] (
        [FeaturesId] int NOT NULL,
        [ProductsId] int NOT NULL,
        CONSTRAINT [PK_FeatureProduct] PRIMARY KEY ([FeaturesId], [ProductsId]),
        CONSTRAINT [FK_FeatureProduct_Feature_FeaturesId] FOREIGN KEY ([FeaturesId]) REFERENCES [Feature] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_FeatureProduct_Products_ProductsId] FOREIGN KEY ([ProductsId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175140_init')
BEGIN
    CREATE INDEX [IX_CategoryProduct_ProductsId] ON [CategoryProduct] ([ProductsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175140_init')
BEGIN
    CREATE INDEX [IX_FeatureProduct_ProductsId] ON [FeatureProduct] ([ProductsId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301175140_init')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301175140_init', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301184835_nuallableeffectedcolumnsAuditTable')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'Type');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [AuditLogs] ALTER COLUMN [Type] nvarchar(10) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301184835_nuallableeffectedcolumnsAuditTable')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'TableName');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [AuditLogs] ALTER COLUMN [TableName] nvarchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301184835_nuallableeffectedcolumnsAuditTable')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'PrimaryKey');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [AuditLogs] ALTER COLUMN [PrimaryKey] nvarchar(100) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301184835_nuallableeffectedcolumnsAuditTable')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'OldValues');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [AuditLogs] ALTER COLUMN [OldValues] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301184835_nuallableeffectedcolumnsAuditTable')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'NewValues');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [AuditLogs] ALTER COLUMN [NewValues] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301184835_nuallableeffectedcolumnsAuditTable')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AuditLogs]') AND [c].[name] = N'AffectedColumns');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [AuditLogs] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [AuditLogs] ALTER COLUMN [AffectedColumns] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301184835_nuallableeffectedcolumnsAuditTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301184835_nuallableeffectedcolumnsAuditTable', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301185832_addUniqueSku')
BEGIN
    CREATE UNIQUE INDEX [IX_Products_SKU] ON [Products] ([SKU]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301185832_addUniqueSku')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301185832_addUniqueSku', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301223841_unique')
BEGIN
    DROP INDEX [IX_Products_SKU] ON [Products];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301223841_unique')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301223841_unique', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301230302_addSkuIsDeletedUnique')
BEGIN
    CREATE UNIQUE INDEX [IX_Products_SKU_IsDeleted] ON [Products] ([IsDeleted], [SKU]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220301230302_addSkuIsDeletedUnique')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220301230302_addSkuIsDeletedUnique', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311165812_invadd')
BEGIN
    CREATE TABLE [Inventories] (
        [Id] bigint NOT NULL IDENTITY,
        [Number] int NOT NULL,
        [DocNo] nvarchar(max) NOT NULL,
        [DocDate] datetime2 NOT NULL,
        [ProductId] int NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Inventories] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Inventories_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311165812_invadd')
BEGIN
    CREATE INDEX [IX_Inventories_ProductId] ON [Inventories] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220311165812_invadd')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220311165812_invadd', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320230715_addFeatureDetail')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Feature]') AND [c].[name] = N'Name');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Feature] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Feature] ALTER COLUMN [Name] nvarchar(256) NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320230715_addFeatureDetail')
BEGIN
    CREATE TABLE [FeatureDetail] (
        [Id] int NOT NULL IDENTITY,
        [Value] nvarchar(256) NOT NULL,
        [FeatureId] int NOT NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_FeatureDetail] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_FeatureDetail_Feature_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [Feature] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320230715_addFeatureDetail')
BEGIN
    CREATE INDEX [IX_FeatureDetail_FeatureId] ON [FeatureDetail] ([FeatureId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220320230715_addFeatureDetail')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220320230715_addFeatureDetail', N'6.0.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    ALTER TABLE [FeatureDetail] DROP CONSTRAINT [FK_FeatureDetail_Feature_FeatureId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    ALTER TABLE [FeatureProduct] DROP CONSTRAINT [FK_FeatureProduct_Feature_FeaturesId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    ALTER TABLE [FeatureDetail] DROP CONSTRAINT [PK_FeatureDetail];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    ALTER TABLE [Feature] DROP CONSTRAINT [PK_Feature];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    EXEC sp_rename N'[FeatureDetail]', N'FeatureDetails';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    EXEC sp_rename N'[Feature]', N'Features';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    EXEC sp_rename N'[FeatureDetails].[IX_FeatureDetail_FeatureId]', N'IX_FeatureDetails_FeatureId', N'INDEX';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    ALTER TABLE [FeatureDetails] ADD CONSTRAINT [PK_FeatureDetails] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    ALTER TABLE [Features] ADD CONSTRAINT [PK_Features] PRIMARY KEY ([Id]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    ALTER TABLE [FeatureDetails] ADD CONSTRAINT [FK_FeatureDetails_Features_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [Features] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    ALTER TABLE [FeatureProduct] ADD CONSTRAINT [FK_FeatureProduct_Features_FeaturesId] FOREIGN KEY ([FeaturesId]) REFERENCES [Features] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220321131057_TAbleNameChanged')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220321131057_TAbleNameChanged', N'6.0.2');
END;
GO

COMMIT;
GO

