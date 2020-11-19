-- =============================================
-- Author:		Niyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[UpdateStaff] 
	-- Add the parameters for the stored procedure here
	@staffId int,
	@name nchar(25), 
	@subjectName nchar(25) = null,
	@role nchar(25) = null,
	@position nchar(25) = null,
	@staffType int
AS
BEGIN

	DECLARE @StaffTypeGet nchar(25);

	SET @StaffTypeGet = 0;

    -- Insert statements for procedure here
	SELECT @StaffTypeGet = StaffTypeID 
	FROM [dbo].[staff] 
	WHERE StaffID = @StaffId;

	IF @StaffTypeGet != @staffType
	BEGIN
		RETURN;
	END

	UPDATE [dbo].[staff]
	SET [Name] = @name
	WHERE StaffID = @staffId;

	IF @staffType = 1 --Teaching staff
	BEGIN

		UPDATE [dbo].[teachingStaff]
		SET [SubjectName] = @subjectName
		WHERE StaffID = @staffId;

	END

	IF @staffType = 2 --Administrative staff
	BEGIN

		UPDATE [dbo].[administrativeStaff]
		SET [Position] = @position
		WHERE StaffID = @staffId;

	END

	IF @staffType = 3 --Support staff
	BEGIN

		UPDATE [dbo].[supportStaff]
		SET [Role] = @role
		WHERE StaffID = @staffId;

	END

END
