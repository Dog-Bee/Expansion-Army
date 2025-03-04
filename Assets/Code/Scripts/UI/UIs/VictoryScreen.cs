using UnityEngine;
using UnityEngine.UI;

    public class VictoryScreen : MonoBehaviour
    {
        [SerializeField] private Button nextLevelButton;

        private void Start()
        {
            AssignNextLevelButton();
        }

        private void AssignNextLevelButton()
        {
            if(nextLevelButton == null) return;
            
            // TODO: Add logic
        }
    }

