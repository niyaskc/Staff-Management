-- =============================================
-- Author:		Niyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[UpdateStaffBulk] 
	-- Add the parameters for the stored procedure here
	@staff StaffType READONLY,
	@teachingStaff TeachingStaffType READONLY,
	@administrativeStaff AdministrativeStaffType READONLY,
	@supportStaff SupportStaffType READONLY
AS
BEGIN

	UPDATE [dbo].[staff]
	SET [Name] = [@staff].[Name]
	FROM staff INNER JOIN @staff
	ON [@staff].StaffID = staff.StaffID AND [@staff].StaffTypeID = staff.StaffTypeID;

	UPDATE [dbo].[teachingStaff]
		SET [SubjectName] = [@teachingStaff].SubjectName
		FROM teachingStaff INNER JOIN @teachingStaff 
		ON [@teachingStaff].StaffID = teachingStaff.StaffID;

	UPDATE [dbo].[administrativeStaff]
		SET [Position] = [@administrativeStaff].Position
		FROM administrativeStaff INNER JOIN @administrativeStaff 
		ON [@administrativeStaff].StaffID = administrativeStaff.StaffID;

	UPDATE [dbo].[supportStaff]
		SET [Role] = [@supportStaff].Role
		FROM supportStaff INNER JOIN @supportStaff
		ON [@supportStaff].StaffID = supportStaff.StaffID;

END