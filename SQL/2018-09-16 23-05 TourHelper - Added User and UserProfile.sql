USE [master]
GO
/****** Object:  Database [tourhelper]    Script Date: 16.09.2018 23:06:09 ******/
CREATE DATABASE [tourhelper]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tourhelper', FILENAME = N'c:\databases\tourhelper\tourhelper.mdf' , SIZE = 3584KB , MAXSIZE = 20971520KB , FILEGROWTH = 10%)
 LOG ON 
( NAME = N'tourhelper_log', FILENAME = N'c:\databases\tourhelper\tourhelper_log.ldf' , SIZE = 136064KB , MAXSIZE = 1048576KB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tourhelper].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tourhelper] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tourhelper] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tourhelper] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tourhelper] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tourhelper] SET ARITHABORT OFF 
GO
ALTER DATABASE [tourhelper] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [tourhelper] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [tourhelper] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tourhelper] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tourhelper] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tourhelper] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tourhelper] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tourhelper] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tourhelper] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tourhelper] SET  DISABLE_BROKER 
GO
ALTER DATABASE [tourhelper] SET AUTO_UPDATE_STATISTICS_ASYNC ON 
GO
ALTER DATABASE [tourhelper] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tourhelper] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tourhelper] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tourhelper] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tourhelper] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tourhelper] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tourhelper] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [tourhelper] SET  MULTI_USER 
GO
ALTER DATABASE [tourhelper] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tourhelper] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tourhelper] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tourhelper] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [tourhelper] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'tourhelper', N'ON'
GO
USE [tourhelper]
GO
/****** Object:  Table [dbo].[Coordinate]    Script Date: 16.09.2018 23:06:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coordinate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[Altitude] [float] NULL,
	[VerticalAccuracy] [float] NULL,
	[HorizontalAccuracy] [float] NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Coordinate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tour]    Script Date: 16.09.2018 23:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tour](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Tour] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TourPoint]    Script Date: 16.09.2018 23:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TourPoint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[TourId] [int] NULL,
	[CoordinateId] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_TourPoint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 16.09.2018 23:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserProfileId] [int] NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 16.09.2018 23:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Coordinate] ON 

INSERT [dbo].[Coordinate] ([Id], [Latitude], [Longitude], [Altitude], [VerticalAccuracy], [HorizontalAccuracy], [CreatedOn]) VALUES (1, 1, 1, 1, 1, 1, CAST(N'2018-09-16T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Coordinate] OFF
SET IDENTITY_INSERT [dbo].[Tour] ON 

INSERT [dbo].[Tour] ([Id], [Name], [CreatedOn]) VALUES (1, N'Stary Rynek', CAST(N'2018-09-16T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Tour] OFF
SET IDENTITY_INSERT [dbo].[TourPoint] ON 

INSERT [dbo].[TourPoint] ([Id], [Name], [TourId], [CoordinateId], [Description], [CreatedOn]) VALUES (1, N'Pręgierz', 1, 1, N'Miejsce spotkań młodzieży', CAST(N'2018-09-16T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[TourPoint] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [UserProfileId], [Login], [Password], [CreatedOn]) VALUES (1, 1, N'cycu', N'123', CAST(N'2018-01-09T23:58:24.000' AS DateTime))
INSERT [dbo].[User] ([Id], [UserProfileId], [Login], [Password], [CreatedOn]) VALUES (2, 2, N'wosiu', N'321', CAST(N'2019-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[User] ([Id], [UserProfileId], [Login], [Password], [CreatedOn]) VALUES (3, 3, N'maciek', N'111', CAST(N'2020-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([Id], [FirstName], [LastName], [Email], [Age], [CreatedOn]) VALUES (1, N'H', N'D', N'kixar@wp.pl', 33, CAST(N'2018-01-09T23:36:17.000' AS DateTime))
INSERT [dbo].[UserProfile] ([Id], [FirstName], [LastName], [Email], [Age], [CreatedOn]) VALUES (2, N'H', N'D', N'kixar@wp.pl', 33, CAST(N'2018-01-09T23:36:17.000' AS DateTime))
INSERT [dbo].[UserProfile] ([Id], [FirstName], [LastName], [Email], [Age], [CreatedOn]) VALUES (3, N'H', N'D', N'kixar@wp.pl', 33, CAST(N'2018-01-09T23:36:17.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
ALTER TABLE [dbo].[Tour]  WITH CHECK ADD  CONSTRAINT [FK_Tour_Tour] FOREIGN KEY([Id])
REFERENCES [dbo].[Tour] ([Id])
GO
ALTER TABLE [dbo].[Tour] CHECK CONSTRAINT [FK_Tour_Tour]
GO
ALTER TABLE [dbo].[TourPoint]  WITH CHECK ADD  CONSTRAINT [FK_TourPoint_Coordinate] FOREIGN KEY([CoordinateId])
REFERENCES [dbo].[Coordinate] ([Id])
GO
ALTER TABLE [dbo].[TourPoint] CHECK CONSTRAINT [FK_TourPoint_Coordinate]
GO
ALTER TABLE [dbo].[TourPoint]  WITH CHECK ADD  CONSTRAINT [FK_TourPoint_Tour] FOREIGN KEY([TourId])
REFERENCES [dbo].[Tour] ([Id])
GO
ALTER TABLE [dbo].[TourPoint] CHECK CONSTRAINT [FK_TourPoint_Tour]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserProfile]
GO
USE [master]
GO
ALTER DATABASE [tourhelper] SET  READ_WRITE 
GO
