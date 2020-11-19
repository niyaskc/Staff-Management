CREATE TABLE [dbo].[supportStaff] (
    [PID]     INT        IDENTITY (1, 1) NOT NULL,
    [StaffID] INT        NOT NULL,
    [Role]    NCHAR (25) NOT NULL,
    CONSTRAINT [PK_tbSupportStaff] PRIMARY KEY CLUSTERED ([PID] ASC),
    CONSTRAINT [FK_tbSupportStaff_tbStaff] FOREIGN KEY ([StaffID]) REFERENCES [dbo].[staff] ([StaffID])
);

