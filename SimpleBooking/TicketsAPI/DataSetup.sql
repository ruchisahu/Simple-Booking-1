select * from Event

select * from Place

select * from Ticket

select * from [Transaction]

--delete from Event
begin transaction

declare @EventId int
select @EventId = isnull(max(Id),0) from Event
insert into Event(Id, Name, Description, PriceType, Category,Date, Price, ImageURL, PlaceId, TicketId) values(@EventId, 'Running around in Bellevue', 'Bellevue site seeing', 0, 2, '8/10/2018 10:00:00 AM', 100, 'http://running', 0, 0 )

declare @PlaceId int
select @PlaceId = isnull(max(Id),0) from Place
insert into Place(Id, Name, Address, City, State, ZipCode, Price, EventId) values(@PlaceId, 'Bellevue events', 'Bellevue Way', 'Bellevue', 'WA',98075,100, @EventId)

update Event set PlaceId = @PlaceId where Id= @EventId

-- commit transaction
-- rollback transaction
