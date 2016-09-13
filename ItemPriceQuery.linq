<Query Kind="Statements">
  <Connection>
    <ID>417ae290-8fe8-43af-8969-430868343d37</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Waiters
var results = from x in Items
where x.CurrentPrice >= 5.00m
select new{
		x.Description,
		x.Calories
};
results.Dump();