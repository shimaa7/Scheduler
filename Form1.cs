using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Schedualing
{
    public partial class Form1 : Form
    {
        int Counter = 0,watingTime =0,timeline = 0, pCounter = 0, sliceOfTime=0;
        string GantChart = "";
        int[] ProcessBurstTime = new int[1000];
        int[] ProcessArrivalTime = new int[1000];
        String[] ProcessName = new string[1000];
        int[] Piriority = new int[1000];
        public void FCFS()
        {
         for(int k = 0; k < Counter; k++) //sorting 
            {
                for (int i = k + 1; i < Counter; i++) 
                {
                    if (ProcessArrivalTime[k] > ProcessArrivalTime[i] || (ProcessArrivalTime[k] == ProcessArrivalTime[i] && ProcessBurstTime[k] > ProcessBurstTime[i]))
                    {
                       int temp1 = ProcessArrivalTime[i];
                       int temp2 = ProcessBurstTime[i];
                       String temp3 = ProcessName[i];
                        ProcessArrivalTime[i] = ProcessArrivalTime[k];
                        ProcessArrivalTime[k] = temp1;
                        ProcessBurstTime[i] = ProcessBurstTime[k];
                        ProcessBurstTime[k] = temp2;
                        ProcessName[i] = ProcessName[k];
                        ProcessName[k] = temp3;

                    }
                }
            }
            int wait = 0;
            for (int k = 0; k < Counter; k++) //ganttchart 
            {
                GantChart += timeline;
                GantChart += " ";
                GantChart += ProcessName[k];
                GantChart += " ";
                for (int o = 0; o < ProcessBurstTime[k]; o++)
                {
                    chart1.Series["Processes"].Points.AddXY(timeline + o, 20);
                    chart1.Series["Processes"].Points[timeline + o].AxisLabel += "t" + (timeline + 1 + o).ToString() + " " + ProcessName[k];
                }
                timeline += ProcessBurstTime[k];
                GantChart += timeline;
                GantChart += " ";
                if (k != Counter - 1) watingTime += ProcessBurstTime[k] - ProcessArrivalTime[k + 1] + wait;
                wait += ProcessBurstTime[k];
            }
         
            GantChart = (watingTime*1.0/Counter).ToString();
        }
        public void SJFNonPreemptive()
        {
            int n = Counter;
            float total;
            float[] wait = new float[1000];
            float twaiting = 0, waiting = 0;
            int proc;
            int[] stack = new int[1000];
            float[] brust = new float[1000];
            float[] arrival = new float[1000];
            float sbrust = 0, top = Counter;
            float[] temp= new float[1000];

            for (int i = 0; i < n; i++)
            {
                stack[i] = i;
                arrival[i] = ProcessArrivalTime[i];
                brust[i] = ProcessBurstTime[i];
                temp[i] = arrival[i];
                sbrust = brust[i] + sbrust;


            }

            for (int i = 0; i < sbrust; i++)
            {
                proc = stack[0];
                if (temp[proc] == i)
                {
                    //temp[proc]=i+1;;
                    twaiting = 0;
                }
                else
                {
                    twaiting = i - (temp[proc]);

                }
                temp[proc] = i + 1;

                wait[proc] = wait[proc] + twaiting;
                waiting = waiting + (twaiting);
                brust[proc] = brust[proc] - 1;

                if (brust[proc] == 0)
                {
                    for (int x = 0; x < top - 1; x++)
                    {
                        stack[x] = stack[x + 1];
                    }
                    top = top - 1;
                    for (int z = 0; z < top - 1; z++)
                    {
                        if ((brust[stack[0]] > brust[stack[z + 1]]) && (arrival[stack[z + 1]] <= i + 1))
                        {
                            int t = stack[0];
                            stack[0] = stack[z + 1];
                            stack[z + 1] = t;
                        }
                    }
                }
            }

            GantChart = (waiting*1.0/Counter).ToString();
        }
        public void SJFPreemptive()
        {
            int n = Counter;
            float total;
            float[] wait = new float[1000];
            float twaiting = 0, waiting = 0;
            int proc;
            int[] stack = new int[1000];
            float[] brust = new float[1000];
            float[] arrival = new float[1000];
            float sbrust = 0, top = Counter;
            float[] temp = new float[1000];

            for (int i = 0; i < n; i++)
            {
                stack[i] = i;
                arrival[i] = ProcessArrivalTime[i];
                brust[i] = ProcessBurstTime[i];
                temp[i] = arrival[i];
                sbrust = brust[i] + sbrust;


            }

            for (int i = 0; i < sbrust; i++)
            {
                proc = stack[0];
                if (temp[proc] == i)
                    twaiting = 0;
                else
                    twaiting = i - (temp[proc]);
                temp[proc] = i + 1;

                wait[proc] = wait[proc] + twaiting;
                waiting = waiting + (twaiting);
                brust[proc] = brust[proc] - 1;

                if (brust[proc] == 0)
                {
                    for (int x = 0; x < top - 1; x++)
                        stack[x] = stack[x + 1];
                    top = top - 1;
                }
                for (int z = 0; z < top - 1; z++)
                {
                    if ((brust[stack[0]] > brust[stack[z + 1]]) && (arrival[stack[z + 1]] <= i + 1))
                    {
                        int t = stack[0];
                        stack[0] = stack[z + 1];
                        stack[z + 1] = t;
                    }
                }
            }
            GantChart = (waiting*1.0/Counter).ToString();
        }
        public void PiriorityNonPreemptive()
        {
            int n = Counter;
            float total;
            float[] wait = new float[1000];
            float[] p = new float[1000];
            float twaiting = 0, waiting = 0;
            int proc;
            int[] stack=new int[1000];
            float[] brust = new float[1000];
            float[] arrival = new float[1000];
            float[] temp = new float[1000];
            float[] prority = new float[1000];
            float sbrust = 0,top = Counter;
            int i;
            for (i = 0; i < n; i++)
            {
                stack[i] = i;
                arrival[i] = ProcessArrivalTime[i];
                brust[i] = ProcessBurstTime[i];
                prority[i]= Piriority[i];
                temp[i] = arrival[i];
                sbrust = brust[i] + sbrust;

            }

            for (i = 0; i < sbrust; i++)
            {
                //section 1
                proc = stack[0];
                if (temp[proc] == i)
                {

                    twaiting = 0;
                }
                else
                {
                    twaiting = i - (temp[proc]);
                }
                temp[proc] = i + 1;
                wait[proc] = wait[proc] + twaiting;
                waiting = waiting + (twaiting);
                brust[proc] = brust[proc] - 1;

                if (brust[proc] == 0)
                {
                    for (int x = 0; x < top - 1; x++)
                    {
                        stack[x] = stack[x + 1];

                    }
                    top = top - 1;


                    for (int z = 0; z < top - 1; z++)
                    {
                        if ((prority[stack[0]] > prority[stack[z + 1]]) && (arrival[stack[z + 1]] <= i + 1))
                        {
                            int t = stack[0];
                            stack[0] = stack[z + 1];
                            stack[z + 1] = t;
                        }

                    }

                }


            }
            GantChart = (waiting*1.0 / n).ToString();
        }
        public void PriorityPreemptive()
        {
            int n = Counter;
            float total;
            float[] wait = new float[1000];
            float[] p = new float[1000];
            float twaiting = 0, waiting = 0;
            int proc;
            int[] stack = new int[1000];
            float[] brust = new float[1000];
            float[] arrival = new float[1000];
            float[] temp = new float[1000];
            float[] prority = new float[1000];
            float sbrust = 0, top = Counter;
            int i;
            for (i = 0; i < n; i++)
            {
                stack[i] = i;
                arrival[i] = ProcessArrivalTime[i];
                brust[i] = ProcessBurstTime[i];
                prority[i] = Piriority[i];
                temp[i] = arrival[i];
                sbrust = brust[i] + sbrust;

            }
            for (i = 0; i < sbrust; i++)
            {
                //section 1
                proc = stack[0];
                if (temp[proc] == i)
                    twaiting = 0;
                else
                    twaiting = i - (temp[proc]);
                temp[proc] = i + 1;
                wait[proc] = wait[proc] + twaiting;
                waiting = waiting + (twaiting);
                brust[proc] = brust[proc] - 1;

                if (brust[proc] == 0)
                {
                    for (int x = 0; x < top - 1; x++)
                        stack[x] = stack[x + 1];
                    top = top - 1;
                }
                for (int z = 0; z < top - 1; z++)
                {
                    if ((prority[stack[0]] > prority[stack[z + 1]]) && (arrival[stack[z + 1]] <= i + 1))
                    {
                        int t = stack[0];
                        stack[0] = stack[z + 1];
                        stack[z + 1] = t;
                    }
                }
            }
            GantChart = (waiting *1.0/ n).ToString();
        }
        public void RR2()
        {
            int count, j=0, n = Counter, time, remain, flag = 0, time_quantum;
            int wait_time = 0, turnaround_time = 0;
            int[] at = new int[1000];
            int[] bt = new int[1000];
            int[] rt = new int[1000];
            remain = n;
            for (count = 0; count < n; count++)
            {
                at[count] = ProcessArrivalTime[count];
                bt[count] = ProcessBurstTime[count];
                rt[count] = bt[count];
            }
            time_quantum = sliceOfTime;
            for (time = 0, count = 0; remain != 0;)
            {
                if (rt[count] <= time_quantum && rt[count] > 0)
                {
                    time += rt[count];
                    rt[count] = 0;
                    flag = 1;
                }
                else if (rt[count] > 0)
                {
                    rt[count] -= time_quantum;
                    time += time_quantum;
                }
                if (rt[count] == 0 && flag == 1)
                {
                    remain--;
                   // printf("P[%d]\t|\t%d\t|\t%d\n", count + 1, time - at[count], time - at[count] - bt[count]);
                    wait_time += time - at[count] - bt[count];
                    turnaround_time += time - at[count];
                    flag = 0;
                }
                if (count == n - 1)
                    count = 0;
                else if (at[count + 1] <= time)
                    count++;
                else
                    count = 0;
            }
            GantChart = (wait_time * 1.0 / n).ToString();

        }
        public void RR()
        {
            int n = Counter;
            float total;
            float[] wait = new float[1000];
            float[] p = new float[1000];
            float twaiting = 0, waiting = 0;
            int proc;
            int[] stack = new int[1000];
            float[] brust = new float[1000];
            float[] arrival = new float[1000];
            float[] temp = new float[1000];
            float[] prority = new float[1000];
            float sbrust = 0, top = Counter;
            for (int j = 0; j < n; j++)
            { 
                p[j] = j;
                stack[j] = j;
                arrival[j] = ProcessArrivalTime[j];
                brust[j]= ProcessBurstTime[j];
                temp[j] = arrival[j];
                sbrust = brust[j] + sbrust;
            }
            int cont = sliceOfTime;
            int i = 0;
            while (true)
            {
                for (int m = 0; m < cont; m++)
                {
                    if (i == sbrust)
                        break;
                    proc = stack[0];
                    if (temp[proc] == i)
                        twaiting = 0;
                    else
                        twaiting = i - (temp[proc]);
                    temp[proc] = i + 1;
                    wait[proc] = wait[proc] + twaiting;
                    waiting = waiting + (twaiting);
                    brust[proc] = brust[proc] - 1;
                    if (brust[proc] == 0)
                    {
                        for (int x = 0; x < top - 1; x++)
                            stack[x] = stack[x + 1];
                        top = top - 1;
                        m = -1;
                    }
                    i = i + 1;
                }
                if (i == sbrust)
                    break;
                int tp = stack[0];
                for (int x = 0; x < top - 1; x++)
                    stack[x] = stack[x + 1];
                stack[(int)(top - 1)] = tp;
              //  m = -1;
            }
            GantChart = ((float)waiting / n).ToString();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            domainUpDown1.ReadOnly = true;
        }
  
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void domainUpDown2_SelectedItemChanged(object sender, EventArgs e)
        {
            domainUpDown2.ReadOnly = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void domainUpDown3_SelectedItemChanged(object sender, EventArgs e)
        {
            domainUpDown3.ReadOnly = true;

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            domainUpDown1.Items.Clear();
            domainUpDown2.Items.Clear();
            domainUpDown3.Items.Clear();
            domainUpDown4.Items.Clear();
            domainUpDown1.Text = "";
            domainUpDown2.Text = "";
            domainUpDown3.Text = "";
            domainUpDown4.Text = "";
            label10.Text = "0";
            textBox1.Text = "Enter ...";
            textBox2.Text = "Enter ...";
            textBox3.Text = "Enter ...";
            textBox6.Text = "Enter ...";
            domainUpDown1.Text = "Press Down";
            domainUpDown2.Text = "Press Down";
            domainUpDown3.Text = "Press Down";
            domainUpDown4.Text = "Press Down";
            GantChart = "";
            watingTime = 0;
            timeline = 0;
            for (int i = 0; i < Counter; i++)
            {
                ProcessName[i] = "";
                ProcessArrivalTime[i] = 0;
                ProcessBurstTime[i] = 0;
            }
            for (int i = 0; i < pCounter; i++)
            {
                Piriority[i] = 0;
            }
            pCounter = 0;
            Counter = 0;
            label10.Text = Counter.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GantChart = "";
            label11.Text = "0";
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            timeline = 0;
            watingTime = 0;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //textBox4.ReadOnly = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
                     int preemptive = 0;
                     if(radioButton1.Checked == true)
                     {
                         FCFS();
                     }else if (radioButton2.Checked == true)
                     {
                         if (checkBox1.Checked == true) preemptive = 1;
                         if (preemptive == 1) SJFPreemptive();
                         else SJFNonPreemptive();
                     }
                     else if (radioButton4.Checked == true)
                     {
                         if (checkBox1.Checked == true) preemptive = 1;
                         if (preemptive == 1) PriorityPreemptive();
                         else PiriorityNonPreemptive();
                     }
                     else if(radioButton3.Checked == true)
                     {
                         RR2();
                     }

            label11.Text = GantChart;
            //int i = 0;
            // chart1.Series.Add("Processes");
            //chart1.Series.
          /*  foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }*/

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        if(checkBox1.Checked == true)
            {
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Enabled = false;
            }
            else
            {
                checkBox1.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true || radioButton4.Checked == true)
            {
                checkBox1.Visible = true;
                checkBox2.Visible = true;
            }
            if (radioButton2.Checked == true)
            {
                label7.Visible = true;
                label7.Text = "Shortest Job First:\nthe process with the smallest execution time is selected for execution next.\nShortest job first can be either preemptive or non-preemptive.\nOwing to its simple nature,\nshortest job first is considered optimal.\nIt also reduces the average waiting time for\nother processes awaiting execution.\n*Preemptive: each process take slice of time and waiting\n to take another time until finished.\n*Non-Preemptive: each process execute until finished.";
                label9.Visible = false;
                label14.Visible = false;
                textBox6.Visible = false;
                domainUpDown4.Visible = false;
                button4.Visible = false;
            }

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true || radioButton4.Checked == true)
            {
                checkBox1.Visible = true;
                checkBox2.Visible = true;
            }
            if (radioButton4.Checked == true)
            {
                label7.Visible = true;
                label7.Text = "Priority:\nPriority scheduling involves priority assignment to every process,\nand processes with higher priorities are carried out first.\n*Preemptive: each process take slice of time and waiting\n to take another time until finished.\n*Non-Preemptive: each process execute until finished.";
                label9.Visible = true;
                label14.Visible = true;
                label9.Text = "Enter Priority Of Process";
                textBox6.Visible = true;
                textBox6.Text = "Enter ...";
                domainUpDown4.Visible = true;
                button4.Visible = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true || radioButton3.Checked == true)
            {
                checkBox1.Visible = false;
                checkBox2.Visible = false;
            }
            if (radioButton3.Checked == true)
            {
                label7.Visible = true;
                label7.Text = "Round Robain:\nIs considered to be very fair, as it uses time slices\nthat are assigned to each process in the queue or line.\nEach process is then allowed to use the CPU for a given amount\nof time, and if it does not finish within the allotted time.\n*It is preempted and then moved at the back of the line\nso that the next process in line is able to use the CPU\nfor the same amount of time.";
                label9.Visible = true;
                label14.Visible = false;
                label9.Text = "Enter The Countum Of Time";
                textBox6.Visible = true;
                textBox6.Text = "Enter ...";
                domainUpDown4.Visible = false;
                button4.Visible = true;
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
   
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
   
        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {
          
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
         //   trackBar1.Value = 50;
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text == "Enter ...")
            {
                textBox3.Text = "";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "Enter ...")
            {
                textBox2.Text = "";
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter ...")
            {
                textBox1.Text = "";
            }
        }

        private void domainUpDown3_Enter(object sender, EventArgs e)
        {
            if (domainUpDown3.Text == "Press Down")
            {
                domainUpDown3.Text = "";
            }
        }

        private void domainUpDown2_Enter(object sender, EventArgs e)
        {
            if (domainUpDown2.Text == "Press Down")
            {
                domainUpDown2.Text = "";
            }
        }

        private void domainUpDown1_Enter(object sender, EventArgs e)
        {
            if (domainUpDown1.Text == "Press Down")
            {
                domainUpDown1.Text = "";
            }
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Enter ...")
            {
                textBox6.Text = "";
            }
        }

        private void domainUpDown4_Enter(object sender, EventArgs e)
        {
            if (domainUpDown4.Text == "Press Down")
            {
                domainUpDown4.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                domainUpDown4.Items.Add(textBox6.Text);
                domainUpDown4.Text = textBox6.Text;
                string temp = textBox6.Text;
                int inttemp = Int32.Parse(temp);
                Piriority[pCounter] = inttemp;
                label14.Text = pCounter.ToString();
                pCounter++;
            }
            else if (radioButton3.Checked == true)
            {
                sliceOfTime = Int32.Parse(textBox6.Text);
            }
        }

        private void domainUpDown4_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true || radioButton3.Checked == true)
            {
                checkBox1.Visible = false;
                checkBox2.Visible = false;
            }
            if(radioButton1.Checked == true)
            {
                label7.Visible = true;
                label7.Text = "First Come First Servese:\nWhat comes first is handled first,\nit is non-preemptive,\nthe next request in line will be executed once\nthe one before it is complete.\n*Non-Preemptive: each process execute until finished.";
                label9.Visible = false;
                label14.Visible = false;
                textBox6.Visible = false;
                domainUpDown4.Visible = false;
                button4.Visible = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            domainUpDown1.Items.Add(textBox1.Text);
            domainUpDown2.Items.Add(textBox2.Text);
            domainUpDown3.Items.Add(textBox3.Text);
            domainUpDown1.Text=textBox1.Text;
            domainUpDown2.Text=textBox2.Text;
            domainUpDown3.Text=textBox3.Text;
            string temp = textBox3.Text;
            int inttemp = Int32.Parse(temp);
            ProcessBurstTime[Counter] = inttemp;
            temp = textBox2.Text;
            inttemp = Int32.Parse(temp);
            ProcessArrivalTime[Counter] = inttemp;
            temp = textBox1.Text;
            ProcessName[Counter] = temp;
            Counter++;
            label10.Text = Counter.ToString();
        }
    }
}
