using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using vms.entity.models;
using vms.entity.viewModels;
using vms.repository;

namespace vms.repository.dbo
{
    public interface IAttendenceDetailRepository : IRepositoryBase<AttendenceDetail>
    {
        Task<IEnumerable<AttendenceDetail>> GetAll();
        Task<AttendenceDetail> GetById(int id);
    }
    public class AttendenceDetailRepository : RepositoryBase<AttendenceDetail>, IAttendenceDetailRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public AttendenceDetailRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<AttendenceDetail>> GetAll()
        {
            var AttendenceDetails = await this.Query().SelectAsync();

            return AttendenceDetails;
        }

        public async Task<AttendenceDetail> GetById(int id)
        {

            var AttendenceDetails = await this.Query().SingleOrDefaultAsync(c=>c.AttendenceDetailsId==id,CancellationToken.None);

            return AttendenceDetails;
        }
    }
}
