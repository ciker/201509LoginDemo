
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/22/2015 13:59:01
-- Generated from EDMX file: E:\学习\SVN\Blogs\Blogs\Blogs.Model\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [qds164306297_db];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BlogsBlogComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogCommentSet] DROP CONSTRAINT [FK_BlogsBlogComment];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogUsersBlogComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogCommentSet] DROP CONSTRAINT [FK_BlogUsersBlogComment];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersBlogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Blogs] DROP CONSTRAINT [FK_UsersBlogs];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersBlogTags]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogTags] DROP CONSTRAINT [FK_UsersBlogTags];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersBlogTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogTypes] DROP CONSTRAINT [FK_UsersBlogTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogsBlogTag_Blogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogsBlogTag] DROP CONSTRAINT [FK_BlogsBlogTag_Blogs];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogsBlogTag_BlogTags]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogsBlogTag] DROP CONSTRAINT [FK_BlogsBlogTag_BlogTags];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogsBlogType_Blogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogsBlogType] DROP CONSTRAINT [FK_BlogsBlogType_Blogs];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogsBlogType_BlogTypes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogsBlogType] DROP CONSTRAINT [FK_BlogsBlogType_BlogTypes];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogUsersSetUserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoSet] DROP CONSTRAINT [FK_BlogUsersSetUserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_BlogsBlogReadInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BlogReadInfoSet] DROP CONSTRAINT [FK_BlogsBlogReadInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BlogCommentSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogCommentSet];
GO
IF OBJECT_ID(N'[dbo].[Blogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Blogs];
GO
IF OBJECT_ID(N'[dbo].[BlogTags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogTags];
GO
IF OBJECT_ID(N'[dbo].[BlogTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogTypes];
GO
IF OBJECT_ID(N'[dbo].[BlogUsersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogUsersSet];
GO
IF OBJECT_ID(N'[dbo].[UserInfoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoSet];
GO
IF OBJECT_ID(N'[dbo].[BlogReadInfoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogReadInfoSet];
GO
IF OBJECT_ID(N'[dbo].[BlogsBlogTag]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogsBlogTag];
GO
IF OBJECT_ID(N'[dbo].[BlogsBlogType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BlogsBlogType];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BlogCommentSet'
CREATE TABLE [dbo].[BlogCommentSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreateTime] datetime  NULL,
    [UpTime] datetime  NULL,
    [IsDel] bit  NOT NULL,
    [Content] nvarchar(max)  NULL,
    [CommentSort] nvarchar(max)  NULL,
    [CommentID] int  NOT NULL,
    [ContentLevy] nvarchar(max)  NULL,
    [BlogUsersId] int  NOT NULL,
    [BlogsId] int  NOT NULL,
    [IsInitial] bit  NULL,
    [ReplyUserID] int  NULL,
    [ReplyUserName] nvarchar(max)  NULL
);
GO

-- Creating table 'Blogs'
CREATE TABLE [dbo].[Blogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreateTime] datetime  NULL,
    [UpTime] datetime  NULL,
    [IsDel] bit  NOT NULL,
    [BlogContent] nvarchar(max)  NULL,
    [BlogRemarks] nvarchar(max)  NULL,
    [BlogTitle] nvarchar(max)  NULL,
    [BlogUrl] nvarchar(max)  NULL,
    [IsForwarding] bit  NULL,
    [BlogCreateTime] datetime  NULL,
    [BlogUpTime] datetime  NULL,
    [UsersId] int  NOT NULL,
    [BlogReadNum] int  NULL,
    [BlogForUrl] nvarchar(max)  NULL,
    [IsShowHome] bit  NULL,
    [IsShowMyHome] bit  NULL,
    [BlogCommentNum] int  NULL
);
GO

-- Creating table 'BlogTags'
CREATE TABLE [dbo].[BlogTags] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TagName] nvarchar(max)  NULL,
    [TagRemarks] nvarchar(max)  NULL,
    [CreateTime] datetime  NULL,
    [UpTime] datetime  NULL,
    [IsDel] bit  NOT NULL,
    [UsersId] int  NOT NULL
);
GO

-- Creating table 'BlogTypes'
CREATE TABLE [dbo].[BlogTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(max)  NULL,
    [TypeRemarks] nvarchar(max)  NULL,
    [CreateTime] datetime  NULL,
    [UpTime] datetime  NULL,
    [IsDel] bit  NOT NULL,
    [UsersId] int  NOT NULL
);
GO

-- Creating table 'BlogUsersSet'
CREATE TABLE [dbo].[BlogUsersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreateTime] datetime  NULL,
    [UpTime] datetime  NULL,
    [IsDel] bit  NOT NULL,
    [UserName] nvarchar(100)  NOT NULL,
    [UserPass] nvarchar(100)  NOT NULL,
    [UserNickname] nvarchar(100)  NULL,
    [UserMail] nvarchar(100)  NULL,
    [IsLock] bit  NOT NULL,
    [UserImage] nvarchar(100)  NULL
);
GO

-- Creating table 'UserInfoSet'
CREATE TABLE [dbo].[UserInfoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Age] nvarchar(max)  NULL,
    [Sex] nvarchar(max)  NULL,
    [Hobby] nvarchar(max)  NULL,
    [Profile] nvarchar(max)  NULL,
    [BlogLastUpTime] datetime  NULL,
    [BlogUpNum] int  NOT NULL,
    [IsDel] bit  NOT NULL,
    [CreateTime] datetime  NULL,
    [UpTime] datetime  NULL,
    [IsDisCSS] bit  NULL,
    [IsShowCSS] bit  NULL,
    [IsShowHTML] bit  NULL,
    [IsShowJS] bit  NULL,
    [IsShowMCSS] bit  NULL,
    [IsDisMCSS] bit  NULL,
    [BlogTheme] nvarchar(max)  NULL,
    [BlogUsersSet_Id] int  NOT NULL
);
GO

-- Creating table 'BlogReadInfoSet'
CREATE TABLE [dbo].[BlogReadInfoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreateTime] datetime  NULL,
    [UpTime] datetime  NULL,
    [IsDel] bit  NOT NULL,
    [MD5] nvarchar(max)  NOT NULL,
    [LastTime] datetime  NOT NULL,
    [BlogsId] int  NOT NULL
);
GO

-- Creating table 'BlogsBlogTag'
CREATE TABLE [dbo].[BlogsBlogTag] (
    [Blogs_Id] int  NOT NULL,
    [BlogTags_Id] int  NOT NULL
);
GO

-- Creating table 'BlogsBlogType'
CREATE TABLE [dbo].[BlogsBlogType] (
    [Blogs_Id] int  NOT NULL,
    [BlogTypes_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BlogCommentSet'
ALTER TABLE [dbo].[BlogCommentSet]
ADD CONSTRAINT [PK_BlogCommentSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Blogs'
ALTER TABLE [dbo].[Blogs]
ADD CONSTRAINT [PK_Blogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BlogTags'
ALTER TABLE [dbo].[BlogTags]
ADD CONSTRAINT [PK_BlogTags]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BlogTypes'
ALTER TABLE [dbo].[BlogTypes]
ADD CONSTRAINT [PK_BlogTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BlogUsersSet'
ALTER TABLE [dbo].[BlogUsersSet]
ADD CONSTRAINT [PK_BlogUsersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserInfoSet'
ALTER TABLE [dbo].[UserInfoSet]
ADD CONSTRAINT [PK_UserInfoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BlogReadInfoSet'
ALTER TABLE [dbo].[BlogReadInfoSet]
ADD CONSTRAINT [PK_BlogReadInfoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Blogs_Id], [BlogTags_Id] in table 'BlogsBlogTag'
ALTER TABLE [dbo].[BlogsBlogTag]
ADD CONSTRAINT [PK_BlogsBlogTag]
    PRIMARY KEY CLUSTERED ([Blogs_Id], [BlogTags_Id] ASC);
GO

-- Creating primary key on [Blogs_Id], [BlogTypes_Id] in table 'BlogsBlogType'
ALTER TABLE [dbo].[BlogsBlogType]
ADD CONSTRAINT [PK_BlogsBlogType]
    PRIMARY KEY CLUSTERED ([Blogs_Id], [BlogTypes_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BlogsId] in table 'BlogCommentSet'
ALTER TABLE [dbo].[BlogCommentSet]
ADD CONSTRAINT [FK_BlogsBlogComment]
    FOREIGN KEY ([BlogsId])
    REFERENCES [dbo].[Blogs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogsBlogComment'
CREATE INDEX [IX_FK_BlogsBlogComment]
ON [dbo].[BlogCommentSet]
    ([BlogsId]);
GO

-- Creating foreign key on [BlogUsersId] in table 'BlogCommentSet'
ALTER TABLE [dbo].[BlogCommentSet]
ADD CONSTRAINT [FK_BlogUsersBlogComment]
    FOREIGN KEY ([BlogUsersId])
    REFERENCES [dbo].[BlogUsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogUsersBlogComment'
CREATE INDEX [IX_FK_BlogUsersBlogComment]
ON [dbo].[BlogCommentSet]
    ([BlogUsersId]);
GO

-- Creating foreign key on [UsersId] in table 'Blogs'
ALTER TABLE [dbo].[Blogs]
ADD CONSTRAINT [FK_UsersBlogs]
    FOREIGN KEY ([UsersId])
    REFERENCES [dbo].[BlogUsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersBlogs'
CREATE INDEX [IX_FK_UsersBlogs]
ON [dbo].[Blogs]
    ([UsersId]);
GO

-- Creating foreign key on [UsersId] in table 'BlogTags'
ALTER TABLE [dbo].[BlogTags]
ADD CONSTRAINT [FK_UsersBlogTags]
    FOREIGN KEY ([UsersId])
    REFERENCES [dbo].[BlogUsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersBlogTags'
CREATE INDEX [IX_FK_UsersBlogTags]
ON [dbo].[BlogTags]
    ([UsersId]);
GO

-- Creating foreign key on [UsersId] in table 'BlogTypes'
ALTER TABLE [dbo].[BlogTypes]
ADD CONSTRAINT [FK_UsersBlogTypes]
    FOREIGN KEY ([UsersId])
    REFERENCES [dbo].[BlogUsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersBlogTypes'
CREATE INDEX [IX_FK_UsersBlogTypes]
ON [dbo].[BlogTypes]
    ([UsersId]);
GO

-- Creating foreign key on [Blogs_Id] in table 'BlogsBlogTag'
ALTER TABLE [dbo].[BlogsBlogTag]
ADD CONSTRAINT [FK_BlogsBlogTag_Blogs]
    FOREIGN KEY ([Blogs_Id])
    REFERENCES [dbo].[Blogs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BlogTags_Id] in table 'BlogsBlogTag'
ALTER TABLE [dbo].[BlogsBlogTag]
ADD CONSTRAINT [FK_BlogsBlogTag_BlogTags]
    FOREIGN KEY ([BlogTags_Id])
    REFERENCES [dbo].[BlogTags]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogsBlogTag_BlogTags'
CREATE INDEX [IX_FK_BlogsBlogTag_BlogTags]
ON [dbo].[BlogsBlogTag]
    ([BlogTags_Id]);
GO

-- Creating foreign key on [Blogs_Id] in table 'BlogsBlogType'
ALTER TABLE [dbo].[BlogsBlogType]
ADD CONSTRAINT [FK_BlogsBlogType_Blogs]
    FOREIGN KEY ([Blogs_Id])
    REFERENCES [dbo].[Blogs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [BlogTypes_Id] in table 'BlogsBlogType'
ALTER TABLE [dbo].[BlogsBlogType]
ADD CONSTRAINT [FK_BlogsBlogType_BlogTypes]
    FOREIGN KEY ([BlogTypes_Id])
    REFERENCES [dbo].[BlogTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogsBlogType_BlogTypes'
CREATE INDEX [IX_FK_BlogsBlogType_BlogTypes]
ON [dbo].[BlogsBlogType]
    ([BlogTypes_Id]);
GO

-- Creating foreign key on [BlogUsersSet_Id] in table 'UserInfoSet'
ALTER TABLE [dbo].[UserInfoSet]
ADD CONSTRAINT [FK_BlogUsersSetUserInfo]
    FOREIGN KEY ([BlogUsersSet_Id])
    REFERENCES [dbo].[BlogUsersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogUsersSetUserInfo'
CREATE INDEX [IX_FK_BlogUsersSetUserInfo]
ON [dbo].[UserInfoSet]
    ([BlogUsersSet_Id]);
GO

-- Creating foreign key on [BlogsId] in table 'BlogReadInfoSet'
ALTER TABLE [dbo].[BlogReadInfoSet]
ADD CONSTRAINT [FK_BlogsBlogReadInfo]
    FOREIGN KEY ([BlogsId])
    REFERENCES [dbo].[Blogs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BlogsBlogReadInfo'
CREATE INDEX [IX_FK_BlogsBlogReadInfo]
ON [dbo].[BlogReadInfoSet]
    ([BlogsId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------