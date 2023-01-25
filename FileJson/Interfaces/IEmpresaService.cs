using FileJson.Models;

namespace FileJson.Interfaces
{
    public interface IEmpresaService
    {
        Task<List<Empresa>> Get();
        Task<Empresa> Get(int id);
        Task<bool> Update(int id, Empresa empresa);
        Task<bool> Delete(int id);
    }
}
