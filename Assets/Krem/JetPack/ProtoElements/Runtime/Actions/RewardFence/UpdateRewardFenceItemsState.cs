using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.ProtoElements.Actions.RewardFence
{
    [NodeGraphGroupName("Jet Pack/Proto Elements/Reward Fence")] 
    public class UpdateRewardFenceItemsState : CoreAction
    {
        [InjectComponent] private Components.RewardFence.RewardFence _rewardFence;
        
        protected override bool Action()
        {
            for (int i = 0; i < _rewardFence.ReceivedItemsCount; i++)
            {
                _rewardFence.Items[i].ReceivedState = true;
            }
        
            return true;
        }
    }
}