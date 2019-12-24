
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using vms.entity.models;
using vms.repository.dbo;
using vms.service;

namespace vms.service.dbo
{
    public interface IAttendenceDetailService : IServiceBase<AttendenceDetail>
    {
        Task<IEnumerable<AttendenceDetail>> GetAll();
        Task<AttendenceDetail> GetById(int id);
    }
    public class AttendenceDetailService : ServiceBase<AttendenceDetail>, IAttendenceDetailService
    {
        public IAttendenceDetailRepository _repository { get; }
        public AttendenceDetailService(IAttendenceDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<AttendenceDetail>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<AttendenceDetail> GetById(int id)
        {
            return await _repository.GetById(id);
        }
    }
}
