CREATE PROCEDURE [dbo].[GetDistributorList]
AS
	SELECT *
	FROM tblDistributors
	ORDER BY Name