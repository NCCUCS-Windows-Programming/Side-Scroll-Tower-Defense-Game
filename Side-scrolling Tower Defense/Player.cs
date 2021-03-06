﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Side_scrolling_Tower_Defense
{
    class Player
    {
		private int _money;
        private int _towerUpgradePrice;
        private int moneyGainSpeed;
        private int moneyGainCounter = 0;
        private Grid grid;
        public Label lbMoney;
        public Tower myTower;
        public List<Soldier> soldier = new List<Soldier>();   

        #region get & set
        public int MONEY{
            get { return _money; }
            set { _money = value; }
        }
        public int UPGRADEPRICE{
            get { return _towerUpgradePrice; }
        }
        #endregion

        public Player( Grid grid1, Grid _gridTopBar)
        {
            grid = grid1;
            _money = 5000;      // 初始資金
            _towerUpgradePrice = 100; //塔升級費用
            moneyGainSpeed = 10;
            myTower = new Tower(2000, 50, 250, 1, true, grid1, _gridTopBar);
            lbMoney = new Label();
            lbMoney.Margin = new Thickness(938, 0, 0, 270);
            lbMoney.VerticalAlignment = VerticalAlignment.Bottom;
            lbMoney.HorizontalAlignment = HorizontalAlignment.Left;
            lbMoney.Width = 120;
            lbMoney.Height = 25;
            lbMoney.HorizontalContentAlignment = HorizontalAlignment.Right;
            lbMoney.BorderBrush = System.Windows.Media.Brushes.Black;
            lbMoney.FontWeight = FontWeights.Bold;
            lbMoney.BorderThickness = new System.Windows.Thickness(1, 1, 1, 1);  
            grid1.Children.Add(lbMoney);
        }
        public void MoneyGain()
        {
            if ((++moneyGainCounter % moneyGainSpeed) == 0)
            {
                _money++;
                moneyGainCounter = 0;
                lbMoney.Content = "$ " + MONEY.ToString();
            }
        }
        public void GenerateSolider(Grid grid1, int type, int overPower){
            if(type == 1)
            {
                soldier.Add(new Saber(false, overPower));
                grid1.Children.Add(soldier[soldier.Count - 1].Show(80, 55, "/Images/soldier1Move.gif"));
            }
            else if(type==2)
            {
                soldier.Add(new Archer(false, overPower, grid1));
                grid1.Children.Add(soldier[soldier.Count - 1].Show(75, 65, "/Images/soldier4Move.gif"));
            }
            else if (type == 3)
            {
                soldier.Add(new Caster(false, overPower));
                grid1.Children.Add(soldier[soldier.Count - 1].Show(70, 70, "/Images/soldier3Move.gif"));
            }
            else if (type == 4)
            {
                soldier.Add(new Rider(false, overPower));
                grid1.Children.Add(soldier[soldier.Count - 1].Show(80, 60, "/Images/soldier2Move.gif"));
            }
            else if (type == 5)
            {
                soldier.Add(new Assassin(false, overPower));
                grid1.Children.Add(soldier[soldier.Count - 1].Show(90, 60, "/Images/soldier5Move.gif"));
            }
            else if (type == 6)
            {
                soldier.Add(new Lancer(false, overPower));
                grid1.Children.Add(soldier[soldier.Count - 1].Show(100, 50, "/Images/soldier6Move.gif"));
            }
            else if (type == 7)
            {
                soldier.Add(new Berserker(false, overPower));
                grid1.Children.Add(soldier[soldier.Count - 1].Show(110, 50, "/Images/soldier7Move.gif"));
            }
            else
            {
                MessageBox.Show("Error, GenerateSolider calling error type of soldier");
            }
        }
		public void EarnMoney(int moneyAdd){
            //殺敵 +金錢
            _money += moneyAdd;
            lbMoney.Content = "$"+_money.ToString();
        }
		public void UpgradeTower(){
            //玩家花錢升級塔，塔等級提升，下級所需金費增加(2倍)
            _money -= _towerUpgradePrice;

            if (moneyGainSpeed > 1) //升等加快生錢速度
                moneyGainSpeed--;
            //個別數值提升
            _towerUpgradePrice = _towerUpgradePrice * 2;
            myTower.Upgrade('t', myTower.TowerLevel);
            myTower.Upgrade('h', myTower.MAXHP +100);
            myTower.Upgrade('a', myTower.ATK + 10);
            myTower.Upgrade('r', myTower.RANGE+ 10);
            
            //顯示
       //     lb.Content = "level:" + myTower.TowerLevel.ToString() + '\n' + "hp:" + myTower.HP.ToString() + '\n' + "range:" + myTower.RANGE.ToString() + '\n' + "atk:" + myTower.ATK.ToString();
        }
        public void MaintainSolidersPosition(List<Soldier> enemyS, Tower enemyTower)
        {
            for (int i = 0; i <soldier.Count; i++)
            {
                // 每個士兵該往前的往前，該打的打
                soldier[i].Move(enemyS, enemyTower);
                // Checking life
                if (soldier[i].HP <= 0)
                {
                    grid.Children.Remove(soldier[i].LifeCheck());
                    soldier.RemoveAt(i);
                }
            }
            for (int i = 0; i < enemyS.Count; i++)
            {
                if (enemyS[i].HP <= 0)
                {
                    EarnMoney(enemyS[i].PRICE);
                    grid.Children.Remove( enemyS[i].LifeCheck());
                    enemyS.RemoveAt(i);
                }
            }
        }
	}
}
