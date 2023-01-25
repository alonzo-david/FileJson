using FileJson.Interfaces;
using FileJson.Models;
using Microsoft.EntityFrameworkCore;

namespace FileJson.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly FileContext _context;

        public EmpresaService(FileContext context)
        {
            _context = context;
        }

        public Task<List<Empresa>> Get()
        {
            var model = _context.Empresas.Include(e => e.Sucursales).ThenInclude(s => s.Colaboradores).ToListAsync();
            return model;
        }

        public Task<Empresa> Get(int id)
        {
            var model = _context.Empresas.Where(x => x.Id == id).Include(e => e.Sucursales).ThenInclude(s => s.Colaboradores).FirstOrDefaultAsync();
            return model;
        }

        public async Task<bool> Delete(int id)
        {
            var _empresa = _context.Empresas
                .Where(e => e.Id == id)
                .Include(s => s.Sucursales)
                .ThenInclude(c => c.Colaboradores)
                .FirstOrDefault();

            if (_empresa != null)
            {
                foreach (var sucursal in _empresa.Sucursales)
                {
                    foreach (var colaborador in sucursal.Colaboradores)
                    {
                        _context.Colaboradores.Remove(colaborador);
                    }
                    _context.Sucursales.Remove(sucursal);
                }
                _context.Empresas.Remove(_empresa);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> Update(int id, Empresa model)
        {
            var _empresa = _context.Empresas
                .Where(e => e.Id == model.Id)
                .Include(s => s.Sucursales)
                .ThenInclude(c => c.Colaboradores)
                .FirstOrDefault();

            if (_empresa != null)
            {
                _context.Entry(_empresa).CurrentValues.SetValues(model);

                // Update and Insert children
                foreach (var sucursal in model.Sucursales)
                {
                    var _sucursal = _empresa.Sucursales
                        .Where(s => s.Id == sucursal.Id && s.Id != default(int))
                        .SingleOrDefault();

                    if (_sucursal != null)
                    {
                        // Update child
                        _context.Entry(_sucursal).CurrentValues.SetValues(sucursal);

                        foreach (var colaborador in sucursal.Colaboradores)
                        {
                            var _colaborador = _sucursal.Colaboradores
                                .Where(c => c.Id == colaborador.Id && c.Id != default(int))
                                .SingleOrDefault();

                            if (_colaborador != null)
                            {
                                _context.Entry(_colaborador).CurrentValues.SetValues(colaborador);
                            }
                        }
                    }
                    else
                    {
                        _empresa.Sucursales.Add(sucursal);
                    }
                }

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }



        }

    }
}
