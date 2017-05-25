<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/GramaticApp.Master" AutoEventWireup="true" CodeBehind="GramaticApp.aspx.cs" Inherits="GramaticApp.Pages.GramaticApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container " role="main">
        <!-- Main jumbotron for a primary marketing message or call to action -->
        <div class="jumbotron">
            <h1>GramaticApp</h1>

            <div class="container">
              <div class="row">
                <div class="col-md-10 col-md-offset-2">

                    <h2>Pseudocódigo</h2>

                    <div class="col-md-12">
                        <div class="form-group col-md-6">
                            <asp:TextBox ID="txtCodigo" CssClass="form-control" runat="server" 
                                TextMode="MultiLine" Rows="10"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <asp:Label ID="lblResultado" CssClass="form-control" ForeColor="White" 
                                BackColor="#5BC0DE" Height="213px" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-4" style="float:left">
                            <asp:Button ID="btnLimpiar" CssClass="btn btn-warning" Text="Limpiar" 
                                Width="100px" runat="server" OnClick="btnLimpiar_Click"></asp:Button>
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnCompilar" CssClass="btn btn-success" Text ="Compilar" 
                                Width="100px" runat="server" OnClick="btnCompilar_Click"></asp:Button>
                        </div>
                         <div class="col-md-2">
                            <asp:Button ID="btnVisualizar" CssClass="btn btn-info" Text ="Visualizar" 
                                Width="100px" runat="server" OnClick="btnVisualizar_Click"></asp:Button>
                        </div>
                    </div>

                </div>
              </div>
            </div>
        </div>
    </div>

</asp:Content>
