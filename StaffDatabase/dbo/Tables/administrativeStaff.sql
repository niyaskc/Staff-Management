CREATE TABLE [dbo].[administrativeStaff] (
    [PID]      INT        IDENTITY (1, 1) NOT NULL,
    [StaffID]  INT        NOT NULL,
    [Position] NCHAR (25) NOT NULL,
    CONSTRAINT [PK_tbAdministrativeStaff] PRIMARY KEY CLUSTERED ([PID] ASC),
    CONSTRAINT [FK_tbAdministrativeStaff_tbStaff] FOREIGN KEY ([StaffID]) REFERENCES [dbo].[staff] ([StaffID])
);

