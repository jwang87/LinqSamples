<Query Kind="Statements">
  <Connection>
    <ID>417ae290-8fe8-43af-8969-430868343d37</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

var PopWaiter = from x in Waiters
                where x.Bills.Count() == ((from y in Waiters select y.Bills.Count()).Max())
				select new
				{
				WaiterID = x.WaiterID,
				FN = x.FirstName,
				LN = x.LastName,
				//tbill = x.Bills.Count()
				};
PopWaiter.Dump();

//create a data set which contain the summary bills by waiter
var WaiterBills = from x in Waiters
                  orderby x.LastName, x.FirstName
				  select new {
				              Name = x.LastName + ", " + x.FirstName,
							  BillInfo = (from y in x.Bills
							              where y.BillItems.Count() > 0
							              select new {
										              BillID = y.BillID,
													  BillDate = y.BillDate,
													  TableID = y.TableID,
													  Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
										             }
										  )
				             };
WaiterBills.Dump();