EXEC tSQLt.NewTestClass 'Private_SqlVariantFormatterTests';
GO

CREATE PROC Private_SqlVariantFormatterTests.[test formats INT]
AS
BEGIN
    DECLARE @Actual NVARCHAR(MAX);
    DECLARE @Parameter INT;SET @Parameter = 123;
    SET @Actual =  tSQLt.Private_SqlVariantFormatter(@Parameter);
    EXEC tSQLt.AssertEqualsString '123',@Actual;
END;
GO

CREATE PROC Private_SqlVariantFormatterTests.[test formats other data types]
AS
BEGIN
  CREATE TABLE #Input(
    [BIGINT] BIGINT,
    [BINARY] BINARY(5),
    [CHAR] CHAR(3),
    [DATETIME] DATETIME,
    [DECIMAL] DECIMAL(10,5),
    [FLOAT] FLOAT,
    [INT] INT,
    [MONEY] MONEY,
    [NCHAR] NCHAR(3),
    [NUMERIC] NUMERIC(10,5),
    [NVARCHAR] NVARCHAR(32),
    [REAL] REAL,
    [SMALLDATETIME] SMALLDATETIME,
    [SMALLINT] SMALLINT,
    [SMALLMONEY] SMALLMONEY,
    [TINYINT] TINYINT,
    [UNIQUEIDENTIFIER] UNIQUEIDENTIFIER,
    [VARBINARY] VARBINARY(32),
    [VARCHAR] VARCHAR(32)
  );
  INSERT INTO #Input
  SELECT
    12345 AS [BIGINT],
    0x1F2E3D AS [BINARY],
    'C' AS [CHAR],
    '2013-04-05T06:07:08.987' AS [DATETIME],
    12345.00200 AS [DECIMAL],
    '12345.6789' AS [FLOAT],
    123 AS [INT],
    12345.6789 AS [MONEY],
    N'N' AS [NCHAR],
    12345.00100 AS [NUMERIC],
    N'NVARCHAR' AS [NVARCHAR],
    12345.6789 AS [REAL],
    '2013-04-05T06:07:29' AS [SMALLDATETIME],
    12 AS [SMALLINT],
    12345.6789 AS [SMALLMONEY],
    2 AS [TINYINT],
    'B7F95DDE-1682-4BC6-8511-D6CD8EF947BC' AS [UNIQUEIDENTIFIER],
    0x1234567890ABCDEF AS [VARBINARY],
    'VARCHAR' AS [VARCHAR];

  CREATE TABLE #Actual(
    [BIGINT] NVARCHAR(MAX),
    [BINARY] NVARCHAR(MAX),
    [CHAR] NVARCHAR(MAX),
    [DATETIME] NVARCHAR(MAX),
    [DECIMAL] NVARCHAR(MAX),
    [FLOAT] NVARCHAR(MAX),
    [INT] NVARCHAR(MAX),
    [MONEY] NVARCHAR(MAX),
    [NCHAR] NVARCHAR(MAX),
    [NUMERIC] NVARCHAR(MAX),
    [NVARCHAR] NVARCHAR(MAX),
    [REAL] NVARCHAR(MAX),
    [SMALLDATETIME] NVARCHAR(MAX),
    [SMALLINT] NVARCHAR(MAX),
    [SMALLMONEY] NVARCHAR(MAX),
    [TINYINT] NVARCHAR(MAX),
    [UNIQUEIDENTIFIER] NVARCHAR(MAX),
    [VARBINARY] NVARCHAR(MAX),
    [VARCHAR] NVARCHAR(MAX)
  );
  INSERT INTO #Actual
  SELECT
    tSQLt.Private_SqlVariantFormatter([BIGINT]) AS [BIGINT],
    tSQLt.Private_SqlVariantFormatter([BINARY]) AS [BINARY],
    tSQLt.Private_SqlVariantFormatter([CHAR]) AS [CHAR],
    tSQLt.Private_SqlVariantFormatter([DATETIME]) AS [DATETIME],
    tSQLt.Private_SqlVariantFormatter([DECIMAL]) AS [DECIMAL],
    tSQLt.Private_SqlVariantFormatter([FLOAT]) AS [FLOAT],
    tSQLt.Private_SqlVariantFormatter([INT]) AS [INT],
    tSQLt.Private_SqlVariantFormatter([MONEY]) AS [MONEY],
    tSQLt.Private_SqlVariantFormatter([NCHAR]) AS [NCHAR],
    tSQLt.Private_SqlVariantFormatter([NUMERIC]) AS [NUMERIC],
    tSQLt.Private_SqlVariantFormatter([NVARCHAR]) AS [NVARCHAR],
    tSQLt.Private_SqlVariantFormatter([REAL]) AS [REAL],
    tSQLt.Private_SqlVariantFormatter([SMALLDATETIME]) AS [SMALLDATETIME],
    tSQLt.Private_SqlVariantFormatter([SMALLINT]) AS [SMALLINT],
    tSQLt.Private_SqlVariantFormatter([SMALLMONEY]) AS [SMALLMONEY],
    tSQLt.Private_SqlVariantFormatter([TINYINT]) AS [TINYINT],
    tSQLt.Private_SqlVariantFormatter([UNIQUEIDENTIFIER]) AS [UNIQUEIDENTIFIER],
    tSQLt.Private_SqlVariantFormatter([VARBINARY]) AS [VARBINARY],
    tSQLt.Private_SqlVariantFormatter([VARCHAR]) AS [VARCHAR]
  FROM #Input;

  SELECT TOP(0) * INTO #Expected FROM #Actual;
  INSERT INTO #Expected
  SELECT
    '12345' AS [BIGINT],
    '0x1F2E3D0000' AS [BINARY],
    'C' AS [CHAR],
    '2013-04-05T06:07:08.987' AS [DATETIME],
    '12345.00200' AS [DECIMAL],
    '1.234567890000000e+004' AS [FLOAT],
    '123' AS [INT],
    '12345.6789' AS [MONEY],
    'N' AS [NCHAR],
    '12345.00100' AS [NUMERIC],
    'NVARCHAR' AS [NVARCHAR],
    '1.2345679e+004' AS [REAL],
    '2013-04-05T06:07:00' AS [SMALLDATETIME],
    '12' AS [SMALLINT],
    '12345.6789' AS [SMALLMONEY],
    '2' AS [TINYINT],
    'B7F95DDE-1682-4BC6-8511-D6CD8EF947BC' AS [UNIQUEIDENTIFIER],
    '0x1234567890ABCDEF' AS [VARBINARY],
    'VARCHAR' AS [VARCHAR];

  EXEC tSQLt.AssertEqualsTable '#Expected', '#Actual';
END;
GO
