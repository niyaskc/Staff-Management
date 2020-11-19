CREATE TABLE [dbo].[staff] (
    [StaffID]     INT        IDENTITY (1, 1) NOT NULL,
    [Name]        NCHAR (25) NOT NULL,
    [StaffTypeID] INT        NOT NULL,
    CONSTRAINT [PK_tbStaffs] PRIMARY KEY CLUSTERED ([StaffID] ASC),
    CONSTRAINT [FK_tbStaff_tbStaffType] FOREIGN KEY ([StaffTypeID]) REFERENCES [dbo].[staffType] ([StaffTypeID])
);

