namespace NodeMap.UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnTest = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            SuspendLayout();
            // 
            // btnTest
            // 
            btnTest.Location = new Point(575, 76);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(125, 80);
            btnTest.TabIndex = 0;
            btnTest.Text = "Test Graph";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnCreateGraph;
            // 
            // button1
            // 
            button1.Location = new Point(585, 254);
            button1.Name = "button1";
            button1.Size = new Size(106, 70);
            button1.TabIndex = 1;
            button1.Text = "BFS Çalıştır";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnBFS;
            // 
            // button2
            // 
            button2.Location = new Point(663, 345);
            button2.Name = "button2";
            button2.Size = new Size(125, 69);
            button2.TabIndex = 2;
            button2.Text = "DFS Çalıştır";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnDFS;
            // 
            // button3
            // 
            button3.Location = new Point(504, 345);
            button3.Name = "button3";
            button3.Size = new Size(115, 69);
            button3.TabIndex = 3;
            button3.Text = "Dijkstra Çalıştır";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnDijkstra_Click;
            // 
            // button4
            // 
            button4.Location = new Point(24, 345);
            button4.Name = "button4";
            button4.Size = new Size(126, 71);
            button4.TabIndex = 4;
            button4.Text = "Degree Centrality";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnCentrality;
            // 
            // button5
            // 
            button5.Location = new Point(274, 336);
            button5.Name = "button5";
            button5.Size = new Size(148, 80);
            button5.TabIndex = 5;
            button5.Text = "AStar Çalıştır";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btnAStar_Click;
            // 
            // button6
            // 
            button6.Location = new Point(183, 282);
            button6.Name = "button6";
            button6.Size = new Size(75, 23);
            button6.TabIndex = 6;
            button6.Text = "Closeness ";
            button6.UseVisualStyleBackColor = true;
            button6.Click += btnCloseness_Click;
            // 
            // button7
            // 
            button7.Location = new Point(330, 284);
            button7.Name = "button7";
            button7.Size = new Size(75, 23);
            button7.TabIndex = 7;
            button7.Text = "btnBetweenness_Click";
            button7.UseVisualStyleBackColor = true;
            button7.Click += btnBetweenness_Click;
            // 
            // button8
            // 
            button8.Location = new Point(24, 281);
            button8.Name = "button8";
            button8.Size = new Size(98, 58);
            button8.TabIndex = 8;
            button8.Text = "Random Graph";
            button8.UseVisualStyleBackColor = true;
            button8.Click += btnRandomGraph_Click;
            // 
            // button9
            // 
            button9.Location = new Point(478, 280);
            button9.Name = "button9";
            button9.Size = new Size(75, 23);
            button9.TabIndex = 9;
            button9.Text = "Edge Aç/Kapat";
            button9.UseVisualStyleBackColor = true;
            button9.Click += btnToggleEdges_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnTest);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Paint += Form1_Paint;
            ResumeLayout(false);
        }

        #endregion

        private Button btnTest;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
    }
}
