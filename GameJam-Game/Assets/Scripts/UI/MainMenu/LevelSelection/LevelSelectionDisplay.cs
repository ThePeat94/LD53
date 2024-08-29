using Nidavellir.Scriptables;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nidavellir.UI
{
    public class LevelSelectionDisplay : MonoBehaviour
    {
        [SerializeField] private LevelData m_levelSelectionData;
        
        private Button m_button;
        private TextMeshProUGUI m_text;
        
        private void Awake()
        {
            this.m_button = this.GetComponentInChildren<Button>();
            this.m_text = this.GetComponentInChildren<TextMeshProUGUI>();
        }
        
        public void Init(LevelData levelSelectionData)
        {
            this.m_levelSelectionData = levelSelectionData;
            this.m_text.text = this.m_levelSelectionData.Name;
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(this.m_levelSelectionData.SceneName);
        }
    }
}