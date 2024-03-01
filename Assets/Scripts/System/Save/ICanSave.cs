using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    public interface ICanSave
    {
        void Load();

        void Save();
    }
}
