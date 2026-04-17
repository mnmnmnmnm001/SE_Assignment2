CREATE DATABASE OrderDB;
GO
USE OrderDB;
GO

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(50)  NOT NULL UNIQUE,
    email NVARCHAR(100) NOT NULL,
    password NVARCHAR(256) NOT NULL,  -- store SHA256 hash
    Lock tinyint DEFAULT 0
);

CREATE TABLE Item (
    ItemID int IDENTITY(1,1) PRIMARY KEY,
    ItemName NVARCHAR(100) NOT NULL,
    Size NVARCHAR(20) NOT NULL,
    Price DECIMAL(18,2) NOT NULL DEFAULT 0
);

CREATE TABLE Agent (
    AgentID int IDENTITY(1,1) PRIMARY KEY,
    AgentName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200) NOT NULL
);

CREATE TABLE Order1 ( --Order is a reserved keyword in SQL, so I name it Order1
    OrderID int IDENTITY(1,1) PRIMARY KEY,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    AgentID int NOT NULL,
    CONSTRAINT FK_Order_Agent FOREIGN KEY (AgentID) REFERENCES Agent(AgentID)
);

CREATE TABLE OrderDetail (
    ID int IDENTITY(1,1) PRIMARY KEY,
    OrderID int NOT NULL,
    ItemID int,
    Quantity int NOT NULL DEFAULT 1,
    UnitAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    CONSTRAINT FK_OrderDetail_Order  FOREIGN KEY (OrderID) REFERENCES Order1(OrderID),
    CONSTRAINT FK_OrderDetail_Item   FOREIGN KEY (ItemID)  REFERENCES Item(ItemID)
);
GO

-- Users (password = EncodePass("123456"))
-- SHA256 is a one-way hash — it cannot be decoded back to the original password.
INSERT INTO Users (UserName, email, password, Lock) VALUES
('admin','admin@gmail.com','e10adc3949ba59abbe56e057f20f883e', 0),
('qp','pq@gmail.com','e10adc3949ba59abbe56e057f20f883e', 0),
('bb','qq@gmail.com','e10adc3949ba59abbe56e057f20f883e', 0),
('ae','ae@gmail.com','e10adc3949ba59abbe56e057f20f883e', 0),
('dio','diana@gmail.com','e10adc3949ba59abbe56e057f20f883e', 1);

INSERT INTO Item (ItemName, Size, Price) VALUES
('Laptop Dell XPS 13',  '13"', 25000000),
('Laptop HP Envy 15',   '15"', 22000000),
('Laptop Asus ZenBook', '14"', 20000000),
('Keyboard Mechanical', 'Full',  900000),
('Monitor Samsung 24"', '24"',  5500000),
('Mouse Logitech M705', 'N/A',   350000),
('Monitor LG 27"',      '27"',  7200000),
('USB-C Hub 7-in-1',    'N/A',   450000),
('Webcam Logitech C920','N/A',  1800000),
('Headset Sony WH-1000','N/A',  4200000),
('SSD Samsung 500GB',   '500G', 1750000),
('RAM Kingston 16GB',   '16GB', 1200000),
('Laptop Bag 15"',      '15"',   320000),
('Desk Lamp LED',       'N/A',   280000),
('Smartphone Stand',    'N/A',   150000),
('Cable HDMI 2m',       '2m',     85000),
('Printer HP LaserJet', 'A4',   6500000),
('Ink Cartridge Black', 'N/A',   350000),
('A4 Paper 70gsm',      'A4',     65000),
('Power Bank 20000mAh', 'N/A',   850000);

INSERT INTO Agent (AgentName, Address) VALUES
('Tech World JSC',     '12 Nguyen Hue, HCM City'),
('Smart Office Ltd',   '45 Le Loi, Da Nang'),
('Digital Hub Co.',    '78 Tran Phu, Hanoi'),
('Electronics Plus',   '23 Pham Ngu Lao, HCM City'),
('Office Supply Corp', '56 Bach Dang, Da Nang'),
('Tech Solutions VN',  '90 Dinh Tien Hoang, Hanoi'),
('Gadget Galaxy',      '33 Vo Van Tan, HCM City'),
('IT Mega Store',      '67 Hoang Dieu, Hue'),
('Computing Center',   '11 Nguyen Van Linh, HCM City'),
('Business Tech VN',   '44 Ly Thai To, Hanoi'),
('ProTech Distributor','88 Nguyen Trai, HCM City'),
('Global IT Supply',   '55 Truong Chinh, Hanoi'),
('Alpha Electronics',  '22 Le Duan, Da Nang'),
('Beta IT Services',   '77 Quang Trung, HCM City'),
('Omega Tech Group',   '99 Nguyen Chi Thanh, Hanoi');

INSERT INTO Order1 (OrderDate, AgentID) VALUES
('2025-01-05', 1),
('2025-01-10', 2),
('2025-01-15', 3),
('2025-01-20', 4),
('2025-02-01', 5),
('2025-02-05', 6),
('2025-02-10', 7),
('2025-02-14', 8),
('2025-02-20', 1),
('2025-03-01', 2),
('2025-03-05', 9),
('2025-03-10', 10),
('2025-03-15', 3),
('2025-03-22', 11),
('2025-04-01', 4);

INSERT INTO OrderDetail (OrderID, ItemID, Quantity, UnitAmount) VALUES
(1, 1, 1,25000000),
(1, 4, 5,  350000),
(1, 8, 10, 450000),
(2, 2, 1,22000000),
(2, 5, 1,  900000),
(3, 3, 0,20000000),
(3, 6, 1,5500000),
(4, 4, 10,350000),
(4, 16,20, 85000),
(5, 9, 2,1800000), 
(5, 10,1,4200000),
(5, 12,2,1200000),
(6, 1, 1,25000000),
(6, 2, 1,22000000),
(7, 13,5,320000),
(7, 14,5,280000),
(8, 6, 2,5500000), 
(8, 7, 1,7200000),
(9, 11,4,1750000), 
(9, 12,4,1200000),
(9, 8, 10,450000),
(10,1, 1,25000000),
(10,3, 1,20000000),
(11,4, 15,350000),
(11,5, 5, 900000),
(12,16,30, 85000),
(12,17,1,6500000),
(13,1, 2,25000000),
(13,2, 1,22000000),
(13,7, 1,7200000),
(14,19,30, 65000),
(14,15,15,150000),
(14,13,10,320000),
(15,9, 3,1800000), 
(15,10,2,4200000),
(15,11,2,1750000);