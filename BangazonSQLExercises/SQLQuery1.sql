--SELECT e.FirstName, e.LastName, e.IsSupervisor, d.[Name]
--FROM Employee e
--LEFT JOIN Department d on e.DepartmentId = d.Id
--ORDER BY d.[Name], e.LastName, e.FirstName

--SELECT [Name],Budget
--FROM Department
--ORDER BY Budget DESC

--SELECT d.[Name], e.FirstName, e.LastName
--FROM Department d
--LEFT JOIN Employee e on e.DepartmentId = d.Id
--WHERE e.IsSupervisor = 'True'

--SELECT d.[Name], COUNT(e.Id) AS 'Number of Employees'
--FROM Department d
--LEFT JOIN Employee e on e.DepartmentId = d.Id
--GROUP BY d.[Name]

--UPDATE Department
--SET Budget = Budget * 1.2

--SELECT e.FirstName, e.LastName, e.Id AS 'Employee Id', et.EmployeeId
--FROM Employee e
--LEFT JOIN EmployeeTraining et ON e.Id = et.EmployeeId
--WHERE et.EmployeeId IS NULL

--SELECT e.FirstName, e.LastName, COUNT(et.Id) AS 'Number of Training Programs'
--FROM Employee e
--LEFT JOIN EmployeeTraining et ON e.Id = et.EmployeeId
--WHERE et.EmployeeId IS NOT NULL
--GROUP BY e.FirstName, e.LastName

--SELECT t.[Name], COUNT(et.Id) AS 'Number of Participants'
--FROM TrainingProgram t
--LEFT JOIN EmployeeTraining et ON t.Id = et.TrainingProgramId
--GROUP BY t.[Name]

--SELECT t.[Name], COUNT(et.EmployeeId) AS 'Number of Participants'
--FROM TrainingProgram t
--LEFT JOIN EmployeeTraining et ON t.Id = et.TrainingProgramId
--GROUP BY t.[Name], t.MaxAttendees
--HAVING COUNT(et.EmployeeId) = t.MaxAttendees

--SELECT [Name], StartDate
--FROM TrainingProgram
--WHERE StartDate > GETDATE()

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId)
--VALUES (3, 1)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId)
--VALUES (4, 1)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId)
--VALUES (5, 1)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId)
--VALUES (40, 7)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId)
--VALUES (39, 4)

--INSERT INTO EmployeeTraining (EmployeeId, TrainingProgramId)
--VALUES (38, 5)

--SELECT TOP 3 t.[Name], t.Id AS 'Training Program Id', COUNT(et.TrainingProgramId) AS 'Number of Participants'
--FROM TrainingProgram t
--LEFT JOIN EmployeeTraining et ON et.TrainingProgramId = t.Id
--GROUP BY t.[Name], t.Id
--ORDER BY [Number of Participants] DESC

--SELECT TOP 3 t.[Name],  COUNT(et.TrainingProgramId) AS 'Number of Participants'
--FROM TrainingProgram t
--LEFT JOIN EmployeeTraining et ON et.TrainingProgramId = t.Id
--GROUP BY t.[Name]
--ORDER BY [Number of Participants] DESC

--SELECT e.FirstName, e.LastName
--FROM Employee e
--LEFT JOIN ComputerEmployee ce ON e.Id = ce.EmployeeId
--WHERE ce.EmployeeId IS NULL

--SELECT e.FirstName, e.LastName, COALESCE((c.Make + ' '+ c.Manufacturer), 'N/A') AS ComputerInfo
--FROM Employee e
--LEFT JOIN ComputerEmployee ce ON ce.EmployeeId = e.Id
--LEFT JOIN Computer c ON c.Id = ce.ComputerId
--WHERE ce.UnassignDate IS NULL

--SELECT c.Id, c.PurchaseDate
--FROM Computer c
--WHERE c.PurchaseDate < CONVERT(datetime, '2019-07-01') AND c.DecomissionDate IS NULL

--SELECT CONCAT(e.FirstName, ' ', e.LastName) AS FullName, COUNT(c.Id) AS 'Number of Computers'
--FROM Employee e
--LEFT JOIN ComputerEmployee ce ON ce.EmployeeId = e.Id
--LEFT JOIN Computer c ON c.Id = ce.ComputerId
--GROUP BY e.FirstName, e.LastName

--SELECT pt.[Name], COUNT(pt.CustomerId) AS 'Number of Users'
--FROM PaymentType pt
--GROUP BY pt.[Name]

--SELECT TOP 10 p.Title, p.Price, CONCAT(c.FirstName, ' ', c.LastName) AS 'Seller Name'
--FROM Product p
--LEFT JOIN Customer c ON p.CustomerId = c.Id
--ORDER BY p.Price DESC

--SELECT TOP 10 p.Title, CONCAT(c.FirstName, ' ', c.LastName) AS 'Seller Name'
--FROM Product p
--LEFT JOIN Customer c ON p.CustomerId = c.Id
--LEFT JOIN OrderProduct op ON p.Id = op.ProductId
--GROUP BY p.Title, c.FirstName, c.LastName
--ORDER BY COUNT(op.ProductId) DESC

--SELECT TOP 1 CONCAT(c.FirstName, ' ', c.LastName) AS 'Customer Name'
--FROM Customer c
--LEFT JOIN [Order] o ON o.customerId = c.Id
--GROUP BY c.FirstName, c.LastName
--ORDER BY COUNT(o.CustomerId) DESC

--SELECT pt.[Name], COUNT(p.ProductTypeId) AS 'Total Sales'
--FROM ProductType pt
--LEFT JOIN Product P ON pt.Id = p.ProductTypeId
--LEFT JOIN OrderProduct op ON op.ProductId = p.Id
--GROUP BY pt.[Name]

--SELECT c.Id, CONCAT(c.FirstName, ' ', c.LastName) AS 'Seller Name', COALESCE(SUM(p.Price), 0) AS 'Total Sales'
--FROM Customer c
--LEFT JOIN Product p ON p.CustomerId = c.Id
--LEFT JOIN OrderProduct op ON op.ProductId = p.Id
--GROUP BY c.Id, c.FirstName, c.LastName
--ORDER BY SUM(p.Price) DESC