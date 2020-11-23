-- =============================================
-- Author:		Niyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[AddStaffBulk] 
	-- Add the parameters for the stored procedure here
	@staffTable StaffTableType READONLY
AS
BEGIN
	
	DECLARE @crossIdTable TABLE ([StaffID] INT, [StaffTypeID] INT, [SubjectName] NCHAR (25), [Position] NCHAR (25), [Role] NCHAR (25));

	-- Adding Staff Common
	MERGE INTO staff
	USING @staffTable ON 1=0
	WHEN NOT MATCHED BY TARGET
	THEN
		INSERT
           ([Name]
           ,[StaffTypeID])
		VALUES
           ([@staffTable].[Name]
           ,[@staffTable].[StaffTypeID])
		OUTPUT inserted.StaffID, inserted.[StaffTypeID], [@staffTable].[SubjectName], [@staffTable].[Position], [@staffTable].[Role]  INTO @crossIdTable ([StaffID], [StaffTypeID], [SubjectName], [Position], [Role]);

	-- Adding Teaching Staff
	INSERT INTO [dbo].[teachingStaff]
           ([StaffID]
           ,[SubjectName])
     SELECT [@crossIdTable].[StaffID], [@crossIdTable].SubjectName 
	 FROM @crossIdTable
	 Where [@crossIdTable].StaffTypeID = 1

	 --Adding Administrative Staff
	 INSERT INTO [dbo].[administrativeStaff]
           ([StaffID]
           ,[Position])
     SELECT [@crossIdTable].[StaffID], [@crossIdTable].Position 
	 FROM @crossIdTable
	 Where [@crossIdTable].StaffTypeID = 2

	 --Adding Support Staff
	 INSERT INTO [dbo].[supportStaff]
           ([StaffID]
           ,[Role])
     SELECT [@crossIdTable].[StaffID], [@crossIdTable].[Role] 
	 FROM @crossIdTable
	 Where [@crossIdTable].StaffTypeID = 3

END
GO

