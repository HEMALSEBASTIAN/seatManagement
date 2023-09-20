using SeatManagement.DTO;
using SeatManagement.Models;

namespace SeatManagement.Interface
{
    public interface IAssetService
    {
        public int Add(LookUpAssetDTO assetDTO);
        public void Add(List<LookUpAssetDTO> assetDTOList);    
        public List<LookUpAsset> Get();
    }
}
