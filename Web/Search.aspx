﻿<%@ Page Title="Vyhledavani" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Web.Search" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Vyhledavani</h2>
    <div class="container">
            <div runat="server" Visible="False" id="errorMessage" class="alert alert-danger" role="alert">
        <asp:Literal ID="errorMessageText" runat="server" Text='' />

    </div>
        <div class="row">

        <div class="mb-3" style="display: flex">
    <input runat="server" id="searchInput" class="form-control" placeholder="Nazev knihy, autor" aria-label="Recipient's username" aria-describedby="basic-addon2"/>
    <div class="input-group-append">

    <asp:button runat="server" Text="Vyhledat" onclick="searchButton_Click"  class="btn btn-outline-secondary"></asp:button>
        </div>
    </div>
    </div>        </div>

    <asp:ListView ID="listView" runat="server">
        <LayoutTemplate>
          <table cellpadding="2" width="640px" border="1" runat="server" id="tblProducts" class="table table-bordered">
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
          <asp:DataPager runat="server" ID="ContactsDataPager" PageSize="10">
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
                 <asp:Button ID="detail" runat="Server" Text="Detail" OnClick="detail_Click" CommandArgument='<%#Eval("Id") %>' />
            </td>
          </tr>
        </ItemTemplate>
    </asp:ListView>
    <asp:Button Text="Next page" runat="server" ID="nextPage" OnClick="nextPage_Click"/>
    <asp:Button Text="Prev page" runat="server" ID="prevPage" OnClick="prevPage_Click"/>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
</asp:Content>
