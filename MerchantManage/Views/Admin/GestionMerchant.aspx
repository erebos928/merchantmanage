<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8" />
    <title></title>
    <script src="~/Scripts/jquery-1.10.2.js"></script>
    <link href="~/Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.css" rel="stylesheet" />
    <script src="~/Scripts/bootstrap.js"></script>
</head>
<body>
    <div>
        welcome to GestionMerchant
        <table class="table table-striped table-hover table-responsive" >
            <tr>
                <th> Merchant ID </th>
                <th> Merchant Name </th>
                <th> UserName </th>
                <th> Password </th>
                <th> Logo </th>
                <th> Description </th>
                <th> XsltTemplate </th>
                
            </tr>
            <tr>
                <td> Merchant1 </td>
                <td> Merchant Name 1 </td>
                <td> UserName1 </td>
                <td> Password1 </td>
                <td> Logo1 </td>
                <td> Description 1</td>
                <td> XsltTemplate1 </td>

            </tr>
        </table>
       <input id="ajouter" type="button" value="Ajouter Merchant" onclick="location.href='<%: Url.Action("AddMerchant", "Admin") %>'" />
        
    </div>
</body>
</html>
