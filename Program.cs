using EscapeRoom.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

// app.Use(async (context, next) => Ändring från pull
// {
// 	var allowedReferrer = new Uri("http://room1.runasp.net/");

// 	if (!context.Request.Headers.TryGetValue("Referer", out var refererValue) ||
// 		!Uri.TryCreate(refererValue.ToString(), UriKind.Absolute, out var refererUri) ||
// 		refererUri.Host != allowedReferrer.Host)
// 	{
// 		context.Response.Redirect("http://room1.runasp.net/");
// 		return;
// 	}
// 	await next();
// });

// app.Use(async (context, next) => gammal kod
// {
// 	var allowedReferrer = new Uri("http://room1.runasp.net/");

// 	if (!context.Request.Headers.TryGetValue("Referer", out var refererValue) ||
// 		!Uri.TryCreate(refererValue.ToString(), UriKind.Absolute, out var refererUri) ||
// 		refererUri.Host != allowedReferrer.Host)
// 	{
// 		context.Response.Redirect("http://room1.runasp.net/");
// 		return;
// 	}
// 	await next();
// });

if (!app.Environment.IsDevelopment())
{
	app.Use(async (context, next) =>
	{
		var allowedReferrer = new Uri("http://room1.runasp.net/");

		if (!context.Request.Headers.TryGetValue("Referer", out var refererValue) ||
			!Uri.TryCreate(refererValue.ToString(), UriKind.Absolute, out var refererUri) ||
			refererUri.Host != allowedReferrer.Host)
		{
			context.Response.Redirect("http://room1.runasp.net/");
			return;
		}
		await next();
	});
}

app.MapStaticAssets();
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
