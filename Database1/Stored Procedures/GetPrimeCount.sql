/****** Object:  StoredProcedure [dbo].[GetPrimeCount]    Script Date: 04/10/2016 16:39:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPrimeCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetPrimeCount]
GO

/****** Object:  StoredProcedure [dbo].[GetPrimeCount]    Script Date: 04/10/2016 16:39:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetPrimeCount] 
	@topRange BIGINT
AS
SET NOCOUNT ON

DECLARE @count INT = 0

WHILE @topRange > 0
BEGIN
	SET @count = CASE WHEN (select dbo.fn_IsPrime(@topRange))=1 THEN @count+1  ELSE @count END
	SET @topRange = @topRange-1
END

SELECT @count
GO


