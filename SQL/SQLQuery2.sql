create database sqlpractice;

use sqlpractice;


-- Q1) A college has the tables student' (id, name, score, branch) and 'scholarship' (id, Sdate, 
-- amount). Update the scholarship amount of the student named Anup to 20000 and the 
-- scholarship date to 25 Jan 2014.
CREATE TABLE student (
    id INT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    score DECIMAL(5,2),
    branch VARCHAR(50)
);

-- Creating the scholarship table
CREATE TABLE scholarship (
    id INT PRIMARY KEY,
    Sdate DATE,
    amount DECIMAL(10,2),
    FOREIGN KEY (id) REFERENCES student(id)
);

-- Insert sample entries into the student table
INSERT INTO student (id, name, score, branch) VALUES
(1, 'Anup', 85.50, 'Computer Science'),
(2, 'Priya', 78.25, 'Electronics'),
(3, 'Rahul', 92.00, 'Mechanical'),
(4, 'Sneha', 88.75, 'Civil');

-- Insert sample entries into the scholarship table
INSERT INTO scholarship (id, Sdate, amount) VALUES
(1, '2013-06-15', 15000.00),
(2, '2013-07-01', 12000.00),
(3, '2013-08-10', 18000.00);

Update scholarship
SET amount = 20000, Sdate = '2014-01-25'
WHERE id = ( Select id from student where name = 'Anup');

select * from scholarship;

-- Q2) You are designing a database for the city's architectural planning. The database currently has 
-- two tables. city_demographic (city id, state_id, longitude, latitude) and states (state id state 
-- name). You want to enforce a referential integrity such that every city in the city_demographic 
-- table has a valid state_id reference from the states table. Alter the city_demographic 
-- schema to do this.

-- Create states table
CREATE TABLE states (
    state_id INT PRIMARY KEY,
    state_name VARCHAR(50) NOT NULL
);

-- Create city_demographic table
CREATE TABLE city_demographic (
    city_id INT PRIMARY KEY,
    state_id INT,
    longitude DECIMAL(9,6),
    latitude DECIMAL(9,6)
);

-- Insert into states
INSERT INTO states (state_id, state_name) VALUES
(1, 'California'),
(2, 'Texas'),
(3, 'New York');

-- Insert into city_demographic (before adding foreign key)
INSERT INTO city_demographic (city_id, state_id, longitude, latitude) VALUES
(101, 1, -122.4194, 37.7749), -- San Francisco, CA
(102, 2, -95.3698, 29.7604),  -- Houston, TX
(103, 3, -74.0060, 40.7128); -- New York City, NY

select * from city_demographic;
select * from states;

alter table city_demographic
Add constraint fk_state
Foreign key(state_id) 
references states(state_id);

-- test
INSERT INTO city_demographic (city_id, state_id, longitude, latitude)
VALUES (104, 4, -87.6298, 41.8781); -- Chicago with invalid state_id

DELETE FROM states WHERE state_id = 1;

SELECT 
    CONSTRAINT_NAME, 
    TABLE_NAME, 
    COLUMN_NAME
FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
WHERE TABLE_NAME = 'city_demographic' AND CONSTRAINT_NAME = 'fk_state';

-- Q3) A bank database consists of the tables 'customer' (name, ac_num. place) and 'loan'
-- (ac_num. loan_num, amount). Find the names of the customers who have an account with the 
-- bank but no loan. If a customer has more than one account, name must be displayed the same 
-- number of times.

/* Create customer table with primary key */
CREATE TABLE customer (
    name VARCHAR(50) NOT NULL,
    ac_num INT PRIMARY KEY,
    place VARCHAR(100)
);

/* Create loan table with composite primary key and foreign key */
CREATE TABLE loan (
    ac_num INT,
    loan_num INT,
    amount DECIMAL(10,2),
    PRIMARY KEY (ac_num, loan_num),
    FOREIGN KEY (ac_num) REFERENCES customer(ac_num)
);

/* Insert sample customers */
INSERT INTO customer (name, ac_num, place) VALUES
('Alice', 101, 'New York'),
('Bob', 102, 'Chicago'),
('Alice', 103, 'Boston'), -- Alice has two accounts
('Charlie', 104, 'Miami');

/* Insert sample loans */
INSERT INTO loan (ac_num, loan_num, amount) VALUES
(101, 1, 5000.00),  -- Alice’s first account has a loan
(102, 1, 10000.00); -- Bob has a loan

select c.name from customer c left join loan l on c.ac_num = l.ac_num where l.ac_num IS NULL;

-- Q4) On the planet Naruka the best performer is the one who scores the second highest number 
-- of runs, not the highest. You are given a database with the tables 'sportsman' (ld, name) and 
-- "game" (id, runs). Identify the player who is the 'best performer' as described in the statement. 
-- The final answer must have columns (id, name, runs);

/* Create sportsman table */
CREATE TABLE sportsman (
    id INT PRIMARY KEY,
    name VARCHAR(50) NOT NULL
);

/* Create game table with foreign key */
CREATE TABLE game (
    id INT,
    runs INT,
    FOREIGN KEY (id) REFERENCES sportsman(id)
);

/* Insert sample sportsmen */
INSERT INTO sportsman (id, name) VALUES
(1, 'Alice'),
(2, 'Bob'),
(3, 'Charlie'),
(4, 'Diana');

/* Insert sample game runs */
INSERT INTO game (id, runs) VALUES
(1, 120), -- Alice
(2, 150), -- Bob (highest)
(3, 100), -- Charlie
(3, 120), -- Charlie (tied with Alice)
(4, 80);  -- Diana
select * from game;

select g.id, s.name, g.runs from game g join sportsman s on g.id = s.id 
where runs = ( select Max(runs) from game where runs < (select Max(runs) from game ));