using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.ImageFieldBsModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IPlayFieldService
    {
        public Task<PageEntity<PlayFieldModel>> GetAllPlayField(string? searchKey, int? pageSize, int? pageIndex);
        public Task<PlayFieldModel> GetPlayFieldById(int playfieldId);

        public Task<bool> DeletePlayField(int PlayfieldId);
        public Task<PlayFieldModel> CreatePlayField(PlayFieldModel playFieldModel, List<IFormFile> listImage, IFormFile AvartarImage, List<string> subPlayfields );
        public Task<bool> UpdatePlayField(PlayFieldModel updateplayField);
        public Task<IEnumerable<PlayFieldModel>> GetPlayFieldsByUserId(int userId, int? pageSize, int? pageIndex);
        public Task<bool> UpdateAvatarImage(IFormFile avatarImage, int PlayFielId);

        public Task<bool> UpdateStatusPlayfield(int playfieldId, int status);
        public Task<bool> UpdatePlayFieldForAdmin(PlayFieldModel updateplayField);
    }
}
