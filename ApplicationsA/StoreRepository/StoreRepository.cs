using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistansce.StoreProcedureRepo;

namespace ApplicationsA.StoreRepository
{
    public class StoreRepository
    {
        StoreProceduresRepo storeProceduresRepo = new StoreProceduresRepo();
        public List<UpGetProcessModel> GetProcess()
        {
            return storeProceduresRepo.GetProcess();
        }
        public string GetServerPathFromProcessID(int processID)
        {
            return storeProceduresRepo.GetServerPathFromProcessID(processID);
        }
        public ImageRepositoryModel insertImageRecord(ImageRepositoryModel image)
        {
            return storeProceduresRepo.InsertAndGetImageRepositoryModel(image);
        }

    }
}
