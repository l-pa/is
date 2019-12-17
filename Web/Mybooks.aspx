<%@ Page Title="Moje knihy" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mybooks.aspx.cs" Inherits="Web.MyBooks" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
        <div runat="server" Visible="False" id="errorMessage" class="alert alert-danger" role="alert">
        <asp:Literal ID="errorMessageText" runat="server" Text='' />
    </div>
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
                    <asp:Label ID="nameLabel" runat="Server" Text='<%#Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="authorLabel" runat="Server" Text='<%#Eval("Author") %>' />
                </td>
                <td>
                    <asp:Label ID="publishYearLabel" runat="Server" Text='<%#Eval("PublishYear") %>' />
                </td>
                <td>
                    <asp:Label ID="genreLabel" runat="Server" Text='<%#Eval("Genre") %>' />
                </td>
                <td>
                    <asp:Label ID="languageLabel" runat="Server" Text='<%#Eval("Language") %>' />
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
