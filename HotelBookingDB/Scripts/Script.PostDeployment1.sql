/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- Add rows in RoomTypes (Lookup Table)
if not exists (select 1 from dbo.RoomTypes)
begin
    insert into dbo.RoomTypes (Title, Description, Price) values
    ('Executive Suite', 'Two rooms each with 2 king-size, family room and a kitchen area.', 399),
    ('Double King Room', 'One room with 2 king-size beds and a window.', 299),
    ('Single King Room', 'One room with 1 king-size beds and a window.', 199),
    ('Double Queen Room', 'One room with 2 queen-size beds and a window.', 149),
    ('Single Room','One room with 1 queen-size bed and a window', 99)
end

-- Add rows in Rooms
if not exists(select 1 from dbo.Rooms)
begin
    declare @roomTypeId1 int;
    declare @roomTypeId2 int;
    declare @roomTypeId3 int;
    declare @roomTypeId4 int;
    declare @roomTypeId5 int;
    
    select @roomTypeId1 = Id from dbo.RoomTypes where Title = 'Executive Suite';
    select @roomTypeId2 = Id from dbo.RoomTypes where Title = 'Double King Room';
    select @roomTypeId3 = Id from dbo.RoomTypes where Title = 'Single King Room';
    select @roomTypeId4 = Id from dbo.RoomTypes where Title = 'Double Queen Room';
    select @roomTypeId5 = Id from dbo.RoomTypes where Title = 'Single Room';

    insert into dbo.Rooms (RoomTypeId, RoomNumber) values
    (@roomTypeId1, '101'),
    (@roomTypeId2, '102'),
    (@roomTypeId3, '103'),
    (@roomTypeId4, '104'),
    (@roomTypeId5, '105'),
    (@roomTypeId1, '201'),
    (@roomTypeId2, '202'),
    (@roomTypeId3, '203'),
    (@roomTypeId4, '204'),
    (@roomTypeId5, '205')
end
