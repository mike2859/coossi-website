#nullable enable
namespace Coossi.Blazor.Data.Models;

public record HomePage(
    string Id,
    HomeHeroBlock Hero,
    IntroBlock Intro,
    ServicesBlock Services,
    ExpertiseBlock Expertise,
    ProcessBlock Process,
    ZonesBlock Zones,
    CtaBlock Cta
);

public record HomeHeroBlock(string Title, string LeadStrong, string TextStrong, List<HeroButton> Buttons, string RightIcon);
public record HeroButton(string Kind, string Href, string Icon, string Label);

public record IntroBlock(string Title, string Lead, string P1Strong, string P2Strong);

// ⚠️ Note: la propriété s’appelle *Cards* pour matcher le JSON "cards"
public record ServicesBlock(string Title, string Subtitle, List<HomeServiceCard> Cards);
public record HomeServiceCard(string Code, string Icon, string Title, string Subtitle, string Text);

public record ExpertiseBlock(string Title, string Subtitle, List<ExpertiseItem> Items);
public record ExpertiseItem(string Icon, string Title, string Text);

public record ProcessBlock(string Title, string Subtitle, List<ProcessStep> Steps);
public record ProcessStep(int Num, string Title, string Text);

public record ZonesBlock(string Title, string Subtitle, List<string> Badges, ZonesLink Link);
public record ZonesLink(string Href, string Icon, string Label);

public record CtaBlock(string Title, string Subtitle, List<CtaButton> Buttons);
public record CtaButton(string Kind, string Href, string Icon, string Label);
