using ReposteriaLili_Front.Models.ReqResModels;

namespace ReposteriaLili_Front.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse responseMoldel { get; set; }

        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
