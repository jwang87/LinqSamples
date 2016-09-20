<Query Kind="Expression">
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
(from x in Albums
orderby x.Title
where x.Tracks.Count() > 0
select new{
		   Title = x.Title,
		   NumofTracks = x.Tracks.Count(),
		   TotalAlbumPrice = x.Tracks.Sum(y => y.UnitPrice),
		   AvgA = (x.Tracks.Average(y => y.Milliseconds))/1000,
		   AvgB = x.Tracks.Average(y => y.Milliseconds/1000)
}).Union(
from x in Albums
orderby x.Title
where x.Tracks.Count() == 0
select new{
		   Title = x.Title,
		   NumofTracks = 0,
		   TotalAlbumPrice = 0,
		   AvgA = 0,
		   AvgB = 0
})
//results.Dump();
//media type with the most track

//can this set of statements be written as one complete query
//the answer is possibly, and in this case yes
//in this example maxcount could be exchanged for the query that
//   actually create the value in the first place

var results = from x in MediaTypes
where x.Tracks.Count()==((from y in MediaTypes select y.Tracks.Count()).Max())
select new{
	       MeidaTypeId = x.MediaTypeId,
		   Name = x.Name
		   };
results.Dump();

//using the method
var popularMeidaTypesSubMethod = from x in MediaTypes
							     where x.Tracks.Count() ==
								 MediaTypes.Select(mt => mt.Tracks.Count()).Max()
								 select new{
	       MeidaTypeId = x.MediaTypeId,
		   Name = x.Name
		   };
popularMeidaTypesSubMethod.Dump();


//when you need to use maltiple steps
//to solve a problem, wwitch your langauge
//choice to either Statement(s) or Program

//the results of each query will now be save in a variable
//the variable can then be used in future queries

var maxcount = (from x in MediaTypes
	select x.Tracks.Count()).Max();
//to dispaly the contents of a variable in LinqPad
//you use the method .Dump()
maxcount.Dump();
var results = from x in MediaTypes
			  where x.Tracks.Count() == maxcount
select new{
	       MeidaTypeId = x.MediaTypeId,
		   Name = x.Name
		   };
results.Dump();