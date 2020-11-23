-- =============================================
-- Author:		Niyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[UpdateStaffBulk] 
	-- Add the parameters for the stored procedure here
	@staffTable StaffTableType READONLY
AS
BEGIN

	UPDATE [dbo].[staff]
	SET [Name] = [@staffTable].[Name]
	FROM staff INNER JOIN @staffTable
	ON [@staffTable].StaffID = staff.StaffID AND [@staffTable].StaffTypeID = staff.StaffTypeID;

	UPDATE [dbo].[teachingStaff]
		SET [SubjectName] = [@staffTable].SubjectName
		FROM teachingStaff INNER JOIN @staffTable 
		ON [@staffTable].StaffID = teachingStaff.StaffID;

	UPDATE [dbo].[administrativeStaff]
		SET [Position] = [@staffTable].Position
		FROM administrativeStaff INNER JOIN @staffTable 
		ON [@staffTable].StaffID = administrativeStaff.StaffID;

	UPDATE [dbo].[supportStaff]
		SET [Role] = [@staffTable].[Role]
		FROM supportStaff INNER JOIN @staffTable
		ON [@staffTable].StaffID = supportStaff.StaffID;

END