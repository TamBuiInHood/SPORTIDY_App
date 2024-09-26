using AutoMapper;
using Firebase.Storage;
using FSU.SPORTIDY.Common.FirebaseRootFolder;
using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.ImageFieldBsModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace FSU.SPORTIDY.Service.Services
{
    public class PlayFieldService : IPlayFieldService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlayFieldService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PlayFieldModel> CreatePlayField(PlayFieldModel playFieldModel, List<IFormFile> listImage, IFormFile AvatarImage)
        {
            var playfield = new PlayField();
            _mapper.Map(playFieldModel, playfield);
            playfield.PlayFieldCode = Guid.NewGuid().ToString();
            playfield.Status = (int)PlayFieldStatus.WaitingAccept;

            // luu hinh anh cua playfield
            // check-day hinh len server trc roi moi Add playfield sau 
            // nen tat ten hinh theo code - code nen dc lay tu playfield + Code de lan sau update vao tam hinh do luon
            if (AvatarImage != null)
            {
                string fileName = Path.GetFileName(playfield.PlayFieldCode);
                var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                await firebaseStorage.Child($"{FirebaseRoot.PLAYFIELD}/{playfield.PlayFieldCode}").Child(fileName).PutAsync(AvatarImage.OpenReadStream());
                playfield.AvatarImage = await firebaseStorage.Child($"{FirebaseRoot.PLAYFIELD}/{playfield.PlayFieldCode}").Child(fileName).GetDownloadUrlAsync();
            }

            if (!listImage.IsNullOrEmpty())
            {
                var indexOfImage = 1;
                foreach (var image in listImage)
                {
                    var eachImageAdd = new ImageField();
                    string fileName = Path.GetFileName(indexOfImage.ToString());
                    eachImageAdd.ImageIndex = indexOfImage;
                    var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                    await firebaseStorage.Child($"{FirebaseRoot.PLAYFIELD}/{playfield.PlayFieldCode}").Child(fileName).PutAsync(image.OpenReadStream());
                    eachImageAdd.ImageUrl = await firebaseStorage.Child($"{FirebaseRoot.PLAYFIELD}/{playfield.PlayFieldCode}").Child(fileName).GetDownloadUrlAsync();
                    playfield.ImageFields.Add(eachImageAdd);
                    indexOfImage++;
                }

            }

            await _unitOfWork.PlayFieldRepository.Insert(playfield);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result == true)
            {
                var mapdto = _mapper.Map<PlayFieldModel>(playfield);
                return mapdto;
            }
            return null!;
        }

        public async Task<bool> DeletePlayField(int playFieldID)
        {
            var playfield = _unitOfWork.PlayFieldRepository.GetByID(playFieldID);
            if (playfield == null)
            {
                throw new Exception("This playfield not exist");
            }
            _unitOfWork.PlayFieldRepository.Delete(playfield);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<PageEntity<PlayFieldModel>> GetAllPlayField(string? searchKey, int? pageSize, int? pageIndex)
        {
            Expression<Func<PlayField, bool>> filter = !string.IsNullOrEmpty(searchKey)
    ? x => (x.PlayFieldName!.ToLower().Contains(searchKey.ToLower())
            || x.Address!.ToLower().Contains(searchKey.ToLower()))
            && x.Status != (int)PlayFieldStatus.Deleted
    : x => x.Status != (int)PlayFieldStatus.Deleted;

            Func<IQueryable<PlayField>, IOrderedQueryable<PlayField>> orderBy = q => q.OrderBy(x => x.PlayFieldName);

            var entities = await _unitOfWork.PlayFieldRepository.Get(filter: filter, orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize);
            var pagin = new PageEntity<PlayFieldModel>();
            pagin.List = _mapper.Map<IEnumerable<PlayFieldModel>>(entities);
            pagin.TotalRecord = await _unitOfWork.PlayFieldRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, pageSize!.Value);
            return pagin;
        }

        public async Task<PlayFieldModel> GetPlayFieldById(int playfiedId)
        {
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == playfiedId && x.Status != (int)PlayFieldStatus.Deleted;
            string includeProperties = "ImageFields,User";
            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter: filter, includeProperties: includeProperties);
            var dto = _mapper.Map<PlayFieldModel?>(playfield);
            return dto!;
        }

        public async Task<IEnumerable<PlayFieldModel>> GetPlayFieldsByUserId(int userId, int? pageSize, int? pageIndex)
        {
            Expression<Func<PlayField, bool>> filter = x => x.UserId == userId;

            Func<IQueryable<PlayField>, IOrderedQueryable<PlayField>> orderBy = q => q.OrderBy(x => x.PlayFieldName);

            string includeProperties = "ImageFields,User";
            var playfield = await _unitOfWork.PlayFieldRepository.Get(filter: filter, includeProperties: includeProperties, pageSize: pageSize, pageIndex: pageIndex);
            var dto = _mapper.Map<IEnumerable<PlayFieldModel>?>(playfield);
            return dto!;
        }

        public async Task<bool> UpdateAvatarImage(IFormFile avatarImage, int PlayFielId)
        {
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == PlayFielId && x.Status != (int)PlayFieldStatus.Deleted && x.Status != (int)PlayFieldStatus.WaitingAccept;
            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter);
            if (avatarImage != null)
            {
                // update barcode vo url co san
                var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);
                await firebaseStorage.Child($"{FirebaseRoot.PLAYFIELD}/{playfield.PlayFieldCode}").Child(playfield.PlayFieldCode).PutAsync(avatarImage.OpenReadStream());
            }
            return true;
        }

        public async Task<bool> UpdatePlayField(PlayFieldModel updateplayField)
        {
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == updateplayField.PlayFieldId && x.Status != (int)PlayFieldStatus.Deleted && x.Status != (int)PlayFieldStatus.WaitingAccept;
            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter);
            if (playfield == null)
            {
                throw new Exception("This playfield is not exist");
            }
            if (!updateplayField.PlayFieldName.IsNullOrEmpty())
            {
                playfield.PlayFieldName = updateplayField.PlayFieldName;
            }
            if (!updateplayField.Price.HasValue)
            {
                playfield.Price = updateplayField.Price;
            }
            if (!updateplayField.Address.IsNullOrEmpty())
            {
                playfield.Address = updateplayField.Address;
            }
            if (!updateplayField.OpenTime.HasValue)
            {
                playfield.OpenTime = updateplayField.OpenTime;
            }
            if (!updateplayField.CloseTime.HasValue)
            {
                playfield.CloseTime = updateplayField.CloseTime;
            }

            _unitOfWork.PlayFieldRepository.Update(playfield);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<bool> UpdateStatusPlayfield(int playfieldId, int status)
        {
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == playfieldId && x.Status != (int)PlayFieldStatus.Deleted;
            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter);
            if (playfield == null)
            {
                throw new Exception("This playfield is not exist");
            }
            playfield.Status = status;
            _unitOfWork.PlayFieldRepository.Update(playfield);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }
    }
}
