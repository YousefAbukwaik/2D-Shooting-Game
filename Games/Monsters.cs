using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
     class Monsters
    {
        public int mostorPostion { get; set; }
        public int monsterhealth { get; set; }
        public int monsterID { get; set; }
        public Monsters(int monstoerPostion, int monsterHealth, int monsterID)
        {
            this.mostorPostion = monstoerPostion;
            this.monsterhealth = monsterHealth;
            this.monsterID = monsterID;
        }

    }
}
