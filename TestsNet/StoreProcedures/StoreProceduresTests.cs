

using ApplicationsA.ImageRepository;
using Infrastructure.Persistansce;
using Infrastructure.Persistansce.AR_ImageRepo;
using Infrastructure.Persistansce.StoreProcedureRepo;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace TestsNet.StoreProcedures
{
    public class StoreProceduresTests
    {

        [Fact]

        public void GetProcessTest()
        {
            AR_ImageRepo _context = new AR_ImageRepo();

            var process = _context.GetAll();


           // TE_ImageRepository _ImageRepository = new TE_ImageRepository();
           // var infro = _ImageRepository.AR_ImageRepository.ToList();


        }

        [Fact]
        public void GetProcessFromStoreup_GetProcessTest()
        {
            StoreProceduresRepo storeProceduresRepo = new StoreProceduresRepo();
            var process = storeProceduresRepo.GetProcess();
        }
    }
}
