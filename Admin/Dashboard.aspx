<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Aarambha.Admin.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mb-4">
        <div class="col-md-3">
            <div class="card text-white bg-primary p-3 shadow-sm">
                <h6 class="card-title">Total Students</h6>
                <h2><asp:Literal ID="ltStudents" runat="server">0</asp:Literal></h2>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-white bg-success p-3 shadow-sm">
                <h6 class="card-title">Total Subjects</h6>
                <h2><asp:Literal ID="ltSubjects" runat="server">0</asp:Literal></h2>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-white bg-warning p-3 shadow-sm">
                <h6 class="card-title">Total Quizzes</h6>
                <h2><asp:Literal ID="ltQuizzes" runat="server">0</asp:Literal></h2>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-white bg-danger p-3 shadow-sm">
                <h6 class="card-title">Unread Messages</h6>
                <h2><asp:Literal ID="ltMessages" runat="server">0</asp:Literal></h2>
            </div>
        </div>
    </div>
    <div class="row mb-4">
        <div class="col-12">
            <div class="d-flex flex-wrap gap-2">
                <a class="btn btn-outline-primary" href="ManageSubjects.aspx">Add Subject</a>
                <a class="btn btn-outline-primary" href="ManageAnnouncements.aspx">Post Announcement</a>
                <a class="btn btn-outline-primary" href="ViewMessages.aspx">View Messages</a>
            </div>
        </div>
    </div>
</asp:Content>