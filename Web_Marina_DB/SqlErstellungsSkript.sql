USE [Marina2DB]
GO

/****** Objekt: Table [dbo].[Boot] Skriptdatum: 01.11.2023 17:41:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Boot] (
    [BID]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [Laenge]        FLOAT (53)    NOT NULL,
    [Motorleistung] INT           NULL,
    [IstSegler]     BIT           NOT NULL,
    [Tiefgang]      FLOAT (53)    NOT NULL,
    [Baujahr]       NVARCHAR (4)  NULL,
    [Bildname]      NVARCHAR (45) NULL
);
GO

SET IDENTITY_INSERT [dbo].[Boot] ON
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (1, N'Kybele I', 9.2, 20, 1, 1.9, N'2016', N'b09d09cb-5079-4217-ac66-71a496c54b8c.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (2, N'Oligarchin', 36.58, 2636, 0, 2.01, N'2022', N'c43b8e4d-3d54-46a6-b45b-88dd1bcd1ecd.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (3, N'Erika', 10.45, 130, 0, 0.95, N'1990', N'09a3a915-e6ab-4d8d-95c6-700d8c4b36ee.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (4, N'Turquoise', 10.18, 18, 1, 1.5, N'2001', N'21b1df5e-2cf9-40b5-afeb-54ae8c6f2f22.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (5, N'Abendröte', 6.7, 6, 1, 0.5, NULL, N'd25c07ca-62c0-4882-a1ea-dbe312dd9959.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (6, N'More Express', 15, 65, 0, 1.25, N'1987', N'9810d52c-2578-4a51-85b7-823e5c0b7e11.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (7, N'Alegría', 12.34, 25, 1, 2.2, N'1989', N'48b4cff3-ae20-4740-b9e0-b4b51003bb52.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (8, N'Invictus', 10.3, 110, 0, 1.1, N'1975', N'11e2e904-849c-4480-8bf1-7f05d150c784.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (9, N'Bounty', 19.87, 40, 1, 1.3, N'1967', N'f1973bd0-5d93-4d70-bbbd-3a75aafece23.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (10, N'Spitbergen Svaibard', 7.55, 19, 1, 1.22, N'2000', N'4d2b9625-bd57-460b-9b5f-a3247ab00b51.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (11, N'Miyu', 9.61, 36, 1, 1.51, N'1980', N'2402fd61-b132-435b-9ff5-ea3352b94a8e.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (12, N'Explorer', 4.5, NULL, 0, 0.2, N'1997', NULL)
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (13, N'Decadence', 38.47, 2200, 0, 2.15, N'2022', N'cd944f09-7fc3-4f73-86f8-292b5a58512c.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (14, N'Trail Break', 8.25, 8, 1, 1.27, N'1982', N'6ee0f712-9b4b-480b-a04e-3b025c900679.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (15, N'Amazonas', 16.3, 370, 0, 1.8, N'2012', N'586d6974-b3bc-4ce7-aebd-c8d99b3b8cd9.jpg')
INSERT INTO [dbo].[Boot] ([BID], [Name], [Laenge], [Motorleistung], [IstSegler], [Tiefgang], [Baujahr], [Bildname]) VALUES (16, N'Ruhrperle', 12, 55, 1, 1.63, N'2004', N'108294cf-333a-42d7-8353-abda942364dc.jpg')
SET IDENTITY_INSERT [dbo].[Boot] OFF
GO


