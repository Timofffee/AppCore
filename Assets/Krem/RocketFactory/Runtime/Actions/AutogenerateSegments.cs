using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.RocketFactory.Components;
using Krem.RocketFactory.Services;

namespace Krem.RocketFactory.Actions
{
    [NodeGraphGroupName("Rocket Factory")]
    public class AutogenerateSegments : CoreAction
    {

        public InputComponent<RocketSegmentCollectionProvider> StoredRepository;
        public InputComponent<RocketSegmentCollectionProvider> PrefabRepository;
        
        protected override bool Action()
        {
            if (StoredRepository.Component.Collection.Count == 0)
            {
                return false;
            }
            
            RocketBuilderService.AutogenerateSegmentTypes(StoredRepository.Component.Collection, PrefabRepository.Component.ScriptableCollection);

            return true;
        }
    }
}