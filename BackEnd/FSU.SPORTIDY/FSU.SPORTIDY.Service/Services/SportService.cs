using AutoMapper;
using FSU.SPORTIDY.Common.Utils;
using FSU.SPORTIDY.Repository.Entities;
using FSU.SPORTIDY.Repository.UnitOfWork;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.SportBsModels;
using FSU.SPORTIDY.Service.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace FSU.SPORTIDY.Service.Services
{
    public class SportService : ISportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int sportId)
        {
            var sportExist = await _unitOfWork.SportRepository.GetByID(sportId);
            if (sportExist == null)
            {
                throw new Exception("This sport are not exist");
            }
            _unitOfWork.SportRepository.Delete(sportExist);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            return result;
        }

        public async Task<PageEntity<SportModel>> Get(string searchKey, int? PageSize, int? PageIndex)
        {
            Expression<Func<Sport, bool>> filter = !searchKey.IsNullOrEmpty() ? x => x.SportName!.ToLower().Contains(searchKey.ToLower()) : null!;

            Func<IQueryable<Sport>, IOrderedQueryable<Sport>> orderBy = q => q.OrderBy(x => x.SportName);

            var entities = await _unitOfWork.SportRepository
                .Get(filter: filter, orderBy: orderBy, pageIndex: PageIndex, pageSize: PageSize);
            var pagin = new PageEntity<SportModel>();
            pagin.List = _mapper.Map<IEnumerable<SportModel>>(entities).ToList();
            pagin.TotalRecord = await _unitOfWork.MeetingRepository.Count();
            pagin.TotalPage = PaginHelper.PageCount(pagin.TotalRecord, PageSize!.Value);
            return pagin;
        }

        public async Task<IEnumerable<SportModel>> GetAllSportNotPagin()
        {
            Func<IQueryable<Sport>, IOrderedQueryable<Sport>> orderBy = q => q.OrderBy(x => x.SportName);

            var sport = await _unitOfWork.SportRepository.GetAllNoPaging(filter: null,
                orderBy: orderBy,
                includeProperties: null);

            var mapDTO = _mapper.Map<IEnumerable<SportModel>>(sport);
            return mapDTO;
        }

        public async Task<SportModel> getById(int id)
        {
            Expression<Func<Sport, bool>> filter = x => x.SportId == id;

            var entity = await _unitOfWork.SportRepository.GetByCondition(filter);
            return _mapper?.Map<SportModel?>(entity)!;
        }

        public async Task<SportModel?> Insert(SportModel EntityInsert)
        {
            var sport = new Sport();
            sport.SportCode = Guid.NewGuid().ToString();
            sport.SportName = EntityInsert.SportName;
            sport.SportImage = EntityInsert.SportImage ;

            await _unitOfWork.SportRepository.Insert(sport);
            var result = await _unitOfWork.SaveAsync() > 0 ? true : false;
            if (result == true)
            {
                var mapdto = _mapper.Map<SportModel>(sport);
                return mapdto;
            }
            return null!;
        }

        public async Task<SportModel> Update(SportModel EntityUpdate)
        {
            var sport = await _unitOfWork.SportRepository.GetByCondition(x => x.SportId == EntityUpdate.SportId);
            if (sport == null)
            {
                return null!;
            }
            if (EntityUpdate.SportName != null)
            {
                sport.SportName = EntityUpdate.SportName;   
            }
            if (EntityUpdate.SportImage != null)
            {
                sport.SportImage = EntityUpdate.SportImage;
            }
            _unitOfWork.SportRepository.Update(sport);
            var result = (await _unitOfWork.SaveAsync()) > 0 ? true : false;
            if (result)
            {
                return EntityUpdate;
            }
            return null!;
        }
    }
}
