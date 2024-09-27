using AutoMapper;
using Firebase.Storage;
using FSU.SPORTIDY.Common.FirebaseRootFolder;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.ImageFieldBsModels;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FSU.SPORTIDY.Service.Services
{
    public class ImageFieldService : IImageFieldService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageFieldService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int imageId)
        {
            Expression<Func<ImageField, bool>> filter = x => x.ImageId == imageId;
            var imageField = await _unitOfWork.ImageFieldRepository.GetByCondition(filter: filter);

            var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);

            var fileNameVideo = imageField.ImageUrl!.Substring(imageField.ImageUrl.LastIndexOf('/') + 1);
            fileNameVideo = fileNameVideo.Split('?')[0]; // Remove the query parameters
            var encodedFileVideo = Path.GetFileName(fileNameVideo);
            var fileNameVideoOfficial = Uri.UnescapeDataString(encodedFileVideo);

            // Delete the cover image club from Firebase Storage
            var fileRefVideo = firebaseStorage.Child(fileNameVideoOfficial);
            await fileRefVideo.DeleteAsync();

            _unitOfWork.ImageFieldRepository.Delete(imageField);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<IEnumerable<ImageFieldModel>> Get(int playfieldId)
        {
            Expression<Func<ImageField, bool>> filter = x => x.PlayFieldId == playfieldId;
            string includeProperties = "PlayField";
            var imageField = await _unitOfWork.ImageFieldRepository.GetAllNoPaging(filter: filter, includeProperties: includeProperties);
            var mapdto = _mapper.Map<IEnumerable<ImageFieldModel>>(imageField);
            return mapdto;
        }

        public async Task<ImageFieldModel> GetById(int imageId)
        {
            Expression<Func<ImageField, bool>> filter = x => x.ImageId == imageId;
            string includeProperties = "PlayField";
            var image = await _unitOfWork.ImageFieldRepository.GetByCondition(filter: filter, includeProperties: includeProperties);
            return _mapper.Map<ImageFieldModel>(image);
        }

        public async Task<ImageFieldModel> Insert(IFormFile imageUrl, int playfieldId)
        {
            Expression<Func<ImageField, bool>> filter = x => x.PlayFieldId == playfieldId;

            var imageExist = (await _unitOfWork.ImageFieldRepository.GetAllNoPaging(filter: filter, includeProperties: "PlayField")).OrderByDescending(x => x.ImageIndex).FirstOrDefault();
            var lastestIndex = imageExist.ImageIndex!.Value + 1;

            var image = new ImageField();
            image.ImageIndex = lastestIndex;
            image.PlayFieldId = playfieldId;
            string fileName = Path.GetFileName(lastestIndex.ToString());
            var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
            await firebaseStorage.Child($"{FirebaseRoot.PLAYFIELD}/{imageExist.PlayField!.PlayFieldCode}").Child(fileName).PutAsync(imageUrl.OpenReadStream());
            image.ImageUrl = await firebaseStorage.Child($"{FirebaseRoot.PLAYFIELD}/{imageExist.PlayField.PlayFieldCode}").Child(fileName).GetDownloadUrlAsync();

            await _unitOfWork.ImageFieldRepository.Insert(image);

            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result == true)
            {
                var mapdto = _mapper.Map<ImageFieldModel>(image);
                return mapdto;
            }
            return null!;
        }
    }
}
