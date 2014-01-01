INSERT INTO dbo.Bandgenres	( Band_ID, Genre_ID) VALUES ( ?, ? );
INSERT INTO dbo.Bands	( ID, naam, omschrijving, fotoUri) VALUES ( ?, ?, ?, ? );
INSERT INTO dbo.Gebruikers	( ID, vnaam, anaam, email, wachtwoord, isAdmin) VALUES ( ?, ?, ?, ?, ?, ? );
INSERT INTO dbo.Genres	( ID, naam) VALUES ( ?, ? );
INSERT INTO dbo.Orders	( userid, ticket, aantal) VALUES ( ?, ?, ? );	
INSERT INTO dbo.Tickets	( ID, soort, aantal) VALUES ( ?, ?, ? );



CREATE TABLE dbo.Bandgenres ( 
	Band_ID              int    ,
	Genre_ID             int    
 );
CREATE INDEX idx_bandID ON dbo.Bandgenres ( Band_ID );
CREATE INDEX idx_genreID ON dbo.Bandgenres ( Genre_ID );
ALTER TABLE dbo.Bandgenres ADD CONSTRAINT fk_bandgenre_bands FOREIGN KEY ( Band_ID ) REFERENCES dbo.Bands( ID ) ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE dbo.Bandgenres ADD CONSTRAINT fk_bandgenre_muziekgenres FOREIGN KEY ( Genre_ID ) REFERENCES dbo.Genres( ID ) ON DELETE NO ACTION ON UPDATE NO ACTION;
INSERT INTO dbo.Bandgenres	( Band_ID, Genre_ID) VALUES ( ?, ? );



CREATE TABLE dbo.Bands ( 
	ID                   int NOT NULL   IDENTITY,
	naam                 varchar(50) NOT NULL   ,
	omschrijving         text NOT NULL   ,
	fotoUri              varchar(200) NOT NULL   ,
	CONSTRAINT Pk_Bands PRIMARY KEY ( ID )
 );
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Lijst muziekbands' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bands';;
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Bandbaam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bands', @level2type=N'COLUMN',@level2name=N'naam';
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Korte omschrijving v.e. band' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bands', @level2type=N'COLUMN',@level2name=N'omschrijving';
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Pad naar foto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Bands', @level2type=N'COLUMN',@level2name=N'fotoUri';
INSERT INTO dbo.Bands	( naam, omschrijving, fotoUri) VALUES ( 'Linkin Park', 'Amerikaanse rockband', 'unknown' );
INSERT INTO dbo.Bands	( naam, omschrijving, fotoUri) VALUES ( 'The Killers', 'Amerikaanse postpunk band', 'unknown' );
INSERT INTO dbo.Bands	( naam, omschrijving, fotoUri) VALUES ( 'Metallica', 'Amerikaanse metal band', 'unknown' );
INSERT INTO dbo.Bands	( naam, omschrijving, fotoUri) VALUES ( 'Pearl Jam', 'Amerikaanse rockband', 'unknown' );



CREATE TABLE dbo.Gebruikers ( 
	ID                   int NOT NULL   IDENTITY,
	vnaam                varchar(50) NOT NULL   ,
	anaam                varchar(60) NOT NULL   ,
	email                varchar(120) NOT NULL   ,
	wachtwoord           varchar(150) NOT NULL   ,
	isAdmin              tinyint  CONSTRAINT defo_isAdmin DEFAULT 0  ,
	CONSTRAINT Pk_Gebruikers PRIMARY KEY ( ID )
 );
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Voornaam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Gebruikers', @level2type=N'COLUMN',@level2name=N'vnaam';
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Achternaam' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Gebruikers', @level2type=N'COLUMN',@level2name=N'anaam';
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Email-adres' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Gebruikers', @level2type=N'COLUMN',@level2name=N'email';
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'wachtwoord(hash)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Gebruikers', @level2type=N'COLUMN',@level2name=N'wachtwoord';
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Is de user administrator?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Gebruikers', @level2type=N'COLUMN',@level2name=N'isAdmin';
INSERT INTO dbo.Gebruikers	( vnaam, anaam, email, wachtwoord, isAdmin) VALUES ( 'docent', 'geeftLes', 'docent@docent.com', 'docentdocent', 1 );
INSERT INTO dbo.Gebruikers	( vnaam, anaam, email, wachtwoord, isAdmin) VALUES ( 'gebruiker', 'magBestellen', 'user@user.com', 'docentdocent', 0 );




CREATE TABLE dbo.Genres ( 
	ID                   int NOT NULL   IDENTITY,
	naam                 varchar(25)    ,
	CONSTRAINT Pk_Genres PRIMARY KEY ( ID )
 );
INSERT INTO dbo.Genres	( naam) VALUES ( 'Rock' );
INSERT INTO dbo.Genres	( naam) VALUES ( 'Punk' );
INSERT INTO dbo.Genres	( naam) VALUES ( 'Metal' );
INSERT INTO dbo.Genres	( naam) VALUES ( 'Jazz' );
INSERT INTO dbo.Genres	( naam) VALUES ( 'Hip-Hop' );
INSERT INTO dbo.Genres	( naam) VALUES ( 'Folk' );



CREATE TABLE dbo.Orders ( 
	userid               int    ,
	ticket               int    ,
	aantal               int    
 );
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Lijst van bestelde tickets' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Orders';;
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Gebruikers ID van bestelde ticket(s)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Orders', @level2type=N'COLUMN',@level2name=N'userid';
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Welk ticket?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Orders', @level2type=N'COLUMN',@level2name=N'ticket';
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Aantal bestelde tickets' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Orders', @level2type=N'COLUMN',@level2name=N'aantal';
INSERT INTO dbo.Orders	( userid, ticket, aantal) VALUES ( 1, 1, 4 );
INSERT INTO dbo.Orders	( userid, ticket, aantal) VALUES ( 1, 3, 4 );
INSERT INTO dbo.Orders	( userid, ticket, aantal) VALUES ( 2, 2, 6 );



CREATE TABLE dbo.Tickets ( 
	ID                   int NOT NULL   IDENTITY,
	soort                text    ,
	aantal               int NOT NULL   ,
	prijs                float    ,
	CONSTRAINT Pk_Tickets PRIMARY KEY ( ID )
 );
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Welk (dag)ticket' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tickets', @level2type=N'COLUMN',@level2name=N'soort';
exec sp_addextendedproperty  @name=N'MS_Description', @value=N'Aantal beschikbare tickets' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Tickets', @level2type=N'COLUMN',@level2name=N'aantal';
INSERT INTO dbo.Tickets	( soort, aantal) VALUES ( 'Vrijdag', 15000 );
INSERT INTO dbo.Tickets	( soort, aantal) VALUES ( 'Zaterdag', 15000 );
INSERT INTO dbo.Tickets	( soort, aantal) VALUES ( 'Zondag', 15000 );
INSERT INTO dbo.Tickets	( soort, aantal) VALUES ( 'Comboticket (3 dagen)', 20000 );
INSERT INTO dbo.Tickets	( soort, aantal) VALUES ( 'Parking', 50000 );