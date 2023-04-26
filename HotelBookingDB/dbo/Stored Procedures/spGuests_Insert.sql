CREATE PROCEDURE [dbo].[spGuests_Insert]
	@firstname nvarchar(50),
	@lastName nvarchar(50)
AS

begin

	set nocount on;

	--Check to see if we need to create new guest
	if not exists (select 1 from dbo.Guests where FirstName = @firstname and LastName = @lastname)
	begin
		insert into dbo.Guests (FirstName, LastName) 
		values (@firstname, @lastName)
	end

	--Return guest
	select top 1 Id, FirstName, LastName 
	from dbo.Guests 
	where FirstName = @firstname and LastName = @lastName

end
	


	