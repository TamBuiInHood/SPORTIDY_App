using FSU.SPORTIDY.Service.BusinessModel.MeetingModels;
using FSU.SPORTIDY.Service.BusinessModel.Pagination;
using FSU.SPORTIDY.Service.BusinessModel.SportBsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSU.SPORTIDY.Service.Interfaces
{
    public interface ISportService
    {
        public Task<PageEntity<SportModel>> Get(string searchKey, int? PageSize, int? PageIndex);
        public Task<SportModel> getById(int id);
        public Task<bool> Delete(int sportId);
        public Task<IEnumerable<SportModel>> GetAllSportNotPagin();
        public Task<SportModel?> Insert(SportModel EntityInsert);
        public Task<SportModel> Update(SportModel EntityUpdate);
    }
}
