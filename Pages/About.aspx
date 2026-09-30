<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpages/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Aarambha.Pages.About" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">About</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-header text-center">
        <div class="container">
            <h1>About Aarambha</h1>
            <p class="mb-0">A simple, focused learning portal built for Grade 8 students of Sanskar School.</p>
        </div>
    </div>

    <div class="container mb-5">
        <div class="row g-4">
            <div class="col-md-3 col-sm-6">
                <div class="feature-card text-center">
                    <div class="feature-icon">
                        <img src="<%= ResolveUrl("~/Content/images/subject-english.svg") %>" alt="Learning" />
                    </div>
                    <h5>Learning</h5>
                    <p class="text-muted small mb-0">Browse subjects and read notes prepared by your teachers, organised in one place.</p>
                </div>
            </div>
            <div class="col-md-3 col-sm-6">
                <div class="feature-card text-center">
                    <div class="feature-icon">
                        <img src="<%= ResolveUrl("~/Content/images/subject-math.svg") %>" alt="Practice" />
                    </div>
                    <h5>Practice</h5>
                    <p class="text-muted small mb-0">Attempt quizzes for each subject to test what you've learned.</p>
                </div>
            </div>
            <div class="col-md-3 col-sm-6">
                <div class="feature-card text-center">
                    <div class="feature-icon">
                        <img src="<%= ResolveUrl("~/Content/images/subject-science.svg") %>" alt="Progress" />
                    </div>
                    <h5>Progress</h5>
                    <p class="text-muted small mb-0">Review your past quiz attempts and see your results over time.</p>
                </div>
            </div>
            <div class="col-md-3 col-sm-6">
                <div class="feature-card text-center">
                    <div class="feature-icon">
                        <img src="<%= ResolveUrl("~/Content/images/subject-default.svg") %>" alt="Communication" />
                    </div>
                    <h5>Communication</h5>
                    <p class="text-muted small mb-0">Get important announcements from teachers as soon as they're posted.</p>
                </div>
            </div>
        </div>

        <div class="mt-5">
            <p>
                Aarambha was created to solve a simple problem — study materials scattered across
                printed notes and messaging apps. Instead, every Grade 8 student at Sanskar School
                gets one place to find notes, download PDFs, take quizzes, check results, and read
                announcements, while teachers manage all of it from a single dashboard.
            </p>
        </div>
    </div>

</asp:Content>