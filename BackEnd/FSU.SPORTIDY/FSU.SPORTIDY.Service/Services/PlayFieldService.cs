using AutoMapper;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.ImageFieldBsModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.PlayFieldsModels;
using FSU.SPORTIDY.Service.BusinessModel.SportBsModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using FSU.SPORTIDY.Service.Utils.Common.Enums;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<PlayFieldModel> CreatePlayField(PlayFieldModel playFieldModel, List<ImageFieldModel> listImage)
        {
            var playfield = new PlayField();
            _mapper.Map(playFieldModel, playfield);
            playfield.PlayFieldCode = Guid.NewGuid().ToString();
            playfield.Status = (int)PlayFieldStatus.WaitingAccept;
            // luu hinh anh cua playfield
            var imageField = new List<ImageField>();
            _mapper.Map(imageField, listImage);

            playfield.ImageFields = imageField;
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
            Expression<Func<PlayField, bool>> filter = !searchKey.IsNullOrEmpty() ? x =>
            (x.PlayFieldName!.Contains(searchKey!, StringComparison.OrdinalIgnoreCase)
            || x.Address!.Contains(searchKey, StringComparison.OrdinalIgnoreCase))
            && x.Status != (int)PlayFieldStatus.Deleted : x => x.Status != (int)PlayFieldStatus.Deleted;

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

        public async Task<bool> UpdateAvatarImage(string avatarImage, int PlayFielId)
        {
            Expression<Func<PlayField, bool>> filter = x => x.PlayFieldId == PlayFielId && x.Status != (int)PlayFieldStatus.Deleted && x.Status != (int)PlayFieldStatus.WaitingAccept;
            var playfield = await _unitOfWork.PlayFieldRepository.GetByCondition(filter);
            //if (playfield == null)
            //{
            //    throw new Exception("This playfield is not exist");
            //}
            playfield.AvatarImage = avatarImage;
            _unitOfWork.PlayFieldRepository.Update(playfield);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
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
