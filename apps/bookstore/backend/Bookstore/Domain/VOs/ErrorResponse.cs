namespace Domain.VOs;

public class ErrorResponse
{
    public List<Error> Errors { get; set; } = default!;
}

public record Error
{
    public string Property { get; set; } = default!;
    public string Message { get; set; } = default!;
    public string Code { get; set; } = default!;
}
