using Coossi.Blazor.Components;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.ResponseCompression;
using Coossi.Blazor.Data;
using System.Text;
using Coossi.Blazor.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddSingleton<IPrestationIndex, FilePrestationIndex>();
builder.Services.AddScoped<IKeywordStore, FileKeywordStore>();
builder.Services.AddScoped<ILegalStore, FileLegalStore>();
builder.Services.AddSingleton<IContentStore, FileContentStore>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddResponseCompression(opts =>
{
    opts.EnableForHttps = true;
    opts.Providers.Add<GzipCompressionProvider>();
    opts.Providers.Add<BrotliCompressionProvider>();
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseResponseCompression();

// Static files + cache long
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        if (ctx.File.PhysicalPath?.Contains("wwwroot/images") == true)
        {
            ctx.Context.Response.Headers["Cache-Control"] = "public,max-age=31536000,immutable";
        }
    }
});




// Rewrite images
var rewrite = new RewriteOptions()
    .AddRedirect("^Images/(.*)$", "images/$1", 301)
    .AddRedirect("^images/slider_Images/(.*)$", "images/slider-images/$1", 301)
    .AddRedirect("Images//Partners/partenaire_6.png", "images/partners/partenaire-6.webp", 301);
app.UseRewriter(rewrite);

// Legacy /prestation/{id} - redirections SEO
app.MapGet("/prestation/{id:int}", (int id) =>
{
    var redirects = new Dictionary<int, string>
    {
        { 5, "coordination-ssi" },
        { 6, "creation-dossier-ssi" },
        { 7, "audit-diagnostic" },
        { 8, "duerp" },
        { 9, "notice-securite-accessibilite" },
        { 10, "signaletique" },
        { 11, "assistance-moe" },
        { 12, "responsable-unique-securite" }
    };

    if (!redirects.TryGetValue(id, out var slug))
    {
        return Results.NotFound();
    }

    return Results.Redirect($"/prestation/{slug}", permanent: true);
});

// robots.txt
app.MapGet("/robots.txt", (HttpContext ctx) =>
{
    var sb = new StringBuilder();
    sb.AppendLine("User-agent: *");
    sb.AppendLine("Allow: /");
    sb.AppendLine("Sitemap: " + $"{ctx.Request.Scheme}://{ctx.Request.Host}/sitemap.xml");
    return Results.Text(sb.ToString(), "text/plain");
});

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
