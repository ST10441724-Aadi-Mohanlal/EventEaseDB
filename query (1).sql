CREATE TABLE Venue (
    VenueId INT IDENTITY(1,1) PRIMARY KEY,
    VenueName NVARCHAR(100) UNIQUE NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    Capacity INT NOT NULL,
    ImageUrl VARCHAR(MAX) NOT NULL
);

CREATE TABLE Event (
    EventId INT IDENTITY(1,1) PRIMARY KEY,
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATETIME NOT NULL,
    Description NVARCHAR(MAX) NULL,
    
);

CREATE TABLE Booking (
    BookingId INT IDENTITY(1,1) PRIMARY KEY,
    EventId INT NOT NULL,
    VenueId INT NOT NULL,
    BookingDate DATETIME NOT NULL,
    FOREIGN KEY (EventId) REFERENCES Event(EventId),
    FOREIGN KEY (VenueId) REFERENCES Venue(VenueId),
    CONSTRAINT UQ_Booking UNIQUE (VenueId, BookingDate) -- Prevent double bookings
);

--TABLE INSERTION SECTION
INSERT INTO Venue (VenueName, Location, Capacity, ImageUrl)
VALUES 
('Grand Hall', 'Cape Town', 500, 'https://www.warmkaroo.com/wp-content/uploads/2022/10/Wedding-The-Grand-Hall-Warm-Karoo-08.jpeg'),
('Ocean View Conference Center', 'Durban', 300, 'https://images.squarespace-cdn.com/content/v1/61781f1b6cf388781f401046/53bb92c7-09aa-4a41-9065-98420d92071a/nooitgedacht-estate-wedding-grand-hall-header.JPG');

INSERT INTO Event (EventName, EventDate, Description)
VALUES 
('Tech Summit 2025', '2025-04-15', 'Annual technology conference'),
('AI Expo', '2025-05-10', 'Exploring AI innovations');

INSERT INTO Booking (EventId, VenueId, BookingDate)
VALUES 
(1, 1, '2025-04-15'),
(2, 2, '2025-05-10');

--/-- TABLE ALTERATION SECTION

--TABLE MANIPULATION SECTION
SELECT * FROM Venue
SELECT * FROM Event
SELECT * FROM Booking


