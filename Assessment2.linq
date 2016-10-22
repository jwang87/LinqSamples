<Query Kind="Statements">
  <Connection>
    <ID>5bf884db-c94f-4918-8e5a-fa2c1274839f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eTools</Database>
  </Connection>
</Query>

var results = from x in Positions
			  select new
			  {
			  	Description = x.Description,
				NameList = Employees.Where(y=>y.Position.PositionID==x.PositionID)
				.Select(
				y=>
				new
						{
							Names = y.LastName + ", " + y.FirstName
						}
					)			
			  };
results.Dump();