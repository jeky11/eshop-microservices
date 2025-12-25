var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var gatewayAddress = builder.Configuration["ApiSettings:GatewayAddress"] ?? throw new InvalidOperationException("GatewayAddress is not configured");
builder.Services.AddRefitClient<ICatalogService>()
	.ConfigureHttpClient(c => { c.BaseAddress = new Uri(gatewayAddress); });
builder.Services.AddRefitClient<IBasketService>()
	.ConfigureHttpClient(c => { c.BaseAddress = new Uri(gatewayAddress); });
builder.Services.AddRefitClient<IOrderingService>()
	.ConfigureHttpClient(c => { c.BaseAddress = new Uri(gatewayAddress); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
	.WithStaticAssets();

app.Run();