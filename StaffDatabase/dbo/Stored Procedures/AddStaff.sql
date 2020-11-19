-- =============================================
-- Author:		NIyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AddStaff] 
	-- Add the parameters for the stored procedure here
	@name nchar(25), 
	@subjectName nchar(25) = null,
	@role nchar(25) = null,
	@position nchar(25) = null,
	@staffType int
AS
BEGIN

	INSERT INTO [dbo].[staff]
           ([Name]
           ,[StaffTypeID])
     VALUES
           (@name
           ,@staffType)

	IF @staffType = 1 --Teaching staff
	BEGIN
		INSERT INTO [dbo].[teachingStaff]
           ([StaffID]
           ,[SubjectName])
     VALUES
           (SCOPE_IDENTITY()
           ,@subjectName)
	END

	IF @staffType = 2 --Administrative staff
	BEGIN
		INSERT INTO [dbo].[administrativeStaff]
           ([StaffID]
           ,[Position])
     VALUES
           (SCOPE_IDENTITY()
           ,@position)
	END

	IF @staffType = 3 --Support staff
	BEGIN
		INSERT INTO [dbo].[supportStaff]
           ([StaffID]
           ,[Role])
     VALUES
           (SCOPE_IDENTITY()
           ,@role)
	END

END
