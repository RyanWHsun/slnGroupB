﻿using prjGroupB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Attractions.Views {
    public partial class FormAttractionMain : Form {
        public FormAttractionMain() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            FormAttractionList f = new FormAttractionList();
            f.ShowDialog();
        }

        private void btnCategory_Click(object sender, EventArgs e) {
            FormAttractionCategoryList f = new FormAttractionCategoryList();
            f.ShowDialog();
        }

        private void btnTag_Click(object sender, EventArgs e) {
            FormTagList f = new FormTagList();
            f.ShowDialog();
        }

        private void btnRecommendation_Click(object sender, EventArgs e) {
            FormRecommendationList f = new FormRecommendationList();
            f.ShowDialog();
        }

        private void btnImage_Click(object sender, EventArgs e) {
            FormImageList f = new FormImageList();
            f.ShowDialog();
        }

        private void btnComment_Click(object sender, EventArgs e) {
            FormCommentList f = new FormCommentList();
            f.ShowDialog();
        }

        private void btnTicket_Click(object sender, EventArgs e) {
            FormTicketList f = new FormTicketList();
            f.ShowDialog();
        }

        private void btnAttraction_MouseHover(object sender, EventArgs e) {
            btnAttraction.BackColor = Color.Black;
            btnAttraction.ForeColor = Color.White;
        }

        private void btnAttraction_MouseLeave(object sender, EventArgs e) {
            btnAttraction.BackColor = Color.White;
            btnAttraction.ForeColor = Color.Black;
        }

        private void btnCategory_MouseHover(object sender, EventArgs e) {
            btnCategory.BackColor = Color.Black;
            btnCategory.ForeColor = Color.White;
        }

        private void btnCategory_MouseLeave(object sender, EventArgs e) {
            btnCategory.BackColor = Color.White;
            btnCategory.ForeColor = Color.Black;
        }

        private void btnTag_MouseHover(object sender, EventArgs e) {
            btnTag.BackColor = Color.Black;
            btnTag.ForeColor = Color.White;
        }

        private void btnTag_MouseLeave(object sender, EventArgs e) {
            btnTag.BackColor = Color.White;
            btnTag.ForeColor = Color.Black;
        }

        private void btnRecommendation_MouseHover(object sender, EventArgs e) {
            btnRecommendation.BackColor = Color.Black;
            btnRecommendation.ForeColor = Color.White;
        }

        private void btnRecommendation_MouseLeave(object sender, EventArgs e) {
            btnRecommendation.BackColor = Color.White;
            btnRecommendation.ForeColor = Color.Black;
        }

        private void btnImage_MouseHover(object sender, EventArgs e) {
            btnImage.BackColor = Color.Black;
            btnImage.ForeColor = Color.White;
        }

        private void btnImage_MouseLeave(object sender, EventArgs e) {
            btnImage.BackColor = Color.White;
            btnImage.ForeColor = Color.Black;
        }

        private void btnComment_MouseHover(object sender, EventArgs e) {
            btnComment.BackColor = Color.Black;
            btnComment.ForeColor = Color.White;
        }

        private void btnComment_MouseLeave(object sender, EventArgs e) {
            btnComment.BackColor = Color.White;
            btnComment.ForeColor = Color.Black;
        }

        private void btnTicket_MouseHover(object sender, EventArgs e) {
            btnTicket.BackColor = Color.Black;
            btnTicket.ForeColor = Color.White;
        }

        private void btnTicket_MouseLeave(object sender, EventArgs e) {
            btnTicket.BackColor = Color.White;
            btnTicket.ForeColor = Color.Black;
        }
    }
}
