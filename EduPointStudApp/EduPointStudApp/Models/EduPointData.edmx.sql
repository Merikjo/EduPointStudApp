
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/04/2018 14:16:57
-- Generated from EDMX file: E:\Visual Studio 2017\Projects\EduPointStudApp\EduPointStudApp\EduPointStudApp\Models\EduPointData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Opiskelijarekisteri];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Lasnaolot__Opett__5441852A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lasnaolotiedot] DROP CONSTRAINT [FK__Lasnaolot__Opett__5441852A];
GO
IF OBJECT_ID(N'[dbo].[FK_Lasnaolotiedot_Kurssi]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lasnaolotiedot] DROP CONSTRAINT [FK_Lasnaolotiedot_Kurssi];
GO
IF OBJECT_ID(N'[dbo].[FK_Lasnaolotiedot_Opiskelija]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lasnaolotiedot] DROP CONSTRAINT [FK_Lasnaolotiedot_Opiskelija];
GO
IF OBJECT_ID(N'[dbo].[FK_Luokkahuone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Lasnaolotiedot] DROP CONSTRAINT [FK_Luokkahuone];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Kurssi]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Kurssi];
GO
IF OBJECT_ID(N'[dbo].[Lasnaolotiedot]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lasnaolotiedot];
GO
IF OBJECT_ID(N'[dbo].[Luokkahuone]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Luokkahuone];
GO
IF OBJECT_ID(N'[dbo].[Opettaja]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Opettaja];
GO
IF OBJECT_ID(N'[dbo].[Opiskelija]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Opiskelija];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Kurssi'
CREATE TABLE [dbo].[Kurssi] (
    [Kurssinimi] varchar(50)  NULL,
    [Kurssikoodi] nvarchar(100)  NULL,
    [KurssiID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'Lasnaolotiedot'
CREATE TABLE [dbo].[Lasnaolotiedot] (
    [KirjattuSisaan] datetime  NULL,
    [KirjattuUlos] datetime  NULL,
    [Luokkakoodi] nvarchar(50)  NULL,
    [OpettajaID] int  NULL,
    [RekisteriID] int IDENTITY(1,1) NOT NULL,
    [OpiskelijaID] int  NULL,
    [KurssiID] int  NULL,
    [LuokkaID] int  NULL,
    [KirjattuID] int  NULL
);
GO

-- Creating table 'Luokkahuone'
CREATE TABLE [dbo].[Luokkahuone] (
    [LuokkaID] int  NOT NULL,
    [LuokanNimi] nvarchar(100)  NULL,
    [LuokanKoodi] nvarchar(50)  NULL
);
GO

-- Creating table 'Opettaja'
CREATE TABLE [dbo].[Opettaja] (
    [OpettajaID] int IDENTITY(1,1) NOT NULL,
    [Etunimi] varchar(50)  NULL,
    [Sukunimi] varchar(50)  NULL,
    [Opettajanro] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Opiskelija'
CREATE TABLE [dbo].[Opiskelija] (
    [Etunimi] varchar(50)  NULL,
    [Sukunimi] varchar(50)  NULL,
    [Opiskelijanro] nvarchar(50)  NULL,
    [OpiskelijaID] int IDENTITY(1,1) NOT NULL,
    [Tutkinto] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [KurssiID] in table 'Kurssi'
ALTER TABLE [dbo].[Kurssi]
ADD CONSTRAINT [PK_Kurssi]
    PRIMARY KEY CLUSTERED ([KurssiID] ASC);
GO

-- Creating primary key on [RekisteriID] in table 'Lasnaolotiedot'
ALTER TABLE [dbo].[Lasnaolotiedot]
ADD CONSTRAINT [PK_Lasnaolotiedot]
    PRIMARY KEY CLUSTERED ([RekisteriID] ASC);
GO

-- Creating primary key on [LuokkaID] in table 'Luokkahuone'
ALTER TABLE [dbo].[Luokkahuone]
ADD CONSTRAINT [PK_Luokkahuone]
    PRIMARY KEY CLUSTERED ([LuokkaID] ASC);
GO

-- Creating primary key on [OpettajaID] in table 'Opettaja'
ALTER TABLE [dbo].[Opettaja]
ADD CONSTRAINT [PK_Opettaja]
    PRIMARY KEY CLUSTERED ([OpettajaID] ASC);
GO

-- Creating primary key on [OpiskelijaID] in table 'Opiskelija'
ALTER TABLE [dbo].[Opiskelija]
ADD CONSTRAINT [PK_Opiskelija]
    PRIMARY KEY CLUSTERED ([OpiskelijaID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [KurssiID] in table 'Lasnaolotiedot'
ALTER TABLE [dbo].[Lasnaolotiedot]
ADD CONSTRAINT [FK_Lasnaolotiedot_Kurssi]
    FOREIGN KEY ([KurssiID])
    REFERENCES [dbo].[Kurssi]
        ([KurssiID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Lasnaolotiedot_Kurssi'
CREATE INDEX [IX_FK_Lasnaolotiedot_Kurssi]
ON [dbo].[Lasnaolotiedot]
    ([KurssiID]);
GO

-- Creating foreign key on [OpettajaID] in table 'Lasnaolotiedot'
ALTER TABLE [dbo].[Lasnaolotiedot]
ADD CONSTRAINT [FK__Lasnaolot__Opett__5441852A]
    FOREIGN KEY ([OpettajaID])
    REFERENCES [dbo].[Opettaja]
        ([OpettajaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Lasnaolot__Opett__5441852A'
CREATE INDEX [IX_FK__Lasnaolot__Opett__5441852A]
ON [dbo].[Lasnaolotiedot]
    ([OpettajaID]);
GO

-- Creating foreign key on [OpiskelijaID] in table 'Lasnaolotiedot'
ALTER TABLE [dbo].[Lasnaolotiedot]
ADD CONSTRAINT [FK_Lasnaolotiedot_Opiskelija]
    FOREIGN KEY ([OpiskelijaID])
    REFERENCES [dbo].[Opiskelija]
        ([OpiskelijaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Lasnaolotiedot_Opiskelija'
CREATE INDEX [IX_FK_Lasnaolotiedot_Opiskelija]
ON [dbo].[Lasnaolotiedot]
    ([OpiskelijaID]);
GO

-- Creating foreign key on [LuokkaID] in table 'Lasnaolotiedot'
ALTER TABLE [dbo].[Lasnaolotiedot]
ADD CONSTRAINT [FK_Luokkahuone]
    FOREIGN KEY ([LuokkaID])
    REFERENCES [dbo].[Luokkahuone]
        ([LuokkaID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Luokkahuone'
CREATE INDEX [IX_FK_Luokkahuone]
ON [dbo].[Lasnaolotiedot]
    ([LuokkaID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------