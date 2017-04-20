----------------------------------
-- COMPANY - triElems           --
-- PROJECT NAME - GenericAPI    --
-- CHANNEL - FREELANCER.COM     --
-- CLIENT USER NAME - MALAGOPU  --
----------------------------------

CREATE TABLE TBL_LOAN_DETAILS (
	SEC_ID int,
	LOAN_QTY int
);

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SPSelectLoanDetail 
	@SecurityId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		SEC_ID AS SecurityId,
		LOAN_Qty AS LoanQty
	FROM TBL_LOAN_DETAILS
	WHERE SEC_ID = @SecurityId;
END
GO

--DATA---
INSERT INTO TBL_LOAN_DETAILS (SEC_ID,LOAN_QTY) VALUES (1,1000);
INSERT INTO TBL_LOAN_DETAILS (SEC_ID,LOAN_QTY) VALUES (2,1500);
INSERT INTO TBL_LOAN_DETAILS (SEC_ID,LOAN_QTY) VALUES (3,750);
INSERT INTO TBL_LOAN_DETAILS (SEC_ID,LOAN_QTY) VALUES (3,700);
INSERT INTO TBL_LOAN_DETAILS (SEC_ID,LOAN_QTY) VALUES (3,980);

