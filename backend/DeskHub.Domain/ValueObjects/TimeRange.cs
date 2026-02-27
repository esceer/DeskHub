using System.Text.Json.Serialization;

namespace DeskHub.Domain.ValueObjects;

public record TimeRange
{
    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }

    [JsonConstructor]
    public TimeRange(DateTimeOffset start, DateTimeOffset end)
    {
        if (end <= start)
            throw new ArgumentException("End must be after Start");

        Start = start;
        End = end;
    }

    public TimeSpan Duration => End - Start;

    public bool Overlaps(TimeRange other)
        => Start < other.End && End > other.Start;
}