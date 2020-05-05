using System;
using System.Collections.Generic;
using System.Text;

namespace TheGrader
{
    public class Fach
    {
        #region Properties
        public string Name { get; set; }
        #endregion

        #region Constructor
        public Fach(string name)
        {
            this.Name = name;
        }
        #endregion
    }
}
