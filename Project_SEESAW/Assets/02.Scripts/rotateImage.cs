using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateImage : MonoBehaviour
{
    private enum State {UP, DOWN, LEFT, RIGHT};
    private State ERotateState;
    private Transform m_transform;
    public Text m_text;

    private double eyeSight = 0.1;
    private int wrongAnswer = 0;

    public Transform LocalTransform
    {
        get {
            if (m_transform == null)
                m_transform = GetComponent<Transform>();
            return m_transform;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        RandomRotate();
        m_text.text = eyeSight.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Check(int direction)
    {
        if(wrongAnswer <= 5)
        {
            if (ERotateState == (State)direction)
            {
                LocalTransform.localPosition =
                    new Vector3(LocalTransform.localPosition.x, LocalTransform.localPosition.y, LocalTransform.localPosition.z + 2);
                eyeSight += 0.1;
                m_text.text = eyeSight.ToString();
            }
            else
            {
                wrongAnswer += 1;
                LocalTransform.localPosition =
                    new Vector3(LocalTransform.localPosition.x, LocalTransform.localPosition.y, LocalTransform.localPosition.z - 2);
                eyeSight -= 0.1;
                m_text.text = eyeSight.ToString();
            }
        }
      else
        {
            Debug.Log(eyeSight.ToString());
        }
    }

    private void RandomRotate()
    {
        ERotateState = (State)Random.Range(0, 3);

        switch (ERotateState)
        {
            case State.DOWN:
                LocalTransform.localRotation = Quaternion.Euler(LocalTransform.localRotation.x, LocalTransform.localRotation.y, 0);
                break;
            case State.RIGHT:
                LocalTransform.localRotation = Quaternion.Euler(LocalTransform.localRotation.x, LocalTransform.localRotation.y, 90);
                break;
            case State.UP:
                LocalTransform.localRotation = Quaternion.Euler(LocalTransform.localRotation.x, LocalTransform.localRotation.y, 180);
                break;
            case State.LEFT:
                LocalTransform.localRotation = Quaternion.Euler(LocalTransform.localRotation.x, LocalTransform.localRotation.y, 270);
                break;
        }
    }

    public void RotateImage(int direction)
    {
        Check(direction);
        RandomRotate();
    }
}
