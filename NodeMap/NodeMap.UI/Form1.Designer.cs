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
            button10 = new Button();
            button11 = new Button();
            button12 = new Button();
            txtNodeId = new TextBox();
            txtNodeName = new TextBox();
            txtEdgeFrom = new TextBox();
            txtEdgeTo = new TextBox();
            SuspendLayout();
            // 
            // btnTest
            // 
            btnTest.Location = new Point(885, 21);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(85, 46);
            btnTest.TabIndex = 0;
            btnTest.Text = "Test Graph";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnCreateGraph;
            // 
            // button1
            // 
            button1.Location = new Point(1054, 269);
            button1.Name = "button1";
            button1.Size = new Size(106, 70);
            button1.TabIndex = 1;
            button1.Text = "BFS Çalıştır";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnBFS;
            // 
            // button2
            // 
            button2.Location = new Point(1054, 355);
            button2.Name = "button2";
            button2.Size = new Size(106, 61);
            button2.TabIndex = 2;
            button2.Text = "DFS Çalıştır";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnDFS;
            // 
            // button3
            // 
            button3.Location = new Point(1054, 422);
            button3.Name = "button3";
            button3.Size = new Size(106, 69);
            button3.TabIndex = 3;
            button3.Text = "Dijkstra Çalıştır";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnDijkstra_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1287, 378);
            button4.Name = "button4";
            button4.Size = new Size(126, 71);
            button4.TabIndex = 4;
            button4.Text = "Degree Centrality";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnCentrality;
            // 
            // button5
            // 
            button5.Location = new Point(1054, 511);
            button5.Name = "button5";
            button5.Size = new Size(122, 58);
            button5.TabIndex = 5;
            button5.Text = "AStar Çalıştır";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btnAStar_Click;
            // 
            // button6
            // 
            button6.Location = new Point(1274, 281);
            button6.Name = "button6";
            button6.Size = new Size(172, 74);
            button6.TabIndex = 6;
            button6.Text = "Closeness ";
            button6.UseVisualStyleBackColor = true;
            button6.Click += btnCloseness_Click;
            // 
            // button7
            // 
            button7.Location = new Point(1297, 203);
            button7.Name = "button7";
            button7.Size = new Size(131, 55);
            button7.TabIndex = 7;
            button7.Text = "btnBetweenness_Click";
            button7.UseVisualStyleBackColor = true;
            button7.Click += btnBetweenness_Click;
            // 
            // button8
            // 
            button8.Location = new Point(1301, 480);
            button8.Name = "button8";
            button8.Size = new Size(127, 71);
            button8.TabIndex = 8;
            button8.Text = "Random Graph";
            button8.UseVisualStyleBackColor = true;
            button8.Click += btnRandomGraph_Click;
            // 
            // button9
            // 
            button9.Location = new Point(1297, 137);
            button9.Name = "button9";
            button9.Size = new Size(127, 60);
            button9.TabIndex = 9;
            button9.Text = "Edge Aç/Kapat";
            button9.UseVisualStyleBackColor = true;
            button9.Click += btnToggleEdges_Click;
            // 
            // button10
            // 
            button10.Location = new Point(1067, 219);
            button10.Name = "button10";
            button10.Size = new Size(75, 23);
            button10.TabIndex = 10;
            button10.Text = "Auto node ekle";
            button10.UseVisualStyleBackColor = true;
            button10.Click += btnAddNode_Click;
            // 
            // button11
            // 
            button11.Location = new Point(1088, 12);
            button11.Name = "button11";
            button11.Size = new Size(88, 38);
            button11.TabIndex = 11;
            button11.Text = "Manuel Node ekle";
            button11.UseVisualStyleBackColor = true;
            button11.Click += btnAddNodeWithId_Click;
            // 
            // button12
            // 
            button12.Location = new Point(1088, 78);
            button12.Name = "button12";
            button12.Size = new Size(88, 23);
            button12.TabIndex = 12;
            button12.Text = "Manuel Edge";
            button12.UseVisualStyleBackColor = true;
            button12.Click += btnAddEdge_Click;
            // 
            // txtNodeId
            // 
            txtNodeId.Location = new Point(1287, 21);
            txtNodeId.Name = "txtNodeId";
            txtNodeId.PlaceholderText = "ID";
            txtNodeId.Size = new Size(50, 23);
            txtNodeId.TabIndex = 13;
            // 
            // txtNodeName
            // 
            txtNodeName.Location = new Point(1202, 21);
            txtNodeName.Name = "txtNodeName";
            txtNodeName.PlaceholderText = "Name";
            txtNodeName.Size = new Size(60, 23);
            txtNodeName.TabIndex = 14;
            // 
            // txtEdgeFrom
            // 
            txtEdgeFrom.Location = new Point(1202, 79);
            txtEdgeFrom.Name = "txtEdgeFrom";
            txtEdgeFrom.PlaceholderText = "From";
            txtEdgeFrom.Size = new Size(50, 23);
            txtEdgeFrom.TabIndex = 15;
            // 
            // txtEdgeTo
            // 
            txtEdgeTo.Location = new Point(1287, 79);
            txtEdgeTo.Name = "txtEdgeTo";
            txtEdgeTo.PlaceholderText = "To";
            txtEdgeTo.Size = new Size(50, 23);
            txtEdgeTo.TabIndex = 16;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1530, 641);
            Controls.Add(button12);
            Controls.Add(button11);
            Controls.Add(button10);
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
            Controls.Add(txtNodeId);
            Controls.Add(txtNodeName);
            Controls.Add(txtEdgeFrom);
            Controls.Add(txtEdgeTo);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Paint += Form1_Paint;
            ResumeLayout(false);
            PerformLayout();
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
        private Button button10;
        private Button button11;
        private Button button12;

        private TextBox txtNodeId;
        private TextBox txtNodeName;
        private TextBox txtEdgeFrom;
        private TextBox txtEdgeTo;

    }



}
