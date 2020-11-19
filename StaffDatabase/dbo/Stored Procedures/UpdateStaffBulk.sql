-- =============================================
-- Author:		Niyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE UpdateStaffBulk 
	-- Add the parameters for the stored procedure here
	@staff StaffType READONLY,
	@teachingStaff TeachingStaffType READONLY,
	@administrativeStaff AdministrativeStaffType READONLY,
	@supportStaff SupportStaffType READONLY
AS
BEGIN

	DECLARE @staffId int,
	@name nchar(25), 
	@subjectName nchar(25),
	@role nchar(25),
	@position nchar(25),
	@staffType int;

	-- Adding Teaching staff
	DECLARE cursor_teachingStaff CURSOR
	FOR
	SELECT [@staff].StaffID ,[@staff].[Name], [@staff].[StaffTypeID], [@teachingStaff].SubjectName  FROM @staff inner join @teachingStaff on [@staff].StaffID = [@teachingStaff].StaffID;

	OPEN cursor_teachingStaff;

	FETCH NEXT FROM cursor_teachingStaff INTO 
		@staffId,
		@name, 
		@staffType,
		@subjectName;

	WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC UpdateStaff @staffId = @staffId, @name = @name, @staffType = @staffType, @subjectName = @subjectName;
        FETCH NEXT FROM cursor_teachingStaff INTO 
			@staffId,
			@name, 
			@staffType,
			@subjectName;
    END;

	CLOSE cursor_teachingStaff;

	DEALLOCATE cursor_teachingStaff;


	-- Adding Administrative staff
	DECLARE cursor_administrativeStaff CURSOR
	FOR
	SELECT [@staff].StaffID ,[@staff].[Name], [@staff].[StaffTypeID], [@administrativeStaff].Position  FROM @staff inner join @administrativeStaff on [@staff].StaffID = [@administrativeStaff].StaffID;

	OPEN cursor_administrativeStaff;

	FETCH NEXT FROM cursor_administrativeStaff INTO 
		@staffId,
		@name, 
		@staffType,
		@position;

	WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC UpdateStaff @staffId = @staffId, @name = @name, @staffType = @staffType, @position = @position;
        FETCH NEXT FROM cursor_administrativeStaff INTO 
			@staffId,
			@name, 
			@staffType,
			@position;
    END;

	CLOSE cursor_administrativeStaff;

	DEALLOCATE cursor_administrativeStaff;

	-- Adding Support staff
	DECLARE cursor_supportStaff CURSOR
	FOR
	SELECT [@staff].StaffID ,[@staff].[Name], [@staff].[StaffTypeID], [@supportStaff].[Role] FROM @staff inner join @supportStaff on [@staff].StaffID = [@supportStaff].StaffID;

	OPEN cursor_supportStaff;

	FETCH NEXT FROM cursor_supportStaff INTO 
		@staffId,
		@name, 
		@staffType,
		@role;

	WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC UpdateStaff @staffId = @staffId, @name = @name, @staffType = @staffType, @role = @role;
        FETCH NEXT FROM cursor_supportStaff INTO 
			@staffId,
			@name, 
			@staffType,
			@role;
    END;

	CLOSE cursor_supportStaff;

	DEALLOCATE cursor_supportStaff;

END