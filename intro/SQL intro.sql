--SELECT * FROM Genre

INSERT INTO Artist (ArtistName, YearEstablished) VALUES ('Cohort37', '2020');

SELECT * From Album

INSERT INTO Album (AlbumLength, ArtistId, Label, ReleaseDate, Title) VALUES (2020, 29, 'NSS', '11/01/2019', 'AlbumFunTime')

INSERT INTO Song (Title, SongLength, ReleaseDate, GenreId, ArtistId, AlbumId) VALUES ('SQL', 10000, '03/18/2020', 9, 29, 24) 

INSERT INTO Song (Title, SongLength, ReleaseDate, GenreId, ArtistId, AlbumId) VALUES ('SQL2', 10000, '03/18/2020', 9, 29, 24) 


SELECT * From Song

SELECT s.title, al.title from song s
Left Join Album al  on al.id = s.AlbumId
Left Join Artist ar on s.ArtistId = ar.id
WHERE al.title = 'AlbumFunTime'

SELECT al.title,  COUNT(s.id) as NumberofSongs from Album al
left join Song s on s.AlbumId = al.Id
group by al.Title

SELECT ar.ArtistName,  COUNT(s.id) as NumberofSongs from Artist ar
left join Song s on s.ArtistId = ar.Id
group by ar.ArtistName

SELECT ge.Label,  COUNT(s.id) as NumberofSongs from Genre ge
left join Song s on s.GenreId = ge.Id
group by ge.Label

select title, AlbumLength from Album where AlbumLength = (select max(AlbumLength) from Album);

select s.title, SongLength, al.Title from Song s
left join Album al on s.AlbumId = al.Id
where SongLength = (select max(SongLength) from Song)