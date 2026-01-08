using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace MouseLib
{
    public class MouseTools : MonoBehaviour
    {
        public static async Task AwaitableTimer(float time)
        {
            await Awaitable.WaitForSecondsAsync(time);
        }
        
        public static GameObject RaycastToGameObjectOnLayer(Camera camera, LayerMask targetLayer)
        {
            Ray raycast = new Ray (camera.transform.position, camera.transform.forward);

            if (Physics.Raycast(raycast, out RaycastHit hitInfo, 1000f))
            {
                if (hitInfo.collider.gameObject.layer == targetLayer)
                {
                    return hitInfo.collider.gameObject;
                }
            }
            
            return null;
        }
        
        public static Vector3 RaycastToPositionOnLayer(Camera camera, LayerMask targetLayer)
        {
            Ray raycast = new Ray (camera.transform.position, camera.transform.forward);

            if (Physics.Raycast(raycast, out RaycastHit hitInfo, 1000f))
            {
                if (hitInfo.collider.gameObject.layer == targetLayer)
                {
                    return hitInfo.point;
                }
            }
            
            return Vector3.zero;
        }
        
        public static Vector3 CameraRaycastToWorldPoint(Camera camera, Mouse mouse, LayerMask ignoredLayer)
        {
            Vector3 mouseScreenPosition;
            Vector3 mouseWorldPosition;

            mouseScreenPosition = mouse.position.ReadValue();
            Ray raycast = camera.ScreenPointToRay(mouseScreenPosition);

            if (Physics.Raycast(raycast, out RaycastHit hitInfo, 1000f))
            {
                mouseWorldPosition = hitInfo.point;

                return mouseWorldPosition;
            }
            else
            {
                return Vector3.zero;
            }
        }

        public static RaycastHit CameraRaycastHitInfo(Camera camera, Mouse mouse)
        {
            Vector3 mouseScreenPosition;

            mouseScreenPosition = mouse.position.ReadValue();
            Ray raycast = camera.ScreenPointToRay(mouseScreenPosition);

            Physics.Raycast(raycast, out RaycastHit hitInfo, 1000f);

            return hitInfo;
        }

        public static Vector3 GetCameraForwardDirection(Camera camera, Transform cameraTarget)
        {
            Vector3 cameraForwardDirection = cameraTarget.position - new Vector3(camera.transform.position.x, cameraTarget.position.y, camera.transform.position.z);
            cameraForwardDirection = cameraForwardDirection.normalized;

            return cameraForwardDirection;
        }
        
        public static bool CheckDistanceBetweenAllVectors(Vector3 vectorInput, List<Vector3> vectorList, float maxDistance)
        {
            // Optimise this
            foreach (Vector3 vector in vectorList)
            {
                if (Vector3.Distance(vectorInput, vector) < maxDistance)
                {
                    return false;
                }
            }

            return true;
        }

        public static Vector3 RandomPositionWithinACircle(float radius, Vector3 circleCentre)
        {
            // Maths from https://stackoverflow.com/questions/5837572/generate-a-random-point-within-a-circle-uniformly
            // Generate a random point within a circle
            float r = radius * Mathf.Sqrt(UnityEngine.Random.Range(0f, 1f));
            float theta = UnityEngine.Random.Range(0f, 1f) * 2 * Mathf.PI;

            // Convert to cartesian coordinates
            float x = circleCentre.x + r * Mathf.Cos(theta);
            float y = circleCentre.z + r * Mathf.Sin(theta);

            Vector3 position = new Vector3(x, 0f, y);

            return position;
        }

        public static Vector3 CalculateDirectionBetweenPoints(Vector3 origin, Vector3 destination)
        {
            Vector3 direction = (destination - origin).normalized;

            return direction;
        }

        public static bool IsLayerInLayerMask(int layer, LayerMask layerMask)
        {
            if ((layerMask & 1 << layer) != 1 << layer)
            {
                return false;
            }
            return true;
        }

        /*/// <summary>
        /// 
        /// </summary>
        /// <param name="maxTime">How long to run the timer for.</param>
        /// <returns>The state of the timer - whether it's running or finished.</returns>
        private static IEnumerator Timer(float maxTime)
        {
            float currentTime;
            bool timerIsRunning;
            
            
            currentTime += Time.deltaTime;
        }*/
        
        public static async Task FadeIn(Image image, float increment, float interval, bool fadeOutAfterComplete = false, float delay = 0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            
            for (float f = image.color.a; f < 1; f += increment)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, f);
                await AwaitableTimer(interval);
            }
            
            if (image.color.a - increment >= 1)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            }
            
            if (!fadeOutAfterComplete) return;
            await AwaitableTimer(delay);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            FadeOut(image, increment, interval);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    
        public static async Task FadeIn(TextMeshProUGUI text, float increment, float interval, bool fadeOutAfterComplete = false, float delay = 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
            
            for (float f = text.color.a; f < 1; f += increment)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, f);
                await AwaitableTimer(interval);
            }
            
            if (text.color.a - increment >= 1)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
            }
            
            if (!fadeOutAfterComplete) return;
            await AwaitableTimer(delay);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            FadeOut(text, increment, interval);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
        
        public static async Task FadeOut(Image image, float decrement, float interval, bool fadeInAfterComplete = false, float delay = 0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            
            for (float f = image.color.a; f > 0; f -= decrement)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, f);
                await AwaitableTimer(interval);
            }

            if (image.color.a - decrement <= 0)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            }
            
            if (!fadeInAfterComplete) return;
            await AwaitableTimer(delay);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            FadeIn(image, decrement, interval);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    
        public static async Task FadeOut(TextMeshProUGUI text, float decrement, float interval, bool fadeInAfterComplete = false, float delay = 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
            
            for (float f = text.color.a; f > 0; f -= decrement)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, f);
                await AwaitableTimer(interval);
            }
            
            if (text.color.a - decrement <= 0)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
            }
            
            if (!fadeInAfterComplete) return;
            await AwaitableTimer(delay);
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            FadeIn(text, decrement, interval);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
    }
}


