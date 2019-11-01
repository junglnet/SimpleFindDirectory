
namespace Bochky.FindOrderFolder.Common
{
    public class FindRequest
    {
                
        public FindRequest(string request) =>            
            Request = request.ToLower();

        public string Request { get; }
    
    }
}
