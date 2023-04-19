﻿using pbl3_Cinema.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using pbl3_Cinema.DTO;
using pbl3_Cinema.View.AdminView.ManageScreening;

namespace pbl3_Cinema.View.AdminView.ManageScreen
{
    public partial class Form_ManageScreening : Form
    {
        public Form_ManageScreening()
        {
            InitializeComponent();
            SetCBB();
            SetShowDayPicker();

            Cinema_BLL bll = new Cinema_BLL();
            dataGridView_Screening.DataSource = bll.GetAllScreeningInfor();
        }

        private void SetCBB()
        {
            Cinema_BLL bll = new Cinema_BLL();
            cbb_Auditorium.Items.Add(new CBBAuditorium
            {
                id = 0,
                nameAuditorium = "Tất cả"
            });
            cbb_Auditorium.Items.AddRange(bll.GetAllCBBAuditorimActive().ToArray());
            cbb_Auditorium.SelectedIndex = 0;
        }

        private void SetShowDayPicker()
        {
            dateTimePicker.MinDate = DateTime.Now;
            dateTimePicker.MaxDate = DateTime.Now.AddDays(7);
        }

        private void ShowAllScreeningFollowFilter()
        {
            DateTime dayFilter = dateTimePicker.Value;
            CBBAuditorium cbb = cbb_Auditorium.SelectedItem as CBBAuditorium;

            Cinema_BLL bll = new Cinema_BLL();
            if (cbb.id == 0)
            {
                dataGridView_Screening.DataSource = bll.GetScreeningInforsFilter(dayFilter);
            }
            else
            {
                dataGridView_Screening.DataSource = bll.GetScreeningInforsFilter(dayFilter, cbb.id);
            }
            
        }

        private void btn_Filter_Click(object sender, EventArgs e)
        {
            //ShowAllScreeningFollowFilter();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Form_AddScreening form = new Form_AddScreening();
            form.ShowDialog();
            Cinema_BLL bll = new Cinema_BLL();
            dataGridView_Screening.DataSource = bll.GetAllScreeningInfor();
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            ShowAllScreeningFollowFilter();
        }

        private void btn_AllScreening_Click(object sender, EventArgs e)
        {
            Cinema_BLL bll = new Cinema_BLL();
            dataGridView_Screening.DataSource = bll.GetAllScreeningInfor();
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            if (dataGridView_Screening.SelectedRows.Count == 0)
            {
                MessageBox.Show("Chọn suất chiếu muốn xóa");
                return;
            }

            int screen_id = Convert.ToInt32(dataGridView_Screening.SelectedRows[0].Cells["id"].Value);

            Cinema_BLL bll = new Cinema_BLL();
            if (bll.CanDeleteScreening(screen_id) == true)
            {
                if (bll.DeleteScreeningById(screen_id) == 1) 
                {
                    MessageBox.Show("Xóa thành công");
                    dataGridView_Screening.DataSource = bll.GetAllScreeningInfor();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại");
                }
            }
        }
    }
}
