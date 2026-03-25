using Academy.BLL.Services;
using Academy.Core.Interfaces.Application;
using Academy.Core.Interfaces.Services;
using Academy.Data.Repositories.EF;

namespace Academy.BLL
{
    public class AcademyServiceFactory
    {
        private readonly IAcademyConfiguration _config;

        public AcademyServiceFactory(IAcademyConfiguration config)
        {
            _config = config;
        }

        public IAdmissionsService CreateAdmissionsService()
        {
            return new AdmissionsService(new EFAdmissionsRepository(_config.GetConnectionString()));
        }

        public IStudentService CreateStudentService()
        {
            return new StudentService(new EFStudentRepository(_config.GetConnectionString()));
        }
    }
}