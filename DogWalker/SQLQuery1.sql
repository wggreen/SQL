--CREATE TABLE Neighborhood (
--    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
--    NeighborhoodName VARCHAR(55) NOT NULL,
--);

--CREATE TABLE DogOwner (
--    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
--    DogOwnerName VARCHAR(55) NOT NULL,
--    DogOwnerAddress VARCHAR(55) NOT NULL,
--    NeighborhoodId INTEGER NOT NULL,
--    Phone VARCHAR(55) NOT NULL,
--    CONSTRAINT FK_DogOwner_Neighborhood FOREIGN KEY(NeighborhoodId) REFERENCES Neighborhood(Id)
--);

--CREATE TABLE Walker (
--    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
--    WalkerName VARCHAR (55) NOT NULL,
--    NeighborhoodId INTEGER NOT NULL,
--    CONSTRAINT FK_Walker_Neighborhood FOREIGN KEY(NeighborhoodId) REFERENCES Neighborhood(Id)
--);

--CREATE TABLE Dog (
--    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
--    DogName VARCHAR(55) NOT NULL,
--    DogOwnerId INTEGER NOT NULL,
--    Breed VARCHAR(55) NOT NULL,
--    Notes VARCHAR(255)
--    CONSTRAINT FK_Dog_DogOwner FOREIGN KEY(DogOwnerId) REFERENCES DogOwner(Id)
--);

--CREATE TABLE Walk (
--    Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
--    WalkDate DATETIME NOT NULL,
--    Duration INTEGER NOT NULL,
--    WalkerId INTEGER NOT NULL,
--    DogId INTEGER NOT NULL,
--    CONSTRAINT FK_Walk_Walker FOREIGN KEY(WalkerId) REFERENCES Walker(Id),
--    CONSTRAINT FK_Walk_Dog FOREIGN KEY(DogId) REFERENCES Dog(Id)
--);

--INSERT INTO Neighborhood (NeighborhoodName) VALUES ('NSS');
--INSERT INTO Neighborhood (NeighborhoodName) VALUES ('West Side');
--INSERT INTO Neighborhood (NeighborhoodName) VALUES ('East Side');

--INSERT INTO DogOwner (DogOwnerName, DogOwnerAddress, NeighborhoodId, Phone) VALUES ('Willy', '123 Main St.', 1, '615-555-5555');
--INSERT INTO DogOwner (DogOwnerName, DogOwnerAddress, NeighborhoodId, Phone) VALUES ('Namita', '456 Main St.', 2, '615-555-5555');
--INSERT INTO DogOwner (DogOwnerName, DogOwnerAddress, NeighborhoodId, Phone) VALUES ('Kevin', '789 Main St.', 3, '615-555-5555');
--INSERT INTO DogOwner (DogOwnerName, DogOwnerAddress, NeighborhoodId, Phone) VALUES ('James', '123 NSS St.', 1, '615-555-5555');
--INSERT INTO DogOwner (DogOwnerName, DogOwnerAddress, NeighborhoodId, Phone) VALUES ('Jansen', '456 Main St.', 2, '615-555-5555');

--INSERT INTO Dog (DogName, DogOwnerId, Breed, Notes) VALUES ('Dog 1', 1, 'Jack Russell Terrier', 'This dog sucks');
--INSERT INTO Dog (DogName, DogOwnerId, Breed, Notes) VALUES ('Dog 2', 2, 'Golden Retriever', 'This dog is cute');
--INSERT INTO Dog (DogName, DogOwnerId, Breed, Notes) VALUES ('Dog 3', 3, 'Pitbull', 'This dog will bite');
--INSERT INTO Dog (DogName, DogOwnerId, Breed, Notes) VALUES ('Dog 4', 4, 'St. Bernard', 'This dog saved me from an avalanche');
--INSERT INTO Dog (DogName, DogOwnerId, Breed, Notes) VALUES ('Clifford', 5, 'Big Red Dog', 'This dog destroyed my home');

--INSERT INTO Walker (WalkerName,NeighborhoodId) VALUES ('Adam', 1);
--INSERT INTO Walker (WalkerName,NeighborhoodId) VALUES ('Rose', 2);
--INSERT INTO Walker (WalkerName,NeighborhoodId) VALUES ('Steve', 3);

--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/19/2020', 30, 1, 11);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/19/2020', 30, 1, 12);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/19/2020', 30, 2, 13);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/19/2020', 30, 2, 14);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/19/2020', 30, 3, 15);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/19/2020', 30, 3, 11);

--INSERT INTO DogOwner (DogOwnerName, DogOwnerAddress, NeighborhoodId, Phone) VALUES ('Audrey', '987 Main St.', 1, '615-555-5555');
--INSERT INTO Dog (DogName, DogOwnerId, Breed, Notes) VALUES ('Clifford2', 6, 'Big Red Dog', 'This dog also destroyed my home');
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/19/2020', 30, 1, 18);

--INSERT INTO Dog (DogName, DogOwnerId, Breed, Notes) VALUES ('Clifford3', 6, 'Big Red Dog', 'This dog also destroyed my home');
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/19/2020', 30, 2, 19);

--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/01/2020', 30, 1, 11);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/02/2020', 30, 1, 12);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/03/2020', 30, 2, 13);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/04/2020', 30, 2, 14);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/05/2020', 30, 3, 15);
--Insert INTO Walk (WalkDate, Duration, WalkerId, DogId) VALUES ('03/06/2020', 30, 3, 11);

--UPDATE Walk
--SET WalkDate='02/01/2020'
--where Id=7

--UPDATE Walk
--SET WalkDate='02/01/2020'
--where DogId=11

--DELETE FROM Walk
--WHERE DogId=11