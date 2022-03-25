BEGIN TRANSACTION;
GO

ALTER TABLE [FeatureDetail] DROP CONSTRAINT [FK_FeatureDetail_Feature_FeatureId];
GO

ALTER TABLE [FeatureProduct] DROP CONSTRAINT [FK_FeatureProduct_Feature_FeaturesId];
GO

ALTER TABLE [FeatureDetail] DROP CONSTRAINT [PK_FeatureDetail];
GO

ALTER TABLE [Feature] DROP CONSTRAINT [PK_Feature];
GO

EXEC sp_rename N'[FeatureDetail]', N'FeatureDetails';
GO

EXEC sp_rename N'[Feature]', N'Features';
GO

EXEC sp_rename N'[FeatureDetails].[IX_FeatureDetail_FeatureId]', N'IX_FeatureDetails_FeatureId', N'INDEX';
GO

ALTER TABLE [FeatureDetails] ADD CONSTRAINT [PK_FeatureDetails] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Features] ADD CONSTRAINT [PK_Features] PRIMARY KEY ([Id]);
GO

ALTER TABLE [FeatureDetails] ADD CONSTRAINT [FK_FeatureDetails_Features_FeatureId] FOREIGN KEY ([FeatureId]) REFERENCES [Features] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [FeatureProduct] ADD CONSTRAINT [FK_FeatureProduct_Features_FeaturesId] FOREIGN KEY ([FeaturesId]) REFERENCES [Features] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220321131057_TAbleNameChanged', N'6.0.2');
GO

COMMIT;
GO

