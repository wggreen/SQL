SELECt o.DogOwnerName, n.NeighborhoodName FROM DogOwner o
LEFT JOIN Neighborhood n ON o.NeighborhoodId = n.Id

SELECT o.DogOwnerName, n.NeighborhoodName FROM DogOwner o
LEFT JOIN Neighborhood n ON o.NeighborhoodId = n.Id
WHERE o.Id = 1

SELECT WalkerName FROM Walker
ORDER BY WalkerName

SELECT DISTINCT Breed FROM Dog

SELECT d.DogName, o.DogOwnerName, n.NeighborhoodName from Dog d
LEFT JOIN DogOwner o ON d.DogOwnerId = o.Id
LEFT JOIN Neighborhood n ON o.NeighborhoodId = n.Id

SELECT o.DogOwnerName, COUNT(d.Id) from DogOwner o
LEFT JOIN Dog d ON d.DogOwnerId = o.Id
GROUP BY o.DogOwnerName

SELECT walker.WalkerName, COUNT(walk.Id) as number_of_walks from Walker walker
LEFT JOIN Walk walk ON walk.WalkerId = walker.Id
GROUP BY walker.WalkerName

SELECT n.NeighborhoodName, COUNT(w.WalkerName) as NumberOfWalkers from Neighborhood n
INNER JOIN Walker w ON w.NeighborhoodId = n.Id
GROUP BY n.NeighborhoodName

SELECT * FROM Walk
WHERE WalkDate>='03/12/2020'

SELECT d.DogName FROM Dog d
LEFT JOIN Walk w ON w.DogId = d.Id
WHERE w.WalkDate>='03/12/2020'

SELECT d.DogName FROM Dog d
LEFT JOIN Walk w ON w.DogId = d.Id
GROUP BY d.DogName
HAVING COUNT(w.Id) = 0