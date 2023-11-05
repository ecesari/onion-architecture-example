namespace CoolBlue.Products.Application.Common
{
    public interface IHttpService
    {
        Task<TResponse> CallAsync<TResponse>(HttpMethod httpMethod, string baseAddress, string endpoint);
        Task<TResponse> CallAsync<TRequest, TResponse>(HttpMethod httpMethod, string baseAddress, string endpoint, TRequest content);
    }
}
