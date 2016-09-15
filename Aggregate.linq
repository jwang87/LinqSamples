<Query Kind="Statements">
  <Connection>
    <ID>f93f12bb-a4fb-4649-834b-dbb2c2763d8e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//sample for entity
//sample of entity navigation from child to parent on where
//remember that code is C# and this appropriate methods can be used .Equals()
var results = from x in Customers
where x.SupportRepIdEmployee.FirstName=="Jane" &&
x.SupportRepIdEmployee.LastName=="Peacock"
select new{
		   Name = x.LastName + ", " + x.FirstName,
		   City = x.City,
		   State = x.State,
		   Phone = x.Phone,
		   Email = x.Email
};
results.Dump();


//use of aggregate
//Count() count the number of the collection reference
//Sum() totals a specific field, thus you will likely need to use a delgate
//        to indicate the collection instance attribute to be used
//Average() average a specific field, thus you will likely need to use a delgate
//        to indicate the collection instance attribute to be used
var results = from x in Albums
orderby x.Title
where x.Tracks.Count() > 0
select new{
		   Title = x.Title,
		   NumofTracks = x.Tracks.Count(),
		   TotalAlbumPrice = x.Tracks.Sum(y => y.UnitPrice),
		   AvgA = (x.Tracks.Average(y => y.Milliseconds))/1000,
		   AvgB = x.Tracks.Average(y => y.Milliseconds/1000)
};
results.Dump();
//media type with the most track
var results = from x in MediaTypes
where x.Tracks.Count().Max()
select new{
	       MeidaTypeId = x.MediaTypeId,
		   Name = x.Name
		   };
results.Dump();