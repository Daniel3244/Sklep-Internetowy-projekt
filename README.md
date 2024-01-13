<h1>Project overview</h1><br>
This project is a simulation of a Web Shop with computer parts. Application allows customers to browse products, add them to cart and order them. The project is built using ASP.NET MVC framework and utilizes a SQL Server database to store information about customers and products.<br>
<h2>Installation</h2>
1. Clone the project repository from GitHub and open it in Visual Studio.<br>
2. Open the Package Manager Console by going to Tools > NuGet Package Manager > Package Manager Console.<br>
3. Run the following command to install the Entity Framework package: Install-Package EntityFramework.<br>
4. Run the following command in the Package Manager Console to create the database: Update-Database<br>
5. The database will be created and seeded with sample data. You can check the tables in the SQL Server Object Explorer to verify that the database has been created successfully.<br>
6. Build the project and run it in the browser to verify that the application is working correctly.<br>
<h2>Structure of the project</h2>
<h3><b>Controllers:</b></h3>
AdminController - manages products as admin.<br>
CartController - Cart managment.<br>
HomeController - manages the home page.<br>
OrderController - Orders managment.<br>
ProductController - Products managment.<br>
<h3><b>Models</b></h3>
Order - is responsible for adress of person ordering products in the database.<br>
OrderProducts - is responsible for orders.<br>
Products - is responsible products in the database.<br>
<h3><b>Views</b></h3>
Admin - views responsible for admin side of the appliaction for example creating, editing and deleting products.<br>
Cart - views responsible for Cart managment.<br>
Home - home pages.<br>
Order - views reponsible Orders managment.<br>
Product - views that manage products.<br>
