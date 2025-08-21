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

-- Q5) You are scheduling hockey events across the city. You have a database with the table 
-- 'scores'(player Id.event. Id.is participated goals) containing information regarding the player's 
-- id, a flag for goals scored or not, 0 meaning that the player did not score a goal and 1 meaning 
-- that the player scored a goal for each event denoted by event id.For each event find the number 
-- of players who scored a goal and number of players who did not score a goal for that event id 
-- and goal flag. The output must be sorted in increasing order of event_id, goal.

/* Create scores table */
CREATE TABLE scores (
    player_id INT NOT NULL,
    event_id INT NOT NULL,
    is_participated BIT NOT NULL,  -- 1 = participated, 0 = did not participate
    goals BIT NOT NULL             -- 1 = scored goal, 0 = did not score goal
);


/* Insert sample data into scores */
INSERT INTO scores (player_id, event_id, is_participated, goals) VALUES
(1, 101, 1, 1), -- Player 1 scored in event 101
(2, 101, 1, 0), -- Player 2 did not score in event 101
(3, 101, 1, 1), -- Player 3 scored in event 101
(4, 101, 1, 0), -- Player 4 did not score in event 101
(1, 102, 1, 0), -- Player 1 did not score in event 102
(2, 102, 1, 1), -- Player 2 scored in event 102
(3, 102, 1, 0), -- Player 3 did not score in event 102
(5, 103, 1, 1); -- Player 5 scored in event 103

truncate table scores;

select * from scores;

INSERT INTO scores (player_id, event_id, is_participated, goals) VALUES
-- Event 1
(101, 1, 1, 1),  -- Player 101 participated and scored
(102, 1, 1, 0),  -- Player 102 participated but didn't score
(103, 1, 1, 1),  -- Player 103 participated and scored
(104, 1, 1, 0),  -- Player 104 participated but didn't score
(105, 1, 1, 0),  -- Player 105 participated but didn't score

-- Event 2
(101, 2, 1, 0),  -- Player 101 participated but didn't score
(102, 2, 1, 1),  -- Player 102 participated and scored
(106, 2, 1, 1),  -- Player 106 participated and scored
(107, 2, 1, 1),  -- Player 107 participated and scored
(108, 2, 1, 0),  -- Player 108 participated but didn't score

-- Event 3
(103, 3, 1, 1),  -- Player 103 participated and scored
(104, 3, 1, 0),  -- Player 104 participated but didn't score
(109, 3, 1, 0),  -- Player 109 participated but didn't score
(110, 3, 1, 0),  -- Player 110 participated but didn't score

-- Event 4
(105, 4, 1, 1),  -- Player 105 participated and scored
(106, 4, 1, 1),  -- Player 106 participated and scored
(107, 4, 1, 1),  -- Player 107 participated and scored
(108, 4, 1, 1),  -- Player 108 participated and scored
(111, 4, 1, 0),  -- Player 111 participated but didn't score
(112, 4, 1, 0);  -- Player 112 participated but didn't score

select event_id, goals, count(player_id) as player_count from scores 
group by event_id, goals 
order by event_id asc, goals asc;

select event_id, goals from scores group by event_id, goals;

--Q6) A website owner has the table 'user' (name, id, dob). The date of birth (dob) field has been 
--left empty in some places. Write a query to alter the table schema to avoid this for new entries.

/* Create user table with dob allowing NULL initially */
CREATE TABLE [user] (
    name VARCHAR(50) NOT NULL,
    id INT PRIMARY KEY,
    dob DATE
);

/* Insert sample data with some NULL dob values */
INSERT INTO [user] (name, id, dob) VALUES
('Alice', 1, '1990-05-15'),
('Bob', 2, NULL),
('Charlie', 3, '1985-10-20'),
('Diana', 4, NULL);
select * from [user]
alter table [user]
update dob date is not null;
