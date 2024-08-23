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
        public Task<PageEntity<SportDTO>> Get(string searchKey, int? PageSize, int? PageIndex);
        public Task<SportDTO> getById(int id);
        public Task<bool> Delete(int sportId);
        public Task<IEnumerable<SportDTO>> GetAllSportNotPagin();
        public Task<SportDTO?> Insert(SportDTO EntityInsert);
        public Task<SportDTO> Update(SportDTO EntityUpdate);
    }
}
