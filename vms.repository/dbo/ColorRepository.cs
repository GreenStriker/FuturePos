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
    public interface IColorRepository : IRepositoryBase<Color>
    {
        Task<IEnumerable<Color>> GetAll();
        Task<Color> GetById(int id);
    }
    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        private readonly DbContext _context;
        private readonly IDataProtectionProvider _protectionProvider;
        private readonly PurposeStringConstants _purposeStringConstants;
        private IDataProtector _dataProtector;
        public ColorRepository(DbContext context, IDataProtectionProvider p_protectionProvider, PurposeStringConstants p_purposeStringConstants) : base(context)
        {
            this._context = context;
            _protectionProvider = p_protectionProvider;
            _purposeStringConstants = p_purposeStringConstants;
            _dataProtector = _protectionProvider.CreateProtector(_purposeStringConstants.UserIdQueryString);
        }
        public async Task<IEnumerable<Color>> GetAll()
        {
            var Colors = await this.Query().SelectAsync();

            return Colors;
        }

        public async Task<Color> GetById(int id)
        {

            var Colors = await this.Query().SingleOrDefaultAsync(c=>c.ColorId==id,CancellationToken.None);

            return Colors;
        }
    }
}
