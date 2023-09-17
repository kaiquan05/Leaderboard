using MySqlConnector;
using Leaderboard_System__Dell_.DAL;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<MySqlConnection>(_ =>
    new MySqlConnection(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();
// Add in SQL data
PointsDAL p = new PointsDAL();
try
{
	p.AssignLeaderboard();
}
catch
{
	Debug.WriteLine("Unable to access DB");
	Debug.WriteLine("Initialising DB");
	InitDB();
}
void InitDB()
{
	string connstr = "Server=localhost;User ID=root;Password=#;";
	string pathtosql = "./SQLScripts/Setup.sql";
	FileInfo f = new FileInfo(pathtosql);
	string script = f.OpenText().ReadToEnd();
	MySqlConnection conn = new MySqlConnection(connstr);
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = script;
	cmd.ExecuteNonQuery();
	conn.Close();
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
