using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController Instance = null;
    private static float SHAKE_FRAME = 30;
    private static float SHAKE_RANGE = 0.25f;
    private Transform playerTrans;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    // 
    void Start()
    {
        Application.targetFrameRate = 60;
        playerTrans = GameObject.Find("Player").transform;
    }

    private float mShakeCount;
    private bool bShaking;
    private Vector3 mTargetPosition;

    public static void Shake()
    {
        Instance.mShakeCount = CameraController.SHAKE_FRAME;
        Instance.bShaking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTrans)
        {

            mTargetPosition = new Vector3(
                Mathf.Lerp(transform.position.x, playerTrans.position.x, (1.0f - Mathf.Exp(-Time.deltaTime * 20))),
                Mathf.Lerp(transform.position.y, playerTrans.position.y, (1.0f - Mathf.Exp(-Time.deltaTime * 20))), 
                transform.position.z);

            //
            if (bShaking)
            {
                mShakeCount--;
                var shakeRange = Mathf.Lerp(0, SHAKE_RANGE, mShakeCount/ SHAKE_FRAME);
                mTargetPosition.x = mTargetPosition.x + Random.Range(-shakeRange, shakeRange);
                mTargetPosition.y = mTargetPosition.y + Random.Range(-shakeRange, shakeRange);

                transform.position = mTargetPosition;

                if (mShakeCount <= 0)
                {
                    bShaking = false;
                }
            }
            else 
            {
                transform.position = mTargetPosition;
            }           
        }
    }
}
