// Utility scripts for the post processing stack
// https://github.com/keijiro/PostProcessingUtilities

using UnityEngine;

namespace UnityEngine.PostProcessing.Utilities
{
    [RequireComponent(typeof(PostProcessingController))]
    public class FocusPuller : MonoBehaviour
    {
        #region Editable properties

        [SerializeField] Transform _target;

        public Transform target {
            get { return _target; }
            set { _target = value; }
        }

        [SerializeField] float _offset = 0;

        public float offset {
            get { return _offset; }
            set { _offset = value; }
        }

        [SerializeField] float _speed = 10;

        public float speed {
            get { return _speed; }
            set { _speed = Mathf.Max(0.01f, value); }
        }

        #endregion

        #region Private members

        PostProcessingController _controller;
        float _velocity;

        #endregion

        #region MonoBehaviour functions

        void Start()
        {
            _controller = GetComponent<PostProcessingController>();
        }

        void OnValidate()
        {
            speed = _speed;
        }

        void Update()
        {
            if (_target == null) return;

            // Retrieve the current value.
            var d1 = _controller.depthOfField.focusDistance;

            // Calculate the depth of the focus point.
            var d2 = Vector3.Dot(_target.position - transform.position, transform.forward);

            // Damped-spring interpolation.
            var dt = Time.deltaTime;
            var n1 = _velocity - (d1 - d2) * speed * speed * dt;
            var n2 = 1 + speed * dt;
            _velocity = n1 / (n2 * n2);
            var d = d1 + _velocity * dt;

            // Apply the result.
            _controller.depthOfField.focusDistance = d;
        }

        #endregion
    }
}
