using FileJson.Interfaces;
using FileJson.Models;
using Newtonsoft.Json;
using System.Text;

namespace FileJson.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly FileContext _context;

        public FileUploadService(FileContext context)
        {
            _context = context;
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            //string path = "";
            try
            {
                if (file.Length > 0)
                {
                    var extension = Path.GetExtension(file.FileName);
                    if (extension.ToLower() != ".json")
                    {
                        throw new Exception("Extension no soportado, unicamente archivos .json");
                    }

                    var result = new StringBuilder();
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                            result.Append(reader.ReadLine());
                    }

                    var dataViewModel = JsonConvert.DeserializeObject<FileViewModel>(result.ToString());

                    _context.Add(dataViewModel.Empresa);
                    await _context.SaveChangesAsync();


                    //path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    //if (!Directory.Exists(path))
                    //{
                    //    Directory.CreateDirectory(path);
                    //}
                    //using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    //{
                    //    await file.CopyToAsync(fileStream);
                    //}
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
