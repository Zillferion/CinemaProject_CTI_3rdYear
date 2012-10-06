CREATE PROCEDURE [dbo].[CreateUpdateDistributor]
	@id UNIQUEIDENTIFIER,
	@name NVARCHAR(255),
	@active BIT,
	@email NVARCHAR(255),
	@contactNumber NVARCHAR(50),
	@address NVARCHAR(255),
	@monthly BIT,
	@quaterly BIT,
	@halfYearly BIT,
	@yearly BIT

AS
	DECLARE @canContinue BIT = 1
	--Checks that the name is not already in use.
	IF EXISTS (SELECT * FROM tblDistributors WHERE Name = @name AND DistributorGuid <> @id)
	BEGIN
		RAISERROR ('Name not available.', 16, 16)
		SET @canContinue = 0
	END
	--Checks that the contact number is not already in use.
	IF EXISTS (SELECT * FROM tblDistributors WHERE ContactNumber = @contactNumber AND DistributorGuid <> @id)
	BEGIN
		RAISERROR ('Contact number not available.', 16, 16)
		SET @canContinue = 0
	END
	--Checks that the email address does not already exist.
	IF EXISTS (SELECT * FROM tblDistributors WHERE Email = @email AND DistributorGuid <> @id)
	BEGIN
		RAISERROR ('Email Address not available.', 16, 16)
		SET @canContinue = 0
	END

	IF @canContinue = 1
	BEGIN
		IF EXISTS (SELECT * FROM tblDistributors WHERE DistributorGuid = @id)
	BEGIN
		UPDATE tblDistributors
		SET Name = @name,
			IsActive = @active,
			Email = @email,
			ContactNumber = @contactNumber,
			PhysicalAddress = @address,
			ReceiveMontly = @monthly,
			ReceiveQuaterly = @quaterly,
			ReceiveHalfYearly = @halfYearly,
			ReceiveYearly = @yearly
		WHERE DistributorGuid = @id
	END
	ELSE
	BEGIN
		INSERT INTO tblDistributors (DistributorGuid, Name, IsActive, Email, ContactNumber, PhysicalAddress, ReceiveMontly, ReceiveQuaterly, ReceiveHalfYearly, ReceiveYearly)
		VALUES (@id, @name, @active, @email, @contactNumber, @address, @monthly, @quaterly, @halfYearly, @yearly)
	END
	END