namespace Coossi.Blazor.Data.Models.Prestations;


public sealed record AuditDiagnosticPage(
    string Slug,
    SeoBlock Seo,
    HeroBlock Hero,

    AuditDiagnosticIntroBlock Intro,
    AuditDiagnosticPreservationBlock Preservation,
    AuditDiagnosticStepsBlock Steps,
    AuditDiagnosticIntroBlock Intervention,          // même forme (title + paragraphs)

    AuditDiagnosticEstablishmentsBlock Establishments,
    AuditDiagnosticCheckpointsBlock Checkpoints,
    AuditDiagnosticDeliverableBlock Deliverable,
    ContactBlock Contact,

    string CitiesIntro,
    List<string> Cities,
    List<string> Keywords,

    CtaBlock Cta
);


public sealed record AuditDiagnosticIntroBlock(string Title, List<string> Paragraphs);

public sealed record AuditDiagnosticPreservationBlock(string Title, List<string> Paragraphs);

public sealed record AuditDiagnosticStepsBlock(string Title, string Lead, List<AuditDiagnosticStepItem> Items);
public sealed record AuditDiagnosticStepItem(string Title, string Text);

public sealed record AuditDiagnosticEstablishmentsBlock(string Title, List<AuditDiagnosticEstablishmentItem> Items);
public sealed record AuditDiagnosticEstablishmentItem(string Title, string Icon, string Subtitle, string Small);

public sealed record AuditDiagnosticCheckpointsBlock(
    string Title,
    List<CardList> Left,
    List<CardList> Right
);
public sealed record CardList(string Title, List<string> Bullets);

public sealed record AuditDiagnosticDeliverableBlock(string Title, List<string> Bullets);


// public sealed record CardBlock(string Title, List<string>? Bullets = null, string? Text = null, string? Icon = null, string? Badge = null);