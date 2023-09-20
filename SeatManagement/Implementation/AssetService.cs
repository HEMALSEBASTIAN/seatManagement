using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class AssetService : IAssetService
    {
        private readonly IRepositary<LookUpAsset> _repositary;

        public AssetService(IRepositary<LookUpAsset> repositary)
        {
            _repositary=repositary;
        }
        public int Add(LookUpAssetDTO assetDTO)
        {
            var item = new LookUpAsset()
            {
                AssetName = assetDTO.AssetName
            };
            _repositary.Add(item);
            return item.AssetId;
        }

        public void Add(List<LookUpAssetDTO> assetDTOList)
        {
            List<LookUpAssetDTO> uniqueAssetDTOList = assetDTOList
            .GroupBy(a => a.AssetName, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .ToList(); //To get unique asset names.


            List<LookUpAsset> newAssetList = new List<LookUpAsset>();
            var currentAssetList = _repositary.GetAll().ToList();
            foreach(var asset in uniqueAssetDTOList)
            {
                var item = currentAssetList.FirstOrDefault(x =>
                           string.Equals(x.AssetName, asset.AssetName, StringComparison.OrdinalIgnoreCase));
                if (item==null)
                {
                    newAssetList.Add(new LookUpAsset()
                    {
                        AssetName = asset.AssetName,
                    });
                }
            }
            _repositary.Add(newAssetList);
        }

        public List<LookUpAsset> Get()
        {
            return _repositary.GetAll().ToList();
        }
    }
}
