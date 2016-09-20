<Query Kind="Program">
  <Connection>
    <ID>417ae290-8fe8-43af-8969-430868343d37</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	//List of bill counts for all waiters
	//This query will create a flat dataset
	//The columns are native data typesd (ie int, string, ...)
	//One is not concerned with repeated data in a column
	//Instead of using an anoymous datatype (new{...})
	//We wish to use a defined class definition
	var PopWaiter = from x in Waiters
				select new WaiterBillCount
				{
					Name = x.LastName + ", " + x.FirstName,
					Tcount = x.Bills.Count()
				};
	PopWaiter.Dump();

	//create a data set which contain the summary bills by waiter
	var WaiterBills = from x in Waiters
					  where x.LastName.Contains("k")
                  	  orderby x.LastName, x.FirstName
				      select new WaiterBills{
				              	  Name = x.LastName + ", " + x.FirstName,
								  TotalBillCount = x.Bills.Count(),
							  	  BillInfo = (from y in x.Bills
							              	  where y.BillItems.Count() > 0
							              	  select new BillItemSummary{
										              	  BillID = y.BillID,
													  	  BillDate = y.BillDate,
													  	  TableID = y.TableID,
													  	  Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
										             	 }
										  	  ).ToList()
				             	   };
	WaiterBills.Dump();
}

// Define other methods and classes here
//An example of a POCO class (Flat)
public class WaiterBillCount
{
	//whatever recieving field on your query in your Select
	//appears as a property in this class
	public string Name{get; set;}
	public int Tcount{get; set;}
	
}

public class BillItemSummary
{
	public int BillID{get; set;}
	public DateTime BillDate{get; set;}
	public int? TableID{get; set;}
	public decimal Total{get; set;}
}

//An example of a DTO class (structured)
public class WaiterBills
{
	public string Name{get; set;}
	public int TotalBillCount{get; set;}
	public List<BillItemSummary> BillInfo {get; set;}
}
