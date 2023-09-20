using Microsoft.EntityFrameworkCore;
using SeatManagement.DTO;
using SeatManagement.Interface;
using SeatManagement.Models;

namespace SeatManagement.Implementation
{
    public class MeetingRoomAssetService : IMeetingRoomAssetService
    {
        private readonly IRepositary<MeetingRoomAsset> _repositary;

        public MeetingRoomAssetService(IRepositary<MeetingRoomAsset> repositary)
        {
            _repositary=repositary;
        }
        public void Add(MeetingRoomAssetDTO meetingRoomAssetDTO)
        {
            var item = new MeetingRoomAsset()
            {
                AssetId = meetingRoomAssetDTO.AssetId,
                MeetingRoomId = meetingRoomAssetDTO.MeetingRoomId,
                AssetQuantity = meetingRoomAssetDTO.AssetQuantity,
            };
            _repositary.Add(item);
        }

        public List<MeetingRoomAsset> GetAll()
        {
            return _repositary.GetAll().ToList();
        }
        //get join of meetingroomasset and lookupasset
        //get join of meetingroomasset and lookupasset
        public List<MeetingRoomAssetNameDTO> GetAll(int MeetingRoomId)
        {
            var item = _repositary.GetAll()
                .Include(x => x.LookUpAsset)
                .Where(x => x.MeetingRoomId == MeetingRoomId)
                .Select(x => new MeetingRoomAssetNameDTO
                {
                    MeetingRoomAssetId = x.MeetingRoomAssetId,
                    AssetQuantity = x.AssetQuantity,
                    MeetRoomId = MeetingRoomId,
                    AssetName = x.LookUpAsset.AssetName,
                    AssetId=x.AssetId
                }).ToList();
            return item;
        }

        public MeetingRoomAsset GetById(int id)
        {
            var item = _repositary.GetById(id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        public MeetingRoomAsset Update(MeetingRoomAsset meetingRoomAsset)
        {
            var item = _repositary.GetById(meetingRoomAsset.MeetingRoomAssetId);
            if (item == null)
            {
                return null;
            }
            item.MeetingRoomId = meetingRoomAsset.MeetingRoomId;
            item.AssetQuantity = meetingRoomAsset.AssetQuantity;
            item.AssetQuantity = meetingRoomAsset.AssetQuantity;
            _repositary.Update();
            return item;
        }
    }
}
