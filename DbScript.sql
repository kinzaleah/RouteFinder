USE [master]
GO
/****** Object:  Database [RouteFinder]    Script Date: 15/06/2021 09:19:36 ******/
CREATE DATABASE [RouteFinder]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RouteFinder', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RouteFinder.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RouteFinder_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RouteFinder_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RouteFinder] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RouteFinder].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RouteFinder] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RouteFinder] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RouteFinder] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RouteFinder] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RouteFinder] SET ARITHABORT OFF 
GO
ALTER DATABASE [RouteFinder] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RouteFinder] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RouteFinder] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RouteFinder] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RouteFinder] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RouteFinder] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RouteFinder] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RouteFinder] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RouteFinder] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RouteFinder] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RouteFinder] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RouteFinder] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RouteFinder] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RouteFinder] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RouteFinder] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RouteFinder] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RouteFinder] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RouteFinder] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RouteFinder] SET  MULTI_USER 
GO
ALTER DATABASE [RouteFinder] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RouteFinder] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RouteFinder] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RouteFinder] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RouteFinder] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RouteFinder] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [RouteFinder] SET QUERY_STORE = OFF
GO
USE [RouteFinder]
GO
/****** Object:  User [routefinder_user]    Script Date: 15/06/2021 09:19:36 ******/
CREATE USER [routefinder_user] FOR LOGIN [routefinder_user] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [routefinder_user]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [routefinder_user]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [routefinder_user]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [routefinder_user]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [routefinder_user]
GO
ALTER ROLE [db_datareader] ADD MEMBER [routefinder_user]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [routefinder_user]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [routefinder_user]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [routefinder_user]
GO
/****** Object:  Table [dbo].[Paths]    Script Date: 15/06/2021 09:19:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paths](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PointOne] [int] NOT NULL,
	[PointTwo] [int] NOT NULL,
	[Distance] [int] NOT NULL,
 CONSTRAINT [PK_Paths] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Points]    Script Date: 15/06/2021 09:19:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Points](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Points] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Paths] ON 
GO
INSERT [dbo].[Paths] ([Id], [PointOne], [PointTwo], [Distance]) VALUES (3, 1, 4, 20)
GO
INSERT [dbo].[Paths] ([Id], [PointOne], [PointTwo], [Distance]) VALUES (4, 4, 1, 14)
GO
INSERT [dbo].[Paths] ([Id], [PointOne], [PointTwo], [Distance]) VALUES (5, 1, 3, 15)
GO
INSERT [dbo].[Paths] ([Id], [PointOne], [PointTwo], [Distance]) VALUES (6, 1, 2, 5)
GO
INSERT [dbo].[Paths] ([Id], [PointOne], [PointTwo], [Distance]) VALUES (7, 2, 3, 5)
GO
SET IDENTITY_INSERT [dbo].[Paths] OFF
GO
SET IDENTITY_INSERT [dbo].[Points] ON 
GO
INSERT [dbo].[Points] ([id], [name]) VALUES (1, N'A')
GO
INSERT [dbo].[Points] ([id], [name]) VALUES (2, N'B')
GO
INSERT [dbo].[Points] ([id], [name]) VALUES (3, N'C')
GO
INSERT [dbo].[Points] ([id], [name]) VALUES (4, N'D')
GO
INSERT [dbo].[Points] ([id], [name]) VALUES (5, N'E')
GO
INSERT [dbo].[Points] ([id], [name]) VALUES (6, N'F')
GO
INSERT [dbo].[Points] ([id], [name]) VALUES (7, N'G')
GO
INSERT [dbo].[Points] ([id], [name]) VALUES (8, N'H')
GO
SET IDENTITY_INSERT [dbo].[Points] OFF
GO
ALTER TABLE [dbo].[Paths]  WITH CHECK ADD  CONSTRAINT [FK_Paths_Points] FOREIGN KEY([PointOne])
REFERENCES [dbo].[Points] ([id])
GO
ALTER TABLE [dbo].[Paths] CHECK CONSTRAINT [FK_Paths_Points]
GO
ALTER TABLE [dbo].[Paths]  WITH CHECK ADD  CONSTRAINT [FK_Paths_Points1] FOREIGN KEY([PointTwo])
REFERENCES [dbo].[Points] ([id])
GO
ALTER TABLE [dbo].[Paths] CHECK CONSTRAINT [FK_Paths_Points1]
GO
USE [master]
GO
ALTER DATABASE [RouteFinder] SET  READ_WRITE 
GO
