--DATABASE CREATION SELECTION


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
    VenueId INT NULL,
    FOREIGN KEY (VenueId) REFERENCES Venue(VenueId)
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
('Grand Hall', 'Cape Town', 500, 'https://via.placeholder.com/150'),
('Ocean View Conference Center', 'Durban', 300, 'https://via.placeholder.com/150');

INSERT INTO Event (EventName, EventDate, Description, VenueId)
VALUES 
('Tech Summit 2025', '2025-04-15', 'Annual technology conference', NULL),
('AI Expo', '2025-05-10', 'Exploring AI innovations', NULL);

INSERT INTO Booking (EventId, VenueId, BookingDate)
VALUES 
(1, 1, '2025-04-15'),
(2, 2, '2025-05-10');

--/-- TABLE ALTERATION SECTION

--TABLE MANIPULATION SECTION
SELECT * FROM Venue
SELECT * FROM Event
SELECT * FROM Booking

