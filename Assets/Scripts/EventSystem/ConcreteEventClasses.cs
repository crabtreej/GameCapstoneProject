using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts.EventSystem
{
    //"Implement" all the event classes here
    public class ObjectMadeNoiseEvent<GameObject> : UnityEvent<GameObject> { }
}
