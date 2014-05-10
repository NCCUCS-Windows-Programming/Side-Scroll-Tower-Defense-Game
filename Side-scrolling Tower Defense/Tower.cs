﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace Side_scrolling_Tower_Defense
{
    class Tower
    {
        /*attr*/
        private int maxHP; // 定值，不會因受攻而減少
        private int hp;
        private int atk;
        private int range;
        private int towerLevel;
        private double _axis;
        private int _attackspeed;
        private int counter=0;
        private bool isCrash = false;
        Label lbTowerHP;
        
        
        #region get & set
        public double POSITION
        {
            get { return _axis; }
            set { _axis = value; }
        }
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        public int MAXHP
        {
            get { return maxHP; }
            set { maxHP = value; }
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
        public bool CRASHED
        {
            get { return isCrash; }
            set { isCrash = value; }
        }
        #endregion

        /*method*/
        public Tower(int _hp, int _atk, int _range, int _towerLevel, bool isPlayer, Label _lbHP)
        {
            maxHP = _hp;
            hp = _hp;
            atk = _atk;
            range = _range;
            towerLevel = _towerLevel;
            _attackspeed = 100;//單位是10毫秒
            lbTowerHP = _lbHP;

            if (isPlayer)
                _axis = 50;
            else
                _axis = 860;

            lbTowerHP.Content = hp.ToString() + "/" + maxHP.ToString();
        }
        public void Attack(List<Soldier> enemyS)
        {
            int target = Int32.MaxValue;
            double nearest = double.MaxValue;
            for (int i = 0; i < enemyS.Count; i++)
            {
                double distance = Math.Abs(enemyS[i].POSITION - this.POSITION);
                if (nearest > distance && distance >= 0)
                {
                    nearest = distance;
                    target = i;
                }
            }
            if (nearest <= range)
            {
                if ((++counter % _attackspeed) == 0)
                {
                    counter = 0;
                    enemyS[target].HP -= this.ATK;
                    enemyS[target].LifeCheck();
                }
            }
        }
        public void GetHurt(int quaintity)
        {
            hp -= quaintity;
            lbTowerHP.Content = hp.ToString() + "/" + maxHP.ToString();
            if (hp <= 0)
                Crash();
        }
        public void Crash()
        {
            CRASHED = true;

            // display the animation of crashing
            // call game.Game_over();
        }
        public void Upgrade(char item, int quantity)
        {
            if (item == 'h')
            {
                maxHP = quantity;
                hp = quantity;
                lbTowerHP.Content = hp.ToString() + "/" + maxHP.ToString();
            }
            if (item == 'a')
                atk = quantity;
            if (item == 'r')
                range = quantity;
            if (item == 't')
                towerLevel++;
        }
        public void Skill()
        {
//            int i = 0;
      //      while (enemySoldier[i] != NULL)
       //     {
        //        if(enemySoldier[i].POSITION <= this.range)
       //             enemySoldier[i].HP -= this.ATK; // or die?
       //     }
        }

        /*
        public Label ReturnLable()
        {
            Label ImageTower = new Label();
            return ImageTower;
        }
        */

    }
}
