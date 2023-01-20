using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
     class Towers
    {

       public int towercooldown { get; set; }
        public int towerrange { get; set; }
       public int towerpostion { get; set; }

        public Towers(int towercooldown, int towerrange, int towerpostion)
        {
            this.towercooldown = towercooldown;
            this.towerrange = towerrange;
            this.towerpostion = towerpostion;

        }
        





    }
}
