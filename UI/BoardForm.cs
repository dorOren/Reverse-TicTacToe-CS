﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using UI;
using Logic;
using Logic.Enums;

namespace UI
{
    public partial class BoardForm : Form
    {
        public Board GameBoard { get; }
        public GameLogic Game { get; }
        public Player Player1 { get; }
        public Player Player2 { get; }

        private Button[,] m_ButtonMatrix;
        public LastButtonClicked ButtonClicked { get; set; }

        public BoardForm(int i_NumCols, int i_NumRows, ePlayerType i_PlayerType)
        {
            GameBoard = new Board(i_NumCols, i_NumRows);
            Game = new GameLogic(GameBoard);
            Player1 = new Player(eBoardSigns.X, ePlayerType.Human);
            Player2 = new Player(eBoardSigns.O, i_PlayerType);
            int length = i_NumCols * 40+20;
            int heigh = i_NumRows * 40+20;
            InitializeComponent(i_NumCols, i_NumRows, length, heigh);
            generateButtonMatrix(i_NumCols, i_NumRows);
        }


        private void Board_Load(object sender, EventArgs e)
        {

        }

        private void buttons_Click(object sender, EventArgs e)
        {
            Button thisButton = (Button)sender;
            eBoardSigns sign = eBoardSigns.Blank;
            if (thisButton.Enabled)
            {
                if (Game.Turns % 2 == 0)
                {
                    sign = Player1.Sign;
                }
                else
                {
                    sign = Player2.Sign;
                }
                thisButton.Text = sign.ToString();
                Game.Turns++;

                int rowNum = (int)thisButton.Tag % GameBoard.MatrixSideSize;
                int colNum = (int)thisButton.Tag / GameBoard.MatrixSideSize;
                GameBoard.MarkCell(sign, colNum, rowNum);
                if (Game.CheckForLoser(colNum, rowNum, sign))
                {
                    winningForm(sign);
                }

                if (Game.CheckIfBoardFilled())
                {
                    tieForm();
                }

                if (Player2.PlayerType.Equals(ePlayerType.Computer) && Game.Turns % 2 != 0)
                {
                    PlayerTurnInfo turnInfo = new PlayerTurnInfo(colNum,rowNum);
                    computerMove(turnInfo);
                }
            }
            thisButton.Enabled = false;
        }

        private void computerMove(PlayerTurnInfo i_PrevTurnInfo)
        {
            PlayerTurnInfo generatedMove = new PlayerTurnInfo();
            generatedMove = Game.GenerateComputerMove(Player2.Sign, i_PrevTurnInfo);
            //Game.IncreaseTurns();
            m_ButtonMatrix[generatedMove.CellRow, generatedMove.CellColumn].PerformClick();
        }

        private void winningForm(eBoardSigns i_Sign)
        {
            DialogResult result = DialogResult.None;
            if (i_Sign.Equals(eBoardSigns.O))
            {
                string msg = $"Player 1 Won!{Environment.NewLine}Would you like to play another round?";
                string caption = "Player 1 Won!";
                result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);
                Player1.IncreaseScoreByOne();
            }
            else
            {
                string opponent;
                if (Player2.PlayerType.Equals(ePlayerType.Computer))
                {
                    opponent = "Computer";
                }
                else
                {
                    opponent = "Player 2";
                }
                string msg = $"{opponent} Won!{Environment.NewLine}Would you like to play another round?";
                string caption = $"{opponent} Won!";
                result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);
                Player2.IncreaseScoreByOne();
            }
            if (result == DialogResult.Yes)
            {
                ClearBoard();
            }
            else
            {
                Application.Exit();
            }
        }

        public void ClearBoard()
        {
            GameBoard.ClearBoard();
            foreach(Button button in m_ButtonMatrix)
            {
                button.Enabled = true;
                button.Text = "";
            }
        }

        private void tieForm()
        {

            string msg = $"Tie!{Environment.NewLine}Would you like to play another round?";
            string caption = "Tie!";
            DialogResult result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ClearBoard();
            }
            else
            {
                Application.Exit();
            }
        }

        private void BoardForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void BoardForm_Load_1(object sender, EventArgs e)
        {

        }

        public struct LastButtonClicked
        {
            public LastButtonClicked(int i_I, int i_J)
            {
                RowNum = i_I;
                ColNum = i_J;
            }
            public int RowNum { get; }
            public int ColNum { get; }
        }
    }
}