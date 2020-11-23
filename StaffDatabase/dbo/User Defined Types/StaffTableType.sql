CREATE TYPE [dbo].[StaffTableType] AS TABLE (
    [StaffID]     INT        NOT NULL,
    [Name]        NCHAR (25) NOT NULL,
    [StaffTypeID] INT        NOT NULL,
    [SubjectName] NCHAR (25) NULL,
    [Position]    NCHAR (25) NULL,
    [Role]        NCHAR (25) NULL);

