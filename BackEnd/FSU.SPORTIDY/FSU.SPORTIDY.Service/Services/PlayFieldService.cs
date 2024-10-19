using AutoMapper;
using Firebase.Storage;
using FSU.SPORTIDY.Common.FirebaseRootFolder;
using FSU.SPORTIDY.Common.Status;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

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

        public async Task<PlayFieldModel> CreatePlayField(PlayFieldModel playFieldModel, List<IFormFile> listImage, IFormFile AvatarImage, List<string> subPlayfields)
        {
            var playfield = new PlayField();
            User getUser = null;
            if (playFieldModel.UserId != null)
            {
                 getUser = await _unitOfWork.UserRepository.GetUserByIdAsync(playFieldModel.UserId.Value);
            }
            _mapper.Map(playFieldModel, playfield);
            playfield.PlayFieldCode = Guid.NewGuid().ToString();
            playfield.Status = (int)PlayFieldStatus.WAITINGACCEPT;

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


            playfield.User = getUser;
            await _unitOfWork.PlayFieldRepository.Insert(playfield);

            foreach (var nameSubFieldToAdd in subPlayfields)
            {
                var subplayfield = new PlayField();
                _mapper.Map(playFieldModel, subplayfield);
                subplayfield.PlayFieldCode = Guid.NewGuid().ToString();
                subplayfield.Status = (int)PlayFieldStatus.WAITINGACCEPT;

                subplayfield.PlayFieldName = nameSubFieldToAdd;
                subplayfield.UserId = playfield.UserId;
                subplayfield.User = getUser;
                await _unitOfWork.PlayFieldRepository.Insert(subplayfield);
            }


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
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == playFieldID;
            string includeProperties = "ListSubPlayFields";
            var playfieldDelete = await _unitOfWork.PlayFieldRepository.GetByCondition(filter, includeProperties);
            if (playfieldDelete == null)
            {
                throw new Exception("This playfield not exist");
            }
            playfieldDelete.Status = (int)PlayFieldStatus.DELTED;

            var firebaseStorage = new FirebaseStorage(FirebaseConfig.STORAGE_BUCKET);

            foreach (var item in playfieldDelete.ImageFields)
            {
                var fileNameVideo = item.ImageUrl!.Substring(item.ImageUrl.LastIndexOf('/') + 1);
                fileNameVideo = fileNameVideo.Split('?')[0]; // Remove the query parameters
                var encodedFileVideo = Path.GetFileName(fileNameVideo);
                var fileNameVideoOfficial = Uri.UnescapeDataString(encodedFileVideo);

                // Delete the cover image club from Firebase Storage
                var fileRefVideo = firebaseStorage.Child(fileNameVideoOfficial);
                await fileRefVideo.DeleteAsync();
            }
            playfieldDelete.ImageFields.Clear();
            _unitOfWork.PlayFieldRepository.Update(playfieldDelete);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<PageEntity<PlayFieldModel>> GetAllPlayField(string? searchKey, int? pageSize, int? pageIndex)
        {
            Expression<Func<PlayField, bool>> filter = !string.IsNullOrEmpty(searchKey)
    ? x => (x.PlayFieldName!.ToLower().Contains(searchKey.ToLower())
            || x.Address!.ToLower().Contains(searchKey.ToLower()))
            && x.Status != (int)PlayFieldStatus.DELTED && x.IsDependency == null
    : x => x.Status != (int)PlayFieldStatus.DELTED && x.IsDependency == null;

            Func<IQueryable<PlayField>, IOrderedQueryable<PlayField>> orderBy = q => q.OrderBy(x => x.PlayFieldName);

            string includePropoperties = "User,Sport";

            var entities = await _unitOfWork.PlayFieldRepository.Get(filter: filter, orderBy: orderBy, pageIndex: pageIndex, pageSize: pageSize, includeProperties: includePropoperties);
            var pagin = new PageEntity<PlayFieldModel>();
            pagin.List = _mapper.Map<IEnumerable<PlayFieldModel>>(entities);
            pagin.TotalRecord = await _unitOfWork.PlayFieldRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, pageSize!.Value);
            return pagin;
        }

        public async Task<PlayFieldModel> GetPlayFieldById(int playfiedId)
        {
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == playfiedId && x.Status != (int)PlayFieldStatus.DELTED;
            string includeProperties = "User,ImageFields,ListSubPlayFields";
            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter: filter, includeProperties: includeProperties);
            var dto = _mapper.Map<PlayFieldModel?>(playfield);
            return dto!;
        }

        public async Task<IEnumerable<PlayFieldModel>> GetPlayFieldsByUserId(int userId, int? pageSize, int? pageIndex)
        {
            Expression<Func<PlayField, bool>> filter = x => x.UserId == userId && x.IsDependency == null && x.Status != (int)PlayFieldStatus.DELTED;

            Func<IQueryable<PlayField>, IOrderedQueryable<PlayField>> orderBy = q => q.OrderBy(x => x.PlayFieldName);

            string includeProperties = "ImageFields,ListSubPlayFields";
            var playfield = await _unitOfWork.PlayFieldRepository.Get(filter: filter, includeProperties: includeProperties, pageSize: pageSize, pageIndex: pageIndex);
            var dto = _mapper.Map<IEnumerable<PlayFieldModel>?>(playfield.ToList());
            return dto!;
        }

        public async Task<bool> UpdateAvatarImage(IFormFile avatarImage, int PlayFielId)
        {
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == PlayFielId && x.Status != (int)PlayFieldStatus.DELTED && x.Status != (int)PlayFieldStatus.WAITINGACCEPT && x.IsDependency == null;
            string includeProperties = "ListSubPlayFields";
            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter, includeProperties);
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
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == updateplayField.PlayFieldId && x.Status != (int)PlayFieldStatus.DELTED && x.Status != (int)PlayFieldStatus.WAITINGACCEPT && x.IsDependency == null;
            string includeProperties = "ListSubPlayFields";
            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter, includeProperties);
            if (playfield == null)
            {
                throw new Exception("This playfield is not exist");
            }
            if (!updateplayField.PlayFieldName.IsNullOrEmpty())
            {
                playfield.PlayFieldName = updateplayField.PlayFieldName;
            }
            if (updateplayField.Price.HasValue)
            {
                playfield.Price = updateplayField.Price;
                playfield.ListSubPlayFields!.ToList().ForEach(x => x.Price = updateplayField.Price);
            }
            if (!updateplayField.Address.IsNullOrEmpty())
            {
                playfield.Address = updateplayField.Address;
                playfield.ListSubPlayFields!.ToList().ForEach(x => x.Address = updateplayField.Address);

            }
            if (updateplayField.OpenTime.HasValue)
            {
                playfield.OpenTime = updateplayField.OpenTime;
                playfield.ListSubPlayFields!.ToList().ForEach(x => x.OpenTime = updateplayField.OpenTime);

            }
            if (updateplayField.CloseTime.HasValue)
            {
                playfield.CloseTime = updateplayField.CloseTime;
                playfield.ListSubPlayFields!.ToList().ForEach(x => x.CloseTime = updateplayField.CloseTime);

            }

            _unitOfWork.PlayFieldRepository.Update(playfield);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<bool> UpdateStatusPlayfield(int playfieldId, int status)
        {
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == playfieldId && x.Status != (int)PlayFieldStatus.DELTED && x.IsDependency == null;
            string includeProperties = "ListSubPlayFields";

            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter, includeProperties);
            if (playfield == null)
            {
                throw new Exception("This playfield is not exist");
            }
            playfield.Status = status;
            playfield.ListSubPlayFields.ToList().ForEach(x => x.Status = status);
            _unitOfWork.PlayFieldRepository.Update(playfield);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<bool> UpdatePlayFieldForAdmin(PlayFieldModel updateplayField)
        {
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == updateplayField.PlayFieldId;
            string includeProperties = "ListSubPlayFields";
            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter, includeProperties);
            if (playfield == null)
            {
                throw new Exception("This playfield is not exist");
            }
            if (!updateplayField.PlayFieldName.IsNullOrEmpty())
            {
                playfield.PlayFieldName = updateplayField.PlayFieldName;
            }
            if (updateplayField.Price.HasValue)
            {
                playfield.Price = updateplayField.Price;
                playfield.ListSubPlayFields!.ToList().ForEach(x => x.Price = updateplayField.Price);
            }
            if (!updateplayField.Address.IsNullOrEmpty())
            {
                playfield.Address = updateplayField.Address;
                playfield.ListSubPlayFields!.ToList().ForEach(x => x.Address = updateplayField.Address);

            }
            if(updateplayField.Status >= 0)
            {
                playfield.Status = updateplayField.Status;
            }
            _unitOfWork.PlayFieldRepository.Update(playfield);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }
    }
}
