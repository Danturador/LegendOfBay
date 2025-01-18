using System;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Core
{
    public class RenderVisibility
    {
        private Collider2D _collider;

        public bool IsVisible => IsVisibleByCamera();

        public RenderVisibility(Collider2D collider)
        {
            _collider = collider;
        }

        private bool IsVisibleByCamera()
        {
            Camera cam = Camera.main;
            bool isVisible = false;
            
            if (_collider != null)
            {
                Vector3[] corners = new Vector3[4];
                corners[0] = _collider.bounds.min;
                corners[1] = new Vector3(_collider.bounds.min.x, _collider.bounds.max.y);
                corners[2] = _collider.bounds.max;
                corners[3] = new Vector3(_collider.bounds.max.x, _collider.bounds.min.y);

                foreach (var corner in corners)
                {
                    Vector3 viewportPoint = cam.WorldToViewportPoint(corner);
                    if (viewportPoint.x >= 0 && viewportPoint.x <= 1 && 
                        viewportPoint.y >= 0 && viewportPoint.y <= 1 && 
                        viewportPoint.z > 0)
                    {
                        isVisible = true;
                        break;
                    }
                }
            }
            
            return isVisible;
        }
    }
}