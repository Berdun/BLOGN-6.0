namespace BLOGN.MVC.Helper
{
    public class ApiHttpClient
    {
        public HttpClient initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:7027");
            return Client;
        }
    }
}
