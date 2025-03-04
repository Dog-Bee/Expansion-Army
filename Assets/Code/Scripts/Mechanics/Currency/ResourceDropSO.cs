using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(order = 1,fileName = "ResourceDropSO", menuName = "ScriptableObject/ResourceDropSO")]
    public class ResourceDropSO : ScriptableObject
    {
        [NonReorderable]
        public List<ResourceHolder> resourceList;

        public void GetResource(Vector3 position)
        {
            resourceList.ForEach(resource=>
            {
                for (int i = 0; i < resource.ResourceCount; i++)
                {
                    Instantiate(resource.ResourceData.ResourceObj, position, Quaternion.identity)
                        .Initialize(resource.ResourceCount);
                }
            });
        }
    }
