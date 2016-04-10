/****** Object:  UserDefinedFunction [dbo].[fn_CommonCharacters]    Script Date: 04/10/2016 16:34:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_CommonCharacters]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_CommonCharacters]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_CommonCharacters]    Script Date: 04/10/2016 16:34:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_CommonCharacters]
(
    @str1 VARCHAR(100),
	@str2 VARCHAR(100)
)
RETURNS VARCHAR(MAX)

BEGIN

	DECLARE @common VARCHAR(200)
	DECLARE @counter INT

	SET @common = ''
	SET @counter = 1

	WHILE (@counter <= LEN(@str1))
	BEGIN
		IF  CHARINDEX(SUBSTRING(@str1,@counter,1), @str2) > 0
		BEGIN
			IF CHARINDEX(SUBSTRING(@str1,@counter,1), @common) = 0 AND SUBSTRING(@str1,@counter,1) <> ' '
			BEGIN
				SET @common = @common + SUBSTRING(@str1,@counter,1)
			END
		END
		SET @counter = @counter + 1
	END
	RETURN @common

END
GO


