CREATE TABLE [dbo].[teachingStaff] (
    [PID]         INT        IDENTITY (1, 1) NOT NULL,
    [StaffID]     INT        NOT NULL,
    [SubjectName] NCHAR (25) NOT NULL,
    CONSTRAINT [PK_tbTeachingStaff] PRIMARY KEY CLUSTERED ([PID] ASC),
    CONSTRAINT [FK_tbTeachingStaff_tbStaff] FOREIGN KEY ([StaffID]) REFERENCES [dbo].[staff] ([StaffID])
);

