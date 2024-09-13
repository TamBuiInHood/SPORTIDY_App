using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Service.BusinessModel.ClubModels;
using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using Microsoft.AspNetCore.Http;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface IClubService
    {
        public Task<PageEntity<ClubModel>> GetAllClub(PaginationParameter paginationParameter);
        public Task<ClubModel> GetClubById(int clubId);


        public Task<bool> DeleteClub(int clubId);
        public Task<ClubModel> CreateClub(CreateClubModel clubModel, int UserId);
        public Task<bool> UpdateClub(UpdateClubModel updateClub);
        public Task<List<ClubModel>> GetClubJoinedByUserId(int userId);
        public Task<List<MeetingModel>> GetMeetingsByClubId(int clubId);

        public Task<bool> JoinedClub(int userId, int clubId);
        public Task<string> UpdateAvatarClub(IFormFile avartarClub);
        public Task<string> UpdateCoverImageClub(IFormFile coverImageClub);
    }
}
