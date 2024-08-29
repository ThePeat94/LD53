using Nidavellir.Scriptables.Audio;
using UnityEngine;
using UnityEngine.Serialization;

namespace Nidavellir.Scriptables
{
    [CreateAssetMenu(fileName = "Interactable Machine Data", menuName = "Data/Interactable Machine", order = 0)]
    public class InteractableMachineData : ScriptableObject
    {
        [SerializeField] private ComponentData m_neededComponent;
        [SerializeField] private SfxData m_usageSfxData;
        [SerializeField] private SfxData m_noComponentPackageSfxData;
        [SerializeField] private SfxData m_noComponentObjectSfxData;

        public ComponentData NeededComponent => this.m_neededComponent;
        public SfxData UsageSfxData => this.m_usageSfxData;
        public SfxData NoComponentPackageSfxData => this.m_noComponentPackageSfxData;
        public SfxData NoComponentObjectSfxData => this.m_noComponentObjectSfxData;
    }
}