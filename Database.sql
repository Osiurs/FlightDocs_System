create database FlightDocs_System
use FlightDocs_System

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính tự tăng, IDENTITY thay cho AUTO_INCREMENT
    Username VARCHAR(100) NOT NULL,        -- Tên đăng nhập
    Email VARCHAR(100) NOT NULL,			-- Email người dùng (phải có dạng @vietjetair.com)
	Password VARCHAR(255) NOT NULL,			-- Mật khẩu
    Role NVARCHAR(50) NOT NULL,            -- Vai trò trong hệ thống
    FullName NVARCHAR(255),                -- Tên đầy đủ của người dùng
    PhoneNumber VARCHAR(20),               -- Số điện thoại (nếu cần)
    Status NVARCHAR(50) DEFAULT 'Active',  -- Trạng thái tài khoản
    CreatedAt DATETIME DEFAULT GETDATE()   -- Thời gian tạo, dùng GETDATE() cho thời gian hiện tại
);

CREATE TABLE Flight (
    FlightID INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính tự tăng
    FlightNumber VARCHAR(20) NOT NULL,      -- Số hiệu chuyến bay
    DepartureTime DATETIME NOT NULL,        -- Thời gian khởi hành
    ArrivalTime DATETIME NOT NULL,          -- Thời gian hạ cánh
    Route NVARCHAR(100),                    -- Lộ trình (ví dụ: SGN-HAN)
    PlaneType NVARCHAR(50),                 -- Loại máy bay
    Status NVARCHAR(50) DEFAULT 'Scheduled', -- Trạng thái chuyến bay
    CreatedAt DATETIME DEFAULT GETDATE()    -- Thời gian tạo
);

CREATE TABLE UserFlightAssignment (
    AssignmentID INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính tự tăng
    UserID INT NOT NULL,                        -- Khóa ngoại liên kết với bảng Users
    FlightID INT NOT NULL,                      -- Khóa ngoại liên kết với bảng Flight
    RoleOnFlight NVARCHAR(50) NOT NULL,         -- Vai trò trên chuyến bay
    AssignmentDate DATE NOT NULL,               -- Ngày phân công

    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE CASCADE, -- Xóa người dùng thì xóa phân công
    FOREIGN KEY (FlightID) REFERENCES Flight(FlightID) ON DELETE CASCADE -- Xóa chuyến bay thì xóa phân công
);


CREATE TABLE Document (
    DocumentID INT PRIMARY KEY IDENTITY(1,1), -- Khóa chính tự tăng
    FlightID INT NOT NULL,                    -- Khóa ngoại liên kết với bảng Flight
    DocumentType NVARCHAR(100) NOT NULL,      -- Loại tài liệu (ví dụ: Flight Report, Safety Document)
    Content NVARCHAR(MAX) NOT NULL,           -- Nội dung tài liệu
    Status NVARCHAR(50) DEFAULT 'Pending',    -- Trạng thái tài liệu
    CreatedAt DATETIME DEFAULT GETDATE(),     -- Thời gian tạo
    ModifiedAt DATETIME DEFAULT GETDATE(),    -- Thời gian chỉnh sửa cuối cùng
    FOREIGN KEY (FlightID) REFERENCES Flight(FlightID) ON DELETE CASCADE, -- Xóa chuyến bay thì xóa tài liệu
);
