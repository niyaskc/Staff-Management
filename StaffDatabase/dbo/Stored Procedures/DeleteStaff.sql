-- =============================================
-- Author:		Niyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[DeleteStaff] 
	-- Add the parameters for the stored procedure here
	@StaffId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.

	DECLARE @StaffType nchar(25);

    -- Insert statements for procedure here
	SELECT @StaffType = TypeName 
	FROM [dbo].[staff] inner join [dbo].[staffType] 
	ON [dbo].[staff].StaffTypeID = [dbo].[staffType] .StaffTypeID
	WHERE StaffID = @StaffId;

	IF @StaffType = 'Teaching Staff'
	BEGIN
		DELETE FROM teachingStaff WHERE StaffId = @StaffId;
	END

	IF @StaffType = 'Administrative Staff'
	BEGIN
		DELETE FROM administrativeStaff WHERE StaffId = @StaffId;
	END

	IF @StaffType = 'Support Staff            '
	BEGIN
		DELETE FROM supportStaff WHERE StaffId = @StaffId;
	END

	DELETE FROM staff WHERE StaffId = @StaffId;
END
