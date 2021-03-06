create database VehicleCheckDB

USE [VehicleCheckDB]
GO
/****** Object:  Table [dbo].[Sys_Roles]    Script Date: 2015/1/21 17:21:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[RoleDesc] [nvarchar](50) NULL,
	[CreateDate] [smalldatetime] NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [PK_SYS_ROLES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sys_User]    Script Date: 2015/1/21 17:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserTrueName] [nvarchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[CreateDate] [smalldatetime] NOT NULL,
	[Code] [varchar](6) NULL,
	[PreIpAddress] [varchar](16) NULL,
	[PreDate] [smalldatetime] NULL,
	[LastIpAddress] [varchar](16) NULL,
	[LastDate] [smalldatetime] NULL,
	[Status] [smallint] NULL,
 CONSTRAINT [PK_SYS_USER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sys_User_Roles]    Script Date: 2015/1/21 17:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_User_Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_SYS_USER_ROLES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Sys_Roles] ON 

INSERT [dbo].[Sys_Roles] ([Id], [RoleName], [RoleDesc], [CreateDate], [Status]) VALUES (2, N'系统管理员', N'系统管理员', NULL, 0)
INSERT [dbo].[Sys_Roles] ([Id], [RoleName], [RoleDesc], [CreateDate], [Status]) VALUES (3, N'管理员', N'管理员', NULL, 0)
INSERT [dbo].[Sys_Roles] ([Id], [RoleName], [RoleDesc], [CreateDate], [Status]) VALUES (4, N'普通用户', N'普通用户', NULL, 0)
INSERT [dbo].[Sys_Roles] ([Id], [RoleName], [RoleDesc], [CreateDate], [Status]) VALUES (6, N'测试用户', N'测试用户3', NULL, 0)
SET IDENTITY_INSERT [dbo].[Sys_Roles] OFF
SET IDENTITY_INSERT [dbo].[Sys_User] ON 

INSERT [dbo].[Sys_User] ([Id], [UserName], [UserTrueName], [Password], [CreateDate], [Code], [PreIpAddress], [PreDate], [LastIpAddress], [LastDate], [Status]) VALUES (2, N'chenyan', N'陈岩', N'37b5e50f4ec87759955a32a11969405a', CAST(0xA2720000 AS SmallDateTime), N'B9A4', N'192.168.19.222', CAST(0xA3EF039A AS SmallDateTime), N'192.168.19.222', CAST(0xA3F103A2 AS SmallDateTime), 0)
INSERT [dbo].[Sys_User] ([Id], [UserName], [UserTrueName], [Password], [CreateDate], [Code], [PreIpAddress], [PreDate], [LastIpAddress], [LastDate], [Status]) VALUES (3, N'admin', N'管理员', N'37b5e50f4ec87759955a32a11969405a', CAST(0xA2720000 AS SmallDateTime), N'4VYD', N'192.168.19.225', CAST(0xA4020341 AS SmallDateTime), N'192.168.19.225', CAST(0xA40502B3 AS SmallDateTime), 0)
INSERT [dbo].[Sys_User] ([Id], [UserName], [UserTrueName], [Password], [CreateDate], [Code], [PreIpAddress], [PreDate], [LastIpAddress], [LastDate], [Status]) VALUES (4, N'liuweiliang', N'刘伟亮', N'fe85e814fd656a2d490b842c6d33019d', CAST(0xA2740000 AS SmallDateTime), N'ffff', N'192.168.19.14', CAST(0xA2E003F6 AS SmallDateTime), N'192.168.19.14', CAST(0xA2F1033B AS SmallDateTime), 0)
INSERT [dbo].[Sys_User] ([Id], [UserName], [UserTrueName], [Password], [CreateDate], [Code], [PreIpAddress], [PreDate], [LastIpAddress], [LastDate], [Status]) VALUES (5, N'zyf', N'张延分', N'37b5e50f4ec87759955a32a11969405a', CAST(0xA27E0000 AS SmallDateTime), N'rree', N'129.168.15.20', CAST(0xA3A104BC AS SmallDateTime), N'127.0.0.1', CAST(0xA3A104CD AS SmallDateTime), 0)
INSERT [dbo].[Sys_User] ([Id], [UserName], [UserTrueName], [Password], [CreateDate], [Code], [PreIpAddress], [PreDate], [LastIpAddress], [LastDate], [Status]) VALUES (10, N'lilei', N'李雷', N'fe85e814fd656a2d490b842c6d33019d', CAST(0xA28C0000 AS SmallDateTime), N'ffff', N'192.168.19.19', CAST(0xA3760259 AS SmallDateTime), N'192.168.19.19', CAST(0xA376025A AS SmallDateTime), 0)
INSERT [dbo].[Sys_User] ([Id], [UserName], [UserTrueName], [Password], [CreateDate], [Code], [PreIpAddress], [PreDate], [LastIpAddress], [LastDate], [Status]) VALUES (11, N'wangkai', N'王楷', N'fe85e814fd656a2d490b842c6d33019d', CAST(0xA2740000 AS SmallDateTime), N'WFCS', N'192.168.18.32', CAST(0xA4040348 AS SmallDateTime), N'192.168.18.32', CAST(0xA4050277 AS SmallDateTime), 0)
INSERT [dbo].[Sys_User] ([Id], [UserName], [UserTrueName], [Password], [CreateDate], [Code], [PreIpAddress], [PreDate], [LastIpAddress], [LastDate], [Status]) VALUES (26, N'wxd', N'王旭东', N'fe85e814fd656a2d490b842c6d33019d', CAST(0xA37A0418 AS SmallDateTime), N'YNBU', N'127.0.0.1', CAST(0xA40D044C AS SmallDateTime), N'127.0.0.1', CAST(0xA41A03AE AS SmallDateTime), 0)
INSERT [dbo].[Sys_User] ([Id], [UserName], [UserTrueName], [Password], [CreateDate], [Code], [PreIpAddress], [PreDate], [LastIpAddress], [LastDate], [Status]) VALUES (27, N'nyk', N'聂言康2', N'37b5e50f4ec87759955a32a11969405a', CAST(0xA37A0419 AS SmallDateTime), N'ffff', N'192.168.18.186', CAST(0xA38D02AD AS SmallDateTime), N'192.168.18.186', CAST(0xA38D02AE AS SmallDateTime), 0)
INSERT [dbo].[Sys_User] ([Id], [UserName], [UserTrueName], [Password], [CreateDate], [Code], [PreIpAddress], [PreDate], [LastIpAddress], [LastDate], [Status]) VALUES (28, N'huoliran', N'霍立然', N'fe85e814fd656a2d490b842c6d33019d', CAST(0xA3AA028A AS SmallDateTime), N'KYKF', N'127.0.0.1', CAST(0xA3FC02BD AS SmallDateTime), N'127.0.0.1', CAST(0xA40B03E3 AS SmallDateTime), 0)
SET IDENTITY_INSERT [dbo].[Sys_User] OFF
SET IDENTITY_INSERT [dbo].[Sys_User_Roles] ON 

INSERT [dbo].[Sys_User_Roles] ([Id], [UserId], [RoleId]) VALUES (13, 3, 2)
INSERT [dbo].[Sys_User_Roles] ([Id], [UserId], [RoleId]) VALUES (14, 5, 2)
INSERT [dbo].[Sys_User_Roles] ([Id], [UserId], [RoleId]) VALUES (15, 11, 2)
INSERT [dbo].[Sys_User_Roles] ([Id], [UserId], [RoleId]) VALUES (160, 4, 3)
INSERT [dbo].[Sys_User_Roles] ([Id], [UserId], [RoleId]) VALUES (162, 10, 4)
INSERT [dbo].[Sys_User_Roles] ([Id], [UserId], [RoleId]) VALUES (165, 26, 2)
INSERT [dbo].[Sys_User_Roles] ([Id], [UserId], [RoleId]) VALUES (166, 27, 3)
INSERT [dbo].[Sys_User_Roles] ([Id], [UserId], [RoleId]) VALUES (167, 28, 2)
INSERT [dbo].[Sys_User_Roles] ([Id], [UserId], [RoleId]) VALUES (168, 2, 3)
SET IDENTITY_INSERT [dbo].[Sys_User_Roles] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [AK_UQ_NAME_SYS_USER]    Script Date: 2015/1/21 17:21:22 ******/
ALTER TABLE [dbo].[Sys_User] ADD  CONSTRAINT [AK_UQ_NAME_SYS_USER] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
