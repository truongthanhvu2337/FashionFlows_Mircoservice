using System.Text.Json;

namespace PersonalScheduling.BuildingBlock.Application.Middleware
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Detail { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}