#nullable enable
using System.Text.Json;

namespace Coossi.Blazor.Data.Models;

public static class PrestationSectionExtensions
{
    public static IReadOnlyList<string> AsBullets(this PrestationSection s)
    {
        if (s.Items is not JsonElement el) return Array.Empty<string>();
        if (el.ValueKind != JsonValueKind.Array) return Array.Empty<string>();

        var list = new List<string>();
        foreach (var item in el.EnumerateArray())
        {
            if (item.ValueKind == JsonValueKind.String)
                list.Add(item.GetString() ?? "");
        }
        return list;
    }

    public static IReadOnlyList<CardItem> AsCards(this PrestationSection s)
    {
        if (s.Items is not JsonElement el) return Array.Empty<CardItem>();
        if (el.ValueKind != JsonValueKind.Array) return Array.Empty<CardItem>();

        var list = new List<CardItem>();
        foreach (var item in el.EnumerateArray())
        {
            if (item.ValueKind == JsonValueKind.Object)
            {
                // lecture tolérante
                string? title  = item.TryGetProperty("title", out var t) ? t.GetString() : null;
                string? text   = item.TryGetProperty("text", out var tx) ? tx.GetString() : null;
                string? icon   = item.TryGetProperty("icon", out var ic) ? ic.GetString() : null;
                string? badge  = item.TryGetProperty("badge", out var bd) ? bd.GetString() : null;

                List<string>? bullets = null;
                if (item.TryGetProperty("bullets", out var bl) && bl.ValueKind == JsonValueKind.Array)
                {
                    bullets = bl.EnumerateArray()
                                .Where(x => x.ValueKind == JsonValueKind.String)
                                .Select(x => x.GetString() ?? "")
                                .ToList();
                }
                list.Add(new CardItem(title, text, bullets, icon, badge));
            }
        }
        return list;
    }

    public static IReadOnlyList<StepItem> AsSteps(this PrestationSection s)
    {
        if (s.Items is not JsonElement el) return Array.Empty<StepItem>();
        if (el.ValueKind != JsonValueKind.Array) return Array.Empty<StepItem>();

        var list = new List<StepItem>();
        foreach (var item in el.EnumerateArray())
        {
            if (item.ValueKind == JsonValueKind.Object)
            {
                string? title = item.TryGetProperty("title", out var t) ? t.GetString() : null;
                string? text  = item.TryGetProperty("text",  out var tx) ? tx.GetString() : null;
                list.Add(new StepItem(title, text));
            }
        }
        return list;
    }
}
