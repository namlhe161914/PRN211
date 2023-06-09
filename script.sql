USE [master]
GO
/****** Object:  Database [CenimaDB]    Script Date: 4/11/2023 9:45:39 PM ******/
CREATE DATABASE [CenimaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CenimaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CenimaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CenimaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CenimaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CenimaDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CenimaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CenimaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CenimaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CenimaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CenimaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CenimaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [CenimaDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CenimaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CenimaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CenimaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CenimaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CenimaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CenimaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CenimaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CenimaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CenimaDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CenimaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CenimaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CenimaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CenimaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CenimaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CenimaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CenimaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CenimaDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CenimaDB] SET  MULTI_USER 
GO
ALTER DATABASE [CenimaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CenimaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CenimaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CenimaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CenimaDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CenimaDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CenimaDB] SET QUERY_STORE = OFF
GO
USE [CenimaDB]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 4/11/2023 9:45:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[GenreID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[GenreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 4/11/2023 9:45:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Year] [int] NULL,
	[Image] [nvarchar](255) NULL,
	[Description] [ntext] NULL,
	[GenreID] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 4/11/2023 9:45:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[Fullname] [nvarchar](200) NULL,
	[Gender] [nvarchar](10) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](100) NULL,
	[Type] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rates]    Script Date: 4/11/2023 9:45:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rates](
	[MovieID] [int] NOT NULL,
	[PersonID] [int] NOT NULL,
	[Comment] [ntext] NULL,
	[NumericRating] [float] NULL,
	[Time] [datetime] NULL,
 CONSTRAINT [PK_Rates] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC,
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Genres] ON 

INSERT [dbo].[Genres] ([GenreID], [Description], [Status]) VALUES (1, N'Hành động', 1)
INSERT [dbo].[Genres] ([GenreID], [Description], [Status]) VALUES (3, N'Kinh dị', 1)
INSERT [dbo].[Genres] ([GenreID], [Description], [Status]) VALUES (4, N'Hoạt hình', 1)
INSERT [dbo].[Genres] ([GenreID], [Description], [Status]) VALUES (6, N'string 123', 1)
SET IDENTITY_INSERT [dbo].[Genres] OFF
GO
SET IDENTITY_INSERT [dbo].[Movies] ON 

INSERT [dbo].[Movies] ([MovieID], [Title], [Year], [Image], [Description], [GenreID], [Status]) VALUES (2, N'Mặt nạ gương', 2021, N'demo.png', N'Demo', 1, 0)
INSERT [dbo].[Movies] ([MovieID], [Title], [Year], [Image], [Description], [GenreID], [Status]) VALUES (12, N'Mặt nạ gương tttt', 2021, N'demo.png', N'Demo', 1, 1)
INSERT [dbo].[Movies] ([MovieID], [Title], [Year], [Image], [Description], [GenreID], [Status]) VALUES (13, N'hehehe', 2021, N'demo.png', N'Demo', 6, 1)
INSERT [dbo].[Movies] ([MovieID], [Title], [Year], [Image], [Description], [GenreID], [Status]) VALUES (18, N'dgfhg', 2012, N'files\5b0c58es-960.jpg', N'dsfdgfgm', 3, 1)
INSERT [dbo].[Movies] ([MovieID], [Title], [Year], [Image], [Description], [GenreID], [Status]) VALUES (38, N'sgsdhfjkh', 2222, N'336242161_237323008770835_7872614889831218991_n.png', N'hdfghkit', 3, 1)
SET IDENTITY_INSERT [dbo].[Movies] OFF
GO
SET IDENTITY_INSERT [dbo].[Persons] ON 

INSERT [dbo].[Persons] ([PersonID], [Fullname], [Gender], [Email], [Password], [Type], [IsActive]) VALUES (1, N'Phạm Minh Hùng', N'Nam', N'user1@gmail.com', N'123', 1, 1)
INSERT [dbo].[Persons] ([PersonID], [Fullname], [Gender], [Email], [Password], [Type], [IsActive]) VALUES (2, N'Phạm Ngọc Minh Châu', N'Nữ', N'user2@gmail.com', N'1234', 2, 1)
INSERT [dbo].[Persons] ([PersonID], [Fullname], [Gender], [Email], [Password], [Type], [IsActive]) VALUES (3, N'Hoàng Đức Hải', N'Nam', N'user3@gmail.com', N'12345', 2, 1)
INSERT [dbo].[Persons] ([PersonID], [Fullname], [Gender], [Email], [Password], [Type], [IsActive]) VALUES (4, N'Quách Như Thế', N'Nam', N'user4@gmail.com', N'123456', 2, 1)
INSERT [dbo].[Persons] ([PersonID], [Fullname], [Gender], [Email], [Password], [Type], [IsActive]) VALUES (5, N'Nguyễn Thùy Dương', N'Nữ', N'user5@gmail.com', N'1234567', 1, 1)
INSERT [dbo].[Persons] ([PersonID], [Fullname], [Gender], [Email], [Password], [Type], [IsActive]) VALUES (6, N'Trịnh Thị Trang', N'Nữ', N'user6@gmail.com', N'12345678', 2, 0)
INSERT [dbo].[Persons] ([PersonID], [Fullname], [Gender], [Email], [Password], [Type], [IsActive]) VALUES (7, N'Hoàng Tùng', N'Nam', N'user7@gmail.com', N'123456789', 2, 0)
INSERT [dbo].[Persons] ([PersonID], [Fullname], [Gender], [Email], [Password], [Type], [IsActive]) VALUES (8, N'123', N'Nam', N'user10@gmail.com', N'123', 2, 1)
SET IDENTITY_INSERT [dbo].[Persons] OFF
GO
INSERT [dbo].[Rates] ([MovieID], [PersonID], [Comment], [NumericRating], [Time]) VALUES (2, 1, N'xfhd', 5, CAST(N'2022-10-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Rates] ([MovieID], [PersonID], [Comment], [NumericRating], [Time]) VALUES (2, 2, N'fewf', 3, CAST(N'2022-10-06T00:00:00.000' AS DateTime))
INSERT [dbo].[Rates] ([MovieID], [PersonID], [Comment], [NumericRating], [Time]) VALUES (12, 1, N'fssd', 4, CAST(N'2022-10-06T00:00:00.000' AS DateTime))
GO
ALTER TABLE [dbo].[Movies]  WITH CHECK ADD  CONSTRAINT [FK_Movies_Genres] FOREIGN KEY([GenreID])
REFERENCES [dbo].[Genres] ([GenreID])
GO
ALTER TABLE [dbo].[Movies] CHECK CONSTRAINT [FK_Movies_Genres]
GO
ALTER TABLE [dbo].[Rates]  WITH CHECK ADD  CONSTRAINT [FK_Rates_Movies] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[Rates] CHECK CONSTRAINT [FK_Rates_Movies]
GO
ALTER TABLE [dbo].[Rates]  WITH CHECK ADD  CONSTRAINT [FK_Rates_Persons] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Persons] ([PersonID])
GO
ALTER TABLE [dbo].[Rates] CHECK CONSTRAINT [FK_Rates_Persons]
GO
USE [master]
GO
ALTER DATABASE [CenimaDB] SET  READ_WRITE 
GO
