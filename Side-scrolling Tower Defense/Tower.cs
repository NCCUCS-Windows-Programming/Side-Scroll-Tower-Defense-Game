﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
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
        private bool isEnemy = false;
        Label lbTowerHP;//在畫面上顯示的血量
        Label lbTower;//在畫面上顯示的塔
        Label bullet;
        Grid grid;//主畫面(顯示子彈用
        double movePerStepX = 25; //單位時間移動的 X 軸值, 定值
        double movePerStepY = 0; //即時計算每單位時間移動的 Y 軸值
        int startTime = 0; //每發子彈的開槍時機
        double angle = 0;//子彈角度

        
        
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
        public Tower(int _hp, int _atk, int _range, int _towerLevel, bool _isPlayer, Label _lbHP, Label _lbTower, Grid _grid)
        {
            maxHP = _hp;
            hp = _hp;
            atk = _atk;
            range = _range;
            towerLevel = _towerLevel;
            _attackspeed = 100;//單位是10毫秒
            isEnemy = !_isPlayer;
            lbTowerHP = _lbHP;
            lbTower = _lbTower;
            grid = _grid;

            if (isEnemy)
                _axis = 886;//lbTower.Margin.Right;
            else
                _axis = 108;//lbTower.Margin.Right + lbTower.Width;

            lbTowerHP.Content = hp.ToString() + "/" + maxHP.ToString();
        }
        public void Attack(List<Soldier> enemyS)
        {
            int target = Int32.MaxValue;
            double nearest = double.MaxValue;
            for (int i = 0; i < enemyS.Count; i++)
            {
                double distance = Math.Abs(enemyS[i].POSITION - this.POSITION);
                if (nearest > distance )
                {
                    nearest = distance;
                    target = i;
                }
            }

            if (nearest <= range)
            {
                if (startTime == 0)
                {
                    startTime = _attackspeed -(int)(((nearest) / movePerStepX) + 3) ;
                    movePerStepY = (lbTower.Height - (enemyS[target].Image.Height / 2)) / (_attackspeed - startTime);
                }

                bool isShooted = false; //如果已開槍，則在未打到目標前 isShooted == true
                if (bullet == null && counter == startTime)
                {
                    grid.Children.Add(BulletShow()); //把子彈放進Grid
                    angle = Math.Atan2((lbTower.Height - enemyS[target].Image.Height / 2), nearest + 40) * 57; //設定角度
                    RotateTransform transform = transform = new RotateTransform(angle); ;
                    if (!isEnemy)
                        transform = new RotateTransform(-angle);
                    else
                        transform = new RotateTransform(angle);
                    bullet.RenderTransform = transform;
                }
                if (counter > startTime)
                    isShooted = true;
                
                if ((++counter % _attackspeed) == 0) //控制攻速
                {
                    counter = 0;
                    enemyS[target].HP -= this.ATK;
                    enemyS[target].LifeCheck();
                    grid.Children.Remove(bullet);
                    bullet = null;
                    startTime = 0;
                }
                else
                {
                    if (bullet != null && isShooted)
                    {
                        if (isEnemy)
                        {
                            bullet.Margin = new System.Windows.Thickness(0, 0, bullet.Margin.Right - movePerStepX, bullet.Margin.Bottom - movePerStepY);
                            if (bullet.Margin.Right + movePerStepX < enemyS[target].POSITION)
                            {
                                grid.Children.Remove(bullet);
                                bullet = null;
                                counter = _attackspeed - 1; //摸到就直接等同攻擊到
                            }
                        }
                        else
                        {
                            bullet.Margin = new System.Windows.Thickness(0, 0, bullet.Margin.Right + movePerStepX, bullet.Margin.Bottom - movePerStepY);
                            if (bullet.Margin.Right - movePerStepX > enemyS[target].POSITION)
                            {
                                grid.Children.Remove(bullet);
                                bullet = null;
                                counter = _attackspeed - 1;
                            }
                        }
                    }
                }
            }
            else
            {
                grid.Children.Remove(bullet); 
                bullet = null;
                startTime = 0;
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

        /*範圍技大招 傳入對方士兵陣列*/
        public void Skill(List<Soldier> EnemyS)
        {
            for (int i = 0; i < EnemyS.Count; i++)
            {
                if (EnemyS[i].Poisition - this.range <= 100 ){/*caculate the abs value of distance*/
                    Enemy[target].HP -= this.atk;
                }
            }
        }

        
        private Label BulletShow()
        {
            bullet = new Label();
            bullet.Width = 40;
            bullet.Height = 3;
            if(isEnemy)
                bullet.Margin = new System.Windows.Thickness(0, 0, 923, lbTower.Height + 10);
            else
                bullet.Margin = new System.Windows.Thickness(0, 0, 36, lbTower.Height + 10);

            bullet.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            bullet.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            bullet.Background = System.Windows.Media.Brushes.Purple ;

            return bullet;
        }
    }
}
