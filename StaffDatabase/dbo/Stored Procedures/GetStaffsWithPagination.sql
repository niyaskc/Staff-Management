-- =============================================
-- Author:		Niyas
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[GetStaffsWithPagination] 
	-- Add the parameters for the stored procedure here
	@StaffType INT = 1, -- 1) teaching, 2) admin, 3) support
	@PageNumber INT = 1, 
	@PageSize INT = 2,
	@SortByField nvarchar(25) = 'Name', --The field used for sort by
	@SortOrder nvarchar(255) = 'ASC' --ASC or DESC
AS
BEGIN

	IF @StaffType = 1 -- teaching
	BEGIN
		SELECT * FROM TeachingStaffView
		ORDER BY 
		CASE WHEN @SortByField = 'Staff ID' AND @SortOrder = 'ASC'
			THEN TeachingStaffView.[StaffID] END ASC,
		CASE WHEN @SortByField = 'Staff ID' AND @SortOrder = 'DESC'
			THEN TeachingStaffView.[StaffID] END DESC,
			CASE WHEN @SortByField = 'Name' AND @SortOrder = 'ASC'
			THEN TeachingStaffView.[Name] END ASC,
		CASE WHEN @SortByField = 'Name' AND @SortOrder = 'DESC'
			THEN TeachingStaffView.[Name] END DESC,
			CASE WHEN @SortByField = 'Subject Name' AND @SortOrder = 'ASC'
			THEN TeachingStaffView.[SubjectName] END ASC,
		CASE WHEN @SortByField = 'Subject Name' AND @SortOrder = 'DESC'
			THEN TeachingStaffView.[SubjectName] END DESC
		OFFSET @PageSize * (@PageNumber - 1) ROWS FETCH NEXT @PageSize ROWS ONLY;

		SELECT COUNT(*) AS totalItems
		FROM TeachingStaffView;
	END

	IF @StaffType = 2 -- admin
	BEGIN
		SELECT * FROM AdministrativeStaffView
		ORDER BY 
		CASE WHEN @SortByField = 'Staff ID' AND @SortOrder = 'ASC'
			THEN AdministrativeStaffView.[StaffID] END ASC,
		CASE WHEN @SortByField = 'Staff ID' AND @SortOrder = 'DESC'
			THEN AdministrativeStaffView.[StaffID] END DESC,
			CASE WHEN @SortByField = 'Name' AND @SortOrder = 'ASC'
			THEN AdministrativeStaffView.[Name] END ASC,
		CASE WHEN @SortByField = 'Name' AND @SortOrder = 'DESC'
			THEN AdministrativeStaffView.[Name] END DESC,
			CASE WHEN @SortByField = 'Position' AND @SortOrder = 'ASC'
			THEN AdministrativeStaffView.[Position] END ASC,
		CASE WHEN @SortByField = 'Position' AND @SortOrder = 'DESC'
			THEN AdministrativeStaffView.[Position] END DESC
		OFFSET @PageSize * (@PageNumber - 1) ROWS FETCH NEXT @PageSize ROWS ONLY;

		SELECT COUNT(*) AS totalItems
		FROM AdministrativeStaffView;
	END

	IF @StaffType = 3 -- support
	BEGIN
		SELECT * FROM SupportStaffView
		ORDER BY 
		CASE WHEN @SortByField = 'Staff ID' AND @SortOrder = 'ASC'
			THEN SupportStaffView.[StaffID] END ASC,
		CASE WHEN @SortByField = 'Staff ID' AND @SortOrder = 'DESC'
			THEN SupportStaffView.[StaffID] END DESC,
			CASE WHEN @SortByField = 'Name' AND @SortOrder = 'ASC'
			THEN SupportStaffView.[Name] END ASC,
		CASE WHEN @SortByField = 'Name' AND @SortOrder = 'DESC'
			THEN SupportStaffView.[Name] END DESC,
			CASE WHEN @SortByField = 'Role' AND @SortOrder = 'ASC'
			THEN SupportStaffView.[Role] END ASC,
		CASE WHEN @SortByField = 'Role' AND @SortOrder = 'DESC'
			THEN SupportStaffView.[Role] END DESC
		OFFSET @PageSize * (@PageNumber - 1) ROWS FETCH NEXT @PageSize ROWS ONLY;

		SELECT COUNT(*) AS totalItems
		FROM SupportStaffView;
	END

END