
using ApplicationsA.Common;
using Domain.Entities;
using Infrastructure.Persistansce;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace ApplicationsA.ImageRepository
{
    public class ImageRepositoryRepo : IGenericRepository<ImageRepositoryModel>
    {

        private TE_ImageRepository _context = new TE_ImageRepository();

        private AR_ImageRepository imageRepositoryEntity = new AR_ImageRepository();
        private DbSet<AR_ImageRepository> _dbSet;

        public ImageRepositoryRepo()
        {

            _dbSet = _context.Set<AR_ImageRepository>();
        }
        public Task<ImageRepositoryModel> Add(ImageRepositoryModel entity)
        {
            imageRepositoryEntity.Path = entity.Path;
            imageRepositoryEntity.SerialNumber = entity.SerialNumber;
            imageRepositoryEntity.UpdatedAt = DateTime.Now;
            _dbSet.Add(imageRepositoryEntity);
            _context.SaveChanges();
            return Task.FromResult(entity);
        }

        public Task Delete(ImageRepositoryModel entity)
        {
            var objectImage = _dbSet.Find(entity.Id_Image);
            _dbSet.Remove(objectImage);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<ImageRepositoryModel>> GetAll()
        {
            var sp = _context.AR_ImageRepository.Where(x => x.FKProcess == 1).ToList();
            var list = _dbSet.ToList();
            var listImage = new List<ImageRepositoryModel>();
            foreach (var item in list)
            {
                listImage.Add(new ImageRepositoryModel
                {
                    Id_Image = item.PKImage,
                    Path = item.Path,
                    SerialNumber = item.SerialNumber,

                    UpdatedAt = item.UpdatedAt
                });
            }
            return Task.FromResult(listImage.AsEnumerable());

        }

        public Task<ImageRepositoryModel> GetById(int id)
        {
            _dbSet = _context.Set<AR_ImageRepository>();
            var image = _dbSet.Find(id);
            var imageModel = new ImageRepositoryModel
            {
                Id_Image = image.PKImage,
                Path = image.Path,
                SerialNumber = image.SerialNumber,
                UpdatedAt = image.UpdatedAt
            };
            return Task.FromResult(imageModel);
        }

        public Task Save()
        {
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task Update(ImageRepositoryModel entity)
        {
            var image = _dbSet.Find(entity.Id_Image);
            image.Path = entity.Path;
            image.SerialNumber = entity.SerialNumber;
            image.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        public Task executeCustomQuery()
        {
            var list = _context.Database.SqlQuery<AR_ImageRepository>(@"SELECT 
            [Extent1].[PKImage] AS [PKImage], 
            [Extent1].[SerialNumber] AS [SerialNumber], 
            [Extent1].[FKProcess] AS [FKProcess], 
            [Extent1].[Path] AS [Path], 
            [Extent1].[FileDateTime] AS [FileDateTime], 
            [Extent1].[UpdatedAt] AS [UpdatedAt]
            FROM [dbo].[AR_ImageRepository] AS [Extent1]").ToList();
            return Task.CompletedTask;




        }
        public Task<List<UpGetProcessModel>> GetProcess()
        {
            var list = _context.Database.SqlQuery<UpGetProcessModel>("[dbo].[up_GetProcess]").ToList();
            return Task.FromResult(list);
        }
    }
}
