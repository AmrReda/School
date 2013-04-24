
USE [School]
GO
SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Classes_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Classes_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Classes_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Classes table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Classes_Get_List

AS


				
				SELECT
					[Id],
					[ClassNumber]
				FROM
					[dbo].[Classes]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Classes_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Classes_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Classes_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Classes table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Classes_GetPaged
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[Classes]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[Id], O.[ClassNumber]
				FROM
				    [dbo].[Classes] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Classes]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Classes_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Classes_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Classes_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Classes table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Classes_Insert
(

	@Id int    OUTPUT,

	@ClassNumber nvarchar (50)  
)
AS


				
				INSERT INTO [dbo].[Classes]
					(
					[ClassNumber]
					)
				VALUES
					(
					@ClassNumber
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Classes_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Classes_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Classes_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Classes table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Classes_Update
(

	@Id int   ,

	@ClassNumber nvarchar (50)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Classes]
				SET
					[ClassNumber] = @ClassNumber
				WHERE
[Id] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Classes_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Classes_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Classes_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Classes table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Classes_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[Classes] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Classes_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Classes_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Classes_GetById
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Classes table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Classes_GetById
(

	@Id int   
)
AS


				SELECT
					[Id],
					[ClassNumber]
				FROM
					[dbo].[Classes]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Classes_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Classes_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Classes_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Classes table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Classes_Find
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@ClassNumber nvarchar (50)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [ClassNumber]
    FROM
	[dbo].[Classes]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([ClassNumber] = @ClassNumber OR @ClassNumber IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [ClassNumber]
    FROM
	[dbo].[Classes]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([ClassNumber] = @ClassNumber AND @ClassNumber is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Students_Get_List procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Students_Get_List') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Students_Get_List
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets all records from the Students table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Students_Get_List

AS


				
				SELECT
					[Id],
					[Name],
					[Address],
					[ClassId],
					[Birthdate],
					[Gender]
				FROM
					[dbo].[Students]
					
				SELECT @@ROWCOUNT
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Students_GetPaged procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Students_GetPaged') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Students_GetPaged
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Gets records from the Students table passing page index and page count parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Students_GetPaged
(

	@WhereClause varchar (2000)  ,

	@OrderBy varchar (2000)  ,

	@PageIndex int   ,

	@PageSize int   
)
AS


				
				BEGIN
				DECLARE @PageLowerBound int
				DECLARE @PageUpperBound int
				
				-- Set the page bounds
				SET @PageLowerBound = @PageSize * @PageIndex
				SET @PageUpperBound = @PageLowerBound + @PageSize

				-- Create a temp table to store the select results
				CREATE TABLE #PageIndex
				(
				    [IndexId] int IDENTITY (1, 1) NOT NULL,
				    [Id] int 
				)
				
				-- Insert into the temp table
				DECLARE @SQL AS nvarchar(4000)
				SET @SQL = 'INSERT INTO #PageIndex ([Id])'
				SET @SQL = @SQL + ' SELECT'
				SET @SQL = @SQL + ' [Id]'
				SET @SQL = @SQL + ' FROM [dbo].[Students]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				IF LEN(@OrderBy) > 0
				BEGIN
					SET @SQL = @SQL + ' ORDER BY ' + @OrderBy
				END
				
				-- Only get the number of rows needed here.
				SET ROWCOUNT @PageUpperBound
				
				-- Populate the temp table
				EXEC sp_executesql @SQL

				-- Reset Rowcount back to all
				SET ROWCOUNT 0
				
				-- Return paged results
				SELECT O.[Id], O.[Name], O.[Address], O.[ClassId], O.[Birthdate], O.[Gender]
				FROM
				    [dbo].[Students] O,
				    #PageIndex PageIndex
				WHERE
				    PageIndex.IndexId > @PageLowerBound
					AND O.[Id] = PageIndex.[Id]
				ORDER BY
				    PageIndex.IndexId
				
				-- get row count
				SET @SQL = 'SELECT COUNT(*) AS TotalRowCount'
				SET @SQL = @SQL + ' FROM [dbo].[Students]'
				IF LEN(@WhereClause) > 0
				BEGIN
					SET @SQL = @SQL + ' WHERE ' + @WhereClause
				END
				EXEC sp_executesql @SQL
			
				END
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Students_Insert procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Students_Insert') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Students_Insert
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Inserts a record into the Students table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Students_Insert
(

	@Id int    OUTPUT,

	@Name nvarchar (MAX)  ,

	@Address nvarchar (MAX)  ,

	@ClassId int   ,

	@Birthdate date   ,

	@Gender nvarchar (10)  
)
AS


				
				INSERT INTO [dbo].[Students]
					(
					[Name]
					,[Address]
					,[ClassId]
					,[Birthdate]
					,[Gender]
					)
				VALUES
					(
					@Name
					,@Address
					,@ClassId
					,@Birthdate
					,@Gender
					)
				
				-- Get the identity value
				SET @Id = SCOPE_IDENTITY()
									
							
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Students_Update procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Students_Update') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Students_Update
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Updates a record in the Students table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Students_Update
(

	@Id int   ,

	@Name nvarchar (MAX)  ,

	@Address nvarchar (MAX)  ,

	@ClassId int   ,

	@Birthdate date   ,

	@Gender nvarchar (10)  
)
AS


				
				
				-- Modify the updatable columns
				UPDATE
					[dbo].[Students]
				SET
					[Name] = @Name
					,[Address] = @Address
					,[ClassId] = @ClassId
					,[Birthdate] = @Birthdate
					,[Gender] = @Gender
				WHERE
[Id] = @Id 
				
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Students_Delete procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Students_Delete') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Students_Delete
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Deletes a record in the Students table
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Students_Delete
(

	@Id int   
)
AS


				DELETE FROM [dbo].[Students] WITH (ROWLOCK) 
				WHERE
					[Id] = @Id
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Students_GetByClassId procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Students_GetByClassId') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Students_GetByClassId
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Students table through a foreign key
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Students_GetByClassId
(

	@ClassId int   
)
AS


				SET ANSI_NULLS OFF
				
				SELECT
					[Id],
					[Name],
					[Address],
					[ClassId],
					[Birthdate],
					[Gender]
				FROM
					[dbo].[Students]
				WHERE
					[ClassId] = @ClassId
				
				SELECT @@ROWCOUNT
				SET ANSI_NULLS ON
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Students_GetById procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Students_GetById') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Students_GetById
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Select records from the Students table through an index
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Students_GetById
(

	@Id int   
)
AS


				SELECT
					[Id],
					[Name],
					[Address],
					[ClassId],
					[Birthdate],
					[Gender]
				FROM
					[dbo].[Students]
				WHERE
					[Id] = @Id
				SELECT @@ROWCOUNT
					
			

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

	

-- Drop the dbo.Students_Find procedure
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'dbo.Students_Find') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
DROP PROCEDURE dbo.Students_Find
GO

/*
----------------------------------------------------------------------------------------------------

-- Created By:  ()
-- Purpose: Finds records in the Students table passing nullable parameters
----------------------------------------------------------------------------------------------------
*/


CREATE PROCEDURE dbo.Students_Find
(

	@SearchUsingOR bit   = null ,

	@Id int   = null ,

	@Name nvarchar (MAX)  = null ,

	@Address nvarchar (MAX)  = null ,

	@ClassId int   = null ,

	@Birthdate date   = null ,

	@Gender nvarchar (10)  = null 
)
AS


				
  IF ISNULL(@SearchUsingOR, 0) <> 1
  BEGIN
    SELECT
	  [Id]
	, [Name]
	, [Address]
	, [ClassId]
	, [Birthdate]
	, [Gender]
    FROM
	[dbo].[Students]
    WHERE 
	 ([Id] = @Id OR @Id IS NULL)
	AND ([Name] = @Name OR @Name IS NULL)
	AND ([Address] = @Address OR @Address IS NULL)
	AND ([ClassId] = @ClassId OR @ClassId IS NULL)
	AND ([Birthdate] = @Birthdate OR @Birthdate IS NULL)
	AND ([Gender] = @Gender OR @Gender IS NULL)
						
  END
  ELSE
  BEGIN
    SELECT
	  [Id]
	, [Name]
	, [Address]
	, [ClassId]
	, [Birthdate]
	, [Gender]
    FROM
	[dbo].[Students]
    WHERE 
	 ([Id] = @Id AND @Id is not null)
	OR ([Name] = @Name AND @Name is not null)
	OR ([Address] = @Address AND @Address is not null)
	OR ([ClassId] = @ClassId AND @ClassId is not null)
	OR ([Birthdate] = @Birthdate AND @Birthdate is not null)
	OR ([Gender] = @Gender AND @Gender is not null)
	SELECT @@ROWCOUNT			
  END
				

GO
SET QUOTED_IDENTIFIER ON 
GO
SET NOCOUNT ON
GO
SET ANSI_NULLS OFF 
GO

