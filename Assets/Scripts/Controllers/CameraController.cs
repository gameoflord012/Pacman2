using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacmanGame
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] float tiltSpeed;

        private void Update()
        {
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(
                    target.transform.position.x,
                    target.transform.position.y,
                    transform.position.z),
                tiltSpeed);
        }
    }
}