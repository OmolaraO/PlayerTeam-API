using System.Text.Json.Serialization;

namespace WebApi.DTOs
{
    public class GenericResponse<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
