using AutoMapper;
using FSU.SPORTIDY.Repository.Common.Enums;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.Interfaces;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Repository.Utils;
using FSU.SPORTIDY.Service.BusinessModel.ClubModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.UserModels;
using FSU.SPORTIDY.Service.Interfaces;
using FSU.SPORTIDY.Service.Utils;
using MailKit.Search;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Services
{
    public class ClubService : IClubService
    {
        public IUnitOfWork _unitOfWork;
        public IMapper _mapper;

        public ClubService(IUnitOfWork unitOfWork, IMapper mapper) 
        {  
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ClubModel> CreateClub(CreateClubModel clubModel, int UserId)
        {
            try
            {
                var checkUser = await _unitOfWork.UserRepository.GetUserByIdAsync(UserId);
                if (checkUser != null)
                {
                    var newClub = _mapper.Map<Club>(clubModel);
                    newClub.ClubCode = Guid.NewGuid().ToString();
                    newClub.CreateDate = DateOnly.FromDateTime(DateTime.Now);


                    await _unitOfWork.ClubRepository.Insert(newClub);
                    var result = await _unitOfWork.SaveAsync();
                    if (result > 0)
                    {
                        var getClub = await _unitOfWork.ClubRepository.GetAllClub();
                        var newewstClub = getClub.OrderByDescending(x => x.ClubId).FirstOrDefault();


                        var userClub = new UserClub()
                        {
                            ClubId = newewstClub.ClubId,
                            UserId = UserId,
                            IsLeader = true,
                        };
                        var insertToUserClub = await _unitOfWork.ClubRepository.InsertToUserClub(userClub);
                        if (insertToUserClub)
                        {
                            var entities = await _unitOfWork.ClubRepository.GetAllClub();
                            var responseNewewstClub = entities.OrderByDescending(x => x.ClubId).FirstOrDefault();
                            var responseClub = _mapper.Map<ClubModel>(responseNewewstClub);
                            return responseClub;
                        }
                        return null;
                    }
                    else
                    {
                        throw new Exception("Have an error when create a club. Please try again ");
                    }
                }
                throw new Exception("User does not exist, so can not create a club");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteClub(int clubId)
        {
            string includeProperties = "UserClubs";
            var EntityDelete = await _unitOfWork.ClubRepository.GetByCondition(x => x.ClubId == clubId, includeProperties: includeProperties);
            if (EntityDelete == null)
            {
                return false;
            }
            EntityDelete!.UserClubs.Clear();
            _unitOfWork.ClubRepository.Delete(EntityDelete);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<PageEntity<ClubModel>> GetAllClub(PaginationParameter paginationParameter)
        {
            Expression<Func<Club, bool>> filter = null!;
            Func<IQueryable<Club>, IOrderedQueryable<Club>> orderBy = null!;
            if (!paginationParameter.Search.IsNullOrEmpty())
            {
                int validInt = 0;
                var checkInt = int.TryParse(paginationParameter.Search, out validInt);
                if (checkInt)
                {
                    filter = x => x.ClubId == validInt || x.TotalMember == validInt;
                }
                else
                {
                    filter = x => x.ClubCode.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.ClubName.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Regulation.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Infomation.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Slogan.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.MainSport.ToLower().Contains(paginationParameter.Search.ToLower())
                                  || x.Location.ToLower().Contains(paginationParameter.Search.ToLower())
                    ;
                }
            }
            switch (paginationParameter.SortBy)
            {
                case "clubid":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.ClubId)
                               : x => x.OrderBy(x => x.ClubId)) : x => x.OrderBy(x => x.ClubId);
                    break;
                case "clubcode":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.ClubCode)
                               : x => x.OrderBy(x => x.ClubCode)) : x => x.OrderBy(x => x.ClubCode);
                    break;
                case "clubname":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.ClubName)
                               : x => x.OrderBy(x => x.ClubName)) : x => x.OrderBy(x => x.ClubName);
                    break;
                case "regulation":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Regulation)
                               : x => x.OrderBy(x => x.Regulation)) : x => x.OrderBy(x => x.Regulation);
                    break;
                case "information":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Infomation)
                               : x => x.OrderBy(x => x.Infomation)) : x => x.OrderBy(x => x.Infomation);
                    break;
                case "slogan":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Slogan)
                               : x => x.OrderBy(x => x.Slogan)) : x => x.OrderBy(x => x.Slogan);
                    break;
                case "mainsport":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.MainSport)
                               : x => x.OrderBy(x => x.MainSport)) : x => x.OrderBy(x => x.MainSport);
                    break;
                case "location":
                    orderBy = !string.IsNullOrEmpty(paginationParameter.Direction)
                                ? (paginationParameter.Direction.ToLower().Equals("desc")
                               ? x => x.OrderByDescending(x => x.Location)
                               : x => x.OrderBy(x => x.Location)) : x => x.OrderBy(x => x.Location);
                    break;
                default:
                    orderBy = x => x.OrderByDescending(x => x.CreateDate);
                    break;
            }
            var entities = await _unitOfWork.ClubRepository.GetAllClub(filter, orderBy, paginationParameter.PageIndex, paginationParameter.PageSize);
            var pagin = new PageEntity<ClubModel>();
            pagin.List = _mapper.Map<IEnumerable<ClubModel>>(entities).ToList();
            pagin.TotalRecord = await _unitOfWork.ClubRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, paginationParameter.PageSize);
            return pagin;
        }

        public async Task<List<ClubModel>> GetClubJoinedByUserId(int userId)
        {
            try
            {
                var result = await _unitOfWork.ClubRepository.GetClubJoinedByUserId(userId);
                var response = _mapper.Map<List<ClubModel>>(result);
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<MeetingModel>> GetMeetingsByClubId(int clubId)
        {
            try
            {
                var result = await _unitOfWork.ClubRepository.GetMeetingsByClubId(clubId);
                var response = _mapper.Map<List<MeetingModel>>(result);
                return response;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool> JoinedClub(int userId, int clubId)
        {
            try
            {
                var result = await _unitOfWork.ClubRepository.JoinedClub(userId, clubId);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> UpdateClub(UpdateClubModel updateClub)
        {
            var oldClub = await _unitOfWork.ClubRepository.GetByID(updateClub.ClubId);
            if(oldClub != null)
            {
                oldClub.ClubName = updateClub.ClubName;
                oldClub.AvartarClub = updateClub.AvartarClub;
                oldClub.CoverImageClub = updateClub.CoverImageClub;
                oldClub.Infomation = updateClub.Infomation;
                oldClub.Slogan = updateClub.Slogan;
                oldClub.Regulation = updateClub.Regulation;
                oldClub.MainSport = updateClub.MainSport;
                oldClub.TotalMember = updateClub.TotalMember;
                oldClub.Location = updateClub.Location;
                 _unitOfWork.ClubRepository.Update(oldClub);
                var result = await _unitOfWork.SaveAsync();
                return result > 0;
            }
            return false;

        }
        public async Task<CreateClubModel> GetClubById(int clubId)
        {
            Expression<Func<Club, bool>> condition = x => x.ClubId == clubId;
            string includeProperties = "UserClubs";

            var entity = await _unitOfWork.ClubRepository.GetByCondition(condition, includeProperties: includeProperties);
            return _mapper?.Map<CreateClubModel?>(entity)!;
        }

    }
}
