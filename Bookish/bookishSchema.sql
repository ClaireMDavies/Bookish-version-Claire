CREATE DATABASE bookish;
Drop Table copies;
USE bookish;

CREATE TABLE books (
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	title varchar(255) NOT NULL,
	author varchar(255) NOT NULL,
);

CREATE TABLE users (
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	name varchar(255) NOT NULL,
);

CREATE TABLE copies (
	id int NOT NULL PRIMARY KEY IDENTITY(1,1),
	book_id int NOT NULL,
	FOREIGN KEY (book_id) REFERENCES books(id),
	user_id int,
	FOREIGN KEY (user_id) REFERENCES users(id),
	due_date Date
);

INSERT INTO books (title, author)Values('Lord of the rings', 'J.R.R Tolkien');
INSERT INTO users (name)Values('Claire');
INSERT INTO copies (book_id, user_id, due_date)Values(1, 1, null);