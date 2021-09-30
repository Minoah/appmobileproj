# appmobileproj
-Vào ==Visual Studio 2019== tạo project Asp.Net Core Web API 5.0  
-Sau khi tạo project vào Tools -> NuGet Package Manager -> Package Manager Console  
-Ở Package Manager Console lần lượt thực hiện các lệnh sau:  

Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design   
Install-Package Microsoft.EntityFrameworkCore.Tools  
Install-Package MySql.EntityFrameworkCore -Version 5.0.0+m8.0.23  
Install-Package Swashbuckle.AspNetCore   
Install-Package Microsoft.EntityFrameworkCore.SqlServer  
Install-Package Z.EntityFramework.Extensions.EFCore  

***
-Sau khi cài đặt các pakage cần thiết ta tiến hành sinh entity từ database. Để sinh entity từ Database thực hiện lệnh theo cú pháp sau ở Package Manager Console:  
Scaffold-DbContext "connection-string" MySql.EntityFrameworkCore -OutputDir Models

=> Lệnh này sẽ sinh ra các entity ở folder Models. “connection-string” là thông tin liên kết với database  
	
>VD:  
>Scaffold-DbContext "server=127.0.0.1;user id=root;port=3306;database=inventory;" MySql.EntityFrameworkCore -OutputDir Models   

Khi đã tạo được các entity ở folder Models, thì trong folder này có file tên là “inventoryContext”. Ta đổi tên file này thành “DataContext”.  
***
-Vào file appsetting.json thêm dòng sau  

  "ConnectionStrings": {  
    "InventoryDatabase": "server=127.0.0.1;user id=root;port=3306;database=inventory;"  
  }  

-Vào file Startup.cs có hàm ConfigureServices(IServiceCollection services)  
Trong hàm này thêm đoạn code dưới đây vào sau phần services.AddControllers():  

var connection = Configuration.GetConnectionString("InventoryDatabase");  
services.AddDbContextPool<DataContext>(options => options.UseMySQL(connection));   

