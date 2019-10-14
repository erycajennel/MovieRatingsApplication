USE `movieratingsdb` ;
/*
-- Query: SELECT * FROM movieratingsdb.rating
LIMIT 0, 1000

-- Date: 2019-10-14 10:04
*/
INSERT INTO rating (`Id`,`Code`,`Description`) VALUES (1,'PG','Parental Guidance');
INSERT INTO rating (`Id`,`Code`,`Description`) VALUES (2,'G','General Audiences');
INSERT INTO rating (`Id`,`Code`,`Description`) VALUES (3,'M','Mature');
INSERT INTO rating (`Id`,`Code`,`Description`) VALUES (4,'MA','Mature Accompanied');
INSERT INTO rating (`Id`,`Code`,`Description`) VALUES (5,'R','Restricted');

/*
-- Query: SELECT * FROM movieratingsdb.movie
LIMIT 0, 1000

-- Date: 2019-10-14 10:04
*/
INSERT INTO movie (`Id`,`Title`,`ReleaseDate`,`RatingId`) VALUES (1,'Glass','2019-01-18 00:00:00',1);
INSERT INTO movie (`Id`,`Title`,`ReleaseDate`,`RatingId`) VALUES (2,'The Kid Who Would Be King','2019-01-25 00:00:00',1);
INSERT INTO movie (`Id`,`Title`,`ReleaseDate`,`RatingId`) VALUES (3,'The Lego Movie 2: The Second Part','2019-02-08 00:00:00',1);
INSERT INTO movie (`Id`,`Title`,`ReleaseDate`,`RatingId`) VALUES (4,'What Men Want','2019-02-08 00:00:00',5);
INSERT INTO movie (`Id`,`Title`,`ReleaseDate`,`RatingId`) VALUES (8,' The Hummingbird Project ','2019-03-15 00:00:00',5);
