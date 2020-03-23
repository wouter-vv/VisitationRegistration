IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Addresses] (
    [Id] int NOT NULL IDENTITY,
    [Street] nvarchar(max) NULL,
    [HouseNumber] int NOT NULL,
    [PostalCode] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [UserName] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [IsAdmin] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [VisitTypes] (
    [Id] int NOT NULL IDENTITY,
    [Type] nvarchar(max) NULL,
    CONSTRAINT [PK_VisitTypes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Persons] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [AddressId] int NULL,
    CONSTRAINT [PK_Persons] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Persons_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Visitations] (
    [Id] int NOT NULL IDENTITY,
    [CheckInDateTime] datetime2 NOT NULL,
    [CheckOutDateTime] datetime2 NOT NULL,
    [PersonId] int NULL,
    [VisitTypeId] int NULL,
    CONSTRAINT [PK_Visitations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Visitations_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Persons] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Visitations_VisitTypes_VisitTypeId] FOREIGN KEY ([VisitTypeId]) REFERENCES [VisitTypes] ([Id]) ON DELETE NO ACTION
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[VisitTypes]'))
    SET IDENTITY_INSERT [VisitTypes] ON;
INSERT INTO [VisitTypes] ([Id], [Type])
VALUES (1, N'Bezoek');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[VisitTypes]'))
    SET IDENTITY_INSERT [VisitTypes] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[VisitTypes]'))
    SET IDENTITY_INSERT [VisitTypes] ON;
INSERT INTO [VisitTypes] ([Id], [Type])
VALUES (2, N'Sollicitatie');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[VisitTypes]'))
    SET IDENTITY_INSERT [VisitTypes] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[VisitTypes]'))
    SET IDENTITY_INSERT [VisitTypes] ON;
INSERT INTO [VisitTypes] ([Id], [Type])
VALUES (3, N'Levering');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Type') AND [object_id] = OBJECT_ID(N'[VisitTypes]'))
    SET IDENTITY_INSERT [VisitTypes] OFF;

GO

CREATE INDEX [IX_Persons_AddressId] ON [Persons] ([AddressId]);

GO

CREATE INDEX [IX_Visitations_PersonId] ON [Visitations] ([PersonId]);

GO

CREATE INDEX [IX_Visitations_VisitTypeId] ON [Visitations] ([VisitTypeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200320184522_InitialCreate', N'3.1.2');

GO

ALTER TABLE [Persons] DROP CONSTRAINT [FK_Persons_Addresses_AddressId];

GO

DROP TABLE [Addresses];

GO

DROP INDEX [IX_Persons_AddressId] ON [Persons];

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persons]') AND [c].[name] = N'AddressId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Persons] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Persons] DROP COLUMN [AddressId];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persons]') AND [c].[name] = N'Email');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Persons] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Persons] DROP COLUMN [Email];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persons]') AND [c].[name] = N'PhoneNumber');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Persons] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Persons] DROP COLUMN [PhoneNumber];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persons]') AND [c].[name] = N'LastName');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Persons] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Persons] ALTER COLUMN [LastName] nvarchar(100) NOT NULL;

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persons]') AND [c].[name] = N'FirstName');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Persons] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Persons] ALTER COLUMN [FirstName] nvarchar(100) NOT NULL;

GO

ALTER TABLE [Persons] ADD [Company] nvarchar(max) NULL;

GO

ALTER TABLE [Persons] ADD [Numberplate] nvarchar(max) NULL;

GO

CREATE TABLE [RegistrationFormViewModel] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [VisitTypeId] int NULL,
    [RegistrationFormViewModelId] int NULL,
    CONSTRAINT [PK_RegistrationFormViewModel] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RegistrationFormViewModel_RegistrationFormViewModel_RegistrationFormViewModelId] FOREIGN KEY ([RegistrationFormViewModelId]) REFERENCES [RegistrationFormViewModel] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_RegistrationFormViewModel_RegistrationFormViewModelId] ON [RegistrationFormViewModel] ([RegistrationFormViewModelId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200321174421_AddExtraFieldsPerson', N'3.1.2');

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Persons]') AND [c].[name] = N'Numberplate');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Persons] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Persons] DROP COLUMN [Numberplate];

GO

ALTER TABLE [Persons] ADD [LicencePlate] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200321174844_ChangeLicencePlate', N'3.1.2');

GO

DROP TABLE [RegistrationFormViewModel];

GO

DROP TABLE [Users];

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(128) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200322092744_AddIdentity', N'3.1.2');

GO

