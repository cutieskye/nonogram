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

    public partial class Form3 : Form
    {
        public Form1.Puzzle ChosenPuzzle { get; private set; }
        private List<Form1.Puzzle> Puzzles { get; set; }

        public Form3()
        {
            InitializeComponent();
        }

        private void WalkDirectory()
        {
            string path = textBox.Text;
            if (path == "")
                return;

            listView.Items.Clear();
            Puzzles = new();
            var fileList = new DirectoryInfo(path).GetFiles();
            var fileQuery =
                from file in fileList
                where file.Extension == ".json"
                orderby file.Name
                select file;

            foreach (var file in fileQuery)
            {
                try
                {
                    string jsonString = File.ReadAllText(file.FullName);
                    var puzzle = JsonSerializer.Deserialize<Form1.Puzzle>(jsonString);
                    ListViewItem listViewItem = new(new[] {
                            puzzle.Title,
                            puzzle.Width.ToString(),
                            puzzle.Height.ToString(),
                            puzzle.Difficulty });
                    listView.Items.Add(listViewItem);
                    Puzzles.Add(puzzle);
                }
                catch (JsonException) { }
            }
        }

        private void chooseDirectoryButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new() { AutoUpgradeEnabled = false };
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox.Text = folderBrowserDialog.SelectedPath;
                WalkDirectory();
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            WalkDirectory();
        }

        private void loadPuzzleButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                int index = listView.SelectedIndices[0];
                ChosenPuzzle = Puzzles[index];
            }
        }
    }

}
