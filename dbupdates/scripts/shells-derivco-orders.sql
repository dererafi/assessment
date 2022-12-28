USE [AdventureWorks2016]
GO

create procedure pr_GetOrderSummary @StartDate datetime, @EndDate datetime, @EmployeeID int, @CustomerID int as
begin
    DECLARE @OrderSummary TABLE(
        [EmployeeFullName] [varchar](250) NULL,
        [ShipperCompanyName] [varchar](120) NULL,
        [CustomerCompanyName] [varchar](120) NULL,
        [NumberOfOrders] [int] NULL,
        [Date] [datetime] NULL,
        [TotalFreightCost] [decimal] NULL,
        [NumberOfDifferentProducts] [int] NULL,
        [TotalOrderValue] [decimal] NULL,
        [ID] [int] NULL
    );

    --Name lookup customer
     with CustomerCTE (EmployeeFullName, ShipperCompanyName, CustomerCompanyName, NumberOfOrders, Date, TotalFreightCost, TotalOrderValue, NumberOfDifferentProducts, ID) as  (
        select
            (select '') as EmployeeFullName,
            OrderStatistics.Name as ShipperCompanyName,
            Customer.Name as CustomerCompanyName,
            OrderStatistics.NumberOfOrders,
            OrderStatistics.Date,
            OrderStatistics.TotalFreightCost,
            OrderStatistics.TotalOrderValue,
            OrderStatistics.NumberOfDifferentProducts,
            Customer.CustomerID as ID
        from (
                select c.CustomerID, p.FirstName, p.LastName, s.Name
                from Sales.Customer c
                join Person.Person p on p.BusinessEntityID = c.PersonID
                join Sales.Store s on s.BusinessEntityID = c.StoreID
              ) as Customer
        left join (
                -- Order Statistics
                select c.CustomerID,
                       h.OrderDate as Date,
                       s.Name,
                       sum(h.Freight) as TotalFreightCost,
                       sum(h.TotalDue) as TotalOrderValue,
                       count(l.ProductID) as NumberOfDifferentProducts,
                       count(h.CustomerID) as NumberOfOrders
                from Sales.Customer c
                join Sales.SalesOrderHeader h on h.CustomerID = c.CustomerID
                join Sales.SalesOrderDetail l on h.SalesOrderID = l.SalesOrderID
                join Purchasing.ShipMethod s on s.ShipMethodID = h.ShipMethodID
                group by  c.CustomerID, h.OrderDate, s.Name
            )  as OrderStatistics on Customer.CustomerID = OrderStatistics.CustomerID
    ),
    EmployeeCTE (EmployeeFullName, ShipperCompanyName, CustomerCompanyName, NumberOfOrders, Date, TotalFreightCost, TotalOrderValue, NumberOfDifferentProducts, ID) as  (
        select
               Employee.FirstName + ' ' + Employee.LastName as EmployeeFullName,
               OrderStatistics.Name as ShipperCompanyName,
               (select '') as CustomerCompanyName,
               OrderStatistics.NumberOfOders as NumberOfOders,
               OrderStatistics.Date,
               OrderStatistics.TotalFreightCost as TotalFreightCost,
               OrderStatistics.TotalOrderValue as TotalOrderValue,
               OrderStatistics.NumberOfDifferentProducts as NumberOfDifferentProducts,
               Employee.BusinessEntityID as ID
        from (
                select e.BusinessEntityID,
                       p.FirstName,
                       p.LastName
                from Sales.SalesPerson e
                join Person.Person p on e.BusinessEntityID = p.BusinessEntityID
              ) as Employee
        left join (
                -- Order Statistics
                select
                       s.BusinessEntityID,
                       h.OrderDate as Date, m.Name,
                       sum(h.Freight) as TotalFreightCost,
                       sum(h.TotalDue) as TotalOrderValue,
                       count(l.ProductID) as NumberOfDifferentProducts,
                       count(h.SalesOrderID) as NumberOfOders
                from Sales.SalesPerson s
                join Sales.SalesOrderHeader h on h.SalesPersonID = s.BusinessEntityID
                join Sales.SalesOrderDetail l on h.SalesOrderID = l.SalesOrderID
                join Purchasing.ShipMethod m on m.ShipMethodID = h.ShipMethodID
                group by s.BusinessEntityID, h.OrderDate, m.Name
        )  as OrderStatistics on Employee.BusinessEntityID = OrderStatistics.BusinessEntityID

    )

    insert into  @OrderSummary( EmployeeFullName, ShipperCompanyName, CustomerCompanyName, NumberOfOrders, Date, TotalFreightCost, NumberOfDifferentProducts,TotalOrderValue, ID )
    select  EmployeeFullName, ShipperCompanyName, CustomerCompanyName, NumberOfOrders, Date, TotalFreightCost, NumberOfDifferentProducts, TotalOrderValue, ID
    from (
            select EmployeeFullName, ShipperCompanyName, CustomerCompanyName, NumberOfOrders, Date, TotalFreightCost, TotalOrderValue, NumberOfDifferentProducts, ID
            from CustomerCTE cust
            where cust.ID = @CustomerID and cust.Date <= @StartDate and cust.Date >= @EndDate
            union all
            select EmployeeFullName, ShipperCompanyName, CustomerCompanyName, NumberOfOrders, Date, TotalFreightCost, TotalOrderValue ,NumberOfDifferentProducts, ID
            from EmployeeCTE emp
            where emp.ID = @EmployeeID and emp.Date <= @StartDate and emp.Date >= @EndDate
         ) as dataset

    select * from @OrderSummary

end

exec pr_GetOrderSummary null, null, null, 29825