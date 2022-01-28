namespace _2048WinFormsApp
{
    public partial class MainForm : Form
    {
        private const int mapSize = 4;
        private Label[,] labelsMap;
        private static readonly Random random = new();
        private int score = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitMap();
            GenerateNumber();
            ShowScore();
        }

        private void ShowScore()
        {
            scoreLabel.Text = score.ToString();
        }

        private void InitMap()
        {
            labelsMap = new Label[mapSize, mapSize];
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    var newLabel = CreateLabel(i, j);
                    Controls.Add(newLabel);
                    labelsMap[i, j] = newLabel;
                }
            }
        }

        private void GenerateNumber()
        {
            while (true)
            {
                var randomNumberLabel = random.Next(mapSize * mapSize);
                var indexRow = randomNumberLabel / mapSize;
                var indexColumn = randomNumberLabel % mapSize;
                if (labelsMap[indexRow, indexColumn].Text == String.Empty)
                {
                    labelsMap[indexRow, indexColumn].Text = "2";
                    break;
                }
            }
        }

        private Label CreateLabel(int indexRow, int indexColumn)
        {
            Label? label = new();
            label.BackColor = SystemColors.ButtonShadow;
            label.Font = new Font("Bahnschrift Condensed", 18F, FontStyle.Bold, GraphicsUnit.Point);
            label.Size = new Size(70, 70);
            label.TextAlign = ContentAlignment.MiddleCenter;
            int x = 12 + indexColumn * 76;
            int y = 112 + indexRow * 76;
            label.Location = new Point(x, y);
            label.TextChanged += Label_TextChanged;
            return label;
        }

        private void Label_TextChanged(object sender, EventArgs e)
        {
            var label = (Label)sender;
            UpdateLabelBackColor(label);
        }

        private void MainForm_KeyDirection(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Right && e.KeyCode != Keys.Left && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                return;
            }
            var isMerged = false;
            if (e.KeyCode == Keys.Right)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = mapSize - 1; j >= 0; j--)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                    {
                                        var number = int.Parse(labelsMap[i, j].Text);
                                        score += number * 2;
                                        isMerged = true;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[i, k].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = mapSize - 1; j >= 0; j--)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[i, k].Text;
                                    labelsMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = j + 1; k < mapSize; k++)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[i, k].Text)
                                    {
                                        var number = int.Parse(labelsMap[i, j].Text);
                                        score += number * 2;
                                        isMerged = true;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[i, k].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = j + 1; k < mapSize; k++)
                            {
                                if (labelsMap[i, k].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[i, k].Text;
                                    labelsMap[i, k].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = 0; i < mapSize; i++)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = i + 1; k < mapSize; k++)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                    {
                                        var number = int.Parse(labelsMap[i, j].Text);
                                        score += number * 2;
                                        isMerged = true;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[k, j].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = 0; i < mapSize; i++)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = i + 1; k < mapSize; k++)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[k, j].Text;
                                    labelsMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = mapSize - 1; i >= 0; i--)
                    {
                        if (labelsMap[i, j].Text != string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    if (labelsMap[i, j].Text == labelsMap[k, j].Text)
                                    {
                                        var number = int.Parse(labelsMap[i, j].Text);
                                        score += number * 2;
                                        isMerged = true;
                                        labelsMap[i, j].Text = (number * 2).ToString();
                                        labelsMap[k, j].Text = string.Empty;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                for (int j = 0; j < mapSize; j++)
                {
                    for (int i = mapSize - 1; i >= 0; i--)
                    {
                        if (labelsMap[i, j].Text == string.Empty)
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (labelsMap[k, j].Text != string.Empty)
                                {
                                    labelsMap[i, j].Text = labelsMap[k, j].Text;
                                    labelsMap[k, j].Text = string.Empty;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            ShowScore();

            if (IsFullMap() && isMerged)
            {
                MessageBox.Show("Конец игры", "Завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            GenerateNumber();

        }

        private void UpdateLabelBackColor(Label label)
        {
            var indicator = 0;
            if (label.Text == string.Empty)
            {
                indicator=0;
            }
            else
            {
                var number = Int32.Parse(label.Text);
                indicator = (int)Math.Log(number, 2);
            }
            Color[] colors = new Color[12];
            colors[0] = SystemColors.ButtonShadow;
            colors[1] = Color.AliceBlue;
            colors[2] = Color.MediumBlue;
            colors[3] = Color.MediumOrchid;
            colors[4] = Color.MediumSlateBlue;
            colors[5] = Color.MintCream;
            colors[6] = Color.OldLace;
            colors[7] = Color.Orange;
            colors[8] = Color.Orchid;
            colors[9] = Color.PaleVioletRed;
            colors[10] = Color.Peru;
            colors[11] = Color.SkyBlue;

            label.BackColor = colors[indicator];
        }

        private bool IsFullMap()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (labelsMap[i, j].Text == string.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}