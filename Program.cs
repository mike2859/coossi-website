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

// Legacy /prestation/{id} - redirections SEO (mappings du site actuel)
app.MapGet("/prestation/{id:int}", (int id) =>
{
    var redirects = new Dictionary<int, string>
    {
        { 5, "coordination-ssi" },
        { 6, "creation-dossier-ssi" },
        { 7, "audit-diagnostic" },
        { 8, "assistance-moe" },
        { 9, "notices-securite-accessibilite" },
        { 10, "document-unique-evaluation" },
        { 11, "responsable-unique-securite" },
        { 12, "signaletique" }
    };

    if (!redirects.TryGetValue(id, out var slug))
    {
        return Results.NotFound();
    }

    return Results.Redirect($"/prestation/{slug}", permanent: true);
});

// Legacy /temoignages -> /references
app.MapGet("/temoignages", () => Results.Redirect("/references", permanent: true));

// Legacy /about-us -> /
app.MapGet("/about-us", () => Results.Redirect("/", permanent: true));

// Legacy /home -> /
app.MapGet("/home", () => Results.Redirect("/", permanent: true));

// Legacy /page/{category}/{id} -> redirection vers la prestation correspondante
app.MapGet("/page/{category}/{id:int}", (string category, int id) =>
{
    var categoryRedirects = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "CoordinationSSI", "coordination-ssi" },
        { "CreationDosierSSI", "creation-dossier-ssi" },
        { "AuditDiagnostic", "audit-diagnostic" },
        { "AssistanceMaitriseOuvrage", "assistance-moe" },
        { "NoticesSecurity", "notices-securite-accessibilite" },
        { "RedactionDocumentUnique", "document-unique-evaluation" },
        { "ResponsableUniqueSecurite", "responsable-unique-securite" },
        { "Signaletique", "signaletique" }
    };

    if (!categoryRedirects.TryGetValue(category, out var slug))
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

// sitemap.xml
app.MapGet("/sitemap.xml", (HttpContext ctx) =>
{
    var baseUrl = $"{ctx.Request.Scheme}://{ctx.Request.Host}";
    var lastmod = DateTime.UtcNow.ToString("yyyy-MM-dd");

    var routes = new List<(string url, string priority)>
    {
        ("/", "1.0"),
        ("/prestations", "0.9"),
        ("/services", "0.8"),
        ("/references", "0.8"),
        ("/ils-nous-font-confiance", "0.7"),
        ("/contact", "0.8"),
        ("/prestation/coordination-ssi", "0.8"),
        ("/prestation/creation-dossier-ssi", "0.8"),
        ("/prestation/audit-diagnostic", "0.8"),
        ("/prestation/document-unique-evaluation", "0.8"),
        ("/prestation/notices-securite-accessibilite", "0.8"),
        ("/prestation/signaletique", "0.8"),
        ("/prestation/assistance-moe", "0.8"),
        ("/prestation/responsable-unique-securite", "0.8"),
        ("/mentions-legales", "0.3"),
        ("/confidentialite", "0.3")
    };

    var xml = new StringBuilder();
    xml.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
    xml.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

    foreach (var (url, priority) in routes)
    {
        xml.AppendLine("  <url>");
        xml.AppendLine($"    <loc>{baseUrl}{url}</loc>");
        xml.AppendLine($"    <lastmod>{lastmod}</lastmod>");
        xml.AppendLine($"    <priority>{priority}</priority>");
        xml.AppendLine("  </url>");
    }

    xml.AppendLine("</urlset>");
    return Results.Text(xml.ToString(), "application/xml", Encoding.UTF8);
});

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
