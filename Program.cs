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
//builder.Services.AddScoped<IPrestationStore, FilePrestationStore>();
builder.Services.AddScoped<IKeywordStore, FileKeywordStore>();
builder.Services.AddScoped<ILegalStore, FileLegalStore>();
builder.Services.AddSingleton<IContentStore, FileContentStore>();


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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //  app.UseHttpsRedirection();
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

// Legacy /prestation/{id}
//app.MapGet("/prestation/{id:int}", async (int id, IPrestationIndex idx) =>
//{
//    var slug = await idx.GetSlugByIdAsync(id);
//    return slug is null
//        ? Results.NotFound()
//        : Results.Redirect($"/prestation/{slug}", permanent: true);
//});


app.MapGet("/prestation/{id:int}", (int id) =>
{
    // Table de correspondance ancienne ID -> nouveau slug
    var redirects = new Dictionary<int, string>
    {
        // À adapter selon ton ancien site
        // Exemples à titre indicatif :
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

    // redirection permanente (301) pour le SEO
    return Results.Redirect($"/prestation/{slug}", permanent: true);
});

/*
app.MapGet("/prestation/{id:int}", async (int id, IPrestationIndex store) =>
{
    var dto = await store.GetSlugByIdAsync(id);
    return dto is null
        ? Results.NotFound()
        : Results.Redirect($"/prestation/{dto.Slug}", true);
});*/

// // Legacy /prestation/{code}
// app.MapGet("/prestation/{code}", async (string code, IPrestationStore store) =>
// {
//     var dto = await store.GetBySlugAsync(code);
//     if (dto is not null) return Results.Redirect($"/prestation/{dto.Slug}", true);

//     string Slugify(string s)
//     {
//         var withDashes = Regex.Replace(s, "([a-z0-9])([A-Z])", "$1-$2");
//         var norm = withDashes.Normalize(NormalizationForm.FormD);
//         var sb = new StringBuilder();
//         foreach (var ch in norm)
//         {
//             var cat = CharUnicodeInfo.GetUnicodeCategory(ch);
//             if (cat == UnicodeCategory.NonSpacingMark) continue;
//             sb.Append(char.IsLetterOrDigit(ch) ? char.ToLowerInvariant(ch) : '-');
//         }
//         return Regex.Replace(sb.ToString(), "-+", "-").Trim('-');
//     }

//     var guess = Slugify(code);
//     dto = await store.GetBySlugAsync(guess);
//     return dto is null ? Results.NotFound() : Results.Redirect($"/prestation/{dto.Slug}", true);
// });

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
/*
app.MapGet("/sitemap.xml", async (HttpContext ctx, IPrestationStore store) =>
{
    var baseUrl = $"{ctx.Request.Scheme}://{ctx.Request.Host}";
    var prestas = await store.ListAsync();
    var lastmod = DateTime.UtcNow.ToString("yyyy-MM-dd");
    var xml = new StringBuilder();
    xml.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
    xml.Append("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

    void Add(string loc, string? priority = "0.8")
    {
        xml.Append("<url>");
        xml.Append($"<loc>{System.Security.SecurityElement.Escape(loc)}</loc>");
        xml.Append($"<lastmod>{lastmod}</lastmod>");
        xml.Append($"<priority>{priority}</priority>");
        xml.Append("</url>");
    }

    Add(baseUrl + "/");
    Add(baseUrl + "/prestations", "0.6");

    foreach (var p in prestas)
        Add(baseUrl + "/prestation/" + p.Slug);

    xml.Append("</urlset>");
    return Results.Text(xml.ToString(), "application/xml", Encoding.UTF8);
});
*/

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
