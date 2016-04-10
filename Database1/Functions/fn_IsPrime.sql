/****** Object:  UserDefinedFunction [dbo].[fn_IsPrime]    Script Date: 04/10/2016 16:35:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_IsPrime]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_IsPrime]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_IsPrime]    Script Date: 04/10/2016 16:35:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION	[dbo].[fn_IsPrime]
(
	@Number INT
)
RETURNS BIT
AS

BEGIN
	IF @Number < 2
		RETURN 0

	IF @Number % 10 IN (0, 2, 4, 5, 6, 8)
		RETURN CASE WHEN @Number IN (2, 5) THEN 1 ELSE 0 END

	DECLARE	@PseudoPrimes BIGINT,
		@PseudoPrime BIGINT,
		@IsPrime BIT

	SELECT	@PseudoPrime = 1,
		@PseudoPrimes = (SQRT(@Number) - 1) / 2,
		@IsPrime = 1,
		@Number = (@Number - 1) / 2

	WHILE @PseudoPrime <= @PseudoPrimes
		IF (@Number - 2 * @PseudoPrime * @PseudoPrime - 2 * @PseudoPrime) % (2 * @PseudoPrime + 1) = 0
			BEGIN
				SELECT	@IsPrime = 0
				BREAK
			END
		ELSE
			SELECT	@PseudoPrime = @PseudoPrime + 1

	RETURN	@IsPrime
END
GO


