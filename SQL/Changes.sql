USE [DKSH_TPI]
GO

/****** Object:  Table [dbo].[tbl_DKSH_TPI_EXTQNS_REQUEST_FILES]    Script Date: 21/7/2020 2:00:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_DKSH_TPI_EXTQNS_REQUEST_FILES](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[QId] [bigint] NOT NULL,
	[Data] [varbinary](max) NULL,
	[FileName] [nvarchar](50) NULL,
	[Field] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
	[ModyfBy] [nvarchar](50) NOT NULL,
	[RecordStatus] [nchar](10) NOT NULL,
 CONSTRAINT [PK_tbl_DKSH_TPI_EXTQNS_REQUEST_FILES] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbl_DKSH_TPI_EXTQNS_REQUEST_FILES] ADD  CONSTRAINT [DF_tbl_DKSH_TPI_EXTQNS_REQUEST_FILES_RecordStatus]  DEFAULT ('A') FOR [RecordStatus]
GO

ALTER TABLE [dbo].[tbl_DKSH_TPI_EXTQNS_REQUEST_FILES]  WITH CHECK ADD  CONSTRAINT [FK_tbl_DKSH_TPI_EXTQNS_REQUEST_FILES_tbl_DKSH_TPI_EXTQNS_REQUEST] FOREIGN KEY([QId])
REFERENCES [dbo].[tbl_DKSH_TPI_EXTQNS_REQUEST] ([id])
GO

ALTER TABLE [dbo].[tbl_DKSH_TPI_EXTQNS_REQUEST_FILES] CHECK CONSTRAINT [FK_tbl_DKSH_TPI_EXTQNS_REQUEST_FILES_tbl_DKSH_TPI_EXTQNS_REQUEST]
GO




alter table tbl_dksh_tpi_extqns_request
add basic_info_bu_others nvarchar(250) default null
GO

alter table tbl_dksh_tpi_extqns_request
add created_by nvarchar(250) default 'system'
GO

alter table tbl_dksh_tpi_extqns_request
add created_date datetime default '01/01/2020'
GO

alter table tbl_dksh_tpi_extqns_request
add updated_by nvarchar(250) default '-'
GO

alter table tbl_dksh_tpi_extqns_request
add updated_date nvarchar(250) default '01/01/2020'
GO

alter table tbl_dksh_tpi_extqns_request
add status nvarchar(10) default 'A'
GO

alter table tbl_dksh_tpi_extqns_request
add contact_person_name nvarchar(250) default null
GO

alter table tbl_dksh_tpi_extqns_request
add relationships_public_officials_3 nvarchar(50) default nul
GO

INSERT INTO [dbo].[tbl_DKSH_TPI_KEYWORDS]
           ([country_code]
           ,[key_words]
           ,[key_values]
           ,[descriptions])
     VALUES
           ('VN'
           ,'TypeOfThirdParty'
           ,'Sales Agent (Vendor); Consultant (Vendor);'
           ,'')
GO

ALTER TABLE tbl_DKSH_TPI_EXTQNS_REQUEST
ALTER COLUMN contact_person_telephone_no nvarchar(50);
GO

alter table tbl_dksh_tpi_extqns_request
add relationships_public_officials_4 nvarchar(max)
GO


USE [DKSH_TPI]
GO

/****** Object:  Table [dbo].[tbl_DKSH_TPI_AUDIT_TRAIL]    Script Date: 28/7/2020 3:05:22 PM ******/
DROP TABLE [dbo].[tbl_DKSH_TPI_AUDIT_TRAIL]
GO

/****** Object:  Table [dbo].[tbl_DKSH_TPI_AUDIT_TRAIL]    Script Date: 28/7/2020 3:05:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_DKSH_TPI_AUDIT_TRAIL](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[RequestID] [bigint] NULL,
	[Action] [nvarchar](250) NULL,
	[Comments] [nvarchar](max) NULL,
	[ActionedDate] [datetime] NULL,
	[ActionedBy] [nvarchar](250) NULL,
	[ActionedByName] [nvarchar](250) NULL,
	[Attachment] [nvarchar](max) NULL,
 CONSTRAINT [PK_tbl_DKSH_TPI_AUDIT_TRAIL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

