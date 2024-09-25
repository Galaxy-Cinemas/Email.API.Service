namespace Galaxi.Bus.Message
{
    public record TickedCreated
    {
        public Guid FunctionId { get; init; }
        public int NumSeat { get; init; }
        public string Email { get; init; }
        public string UserName { get; init; }
    }

    public record MovieDetails
    {
        public Guid FunctionId { get; init; }
        public int NumSeat { get; init; }
        public string Email { get; init; }
    }
}
