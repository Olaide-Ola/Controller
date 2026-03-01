using System.Text.Json;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//app.MapGet("/", () => "Hello world");

app.Run(async (HttpContext context) =>
{
    //string Id = context.Request.Query["Id"];
    //string name = context.Request.Query["Name"];

    //await context.Response.WriteAsync($"{Id}, {name}");

    //if (context.Request.Path.StartsWithSegments("/employees"))
    //{
    //    string id = context.Request.Query["id"];
    //    if (int.TryParse(id, out int employeeID))
    //    {
    //       var result = EmployeeRepository.GetEmployee(employeeID);
    //        if ( result is not null)
    //        {
    //            await context.Response.WriteAsync($"{result.Id}, {result.Name}, {result.Position}, {result.Salary}");
    //        }
    //    }
    //}


    context.Response.ContentType = "text/html";
    if (context.Request.Path.StartsWithSegments("/"))
    {
        if (context.Request.Method == "GET")
        {
            await context.Response.WriteAsync($"The Method is:  {context.Request.Method}<br>");
            await context.Response.WriteAsync($"The URL is: {context.Request.Path}<br>");

            await context.Response.WriteAsync("<ul>");
            await context.Response.WriteAsync($"Header:<br>");
            foreach (var key in context.Request.Headers.Keys)
            {
                await context.Response.WriteAsync($"<li><b>{key}</b>:  {context.Request.Headers[key]}<br></li>");
            }
            await context.Response.WriteAsync("</ul>");
        }
    }
    else if (context.Request.Path.StartsWithSegments("/employees"))
    {
        if (context.Request.Method == "GET")
        {
            if (context.Request.Query.ContainsKey("Id"))
            {
                string id = context.Request.Query["Id"];
                if (int.TryParse(id, out int employeeId))
                {
                    var empID = EmployeeRepository.GetEmployee(employeeId);
                    if (empID is not null)
                    {
                        await context.Response.WriteAsync($"{empID.Id}, {empID.Name}, {empID.Position}, {empID.Salary}");
                    }
                }
            }
            else
            {
                var emp = EmployeeRepository.GetEmployees();
                await context.Response.WriteAsync("<ul>");
                foreach (var item in emp)
                {
                    await context.Response.WriteAsync($"<li>ID: {item.Id}, Name: {item.Name}, Position: {item.Position}, Salary: {item.Salary}</li>");
                }
                await context.Response.WriteAsync("</ul>");
            }
            
        }
        else if (context.Request.Method == "POST")
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            var emp = JsonSerializer.Deserialize<Employee>(body);
            EmployeeRepository.AddEmployees(emp);
            context.Response.StatusCode = 201;
        }
        else if (context.Request.Method == "PUT")
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            var empl = JsonSerializer.Deserialize<Employee>(body);
            var result = EmployeeRepository.UpdateEmployee(empl);

            if (result)
            {
                context.Response.StatusCode = 204;
                await context.Response.WriteAsync("Employee updated successfull");

            }
            else
            {
                await context.Response.WriteAsync("Employee cannot be found");
            }
        }
        else if (context.Request.Method == "DELETE")
        {
            if (context.Request.Headers["Authorization"] == "Olaide")
            {
                using StreamReader reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();
                var emply = JsonSerializer.Deserialize<Employee>(body);
                EmployeeRepository.DeleteEmployee(emply);
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("You are not authorized to delete");
            }

        }
    }
    else if (context.Request.Path.StartsWithSegments("/redirect"))
    {
        context.Response.Redirect("/employees");
    }
    else
    {
        context.Response.StatusCode = 404;
    }
});
app.Run();
