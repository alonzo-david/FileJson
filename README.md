# FileJson
Upload file json and insert into database sql server

#Libraries
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.VisualStudio.Web.CodeGeneration
Microsoft.VisualStudio.Web.CodeGeneration.Core
Microsoft.VisualStudio.Web.CodeGeneration.Design


#Models
Scaffold-DbContext "Server=localhost\SQLEXPRESS;Database=File;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
