using UnityEngine;
using System.Collections;
using Panda;

namespace Panda.Examples.ChangeColor
{
    public class MyCube : MonoBehaviour
    {

        int ctr;
        /*
         * Set the color to the specified rgb value and succeed.
         */
        [Task] // <-- Attribute used to tag a class member as a task implementation.
        void SetColor(float r, float g, float b)
        {
            this.GetComponent<Renderer>().material.color = new Color(r, g, b);
            Task.current.Succeed(); // <-- Task.current gives access to the run-time task bind to this method.
        }

        [Task]
        bool True = true;
        void Failed()
        {
            Task.current.Fail();
        }

        [Task]
        void SucceedTask()
        {
            Task.current.Succeed();
        }

        [Task]
        bool TrueLongTime()
        {
            ctr += 1;
            if (ctr > 1000)
            {
                return false;
            }
            return true;
        }
    }
}
