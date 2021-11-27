﻿namespace OneMind.Data.Entities;

record Organization
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string? Description { get; init; }
    public int LeaderId { get; init; }
    public OrganizationVisibility Visibility { get; init; }
    public int CreatedById { get; init; }
    public DateTimeOffset CreatedOn { get; init; }

    public User Leader { get; init; } = default!;
    public User CreaatedBy { get; init; } = default!;
}
