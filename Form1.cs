using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nonogram
{

    public partial class Form1 : Form
    {
        [Serializable]
        public class Puzzle
        {
            public string Title { get; private set; }
            public int Width { get; private set; }
            public int Height { get; private set; }
            public string Difficulty { get; private set; }
            public bool[] BlackCells1D { get; private set; }

            public Puzzle(string title, int width, int height, string difficulty, bool[] blackCells1D)
            {
                Title = title;
                Width = width;
                Height = height;
                Difficulty = difficulty;
                BlackCells1D = blackCells1D;
            }
        }

        private bool PlayMode { get; set; }
        private bool[,] CorrectColors { get; set; }
        private bool[,] BlackCells2D { get; set; }

        public Form1()
        {
            InitializeComponent();

            PlayMode = true;
            DrawBoard(10, 10);
            CreateNonogram();
            ShowLabels();
        }

        private void DrawBoard(int columns, int rows)
        {
            if (!(columns == gridPanel.ColumnCount && rows == gridPanel.RowCount))
            {
                gridPanel.ColumnCount = columns;
                gridPanel.ColumnStyles.Clear();
                gridPanel.RowCount = rows;
                gridPanel.RowStyles.Clear();
                gridPanel.Size = new(30 * columns, 30 * rows);
                gridPanel.Top = menuStrip.Bottom + (Height - gridPanel.Height) / 2;

                for (int i = 0; i < columns; ++i)
                {
                    gridPanel.ColumnStyles.Add(new(SizeType.Absolute, 30));
                    for (int j = 0; j < rows; ++j)
                        gridPanel.RowStyles.Add(new(SizeType.Absolute, 30));
                }
            }

            puzzleTitleTextBox.Text = "";
            difficultyTextBox.Text = "";
            gridPanel.Controls.Clear();
            gridPanel.Left = PlayMode ? (Width - gridPanel.Width) / 2 : (Width * 3 / 2 - gridPanel.Width) / 2;
            puzzleSettingsBox.Visible = !PlayMode;
            Refresh();
        }

        private void CreateNonogram(bool[] blackCells1D = null)
        {
            int columns = gridPanel.ColumnCount;
            int rows = gridPanel.RowCount;
            CorrectColors = new bool[columns, rows];
            BlackCells2D = new bool[columns, rows];

            for (int i = 0; i < columns; ++i)
            {
                for (int j = 0; j < rows; ++j)
                {

                    if (PlayMode)
                    {
                        if (blackCells1D != null)
                        {
                            Buffer.BlockCopy(blackCells1D, 0, BlackCells2D, 0, blackCells1D.Length);
                            for (int x = 0; x < BlackCells2D.GetLength(0); ++x)
                                for (int y = 0; y < BlackCells2D.GetLength(1); ++y)
                                    CorrectColors[x, y] = !BlackCells2D[x, y];
                        }
                        else if (new Random().Next() % 2 == 0)
                            BlackCells2D[i, j] = true;
                        else
                            CorrectColors[i, j] = true;
                    }

                    Button cell = new();
                    cell.BackColor = Color.White;
                    cell.Dock = DockStyle.Fill;
                    cell.Font = new(cell.Font.FontFamily, 14);
                    cell.FlatStyle = FlatStyle.Flat;
                    cell.Margin = new(0);
                    cell.MouseDown += cell_MouseDown;
                    cell.TextAlign = ContentAlignment.MiddleCenter;

                    gridPanel.Controls.Add(cell, i, j);
                }
            }
        }

        private void cell_MouseDown(object sender, MouseEventArgs e)
        {
            var cell = sender as Button;
            var position = gridPanel.GetPositionFromControl(cell);
            (int column, int row) = (position.Column, position.Row);

            switch (e.Button)
            {
                case MouseButtons.Left:
                    bool blackCell = cell.BackColor == Color.Black;
                    if (blackCell)
                        cell.BackColor = Color.White;
                    else
                    {
                        cell.Text = "";
                        cell.BackColor = Color.Black;
                    }

                    if (PlayMode)
                    {
                        CorrectColors[column, row] = !CorrectColors[column, row];
                        CheckWinCondition();
                    }
                    else
                    {
                        BlackCells2D[column, row] = !blackCell;
                        ShowLabels(column, row);
                    }
                    break;

                case MouseButtons.Right:
                    if (cell.Text == "✕")
                        cell.Text = "";
                    else
                        cell.Text = "✕";
                    cell.BackColor = Color.White;

                    if (PlayMode)
                    {
                        CorrectColors[column, row] = false;
                        CheckWinCondition();
                    }
                    else
                    {
                        BlackCells2D[column, row] = false;
                        ShowLabels(column, row);
                    }
                    break;
            }
        }

        void CheckWinCondition()
        {
            for (int i = 0; i < CorrectColors.GetLength(0); ++i)
                for (int j = 0; j < CorrectColors.GetLength(1); ++j)
                    if (!CorrectColors[i, j])
                        return;

            foreach (Control cell in gridPanel.Controls)
                cell.MouseDown -= cell_MouseDown;

            StringFormat stringFormat = new() { Alignment = StringAlignment.Center };
            var graphics = CreateGraphics();
            graphics.DrawString("Congratulations!", new(Font.FontFamily, 28), new SolidBrush(Color.Black), 500, 700, stringFormat);
        }

        private void ShowLabels(int? column = null, int? row = null)
        {
            Queue<int> clues = new();
            int columns = gridPanel.ColumnCount;
            int rows = gridPanel.RowCount;
            int lastIndex;

            if (row == null)
            {
                leftPanel.Controls.Clear();
                leftPanel.Left = gridPanel.Left - leftPanel.Width;
                leftPanel.RowCount = rows;
                leftPanel.RowStyles.Clear();
                leftPanel.Size = new(leftPanel.Width, 30 * rows);
                leftPanel.Top = gridPanel.Top;
            }
            else
            {
                for (lastIndex = 7; lastIndex >= 0; --lastIndex)
                    leftPanel.Controls.Remove(leftPanel.GetControlFromPosition(lastIndex, (int)row));
            }

            for (int i = row ?? rows - 1; i >= (row ?? 0); --i)
            {
                int count = 0;

                for (int j = columns - 1; j >= 0; --j)
                {
                    if (row == null)
                        leftPanel.RowStyles.Add(new(SizeType.Absolute, 30));

                    if (BlackCells2D[j, i])
                        ++count;
                    else if (count > 0)
                    {
                        clues.Enqueue(count);
                        count = 0;
                    }
                }
                if (count > 0)
                    clues.Enqueue(count);
                if (clues.Count == 0)
                    clues.Enqueue(0);

                lastIndex = 7;
                while (clues.Count > 0)
                {
                    Label label = new();
                    label.Dock = DockStyle.Fill;
                    label.Text = clues.Dequeue().ToString();
                    label.TextAlign = ContentAlignment.MiddleRight;
                    leftPanel.Controls.Add(label, lastIndex--, i);
                }
            }

            if (column == null)
            {
                topPanel.ColumnCount = columns;
                topPanel.ColumnStyles.Clear();
                topPanel.Controls.Clear();
                topPanel.Left = gridPanel.Left;
                topPanel.Size = new(30 * columns, topPanel.Height);
                topPanel.Top = gridPanel.Top - topPanel.Height;
            }
            else
            {
                for (lastIndex = 7; lastIndex >= 0; --lastIndex)
                    topPanel.Controls.Remove(topPanel.GetControlFromPosition((int)column, lastIndex));
            }

            for (int i = column ?? columns - 1; i >= (column ?? 0); --i)
            {
                if (column == null)
                    topPanel.ColumnStyles.Add(new(SizeType.Absolute, 30));

                int count = 0;

                for (int j = rows - 1; j >= 0; --j)
                {
                    if (BlackCells2D[i, j])
                        ++count;
                    else if (count > 0)
                    {
                        clues.Enqueue(count);
                        count = 0;
                    }
                }
                if (count > 0)
                    clues.Enqueue(count);
                if (clues.Count == 0)
                    clues.Enqueue(0);

                lastIndex = 7;
                while (clues.Count > 0)
                {
                    Label label = new();
                    label.Dock = DockStyle.Fill;
                    label.Text = clues.Dequeue().ToString();
                    label.TextAlign = ContentAlignment.BottomCenter;
                    topPanel.Controls.Add(label, i, lastIndex--);
                }
            }
        }

        private void createPuzzleItem_Click(object sender, EventArgs e)
        {
            using Form2 testDialog = new() { Text = "Create your own Puzzle puzzle" };
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                PlayMode = false;
                DrawBoard(testDialog.Columns, testDialog.Rows);
                CreateNonogram();
                ShowLabels();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFileDialog = new() { Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*" };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bool[] blackCells1D = new bool[BlackCells2D.Length];
                Buffer.BlockCopy(BlackCells2D, 0, blackCells1D, 0, BlackCells2D.Length);
                Puzzle puzzle = new(
                    puzzleTitleTextBox.Text,
                    gridPanel.ColumnCount,
                    gridPanel.RowCount,
                    difficultyTextBox.Text,
                    blackCells1D);
                string jsonString = JsonSerializer.Serialize(puzzle, new());
                File.WriteAllText(saveFileDialog.FileName, jsonString);
            }
        }

        private void randomItem_Click(object sender, EventArgs e)
        {
            using Form2 randomDialog = new() { Text = "New Random Puzzle" };
            if (randomDialog.ShowDialog(this) == DialogResult.OK)
            {
                PlayMode = true;
                DrawBoard(randomDialog.Columns, randomDialog.Rows);
                CreateNonogram();
                ShowLabels();
            }
        }

        private void loadPuzzleItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new() { Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*" };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string jsonString = File.ReadAllText(openFileDialog.FileName);
                    var puzzle = JsonSerializer.Deserialize<Puzzle>(jsonString);

                    PlayMode = true;
                    DrawBoard(puzzle.Width, puzzle.Height);
                    CreateNonogram(puzzle.BlackCells1D);
                    ShowLabels();
                }
                catch (JsonException)
                {
                    MessageBox.Show("Incorrect JSON file!");
                }
            }
        }

        private void choosePuzzleItem_Click(object sender, EventArgs e)
        {
            using Form3 choosePuzzleDialog = new();
            if (choosePuzzleDialog.ShowDialog() == DialogResult.OK)
            {
                var puzzle = choosePuzzleDialog.ChosenPuzzle;
                if (puzzle != null)
                {
                    PlayMode = true;
                    DrawBoard(puzzle.Width, puzzle.Height);
                    CreateNonogram(puzzle.BlackCells1D);
                    ShowLabels();
                }
            }
        }
    }

}
