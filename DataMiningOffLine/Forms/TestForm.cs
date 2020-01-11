using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hexagonal;

namespace DataMiningOffLine
{
    public partial class TestForm : Form
    {
        Hexagonal.Board board;
        Hexagonal.GraphicsEngine graphicsEngine;
        decimal[,] neuronsValue;
        List<Stat> inputStatistics;
        NetworkSettings inputSettings;
        MapSettings inputMapSettings;
        int parameterNumber;
        List<TestForm> a;
        double widht;
        double heigt;


        public TestForm(decimal[,] neuronsValue, List<Stat> inputStatistics, NetworkSettings inputSettings, MapSettings inputMapSettings, int parameterNumber, List<TestForm> a)
        {
            InitializeComponent();
            this.neuronsValue = neuronsValue;
            this.inputStatistics = inputStatistics;
            this.inputSettings = inputSettings;
            this.inputMapSettings = inputMapSettings;
            this.parameterNumber = parameterNumber;
            this.a = a;
            Make();

        }

        void Make()
        {
            int boardHeight = inputSettings.YMap;
            int boardWidth = inputSettings.XMap;
            Hexagonal.HexOrientation orientation = HexOrientation.Pointy;
            widht = Math.Sqrt(3) / 2 * Convert.ToDouble(inputMapSettings.HexSide) * 2;
            heigt = Convert.ToDouble(inputMapSettings.HexSide) * 2;

            min.Text = "min:" + Convert.ToString(inputStatistics[parameterNumber].Min);
            max.Text = "max:" + Convert.ToString(inputStatistics[parameterNumber].Max);

            int positionOfBoardX = Convert.ToInt32(inputMapSettings.XOffset) + inputSettings.XMap * Width + Width / 2;
            int positionOfBoardY = Convert.ToInt32(inputMapSettings.YOffset) + inputSettings.YMap * Height;
            //positionOfBoardX * k + Convert.ToInt32(textBoxXOffset.Text);
            //positionOfBoardY * k + Convert.ToInt32(textBoxtYOffset.Text);

            int k = 0;

            Board board = new Board(boardWidth, boardHeight,
                  Convert.ToInt32(inputMapSettings.HexSide),
                  orientation);
            board.BoardState.BackgroundColor = Color.Gray;
            board.BoardState.GridPenWidth = Convert.ToInt32(inputMapSettings.PenWidth);
            board.BoardState.ActiveHexBorderColor = Color.Red;
            board.BoardState.ActiveHexBorderWidth = Convert.ToInt32(inputMapSettings.PenWidth) * 2;

            this.board = board;
            this.graphicsEngine = new GraphicsEngine(board, positionOfBoardX * k + Convert.ToInt32(inputMapSettings.XOffset), positionOfBoardX * k + Convert.ToInt32(inputMapSettings.YOffset));

            int beginX, beginY;
            int currentNeuron = 0;
            decimal colorValue = 0.0M;
            for (int i = 0; i < inputSettings.YMap; i++)
                for (int j = 0; j < inputSettings.XMap; j++)
                {
                    beginY = Convert.ToInt32(heigt / 2) + Convert.ToInt32(inputMapSettings.YOffset);
                    if (i % 2 != 0)
                    {
                        beginX = Convert.ToInt32(widht) + Convert.ToInt32(inputMapSettings.XOffset);
                    }
                    else
                    {
                        beginX = Convert.ToInt32(widht / 2) + Convert.ToInt32(inputMapSettings.XOffset);
                    }
                    Point currentCell = new Point(beginX + Convert.ToInt32(widht * j), beginY + Convert.ToInt32(heigt / 2 + heigt / 4) * i);
                    Hex currentHex = board.FindHexMouseClick(currentCell);
                    if (inputStatistics[parameterNumber].Max == 0)
                        colorValue = 0.0M;
                    else
                        colorValue = (neuronsValue[parameterNumber,currentNeuron]) / Convert.ToDecimal(inputStatistics[parameterNumber].Max);
                    if (colorValue >= 0 && colorValue < 1)
                    {
                        int r = Convert.ToInt32((colorValue) * 255);
                        int b = Convert.ToInt32((1 - colorValue) * 255);
                        int g = Convert.ToInt32((1 - colorValue * 0.5M) * 255);
                        currentHex.HexState.BackgroundColor = Color.FromArgb(r, g, b);
                    }
                    else if (colorValue == 1)
                    {
                        currentHex.HexState.BackgroundColor = Color.FromArgb(255, 50, 0);
                    }
                    else
                        currentHex.HexState.BackgroundColor = Color.FromArgb(255, 255, 255);
                    currentNeuron++;

                }
        }

        private void Form_MouseClick(object sender, MouseEventArgs e)
        {

            if (board != null && graphicsEngine != null)
            {

                Point mouseClick = new Point(e.X - graphicsEngine.BoardXOffset, e.Y - graphicsEngine.BoardYOffset);

                Hex clickedHex = board.FindHexMouseClick(mouseClick);

                if (clickedHex == null)
                {
                    board.BoardState.ActiveHex = null;
                }
                else
                {
                    board.BoardState.ActiveHex = clickedHex;

                    double xn = e.X;
                    double xy = e.Y;
                    int x;
                    int y = Convert.ToInt32(Math.Round((xy / (heigt * 0.75)) + 0.5, 0));

                    if (y % 2 != 0)
                    {
                        x = Convert.ToInt32(Math.Round((xn / widht + 0.5), 0));
                    }
                    else
                    {
                        x = Convert.ToInt32(Math.Round((xn / widht), 0));
                    }

                    int neuron = y * inputSettings.XMap - (inputSettings.XMap - x);
                    if (neuron > 0 && neuron < inputSettings.NumberOfNeurons + 1)
                        currentValue.Text = Convert.ToString(neuronsValue[parameterNumber,neuron - 1]);


                    ((ParentForMaps)MdiParent).childX = e.X;
                    ((ParentForMaps)MdiParent).childY = e.Y;
                    ((ParentForMaps)MdiParent).labelXP.Text = e.X.ToString() + ' ' + e.Y.ToString();


                    //    if (e.Button == MouseButtons.Right)
                    //    {
                    //        clickedHex.HexState.BackgroundColor = Color.Blue;
                    //    }

                    //this.ParentForm.changeXY += new changeXYHendler(ParentForm_ChangeXY);
                }

            }
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {

            foreach (Control c in this.Controls)
            {
                c.Refresh();
            }

            if (graphicsEngine != null)
            {
                graphicsEngine.Draw(e.Graphics);
            }

            //Force the next Paint()
            this.Invalidate();

        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (graphicsEngine != null)
            {
                graphicsEngine = null;
            }

            if (board != null)
            {
                board = null;
            }
        }

        public void ParentForm_ChangeXY(int x, int y)
        {
            if (board != null && graphicsEngine != null)
            {

                Point mouseClick = new Point(x - graphicsEngine.BoardXOffset, y - graphicsEngine.BoardYOffset);
                Hex clickedHex = board.FindHexMouseClick(mouseClick);
                if (clickedHex == null)
                {
                    board.BoardState.ActiveHex = null;
                }
                else
                {
                    board.BoardState.ActiveHex = clickedHex;

                    double xn = x;
                    double xy = y;
                    int cx;
                    int cy = Convert.ToInt32(Math.Round((xy / (heigt * 0.75)) + 0.5, 0));

                    if (cy % 2 != 0)
                    {
                        cx = Convert.ToInt32(Math.Round((xn / widht + 0.5), 0));
                    }
                    else
                    {
                        cx = Convert.ToInt32(Math.Round((xn / widht), 0));
                    }

                    int neuron = cy * inputSettings.XMap - (inputSettings.XMap - cx);
                    if (neuron > 0 && neuron < inputSettings.NumberOfNeurons + 1)
                        currentValue.Text = Convert.ToString(neuronsValue[parameterNumber,neuron - 1]);
                }
            }
        }

    }
    }
