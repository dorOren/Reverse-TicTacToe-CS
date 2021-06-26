﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI
{
    public partial class GameSettingsForm : Form
    {
        public Boolean Player2CheckBox
        {
            get
            {
                return player2CheckBox.Checked;
            }
        }

        public string Player1Name
        {
            get
            {
                return textBoxPlayer1Name.Text;
            }
        }
        public int NumRows
        {
            set { }
            get
            {
                return (int)numRows.Value;
            }
        }
        public int NumCols
        {
            set { }
            get
            {
                return (int)numCols.Value;
            }
        }

        public GameSettingsForm()
        {
            InitializeComponent();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {

            if (this.textBoxPlayer1Name.Text.Equals("") || this.textBoxPlayer2Name.Text.Equals(""))
            {
                MessageBox.Show("Illegal name input. Try again.");
            }

            this.DialogResult = DialogResult.OK;
        }

        private void numRows_ValueChanged(object sender, EventArgs e)
        {
            if (this.numRows.Value != this.numCols.Value)
            {
                this.numCols.Value = this.numRows.Value;
            }
        }

        private void numCols_ValueChanged(object sender, EventArgs e)
        {
            if (this.numRows.Value != this.numCols.Value)
            {
                this.numRows.Value = this.numCols.Value;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.player2CheckBox.Checked)
            {
                this.textBoxPlayer2Name.Enabled = true;
                this.textBoxPlayer2Name.Clear();
            }
            else
            {
                this.textBoxPlayer2Name.Enabled = false;
                this.textBoxPlayer2Name.Text = "Computer";
            }
        }

        private void GameSettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
