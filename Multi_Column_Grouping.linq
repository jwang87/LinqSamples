<Query Kind="Expression">
  <Connection>
    <ID>417ae290-8fe8-43af-8969-430868343d37</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//multi-column group
//grouping data placed in a local data set for further processing
//.Key allows you to have access the value(s) in your group key(s)
//if you have multiple group column they MUST be in an anoymous datatype
//to create a DTO type collection you can use .ToList() on the temp data set
// you can have a custom anoymous data collection by using a nested query

//Step A
from food in Items
	group food by new {food.MenuCategoryID, food.CurrentPrice}
	
//Step B DTO style data set
from food in Items
	group food by new {food.MenuCategoryID, food.CurrentPrice} into tempdataset
	select new{
				MenuCategory = tempdataset.Key.MenuCategoryID,
				CurrentPrice = tempdataset.Key.CurrentPrice,
				FoodItems = tempdataset.ToList()
			  }

//Step C Custom DTO style dataset
from food in Items
	group food by new {food.MenuCategoryID, food.CurrentPrice} into tempdataset
	select new{
				MenuCategory = tempdataset.Key.MenuCategoryID,
				CurrentPrice = tempdataset.Key.CurrentPrice,
				FoodItems = from x in tempdataset
							select new	{
											ItemID = x.ItemID,
											FoodDescription = x.Description,
											TimesServed = x.BillItems.Count()
										}
			  }