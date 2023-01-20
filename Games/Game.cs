using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Games
{
    public class Game
    {
       int difficultylevel { get; set; }
        List<Towers> ListOfTowers = new List<Towers>();
        List<Monsters> ListOfMonsters = new List<Monsters>();
        int[,] array = new int[10, 20];
        Random random = new Random();
        string[,] panel = new string[3, 20];
        //System.Threading.Timer timer = new System.Threading.Timer();
        Thread[] workerThreads = new Thread[2];
        int elimnatedmonsters = 0;
         //static System.Timers.Timer aTimer;
        Stopwatch stopwatch = new Stopwatch();
        static ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Cyan };


        public Game()
           
        {
            Difficultylevel();
            buildTower(difficultylevel);
            
            creatGamePanel(ListOfTowers,ListOfMonsters);
            
            workerThreads[0] = new Thread(createNewMonsterAtRandomInterval);
            workerThreads[0].Start();
            stopwatch.Start();

            {

                while (true)
                {
                    
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        refreshGamePanel(ListOfTowers, ListOfMonsters);
                        for (int i = 0; i < ListOfMonsters.Count; i++)
                        {
                            ListOfMonsters[i].mostorPostion += 1;
                        }
                        for (int i = 0; i < ListOfTowers.Count; i++)
                        {
                            Console.WriteLine($"tower({i}): range: {ListOfTowers[i].towerrange} , cooldown: {ListOfTowers[i].towercooldown}");
                        }

                        Shoot(ListOfTowers, ListOfMonsters);
                        {
                            Console.WriteLine($"  progress :{elimnatedmonsters}  monsters are elimnated , {ListOfMonsters.Count } are left");

                        }
                    if (stopwatch.Elapsed.TotalMilliseconds >3000 && ListOfMonsters.Count ==0)
                    {
                        Console.WriteLine("you won");
                    }
                    else
                    {
                        // 7-game ends with a lose if a m reach the end, and with a win if all ms are eliminated

                        for (int i = 0; i < ListOfMonsters.Count; i++)
                        {
                            if (ListOfMonsters[0].mostorPostion >= 20)
                            {
                                Console.WriteLine("u lost");
                            }
                        }
                    }


                }
               
            }
            
        }

        void createNewMonsterAtRandomInterval()
        {
            
            buildMosters(difficultylevel);
         
        }
        

        // 1- difficulty level
        void Difficultylevel()
        {

            Console.WriteLine("please choose a dificulty level(with numbers)\n");
            Console.WriteLine("1-hard\n2-midium\n3-easy\n");
            this.difficultylevel = int.Parse(Console.ReadLine());
            if (this.difficultylevel != 1 && this.difficultylevel != 2 && this.difficultylevel != 3)
            {


                
                    Console.WriteLine("Please enter correct difficulty level");
                    Difficultylevel();
        
            }
            
            }
        void creatGamePanel(List<Towers> listOfTowers, List<Monsters> listOfMonsters)

			{
            for (int i = 0; i < panel.GetLength(1); i++)
            {
                panel[0, i] = "--";
                for (int j = 0; j < listOfMonsters.Count; j++)
                {
                    if (listOfMonsters[j].mostorPostion == i)
                    {
                        panel[1, i] = " M";
                        break;
                    }
                    else
                    {
                        panel[1, i] = "  ";
                        
                    }

                }
                for (int j = 0; j < listOfTowers.Count; j++)
                {
                    if (listOfTowers[j].towerpostion ==i)
                    {
                        panel[2, i] = " |";
                        break;
                    }
                    else
                    {
                        panel[2, i] = "--";
                    }


                }

            }
            for (int k = 0; k < panel.GetLength(0); k++)
            {
                for (int g = 0; g < panel.GetLength(1); g++)
                {
                    if (k == 2)
                    {
                        if (panel[k, g] == " |" )
                        {
                            // 3-towers coloers differ according to the range

                            int range = listOfTowers.Where(tower => tower.towerpostion == g).Select(t => t.towerrange).FirstOrDefault();
                            Console.ForegroundColor = colors[range];

                            Console.Write(panel[k, g]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(panel[k, g]);

                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(panel[k, g]);

                    }
                }

                Console.WriteLine(" ");
            }

        }

        void refreshGamePanel(List<Towers> listOfTowers, List<Monsters> listOfMonsters)

        {
            for (int i = 0; i < panel.GetLength(1); i++)
            {
                panel[0, i] = "--";
                for (int j = 0; j < listOfMonsters.Count; j++)
                {
                    if (listOfMonsters[j].mostorPostion == i)
                    {
                        panel[1, i] = " M";
                        break;
                    }
                    else
                    {
                        panel[1,i] = "  ";
                    }
                    

                }
                for (int j = 0; j < listOfTowers.Count; j++)
                {
                    if (listOfTowers[j].towerpostion ==i)
                    {
                        panel[2, i] = " |";
                        break;
                    }
                    else
                    {
                        panel[2, i] = "--";
                    }

                }

            }
            for (int k = 0; k < panel.GetLength(0); k++)
            {
                for (int g = 0; g < panel.GetLength(1); g++)
                {
                    if (k == 2)
                    {
                        if (panel[k, g] == " |" )
                        {
                            int range = listOfTowers.Where(tower => tower.towerpostion == g).Select(t => t.towerrange).FirstOrDefault();
                            Console.ForegroundColor = colors[range];

                            Console.Write(panel[k, g]);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(panel[k, g]);

                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(panel[k, g]);

                    }
                }

                Console.WriteLine(" ");
            }

        }

        void buildTower(int difficultylevel)
        {
            
                if (difficultylevel == 1)
                {
                    callTowersCreateFunctionBasedOnDifficultyLevel(5);
                }
                else if (difficultylevel == 2)
                {
                    callTowersCreateFunctionBasedOnDifficultyLevel(10);
                }
                else if (difficultylevel == 3)
                {
                    callTowersCreateFunctionBasedOnDifficultyLevel(15);
                }

            
        }

        void callTowersCreateFunctionBasedOnDifficultyLevel(int numberOfTowers)
        {
            
                this.ListOfTowers = createGameTowers(numberOfTowers);
            
        }
        // generate  distinct random postions, range and cooldown for the towers

        List<Towers> createGameTowers(int numberOfTowers)
        {
            List<Towers> localTowers = new List<Towers>();
            int[] postions = new int[numberOfTowers];
            for (int i = 0; i < numberOfTowers; i++)
            {
                bool x = true;
                while (x)
                {
                    int Towerpostion = random.Next(1, 20);

                    if (postions.Contains(Towerpostion) == false)
                    {
                        // Console.WriteLine(Towerpostion);
                        postions[i] = Towerpostion;
                        Towers tower = new Towers(random.Next(1, 3), random.Next(1, 5), Towerpostion);
                        localTowers.Add(tower);
                        //add postions to the main table
                        array[array.GetLength(0) -1, Towerpostion] = 1;
                        x = false;

                    }
                }
            }
            
            return localTowers;



        }

        
        void buildMosters(int difficultylevel)
        {

            if (difficultylevel == 1)
            {
                callMostersCreateFunctionBasedOnDifficultyLevel(4);
            }
            else if (difficultylevel == 2)
            {
                callMostersCreateFunctionBasedOnDifficultyLevel(3);
            }
            else if (difficultylevel == 3)
            {
                callMostersCreateFunctionBasedOnDifficultyLevel(2);
            }


        }
        void callMostersCreateFunctionBasedOnDifficultyLevel(int numberOfMonsters)
        {
            createGameMosters(numberOfMonsters);

        }
        //5-ms spawned randomly along the path when the game starts, and they headed toward the end path.

        List<Monsters> createGameMosters(int numberOfMonsters)
        {
            List<Monsters> localMosters = new List<Monsters>();
            int[] postions = new int[numberOfMonsters];
            for (int i = 0; i < numberOfMonsters; i++)
            {
                System.Threading.Thread.Sleep(random.Next(1000, 3000));
                Monsters monster = new Monsters(0,20,i);
                this.ListOfMonsters.Add(monster);
                
            }

            return localMosters;
        }

        // 6-towers shoot when ms in their range, causes one unit of damage, refire after cooldown, shooting the closest m

        void Shoot(List<Towers> listOfTowers, List<Monsters> listOfMonsters )
        {
            foreach (Towers tower in listOfTowers)
            {
                List<Monsters> targets = listOfMonsters.Where(monster => Math.Abs(tower.towerpostion - monster.mostorPostion) <= tower.towerrange).OrderBy(monster => monster.monsterhealth).ToList();
                if (targets.Any())
                {
                    // Pick a random target if there is more than one
                    Monsters target = targets[0];
                    for (int i = 0; i < this.ListOfMonsters.Count; i++)
                    {
                        if (this.ListOfMonsters[i].monsterID == target.monsterID)
                        {
                            this.ListOfMonsters[i].monsterhealth -= 1;
                            if (this.ListOfMonsters[i].monsterhealth < 0)
                            {
                                this.ListOfMonsters.Remove(target);
                                elimnatedmonsters++;
                                
                            }
                        }
                    }
                }

            }
            
        }
    }





}


