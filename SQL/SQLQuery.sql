USE ReverseAnalytics;

SELECT * FROM Clients;
SELECT * FROM Payments;
SELECT * FROM Products;
SELECT * FROM Suppliers;
SELECT * FROM Supplies;
SELECT * FROM Payments;
SELECT * FROM Sales;

DECLARE @counter INT;

SET @counter = 0;

WHILE @counter < 10000
	BEGIN
		PRINT(@counter);
		set @counter += 1;
		INSERT INTO Products VALUES ((CONVERT(varchar(255), NEWID())), @counter * 15, @counter * 50, RAND(), RAND());
END;