<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/GramaticApp.Master" AutoEventWireup="true" CodeBehind="InicioPage.aspx.cs" Inherits="GramaticApp.Pages.InicioPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container " role="main">
        <!-- Main jumbotron for a primary marketing message or call to action -->
        <div class="jumbotron">
            <h1>GramaticApp</h1>
            <br/>
            <p>
                Este es un proyecto informático educativo, sin ánimos de lucro, 
                desarrollado para la clase de Autómatas, Gramáticales y Lenguaje; 
                cuyo fin es llevar a cabo la impletación ó práctica de los temas 
                tratados en el salón de clases.
            </p>
            <p>
                <strong>GramaticApp</strong> permite simular parte de las funciones de un compilador, 
                debido a que puede validar la construcción de sentencias comúnmente utilizadas en los 
                lenguajes de programación para el desarrollo de programas usando un pseudocódigo.
            </p>
            <br/>
        </div>
    </div>

</asp:Content>