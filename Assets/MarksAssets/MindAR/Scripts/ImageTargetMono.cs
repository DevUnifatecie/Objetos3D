using UnityEngine;
using UnityEngine.Events;

namespace MarksAssets.MindAR {
    public class ImageTargetMono : MonoBehaviour {
        public int targetIndex = 0;

        public UnityEvent onStart;
        public UnityEvent targetFound;
        public UnityEvent targetLost;

        #pragma warning disable CS0414
        private ImageTarget imageTarget;
        private Vector3 position = new Vector3();
        private Quaternion rotation = new Quaternion();
        private Vector3 scale = new Vector3();

        void Start () {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (!MindAR.isRunning()) MindAR.start();

        imageTarget = MindAR.imageTargets[targetIndex];

        imageTarget.targetFound += () => {
            targetFound.Invoke();
            enabled = true;
        };

        imageTarget.targetLost += () => {
            targetLost.Invoke();
            enabled = false;
        };

        enabled = false;
#endif
        onStart?.Invoke();
        }


        void Update () {
        #if UNITY_WEBGL && !UNITY_EDITOR
        position.Set(imageTarget.posx, imageTarget.posy, imageTarget.posz);
        rotation.Set(imageTarget.rotx, imageTarget.roty, imageTarget.rotz, imageTarget.rotw);
        scale.Set(imageTarget.scale, imageTarget.scale, imageTarget.scale);

        transform.position = position;
        transform.rotation = rotation;
        transform.localScale = scale;
        #endif
        }
    }
}
