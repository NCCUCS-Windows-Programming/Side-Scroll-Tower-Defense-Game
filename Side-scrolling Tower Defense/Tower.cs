﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Side_scrolling_Tower_Defense
{
    class Tower
    {
        /*attr*/
        private int hp;
        private int atk;
        private int range;
        private int towerLevel;

        public int HP 
        {
            get { return hp; }
            set { hp = value; }
        }
        public int ATK
        {
            get { return atk; }
            set { atk = value; }
        }
        public int RANGE
        {
            get { return range; }
            set { range = value; }
        }
        public int TowerLevel
        {
            get { return towerLevel; }
            set { towerLevel = value; }
        }

        /*method*/
        public Tower(int _hp, int _atk, int _range, int _towerLevel)
        {
            hp = _hp;
            atk = _atk;
            range = _range;
            towerLevel = _towerLevel;
        }
        public void Attack()
        {
            Soldier TARGET;
            int i = 0;
            
          //  while (enemySoldier[i] != NULL)/*find the nearest Soldier*/
          //  {
          //     if (enemySoldier[i].POSITION <= TARGET.POSITION)
          //         TARGET = enemySoldier[i];
          //  }
            
          //  TARGET.HP -= this.atk;/*attack the nearst one*/

        }
        public void GetHurt(int quaintity)
        {
            hp -= quaintity;
        }
        public void Crash()
        {
            // display the animation of crashing
            // call game.Game_over();
        }
        public void Upgrade(char item, int quantity)
        {
            if (item == 'h')
                hp = quantity;
            if (item == 'a')
                atk = quantity;
            if (item == 'r')
                range = quantity;
            if (item == 't')
                towerLevel++;
        }
        public void Skill()
        {
            int i = 0;
      //      while (enemySoldier[i] != NULL)
       //     {
        //        if(enemySoldier[i].POSITION <= this.range)
       //             enemySoldier[i].HP -= this.ATK; // or die?
       //     }
        }

        public Label ReturnLable()
        {
            Label ImageTower = new Label();
            return ImageTower;
        }


    }
}
