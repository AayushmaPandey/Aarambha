<%@ Page Title="Quiz Results" Language="C#" MasterPageFile="~/Masterpages/Site.Master" AutoEventWireup="true" CodeBehind="QuizResults.aspx.cs" Inherits="Aarambha.Member.QuizResults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>My Quiz Results</h3>
    <asp:GridView ID="gvResults" runat="server" CssClass="table table-hover align-middle" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="QuizTitle" HeaderText="Quiz" />
            <asp:TemplateField HeaderText="Score">
                <ItemTemplate>
                    <div>
                        <strong><%# Convert.ToInt32(Eval("Score")) %></strong> / <%# Convert.ToInt32(Eval("TotalMarks")) %>
                    </div>
                    <div class="progress mt-1" style="height:8px;">
                        <%# string.Format(
                                "<div class='progress-bar bg-info' role='progressbar' style='width:{0}%' aria-valuenow='{0}' aria-valuemin='0' aria-valuemax='100'></div>",
                                Math.Round(
                                    (Convert.ToDouble(Convert.ToInt32(Eval("TotalMarks"))) == 0)
                                        ? 0
                                        : (Convert.ToDouble(Convert.ToInt32(Eval("Score"))) / Convert.ToDouble(Convert.ToInt32(Eval("TotalMarks")))) * 100
                                , 0)
                            ) %>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Result">
                <ItemTemplate>
                    <%# ((Aarambha.Member.QuizResults)Page).RenderResult(Eval("IsPassed"), Eval("Score"), Eval("TotalMarks")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="AttemptedAt" HeaderText="Attempted At" DataFormatString="{0:g}" />
        </Columns>
    </asp:GridView>
</asp:Content>