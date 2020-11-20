﻿-- =============================================
-- Author:		Niyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AddStaffBulk] 
	-- Add the parameters for the stored procedure here
	@staff StaffType READONLY,
	@teachingStaff TeachingStaffType READONLY,
	@administrativeStaff AdministrativeStaffType READONLY,
	@supportStaff SupportStaffType READONLY
AS
BEGIN
	
	DECLARE @crossIdTable TABLE (OldID INT, [NewID] INT );

	-- Adding Staff Common
	MERGE INTO staff
	USING @staff ON 1=0
	WHEN NOT MATCHED BY TARGET
	THEN
		INSERT
           ([Name]
           ,[StaffTypeID])
		VALUES
           ([@staff].[Name]
           ,[@staff].[StaffTypeID])
		OUTPUT [@staff].StaffID, inserted.StaffID  INTO @crossIdTable (OldID, [NewId]);

	-- Adding Teaching Staff
	INSERT INTO [dbo].[teachingStaff]
           ([StaffID]
           ,[SubjectName])
     SELECT staff.StaffID, [@teachingStaff].SubjectName FROM staff INNER JOIN @crossIdTable
	 ON staff.StaffID = [@crossIdTable].[NewID]
	 INNER JOIN @teachingStaff 
	 ON [@teachingStaff].StaffID = [@crossIdTable].[OldID]

	 --Adding Administrative Staff
	 INSERT INTO [dbo].[administrativeStaff]
           ([StaffID]
           ,[Position])
     SELECT staff.StaffID, [@administrativeStaff].Position FROM staff INNER JOIN @crossIdTable
	 ON staff.StaffID = [@crossIdTable].[NewID]
	 INNER JOIN @administrativeStaff 
	 ON [@administrativeStaff].StaffID = [@crossIdTable].[OldID]

	 --Adding Support Staff
	 INSERT INTO [dbo].[supportStaff]
           ([StaffID]
           ,[Role])
     SELECT staff.StaffID, [@supportStaff].[Role] FROM staff INNER JOIN @crossIdTable
	 ON staff.StaffID = [@crossIdTable].[NewID]
	 INNER JOIN @supportStaff 
	 ON [@supportStaff].StaffID = [@crossIdTable].[OldID]

END