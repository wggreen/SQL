--SELECT * FROM Genre

INSERT INTO Artist (ArtistName, YearEstablished) VALUES ('Cohort37', '2020');

SELECT * From Album

INSERT INTO Album (AlbumLength, ArtistId, Label, ReleaseDate, Title) VALUES (2020, 29, 'NSS', '11/01/2019', 'AlbumFunTime')

INSERT INTO Song (Title, SongLength, ReleaseDate, GenreId, ArtistId, AlbumId) VALUES ('SQL', 10000, '03/18/2020', 9, 29, 24) 

INSERT INTO Song (Title, SongLength, ReleaseDate, GenreId, ArtistId, AlbumId) VALUES ('SQL2', 10000, '03/18/2020', 9, 29, 24) 


SELECT * From Song

SELECT s.title, al.title, ar.ArtistName from song s
Left Join Album al  on al.id = s.AlbumId
Left Join Artist ar on s.ArtistId = ar.id
WHERE al.title = 'AlbumFunTime'

SELECT al.title from album al
Left Join song s  on al.id = s.AlbumId