-- Create Database DellLeaderboard
CREATE DATABASE IF NOT EXISTS DellLeaderboard;

-- Use DellLeaderboard
USE DellLeaderboard;

-- Table: Points
DROP TABLE IF EXISTS Points;

-- Table: Points
CREATE TABLE Points
(
  ID INT AUTO_INCREMENT PRIMARY KEY,
  Name VARCHAR(50) NOT NULL,
  Score INT NOT NULL DEFAULT 0
);

-- Insert data into the Points table
INSERT INTO Points (Name, Score) VALUES ('Samatha', 1000);
INSERT INTO Points (Name, Score) VALUES ('John', 1500);
INSERT INTO Points (Name, Score) VALUES ('Mary', 100);
INSERT INTO Points (Name, Score) VALUES ('Tom', 2000);
INSERT INTO Points (Name, Score) VALUES ('Harry', 3600);
INSERT INTO Points (Name, Score) VALUES ('Jerry', 500);
INSERT INTO Points (Name, Score) VALUES ('Walter', 400);
INSERT INTO Points (Name, Score) VALUES ('Brandon', 1700);
INSERT INTO Points (Name, Score) VALUES ('Adam', 2200);
