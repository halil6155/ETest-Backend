using Core.Entities.Abstract;
using Newtonsoft.Json;

namespace Core.Entities.Concrete
{
    public class ErrorDetails:IError
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}