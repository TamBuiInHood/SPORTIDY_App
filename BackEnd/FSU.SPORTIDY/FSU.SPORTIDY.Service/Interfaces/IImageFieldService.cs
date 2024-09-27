using FSU.SPORTIDY.Service.BusinessModel.ImageFieldBsModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IImageFieldService
    {
        Task<ImageFieldModel> Insert(IFormFile imageUrl, int playfieldId);
        Task<bool> Delete(int imageId);
        Task<ImageFieldModel> GetById(int imageId);
        Task<IEnumerable<ImageFieldModel>> Get(int playfieldId);
    }
}
