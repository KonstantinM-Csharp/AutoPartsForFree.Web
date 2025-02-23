USE [master]
GO
CREATE DATABASE [AutoParts]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Vendor] [varchar](64) NOT NULL,
	[Number] [varchar](64) NOT NULL,
	[SearchVendor] [varchar](64) NOT NULL,
	[SearchNumber] [varchar](64) NOT NULL,
	[Description] [varchar](512) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Count] [int] NOT NULL
) ON [PRIMARY]	
GO
USE [master]
GO
ALTER DATABASE [AutoParts] SET  READ_WRITE 
GO
