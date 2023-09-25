using Microsoft.Data.SqlClient;
using SeatManagement.CustomException;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class AssetService : IAssetService
    {
        private readonly IRepository<LookUpAsset> _repository;

        public AssetService(IRepository<LookUpAsset> repository)
        {
            _repository=repository;
        }

        public void Add(List<LookUpAssetDTO> assetDTOList)
        {
            var uniqueAssetDTOList = assetDTOList
            .GroupBy(a => a.AssetName, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First()); //To get unique asset names.


            List<LookUpAsset> newAssetList = new List<LookUpAsset>();
            
            var currentAssetList = _repository.GetAll();
           
            foreach (var asset in uniqueAssetDTOList)
            {
                var item = currentAssetList?.Where(x => x.AssetName == asset.AssetName ).FirstOrDefault();
                if (item == null)
                {
                    newAssetList.Add(new LookUpAsset()
                    {
                        AssetName = asset.AssetName,
                    });
                };
            }
            
            _repository.Add(newAssetList);
        }

        public IQueryable<LookUpAsset> Get()
        {
            var assetList= _repository.GetAll();

            if (assetList==null || !assetList.Any())
                throw new NoDataException("No asset found\nPlease add asset first");
            return assetList;
        }
    }
}

//public int Add(LookUpAssetDTO assetDTO)
//{
//    var item = new LookUpAsset()
//    {
//        AssetName = assetDTO.AssetName
//    };
//    _repositary.Add(item);
//    return item.AssetId;
//}