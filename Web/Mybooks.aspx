<%@ Page Title="My Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mybooks.aspx.cs" Inherits="Web.MyBooks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:ListView runat="server" ID="myBookListView" OnLoad="myBookListView_OnLoad">
        <LayoutTemplate>
            <table cellpadding="2" width="640px" border="1" runat="server" id="tblProducts">
                <tr runat="server">
                    <th runat="server">Nazev</th>
                    <th runat="server">Autor</th>
                    <th runat="server">Rok vydani</th>
                    <th runat="server">Zanr</th>
                    <th runat="server">Jazyk</th>
                    <th runat="server">Detail</th>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
            <asp:DataPager runat="server" ID="ContactsDataPager" PageSize="12">
                <Fields>

                </Fields>
            </asp:DataPager>
        </LayoutTemplate>

        <ItemTemplate>
            <tr runat="server">
                <td>
                    <asp:Label ID="nameLabel" runat="Server" Text='<%#Eval("Nazev") %>' />
                </td>
                <td>
                    <asp:Label ID="authorLabel" runat="Server" Text='<%#Eval("Autor") %>' />
                </td>
                <td>
                    <asp:Label ID="publishYearLabel" runat="Server" Text='<%#Eval("Rokvydani") %>' />
                </td>
                <td>
                    <asp:Label ID="genreLabel" runat="Server" Text='<%#Eval("Zanr") %>' />
                </td>
                <td>
                    <asp:Label ID="languageLabel" runat="Server" Text='<%#Eval("Jazyk") %>' />
                </td>

                <td>
                    <asp:Button ID="detail" runat="Server" Text="Detail" OnClick="detail_OnClick" CommandArgument='<%#Eval("Id") %>' />
                </td>
            </tr>
        </ItemTemplate>

    </asp:ListView>
    <asp:Button runat="server" Text="Next page" ID="nextPage" OnClick="nextPage_Click"/>
    <asp:Button runat="server" Text="Prev page" ID="prevPage" OnClick="prevPage_Click"/>

</asp:Content>
