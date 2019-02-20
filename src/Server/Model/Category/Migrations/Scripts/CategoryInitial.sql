begin tran
IF  NOT EXISTS (SELECT * FROM sys.objects 
	WHERE object_id = OBJECT_ID(N'[dbo].[Categories]') AND type in (N'U'))
	begin
CREATE TABLE [dbo].[Categories] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](255) NOT NULL,
    [IsApproved] [bit] NOT NULL,
    [Code] [nvarchar](128) NOT NULL,
    [IsActive] [bit] NOT NULL,
    [SortOrder] [int] NOT NULL,
    [DateUpdated] [datetime] NOT NULL,
    [DateCreated] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Categories] PRIMARY KEY ([Id])
)
CREATE INDEX [ix_Categories_Name] ON [dbo].[Categories]([Name], [IsApproved])
CREATE INDEX [ix_Categories_IsApproved] ON [dbo].[Categories]([IsApproved])
end
commit tran