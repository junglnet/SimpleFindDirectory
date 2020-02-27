
namespace Bochky.FindOrderFolder.Common.Entities
{
    public class FindRequest
    {
                
        public FindRequest(string request) =>            
            Request = request.ToLower();

        public string Request { get; }
    
    }
}
