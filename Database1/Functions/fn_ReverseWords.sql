/****** Object:  UserDefinedFunction [dbo].[fn_ReverseWords]    Script Date: 04/10/2016 16:36:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_ReverseWords]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_ReverseWords]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_ReverseWords]    Script Date: 04/10/2016 16:36:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_ReverseWords]
(
    @source VARCHAR(MAX)
)
RETURNS VARCHAR(MAX)

BEGIN

	DECLARE @dest VARCHAR(MAX)
	DECLARE @lenght INT 

	SET @dest = ''

	WHILE LEN(@source) > 0
	BEGIN
		IF CHARINDEX(' ', @source) > 0
		BEGIN
			SET @dest = SUBSTRING(@source,0,CHARINDEX(' ', @source)) + ' ' + @dest
			SET @source = LTRIM(RTRIM(SUBSTRING(@source,CHARINDEX(' ', @source)+1,LEN(@source))))
		END
		ELSE
		BEGIN
			SET @dest = @source + ' ' + @dest
			SET @source = ''
		END
	END
	RETURN @dest

END
GO


