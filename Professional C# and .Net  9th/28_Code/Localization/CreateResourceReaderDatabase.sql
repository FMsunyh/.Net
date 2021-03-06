USE [master]
GO
/****** Object:  Database [LocalizationDemo]    Script Date: 11/24/2009 20:45:28 ******/
CREATE DATABASE [LocalizationDemo] ON  PRIMARY 
( NAME = N'LocalizationDemo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\LocalizationDemo.mdf' , SIZE = 3240KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LocalizationDemo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\LocalizationDemo_log.LDF' , SIZE = 560KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LocalizationDemo] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LocalizationDemo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LocalizationDemo] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [LocalizationDemo] SET ANSI_NULLS OFF
GO
ALTER DATABASE [LocalizationDemo] SET ANSI_PADDING OFF
GO
ALTER DATABASE [LocalizationDemo] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [LocalizationDemo] SET ARITHABORT OFF
GO
ALTER DATABASE [LocalizationDemo] SET AUTO_CLOSE ON
GO
ALTER DATABASE [LocalizationDemo] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [LocalizationDemo] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [LocalizationDemo] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [LocalizationDemo] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [LocalizationDemo] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [LocalizationDemo] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [LocalizationDemo] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [LocalizationDemo] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [LocalizationDemo] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [LocalizationDemo] SET  ENABLE_BROKER
GO
ALTER DATABASE [LocalizationDemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [LocalizationDemo] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [LocalizationDemo] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [LocalizationDemo] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [LocalizationDemo] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [LocalizationDemo] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [LocalizationDemo] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [LocalizationDemo] SET  READ_WRITE
GO
ALTER DATABASE [LocalizationDemo] SET RECOVERY SIMPLE
GO
ALTER DATABASE [LocalizationDemo] SET  MULTI_USER
GO
ALTER DATABASE [LocalizationDemo] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [LocalizationDemo] SET DB_CHAINING OFF
GO
USE [LocalizationDemo]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 11/24/2009 20:45:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Key] [nchar](15) NOT NULL,
	[Default] [nvarchar](50) NOT NULL,
	[de] [nvarchar](50) NULL,
	[es] [nvarchar](50) NULL,
	[fr] [nvarchar](50) NULL,
	[it] [nvarchar](50) NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Messages] ([Key], [Default], [de], [es], [fr], [it]) VALUES (N'Goodbye        ', N'Goodbye', N'Auf Wiedersehen', N'Adios', N'Au revoir', N'Arrivederci')
INSERT [dbo].[Messages] ([Key], [Default], [de], [es], [fr], [it]) VALUES (N'GoodEvening    ', N'Good evening', N'Guten Abend', N'Bueonos noches', N'Bonsoir', N'Buona sera')
INSERT [dbo].[Messages] ([Key], [Default], [de], [es], [fr], [it]) VALUES (N'GoodMorning    ', N'Good morning', N'Guten Morgen', N'Buenos diaz', N'Bonjour', N'Buona mattina')
INSERT [dbo].[Messages] ([Key], [Default], [de], [es], [fr], [it]) VALUES (N'ThankYou       ', N'Thank you', N'Danke', N'Gracia', N'Merci', N'Grazie')
INSERT [dbo].[Messages] ([Key], [Default], [de], [es], [fr], [it]) VALUES (N'Welcome        ', N'Welcome', N'Willkommen', N'Bienvenido', N'Bienvendue', N'Benvenuto')
