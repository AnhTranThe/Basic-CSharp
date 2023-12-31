USE [Basic_CSharpDB]
GO
/****** Object:  Table [dbo].[CART_DETAILS]    Script Date: 17/11/2023 11:23:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CART_DETAILS](
	[CartId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CartId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CARTS]    Script Date: 17/11/2023 11:23:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CARTS](
	[CartId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORDER_DETAILS]    Script Date: 17/11/2023 11:23:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORDER_DETAILS](
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NULL,
	[CurrentPrice] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ORDERS]    Script Date: 17/11/2023 11:23:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ORDERS](
	[OrderId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRODUCTS]    Script Date: 17/11/2023 11:23:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRODUCTS](
	[ProductId] [uniqueidentifier] NOT NULL,
	[Product_Name] [nvarchar](255) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[Inventory] [int] NOT NULL,
	[Category] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 17/11/2023 11:23:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[UserId] [uniqueidentifier] NOT NULL,
	[First_Name] [nvarchar](50) NOT NULL,
	[Last_Name] [nvarchar](50) NOT NULL,
	[Dob] [datetime] NULL,
	[Full_Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Gender] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CART_DETAILS] ([CartId], [ProductId], [Quantity]) VALUES (N'8f3464e9-0131-42b1-a0c1-d6095f798ed8', N'0af9ab88-d360-429f-a9c7-3448174c6ec0', 3)
GO
INSERT [dbo].[CARTS] ([CartId], [UserId]) VALUES (N'8f3464e9-0131-42b1-a0c1-d6095f798ed8', N'b0cd4f48-6036-43b0-87ce-03c15993a1d0')
GO
INSERT [dbo].[ORDER_DETAILS] ([OrderId], [ProductId], [Quantity], [CurrentPrice]) VALUES (N'49c9f093-3b5a-4429-ae31-332613bf67fe', N'0af9ab88-d360-429f-a9c7-3448174c6ec0', 3, CAST(23000000.00 AS Decimal(10, 2)))
INSERT [dbo].[ORDER_DETAILS] ([OrderId], [ProductId], [Quantity], [CurrentPrice]) VALUES (N'f972d575-d2fe-49d7-baf1-74c642d98f06', N'0af9ab88-d360-429f-a9c7-3448174c6ec0', 3, CAST(23000000.00 AS Decimal(10, 2)))
GO
INSERT [dbo].[ORDERS] ([OrderId], [UserId], [Amount], [OrderDate]) VALUES (N'49c9f093-3b5a-4429-ae31-332613bf67fe', N'b0cd4f48-6036-43b0-87ce-03c15993a1d0', CAST(23000000.00 AS Decimal(10, 2)), CAST(N'2023-11-17T21:56:27.553' AS DateTime))
INSERT [dbo].[ORDERS] ([OrderId], [UserId], [Amount], [OrderDate]) VALUES (N'f972d575-d2fe-49d7-baf1-74c642d98f06', N'b0cd4f48-6036-43b0-87ce-03c15993a1d0', CAST(23000000.00 AS Decimal(10, 2)), CAST(N'2023-11-17T21:57:26.087' AS DateTime))
GO
INSERT [dbo].[PRODUCTS] ([ProductId], [Product_Name], [Price], [Inventory], [Category]) VALUES (N'0af9ab88-d360-429f-a9c7-3448174c6ec0', N'Asus Notebook', CAST(23000000.00 AS Decimal(10, 2)), 8, N'Laptop')
INSERT [dbo].[PRODUCTS] ([ProductId], [Product_Name], [Price], [Inventory], [Category]) VALUES (N'e2510f95-cb84-47de-81dd-432ecaee24e3', N'Lightning Iphone Cord', CAST(200000.00 AS Decimal(10, 2)), 50, N'Accessories')
INSERT [dbo].[PRODUCTS] ([ProductId], [Product_Name], [Price], [Inventory], [Category]) VALUES (N'6e841eb7-66bd-4741-bcac-5fe7a592ebce', N'Iphone 13 pro max', CAST(20000000.00 AS Decimal(10, 2)), 12, N'Phone')
INSERT [dbo].[PRODUCTS] ([ProductId], [Product_Name], [Price], [Inventory], [Category]) VALUES (N'9680d795-2540-4a6b-8486-b2235ddceafd', N'Phone 14 Case', CAST(500000.00 AS Decimal(10, 2)), 20, N'Accessories')
INSERT [dbo].[PRODUCTS] ([ProductId], [Product_Name], [Price], [Inventory], [Category]) VALUES (N'4bac02a6-1bbf-4c6b-b1af-b8018f8d9f35', N'Iphone 14 plus', CAST(22000000.00 AS Decimal(10, 2)), 4, N'Phone')
INSERT [dbo].[PRODUCTS] ([ProductId], [Product_Name], [Price], [Inventory], [Category]) VALUES (N'6c094256-b320-4ff4-87de-bec68d030d8f', N'Iphone 14', CAST(19000000.00 AS Decimal(10, 2)), 6, N'Phone')
INSERT [dbo].[PRODUCTS] ([ProductId], [Product_Name], [Price], [Inventory], [Category]) VALUES (N'af375844-0aeb-49e9-a11e-c6a41030e96d', N'Iphone 15 pro max', CAST(30000000.00 AS Decimal(10, 2)), 5, N'Phone')
INSERT [dbo].[PRODUCTS] ([ProductId], [Product_Name], [Price], [Inventory], [Category]) VALUES (N'33e1446e-a306-4844-84a3-e8e5a9b63f2a', N'Acer Notebook', CAST(21000000.00 AS Decimal(10, 2)), 15, N'Laptop')
INSERT [dbo].[PRODUCTS] ([ProductId], [Product_Name], [Price], [Inventory], [Category]) VALUES (N'eeba9564-656f-42a6-a053-ec8ec06d9961', N'Iphone 14 pro max', CAST(25000000.00 AS Decimal(10, 2)), 10, N'Phone')
GO
INSERT [dbo].[USERS] ([UserId], [First_Name], [Last_Name], [Dob], [Full_Name], [Email], [Gender]) VALUES (N'b0cd4f48-6036-43b0-87ce-03c15993a1d0', N'Khải', N'Trần Quang', CAST(N'2000-01-20T00:00:00.000' AS DateTime), N'Trần Quang Khải', N'tranquangkhai@gmail.com', N'Male')
INSERT [dbo].[USERS] ([UserId], [First_Name], [Last_Name], [Dob], [Full_Name], [Email], [Gender]) VALUES (N'b9b4ca4c-5eb5-4d95-9c46-24e70be4707f', N'Sơn', N'Lưu Trường', CAST(N'1990-01-15T00:00:00.000' AS DateTime), N'Lưu Trường Sơn', N'luutruongson@gmail.com', N'Male')
INSERT [dbo].[USERS] ([UserId], [First_Name], [Last_Name], [Dob], [Full_Name], [Email], [Gender]) VALUES (N'59bc3721-4145-485b-ac86-795ddf119f1f', N'Admin', N'Admin', CAST(N'1997-02-13T00:00:00.000' AS DateTime), N'Admin', N'admin@gmail.com', N'Male')
INSERT [dbo].[USERS] ([UserId], [First_Name], [Last_Name], [Dob], [Full_Name], [Email], [Gender]) VALUES (N'29e4f9f1-8ab0-43b6-a3c0-beedead94041', N'Yến', N'Lê Hoàng', CAST(N'1997-01-15T00:00:00.000' AS DateTime), N'Lê Hoàng Yến', N'lehoangyen@gmail.com', N'Female')
INSERT [dbo].[USERS] ([UserId], [First_Name], [Last_Name], [Dob], [Full_Name], [Email], [Gender]) VALUES (N'ab5bbc66-a3fc-4b89-ae17-cb66fdfc8975', N'Anh', N'Nguyễn Tuấn', CAST(N'1996-01-15T00:00:00.000' AS DateTime), N'Nguyễn Tuấn Anh', N'nguyentuananh@gmail.com', N'Male')
INSERT [dbo].[USERS] ([UserId], [First_Name], [Last_Name], [Dob], [Full_Name], [Email], [Gender]) VALUES (N'ba8d0c69-8bb1-4f14-b29c-fa5dbd28c8ed', N'Thảo', N'Phạm Văn', CAST(N'2002-01-15T00:00:00.000' AS DateTime), N'Phạm Văn Thảo', N'phamvanthao@gmail.com', N'Other')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__USERS__A9D105349020B342]    Script Date: 17/11/2023 11:23:07 PM ******/
ALTER TABLE [dbo].[USERS] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CARTS] ADD  DEFAULT (newid()) FOR [CartId]
GO
ALTER TABLE [dbo].[ORDERS] ADD  DEFAULT (newid()) FOR [OrderId]
GO
ALTER TABLE [dbo].[PRODUCTS] ADD  DEFAULT (newid()) FOR [ProductId]
GO
ALTER TABLE [dbo].[USERS] ADD  DEFAULT (newid()) FOR [UserId]
GO
ALTER TABLE [dbo].[CART_DETAILS]  WITH CHECK ADD FOREIGN KEY([CartId])
REFERENCES [dbo].[CARTS] ([CartId])
GO
ALTER TABLE [dbo].[ORDER_DETAILS]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[ORDERS] ([OrderId])
GO
ALTER TABLE [dbo].[CART_DETAILS]  WITH CHECK ADD CHECK  (([Quantity]>=(0)))
GO
ALTER TABLE [dbo].[ORDER_DETAILS]  WITH CHECK ADD CHECK  (([CurrentPrice]>=(0)))
GO
ALTER TABLE [dbo].[ORDER_DETAILS]  WITH CHECK ADD CHECK  (([Quantity]>=(0)))
GO
ALTER TABLE [dbo].[ORDERS]  WITH CHECK ADD CHECK  (([Amount]>=(0)))
GO
ALTER TABLE [dbo].[PRODUCTS]  WITH CHECK ADD CHECK  (([Inventory]>=(0)))
GO
ALTER TABLE [dbo].[PRODUCTS]  WITH CHECK ADD CHECK  (([Price]>=(0)))
GO
ALTER TABLE [dbo].[USERS]  WITH CHECK ADD CHECK  (([Gender]='Other' OR [Gender]='Female' OR [Gender]='Male'))
GO
