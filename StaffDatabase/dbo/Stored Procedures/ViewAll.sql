-- =============================================
-- Author:		Niyas K C
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE ViewAll 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from TeachingStaffView;
	select * from AdministrativeStaffView;
	select * from SupportStaffView;
END
