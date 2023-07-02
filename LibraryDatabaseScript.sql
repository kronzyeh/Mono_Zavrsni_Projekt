create extension if not exists "uuid-ossp";
--TABLE DELETION--
	drop table "PublicationAuthor";	
	drop table "Author";
	drop table "Reservation";
	drop table "Publication";
	drop table "Type";
	drop table "Publisher";
	drop table "Genre";
	drop table "Subscription";
	drop table "User";
	drop table "Role";




--TABLE CREATION--

	create table "Role" (
	"Id" uuid not null constraint "PK_Role" primary key,
	"Name" varchar(10) not null,
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"DateUpdated" timestamp,
	"UpdatedByUserId" uuid 
	);
	
	create table "User" (
	"Id" uuid not null constraint "PK_User" primary key,
	"FirstName" varchar(20) not null,
	"LastName" varchar(50) not null,
	"DateOfBirth" Date not null,
	"PhoneNumber" varchar(20) not null,
	"Email" varchar(50) not null,
	"Password" varchar(100) not null,
	"RoleId" uuid not null,
	constraint "FK_Role_RoleId" foreign key ("RoleId") references "Role"("Id"),
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"UpdatedByUserId" uuid,
	"DateUpdated" timestamp
	);
	
	--SubscriptionHistory?
	create table "SubscriptionHistory" (
	"Id" uuid not null constraint "PK_SubscriptionHistory" primary key,
	"StartDate" timestamp not null,
	"EndDate" timestamp not null,
	"UserId" uuid not null,
	constraint "FK_User_UserId" foreign key ("UserId") references "User"("Id"),
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"UpdatedByUserId" uuid,
	"DateUpdated" timestamp,
	constraint "FK_User_CreatedByUserId" foreign key ("CreatedByUserId") references "User"("Id"),
	constraint "FK_User_UpdatedByUserId" foreign key ("UpdatedByUserId") references "User"("Id")
	);
	
	create table "Type" (
	"Id" uuid not null constraint "PK_Type" primary key,
	"Name" varchar(20) not null,
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"UpdatedByUserId" uuid,
	"DateUpdated" timestamp,
	constraint "FK_User_CreatedByUserId" foreign key ("CreatedByUserId") references "User"("Id"),
	constraint "FK_User_UpdatedByUserId" foreign key ("UpdatedByUserId") references "User"("Id")
	);
	
	create table "Genre" (
	"Id" uuid not null constraint "PK_Genre" primary key,
	"Name" varchar(20) not null,
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"UpdatedByUserId" uuid,
	"DateUpdated" timestamp,
	constraint "FK_User_CreatedByUserId" foreign key ("CreatedByUserId") references "User"("Id"),
	constraint "FK_User_UpdatedByUserId" foreign key ("UpdatedByUserId") references "User"("Id")
	);
	
	create table "Publisher" (
	"Id" uuid not null constraint "PK_Publisher" primary key,
	"Name" varchar(20) not null,
	"ContactNumber" varchar(20) not null,
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"UpdatedByUserId" uuid,
	"DateUpdated" timestamp,
	constraint "FK_User_CreatedByUserId" foreign key ("CreatedByUserId") references "User"("Id"),
	constraint "FK_User_UpdatedByUserId" foreign key ("UpdatedByUserId") references "User"("Id")
	);
	
	create table "Publication" (
	"Id" uuid not null constraint "PK_Publication" primary key,
	"Title" varchar(20) not null,
	"Description" text not null,
	"Edition" integer not null,
	"DatePublished" date not null,
	"Quantity" integer not null,
	"NumberOfPages" integer not null,
	"Language" varchar(20 not null, --ovo smo zaboravili
	"TypeId" uuid not null, 
	"GenreId" uuid not null, -- sto staviti za rjecnik?
	"PublisherId" uuid not null,
	constraint "FK_Type_TypeId" foreign key ("TypeId") references "Type"("Id"),
	constraint "FK_Genre_GenreId" foreign key ("GenreId") references "Genre"("Id"),
	constraint "FK_Publisher_PublisherId" foreign key ("PublisherId") references "Publisher"("Id"),
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"UpdatedByUserId" uuid,
	"DateUpdated" timestamp,
	constraint "FK_User_CreatedByUserId" foreign key ("CreatedByUserId") references "User"("Id"),
	constraint "FK_User_UpdatedByUserId" foreign key ("UpdatedByUserId") references "User"("Id")
	);

	create table "Reservation" (
	"Id" uuid not null constraint "PK_Reservation" primary key,
	"StartDate" date not null,
	"EndDate" date not null,
	"IsReturned" bool not null,
	"PublicationId" uuid not null,
	"UserId" uuid not null,
	constraint "FK_Publication_PublicationId" foreign key ("PublicationId") references "Publication"("Id"),
	constraint "FK_User_UserId" foreign key ("UserId") references "User"("Id"),
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"UpdatedByUserId" uuid,
	"DateUpdated" timestamp,
	constraint "FK_User_CreatedByUserId" foreign key ("CreatedByUserId") references "User"("Id"),
	constraint "FK_User_UpdatedByUserId" foreign key ("UpdatedByUserId") references "User"("Id")
	);

	create table "Author" (
	"Id" uuid not null constraint "PK_Author" primary key,
	"FirstName" varchar(50) not null,
	"LastName" varchar(50) not null,
	"Nationality" varchar(20),
	"DateOfBirth" date not null,
	"DateOfDeath" date,
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"UpdatedByUserId" uuid,
	"DateUpdated" timestamp,
	constraint "FK_User_CreatedByUserId" foreign key ("CreatedByUserId") references "User"("Id"),
	constraint "FK_User_UpdatedByUserId" foreign key ("UpdatedByUserId") references "User"("Id")
	);

	create table "PublicationAuthor" (
	"Id" uuid not null constraint "PK_PublicationAuthor" primary key,
	"PublicationId" uuid not null,
	"AuthorId" uuid, --stavila sam da ne mora biti obavezno zbog časopisa koji nemaju autore, nego izdavače
	constraint "FK_Publication_PublicationId" foreign key ("PublicationId") references "Publication"("Id"),
	constraint "FK_Author_AuthorId" foreign key ("AuthorId") references "Author"("Id"),
	"IsActive" bool not null,
	"CreatedByUserId" uuid not null,
	"DateCreated" timestamp not null,
	"UpdatedByUserId" uuid,
	"DateUpdated" timestamp,
	constraint "FK_User_CreatedByUserId" foreign key ("CreatedByUserId") references "User"("Id"),
	constraint "FK_User_UpdatedByUserId" foreign key ("UpdatedByUserId") references "User"("Id")
	);

--VALUE INSERTION--

	--ROLE--

	
	insert into "Role" ("Id", "Name", "IsActive", "CreatedByUserId", "DateCreated", "UpdatedByUserId", "DateUpdated") values 
		('c72d4c65-4d08-49ab-84e1-6cb3341f8bb6', 'User', true, '9ea84fc7-89c2-4067-b8b6-5df728d5f64c', current_timestamp, null, null),
		('9ea84fc7-89c2-4067-b8b6-5df728d5f64c', 'Admin', true,  '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp, null, null);
	
	select * from "Role";

	
	--ADMIN USER--
	insert into "User" ("Id", "FirstName", "LastName", "DateOfBirth", "PhoneNumber", "Email", "Password", "RoleId", "IsActive", "CreatedByUserId",
		"DateCreated", "UpdatedByUserId", "DateUpdated") values
		('9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', 'Admin', 'Admin', '2000-1-1', '0999999999', 'admin@mail.com', 'admin', '9ea84fc7-89c2-4067-b8b6-5df728d5f64c',
			true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp, null, null);

	--TYPE--

	insert into "Type" ("Id", "Name", "IsActive", "CreatedByUserId", "DateCreated") values
		(gen_random_uuid(), 'Book', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Magazine', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Comic book', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Dictionary', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp);
		
	select * from "Type";

	--GENRE--

	insert into "Genre" ("Id", "Name", "IsActive", "CreatedByUserId", "DateCreated") values
		--books
		(gen_random_uuid(), 'Children''s Book', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Science Fiction', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Novel', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Dictionary', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Drama', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Crime', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Horror', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Self help', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		--magazines
		(gen_random_uuid(), 'Fashion', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Sports', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Lifestyle', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Technology', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Science', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Nature', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		--comic books
		(gen_random_uuid(), 'Humor', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Adventure', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Superhero', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Mystery', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Fantasy', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp);
	
	select * from "Genre";

	--AUTHOR--
	insert into "Author" ("Id", "FirstName", "LastName", "Nationality", "DateOfBirth", "DateOfDeath", "IsActive",
		"CreatedByUserId", "DateCreated") values
		--Books
		(gen_random_uuid(), 'William', 'Shakespeare','English','1565-04-26','1616-04-23', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Jane', 'Austen','English','1775-12-16','1817-07-18', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Fyodor', 'Dostoevsky','Russian','1821-11-11','1881-02-09', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Ernest', 'Hemingway','American','1899-07-21','1961-02-07', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
		(gen_random_uuid(), 'Leo', 'Tolstoy', 'Russian', '1828-09-09', '1910-11-20', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'George', 'Orwell', 'British', '1903-06-25', '1950-01-21', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Agatha', 'Christie', 'British', '1890-09-15', '1976-01-12', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'J.R.R.', 'Tolkien', 'British', '1892-01-03', '1973-09-02', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Virginia', 'Woolf', 'British', '1882-01-25', '1941-03-28', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Franz', 'Kafka', 'Czech', '1883-07-03', '1924-06-03', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Gabriel García', 'Márquez', 'Colombian', '1927-03-06', '2014-04-17', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Toni', 'Morrison', 'American', '1931-02-18', '2019-08-05', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Albert', 'Camus', 'French', '1913-11-07', '1960-01-04', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Chinua', 'Achebe', 'Nigerian', '1930-11-16', '2013-03-21', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    --Comic Books
	    (gen_random_uuid(), 'Stan', 'Lee', 'American', '1922-12-28', '2018-11-12', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Alan', 'Moore', 'English', '1953-11-18', NULL, true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Neil', 'Gaiman', 'British', '1960-11-10', NULL, true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Frank', 'Miller', 'American', '1957-01-27', NULL, true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Chris', 'Claremont', 'British', '1950-11-25', NULL, true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Brian', 'Michael Bendis', 'American', '1967-08-18', NULL, true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Grant', 'Morrison', 'Scottish', '1960-01-31', NULL, true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Gail', 'Simone', 'American', '1964-07-29', NULL, true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Art', 'Spiegelman', 'American', '1948-02-15', NULL, true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Marjorie', 'Liu', 'American', '1979-12-21', NULL, true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	   --Dictionaries
	   	(gen_random_uuid(), 'Noah', 'Webster', 'American', '1758-10-16', '1843-05-28', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
    	(gen_random_uuid(), 'Samuel', 'Johnson', 'English', '1709-09-18', '1784-12-13', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
    	(gen_random_uuid(), 'Noah', 'Porter', 'American', '1811-10-14', '1892-03-04', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'John', 'Walker', 'British', '1732-05-29', '1807-05-01', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Joseph', 'Worcester', 'American', '1784-08-24', '1865-10-27', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Noah', 'Webster', 'American', '1758-10-16', '1843-05-28', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Samuel', 'Johnson', 'English', '1709-09-18', '1784-12-13', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'Daniel', 'Webster', 'American', '1782-01-18', '1852-10-24', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp),
	    (gen_random_uuid(), 'John', 'Jamieson', 'Scottish', '1759-06-03', '1838-07-12', true, '9ac7c94f-1e4a-42c5-b99b-c4ce48d4877f', current_timestamp);
	   
	   select * from "Author";
	   
		
	
			