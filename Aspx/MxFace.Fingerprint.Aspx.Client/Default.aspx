<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<!DOCTYPE html>
<html>
<head>
    <title>MxFace Fingerprint ASPX Client</title>
    <script src="Scripts/fingerprint.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <button type="button" onclick="initializeDevice()">Initialize Device</button>
            <button type="button" onclick="capture()">Capture</button>
            <asp:HiddenField ID="hfTemplate" runat="server" />
        </div>
        <div>
            Person Id: <asp:TextBox ID="txtPersonId" runat="server" />
            Group: <asp:TextBox ID="txtGroup" runat="server" />
        </div>
        <div>
            <asp:Button ID="btnEnroll" runat="server" Text="Enroll" OnClick="Enroll_Click" />
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="Search_Click" />
            <asp:Button ID="btnVerify" runat="server" Text="Verify" OnClick="Verify_Click" />
        </div>
        <div>
            <img id="imgFingerprint" style="width:300px;height:300px;" />
        </div>
        <asp:Literal ID="ltResult" runat="server"></asp:Literal>
    </form>
</body>
</html>
