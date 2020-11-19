-- =============================================
-- Author:		Niyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ViewStaff] 
	-- Add the parameters for the stored procedure here
	@StaffId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @StaffType nchar(25);

    -- Insert statements for procedure here
	SELECT @StaffType = TypeName 
	FROM [dbo].[staff] inner join [dbo].[staffType] 
	ON [dbo].[staff].StaffTypeID = [dbo].[staffType] .StaffTypeID
	WHERE StaffID = @StaffId;

	IF @StaffType = 'Teaching Staff'
	BEGIN
		SELECT * FROM TeachingStaffView WHERE StaffID = @StaffId;
	END

	IF @StaffType = 'Administrative Staff'
	BEGIN
		SELECT * FROM AdministrativeStaffView WHERE StaffID = @StaffId;
	END

	IF @StaffType = 'Support Staff            '
	BEGIN
		SELECT * FROM SupportStaffView WHERE StaffID = @StaffId;
	END

END
